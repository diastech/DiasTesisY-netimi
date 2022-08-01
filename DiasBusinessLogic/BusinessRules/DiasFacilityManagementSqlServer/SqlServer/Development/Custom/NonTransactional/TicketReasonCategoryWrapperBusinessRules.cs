using DiasBusinessLogic.InterfacesAbstracts.Abstracts.BusinessRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransactionalInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Custom;
using DevelopmentUserInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Standart;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using CustomDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.Configuration;
using DiasBusinessLogic.Shared.Configuration;
using DevelopmentContext = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using DiasBusinessLogic.AutoMapper.Configuration;
using CustomDevelopmentTicketReasonCategoryProfile = DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.CustomProfiles.Development;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Base.SqlServer.Custom;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using DiasShared.Errors;
using DiasBusinessLogic.Shared.Error;
using DiasShared.Services.Communication.BusinessLogicMessage;
using DevelopmentModel = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using Newtonsoft.Json;
using Microsoft.Data.SqlClient;

namespace DiasBusinessLogic.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Custom
{
    public class TicketReasonCategoryWrapperBusinessRules : BusinessRuleAbstract, TransactionalInterface.ITicketReasonCategoryWrapperBusinessRules, IBaseTicketReasonCategoryWrapperBusinessRules
    {
        private readonly DevelopmentUserInterface.ITicketReasonCategoryV2BusinessRules _ticketReasonCategoryBusinessRules;
        private readonly DevelopmentUserInterface.ITicketReasonBusinessRules _ticketReasonBusinessRules;
        private static AutoMapperProfileMapper<CustomDevelopmentTicketReasonCategoryProfile.CustomTicketReasonCategoryProfile> DtoMapper_DiasFacilityManagementSqlServer_Development
            => new(new(DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer")));

        public TicketReasonCategoryWrapperBusinessRules() : this(
            DI_ServiceLocator.Current.Get<DevelopmentUserInterface.ITicketReasonCategoryV2BusinessRules>(), 
            DI_ServiceLocator.Current.Get<DevelopmentUserInterface.ITicketReasonBusinessRules>())
        {
        }
        private TicketReasonCategoryWrapperBusinessRules(
            DevelopmentUserInterface.ITicketReasonCategoryV2BusinessRules ticketReasonCategoryBusinessRules, 
            DevelopmentUserInterface.ITicketReasonBusinessRules ticketReasonBusinessRules
            )
        {
            _ticketReasonCategoryBusinessRules = ticketReasonCategoryBusinessRules;
            _ticketReasonBusinessRules = ticketReasonBusinessRules;
        }

        public async Task<Tuple<Error, IEnumerable<CustomDto.CustomTicketReasonCategoryDto>>> GetAllTicketReasonCategoriesWrapperAsync()
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");
            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new Tuple<Error, IEnumerable<CustomDto.CustomTicketReasonCategoryDto>>(Errors.General.NotFoundDatabaseServer(), null);
            }
            else
            {
                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();
                            Tuple<Error, List<DevelopmentDto.TicketReasonCategoryV2Dto>> resultGetTicketReasonCategoryList = await _ticketReasonCategoryBusinessRules.GetAllTicketReasonCategoriesAsync();
                            try
                            {
                                if ((resultGetTicketReasonCategoryList.Item1.BusinessOperationSucceed == true) && (resultGetTicketReasonCategoryList.Item2 != null))
                                {
                                    List<CustomDto.CustomTicketReasonCategoryDto> reasonList = new List<CustomDto.CustomTicketReasonCategoryDto>();
                                    var convertedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<List<DevelopmentDto.TicketReasonCategoryV2Dto>, List<CustomDto.CustomTicketReasonCategoryDto>>(resultGetTicketReasonCategoryList.Item2);
                                    var lastNodes = convertedDto.Where(x => convertedDto.All(y => y.ParentHierarchy != x.HierarchyId));
                                    foreach (var item in lastNodes)
                                    {
                                        var reasonDtoList = await _ticketReasonBusinessRules.GetTicketReasonsByCategoryIdAsync(item.Id);

                                        if (reasonDtoList.Item1.BusinessOperationSucceed)
                                        {
                                            foreach (var reason in reasonDtoList.Item2)
                                            {

                                                CustomTicketReasonCategoryDto value = new();
                                                value.Id = reason.Id;
                                                value.CategoryName = reason.ReasonName + "(ÇS:" + (reason.ResolutionTime).ToString() + "dk" + ")" + "(MS:" + (reason.ResponseTime).ToString() + "dk" + ")";
                                                value.CategoryDescription = reason.ReasonDescription;
                                                value.HierarchyId = $"{item.HierarchyId}{reason.Id}/";
                                                value.IsReason = true;
                                                value.ResolutionTime = reason.ResolutionTime;
                                                value.ResponseTime = reason.ResponseTime;
                                                value.ResolutionTimeText = (reason.ResolutionTime).ToString();
                                                value.ResponseTimeText = (reason.ResponseTime).ToString();
                                                value.CategoryNameToUI = reason.ReasonName;
                                                reasonList.Add(value);
                                            }
                                        }
                                    }

                                    convertedDto.AddRange(reasonList);
                                    return new Tuple<Error, IEnumerable<CustomDto.CustomTicketReasonCategoryDto>>(resultGetTicketReasonCategoryList.Item1, convertedDto);
                                }
                                else
                                {
                                    return new Tuple<Error, IEnumerable<CustomDto.CustomTicketReasonCategoryDto>>(resultGetTicketReasonCategoryList.Item1, null);
                                }
                            }
                            catch (Exception)
                            {
                                throw;
                            }
                        }
                    default:
                        {
                            return new Tuple<Error, IEnumerable<CustomDto.CustomTicketReasonCategoryDto>>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
        }

        public async Task<Tuple<Error, CustomDto.CustomTicketReasonCategoryDto>> GetTicketReasonCategoryWrapperByIdAsync(string hierarchyId)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");
            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new Tuple<Error, CustomDto.CustomTicketReasonCategoryDto>(Errors.General.NotFoundDatabaseServer(), null);
            }
            else
            {
                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();
                            Tuple<Error, DevelopmentDto.TicketReasonCategoryV2Dto> resultGetTicketReasonCategory = await _ticketReasonCategoryBusinessRules.GetTicketReasonCategoryByIdAsync(hierarchyId);
                            try
                            {
                                if ((resultGetTicketReasonCategory.Item1.BusinessOperationSucceed == true) && (resultGetTicketReasonCategory.Item2 != null))
                                {
                                    CustomDto.CustomTicketReasonCategoryDto returnDto = new CustomDto.CustomTicketReasonCategoryDto();
                                    CustomDto.CustomTicketReasonCategoryDto convertedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<DevelopmentDto.TicketReasonCategoryV2Dto, CustomDto.CustomTicketReasonCategoryDto>(resultGetTicketReasonCategory.Item2);
                                    returnDto = convertedDto;

                                    return new Tuple<Error, CustomDto.CustomTicketReasonCategoryDto>(resultGetTicketReasonCategory.Item1, returnDto);
                                }
                                else
                                {
                                    return new Tuple<Error, CustomDto.CustomTicketReasonCategoryDto>(resultGetTicketReasonCategory.Item1, null);
                                }
                            }
                            catch (Exception)
                            {
                                throw;
                            }
                        }
                    default:
                        {
                            return new Tuple<Error, CustomDto.CustomTicketReasonCategoryDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
        }

        /// <summary>
        ///  Bu iş kuralı alınan kategori node'unun en altındaki node'u verir
        /// </summary>
        /// <param name="hierarchyId"></param>
        /// <returns></returns>

        public async Task<Tuple<Error, IEnumerable<CustomDto.CustomTicketReasonCategoryDto>>> GetTicketReasonCategoryWrapperLastNodeByIdAsync(string hierarchyId)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");
            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new Tuple<Error, IEnumerable<CustomDto.CustomTicketReasonCategoryDto>>(Errors.General.NotFoundDatabaseServer(), null);
            }
            else
            {
                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();
                            Tuple<Error, IEnumerable<DevelopmentDto.TicketReasonCategoryV2Dto>> resultGetTicketReasonCategory =
                                await _ticketReasonCategoryBusinessRules.GetLastNodeTicketReasonCategoryByIdAsync(hierarchyId);

                            try
                            {
                                if ((resultGetTicketReasonCategory.Item1.BusinessOperationSucceed == true) && (resultGetTicketReasonCategory.Item2 != null))
                                {
                                    List<CustomTicketReasonCategoryDto> convertedDtoList =
                                        DtoMapper_DiasFacilityManagementSqlServer_Development.Map<List<DevelopmentDto.TicketReasonCategoryV2Dto>,
                                            List<CustomDto.CustomTicketReasonCategoryDto>>(resultGetTicketReasonCategory.Item2.ToList<DevelopmentDto.TicketReasonCategoryV2Dto>());

                                    return new Tuple<Error, IEnumerable<CustomDto.CustomTicketReasonCategoryDto>>(resultGetTicketReasonCategory.Item1, convertedDtoList);
                                }
                                else
                                {
                                    return new Tuple<Error, IEnumerable<CustomDto.CustomTicketReasonCategoryDto>>(resultGetTicketReasonCategory.Item1, null);
                                }
                            }
                            catch (Exception)
                            {
                                throw;
                            }
                        }
                    default:
                        {
                            return new Tuple<Error, IEnumerable<CustomDto.CustomTicketReasonCategoryDto>>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
        }

        public async Task<Tuple<Error, CustomDto.CustomTicketReasonCategoryDto>> InsertTicketReasonOrCategoryAsync(BusinessLogicRequest request)
        {
            //Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");

            //if ((dataContextType == null) || (dataContextType.Name?.Length == 0) || (dataContextType.Name?.Length == 0))
            //    return new(Errors.General.ConnectionTimeout(), null);
            //else
            //{
            //    switch (dataContextType.Name)
            //    {
            //        case "DiasFacilityManagementSqlServer":
            //            {
            //                try
            //                {
            //                    DevelopmentModel.TicketReasonCategoryV2 parentEntity;
            //                    DevelopmentModel.TicketReasonCategoryV2 childEntity;
            //                    DevelopmentModel.TicketReasonCategoryV2 addedEntity = new();
            //                    CustomDto.CustomTicketReasonCategoryDto result = new();
            //                    DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();

            //                    using (DiasFacilityManagementSqlServerContext)
            //                    {
            //                        if ((request == null) || (request.RequestDtosTypes == null) || (request.RequestDtosJsons == null) ||
            //                             (request.RequestDtosTypes.Count < 1) || (request.RequestDtosJsons.Count < 1) ||
            //                                   (!Type.Equals(request.RequestDtosTypes[0], typeof(CustomTicketReasonCategoryDto))))
            //                        {
            //                            return new Tuple<Error, CustomDto.CustomTicketReasonCategoryDto>(Errors.General.RequestNull("Location"), null);
            //                        }

            //                        //test
            //                        //CustomLocationDto test = new() { LocationOriginalName = "test3", LocationNumber = "testNumber3", ParentHierarchyFromUI = null };
            //                        //string testJson =  JsonConvert.SerializeObject(test);
            //                        //CustomLocationDto castedDto = JsonConvert.DeserializeObject<CustomLocationDto>(testJson);

            //                        CustomTicketReasonCategoryDto castedDto = JsonConvert.DeserializeObject<CustomTicketReasonCategoryDto>(request.RequestDtosJsons[0]);

            //                        if ((castedDto != null) && (!(String.IsNullOrEmpty(castedDto.ReasonNameFromUI))))
            //                        {
            //                            //en tepeye eklenecek
            //                            if (String.IsNullOrEmpty(castedDto.ParentHierarchyFromUI))
            //                            {
            //                                //TODO: En tepeye eklemede mevcut mahal tablosunda çok uzun sürüyor, efektif bir yol bulana kadar iptal edildi.
            //                                //Efektif bir yol bulunduğunda altdaki return kaldırılıp, commentler geri alanacak
            //                                return new(Errors.General.ErrorInsert("Location"), null);

            //                                ////En tepeye eklendiği vakit tüm mahaller ağaçta aşağı kayacaktır
            //                                ////Önce kök ile yeni ekleneni yer değiştireceğiz
            //                                ////Burada biraz katakuli yollara sapacağız
            //                                ////Çünkü mevcut tabloda hiyerarşiyi bozamayız ancak kodda bozabiliriz
            //                                ////kodda hiyerarşiyi tekrar dizayn edip tabloya basacağız

            //                                ////root hiyerarşi idsini al
            //                                //HierarchyId rootHiearchyId = HierarchyId.GetRoot();

            //                                ////şimdi yeni ekleneni root yapalım
            //                                //addedEntity = DtoMapper_DiasFacilityManagementSqlServer_Development_Custom.
            //                                //                        Map<CustomDto.CustomLocationDto, DevelopmentModel.LocationV2>(castedDto);

            //                                //addedEntity.HierarchyId = rootHiearchyId;

            //                                ////eski olacak root'uda onun altına alalım
            //                                ////güncel hiyerarşi idsini hesaplayalım
            //                                //HierarchyId newHierarchyIdOldRoot =  addedEntity.HierarchyId.GetDescendant(null, null);

            //                                ////altına almadan eski olacak rootun altındakileri çekelim(kendisi hariç)
            //                                ////IsActive ve IsDeleted kontrolü ileri değişiklerde tehlikeli o yüzden katmayalım
            //                                //List<DevelopmentModel.LocationV2> currentRecordListBelowOldRoot = await Task.Run(() =>
            //                                //            DiasFacilityManagementSqlServerContext.LocationV2s.AsQueryable().Where(x => x.HierarchyId.IsDescendantOf(rootHiearchyId) && (x.HierarchyId != rootHiearchyId)).
            //                                //                ToList<DevelopmentModel.LocationV2>());

            //                                ////şimdi eski rootu alalım
            //                                //DevelopmentModel.LocationV2 oldRootRecord = await Task.Run(() =>
            //                                //            DiasFacilityManagementSqlServerContext.LocationV2s.AsQueryable().Where(x => x.HierarchyId == rootHiearchyId).Single());

            //                                ////eski roota güncel hiyerarşi değerini atayalım
            //                                //oldRootRecord.HierarchyId = newHierarchyIdOldRoot;
            //                                //oldRootRecord.OldHierarchyId = rootHiearchyId;

            //                                ////şimdi eski rootun altındaki childlerin hiyerarşi idsini güncelleyelim
            //                                //foreach (DevelopmentModel.LocationV2 item in currentRecordListBelowOldRoot)
            //                                //{
            //                                //    item.OldHierarchyId = item.HierarchyId;
            //                                //    item.HierarchyId = item.HierarchyId.GetReparentedValue(rootHiearchyId, newHierarchyIdOldRoot);
            //                                //    await Task.Run(() => DiasFacilityManagementSqlServerContext.Update(item));
            //                                //}

            //                                ////Son olarak description null ise boş string yapalım(non null veritabanında)
            //                                //if (addedEntity.LocationDescription == null)
            //                                //{
            //                                //    addedEntity.LocationDescription = "";
            //                                //}

            //                                ////Tüm tabloyu güncelleyelim
            //                                //await Task.Run(() => DiasFacilityManagementSqlServerContext.AddAsync(addedEntity));
            //                                //await Task.Run(() => DiasFacilityManagementSqlServerContext.Update(oldRootRecord));
            //                                //await Task.Run(() => _unitOfWork_EF.CompleteAsync(typeof(DevelopmentContext.DiasFacilityManagementSqlServer).Name, DiasFacilityManagementSqlServerContext));
            //                            }
            //                            else//verilen parentin altına eklenecek
            //                            {
            //                                //IsActive ve IsDeleted kontrolü ileri değişiklerde tehlikeli o yüzden katmayalım
            //                                parentEntity = await Task.Run(() =>
            //                                    DiasFacilityManagementSqlServerContext.LocationV2s.AsQueryable().Where(x => x.HierarchyId.ToString() == castedDto.ParentHierarchyFromUI)
            //                                    .First<DevelopmentModel.LocationV2>());

            //                                HierarchyId parentHierarchyId = parentEntity.HierarchyId;

            //                                //Bu parentin altındaki tüm childlerin(varsa) en sonundakini al
            //                                //IsActive ve IsDeleted kontrolü ileri değişiklerde tehlikeli o yüzden katmayalım
            //                                childEntity = await Task.Run(() =>
            //                                  DiasFacilityManagementSqlServerContext.LocationV2s.AsQueryable().Where(x => x.HierarchyId.GetAncestor(1) == parentHierarchyId)
            //                                      .OrderByDescending(x => x.HierarchyId).FirstOrDefault());

            //                                //Eğer child yoksa eklenecek bu öğe ilk childdir
            //                                HierarchyId lastChildHierarchyId;
            //                                if (childEntity == null)
            //                                {
            //                                    lastChildHierarchyId = null;
            //                                }
            //                                else//değilse son childin yanına eklenecektir
            //                                {
            //                                    lastChildHierarchyId = childEntity.HierarchyId;
            //                                }

            //                                HierarchyId addedHierarchyId = parentHierarchyId.GetDescendant(lastChildHierarchyId, null);
            //                                addedEntity = DtoMapper_DiasFacilityManagementSqlServer_Development_Custom.
            //                                                                            Map<CustomDto.CustomLocationDto, DevelopmentModel.LocationV2>(castedDto);

            //                                addedEntity.HierarchyId = addedHierarchyId;

            //                                //Son olarak description null ise boş string yapalım(non null veritabanında)
            //                                if (addedEntity.LocationDescription == null)
            //                                {
            //                                    addedEntity.LocationDescription = "";
            //                                }

            //                                await Task.Run(() => DiasFacilityManagementSqlServerContext.AddAsync(addedEntity));
            //                                await Task.Run(() => _unitOfWork_EF.CompleteAsync(typeof(DevelopmentContext.DiasFacilityManagementSqlServer).Name, DiasFacilityManagementSqlServerContext));
            //                            }
            //                        }
            //                        else
            //                        {
            //                            return new Tuple<Error, CustomDto.CustomLocationDto>(Errors.General.ErrorInsert("Location"), null);
            //                        }
            //                    }

            //                    result = DtoMapper_DiasFacilityManagementSqlServer_Development_Custom.
            //                                                                Map<DevelopmentModel.LocationV2, CustomDto.CustomLocationDto>(addedEntity);

            //                    return new(Errors.General.SuccessInsert("Location"), result);
            //                }
            //                catch (SqlException e) when ((e.Number == -1) || (e.Number == -2) || (e.Number == 53))
            //                {
            //                    return new(Errors.General.ConnectionTimeout(), null);
            //                }
            //                catch (ArgumentNullException)
            //                {
            //                    return new(Errors.General.ArgumentNullException(), null);
            //                }
            //                catch (Exception)
            //                {
            //                    return new(Errors.General.ErrorInsert("Location"), null);
            //                }
            //            }
            //        default:
            //            {
            //                return new(Errors.General.NotFoundDatabaseServer(), null);
            //            }
            //    }
            //}
            return new(Errors.General.NotFoundDatabaseServer(), null);
        }


    }
}
