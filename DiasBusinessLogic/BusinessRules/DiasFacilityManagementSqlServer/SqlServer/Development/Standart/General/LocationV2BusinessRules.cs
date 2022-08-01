using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiasBusinessLogic.InterfacesAbstracts.Abstracts.BusinessRules;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.BaseBusinessRules.SqlServer.Standart;
using DevelopmentLocationInterface =  DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Standart;
using Microsoft.Data.SqlClient;
using DiasBusinessLogic.AutoMapper.Configuration;
using DevelopmentLocationProfile = DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Development;
using DiasBusinessLogic.Shared.Configuration;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.UnitOfWork;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using DevelopmentModel =  DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using DevelopmentContext =  DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.Configuration;
using Microsoft.EntityFrameworkCore;
using CustomDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using DevelopmentCustomLocationProfile = DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.CustomProfiles.Development;
using DiasShared.Errors;
using DiasBusinessLogic.Shared.Error;
using DiasShared.Services.Communication.BusinessLogicMessage;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using Newtonsoft.Json;


namespace DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.SqlServer.Development.Standart
{
    public class LocationV2BusinessRules : BusinessRuleAbstract, DevelopmentLocationInterface.ILocationV2BusinessRules, IBaseLocationV2BusinessRules
    {
        private static AutoMapperProfileMapper<DevelopmentLocationProfile.LocationV2Profile> DtoMapper_DiasFacilityManagementSqlServer_Development
            => new  (new (DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer")));

        private static AutoMapperProfileMapper<DevelopmentCustomLocationProfile.CustomLocationProfile> DtoMapper_DiasFacilityManagementSqlServer_Development_Custom
            => new(new(DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer")));

        private readonly IUnitOfWork_EF _unitOfWork_EF;

        public LocationV2BusinessRules() : this(DI_ServiceLocator.Current.Get<IUnitOfWork_EF>())
        {
        }

        private LocationV2BusinessRules(IUnitOfWork_EF unitOfWork_EF)
        {
            _unitOfWork_EF = unitOfWork_EF;
        }

        public async Task<Tuple<Error, IEnumerable<CustomDto.CustomLocationDto>>> GetAllLocationsAsync()
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");
            if ((dataContextType == null) || (dataContextType.Name?.Length == 0) || (dataContextType.Name?.Length == 0))
                return new(Errors.General.NotFoundDatabaseServer(), null);
            else
            {
                List<CustomDto.CustomLocationDto> returnDtoList = new();
                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            try
                            {
                                List<DevelopmentModel.LocationV2> sonucEntityList;
                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();
                                using (DiasFacilityManagementSqlServerContext)
                                {                                   
                                    sonucEntityList = await Task.Run(() =>
                                    DiasFacilityManagementSqlServerContext.LocationV2s.AsQueryable().
                                        Where(x => (x.IsActive.HasValue && x.IsDeleted.HasValue) && (!x.IsDeleted.Value) && (x.IsActive == true) &&
                                                     ((x.HierarchyId.ToString() == "/") || (x.HierarchyId.ToString().StartsWith("/5/")))).OrderBy(x => x.LocationNumber)
                                        //Where(x => (x.IsActive.HasValue && x.IsDeleted.HasValue) && (!x.IsDeleted.Value) && (x.IsActive == true))
                                        //             .OrderBy(x => x.LocationNumber)
                                        .AsNoTracking<DevelopmentModel.LocationV2>().ToList<DevelopmentModel.LocationV2>());
                                }

                                List<CustomDto.CustomLocationDto> convertedDto = DtoMapper_DiasFacilityManagementSqlServer_Development_Custom.Map<List<DevelopmentModel.LocationV2>, List<CustomDto.CustomLocationDto>>(sonucEntityList);
                                
                                return new(Errors.General.GetListSuccess("LocationV2"), convertedDto);
                            }
                            catch (SqlException e) when ((e.Number == -1) || (e.Number == -2) || (e.Number == 53))
                            {
                                return new(Errors.General.ConnectionTimeout(), null);
                            }
                            catch (ArgumentNullException)
                            {
                                return new(Errors.General.ArgumentNullException(), null);
                            }
                            catch (Exception)
                            {
                                return new(Errors.General.GridListError("LocationV2"), null);
                            }
                        }
                    default:
                        {
                            return new(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
        }

        public async Task<Tuple<Error, CustomDto.CustomLocationDto>> GetLocationByIdAsync(string hierarchyId)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");

            if ((dataContextType == null) || (dataContextType.Name?.Length == 0) || (dataContextType.Name?.Length == 0))
                return new(Errors.General.NotFoundDatabaseServer(), null);
            else
            {
                DevelopmentDto.LocationV2Dto returnDto = new();
                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            try
                            {
                                DevelopmentModel.LocationV2 sonucEntity;
                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();

                                using (DiasFacilityManagementSqlServerContext)
                                {
                                    sonucEntity = await Task.Run(() =>
                                    DiasFacilityManagementSqlServerContext.LocationV2s.AsQueryable().Where(x => x.HierarchyId.ToString() == hierarchyId && (x.IsActive.HasValue && x.IsDeleted.HasValue) && (!x.IsDeleted.Value) && x.IsActive == true)
                                    .FirstOrDefault<DevelopmentModel.LocationV2>());
                                }

                                CustomDto.CustomLocationDto convertedDto = DtoMapper_DiasFacilityManagementSqlServer_Development_Custom.Map<DevelopmentModel.LocationV2, CustomDto.CustomLocationDto>(sonucEntity);

                                return new(Errors.General.SuccessGetById("LocationV2"), convertedDto);
                            }
                            catch (SqlException e) when ((e.Number == -1) || (e.Number == -2) || (e.Number == 53))
                            {
                                return new(Errors.General.ConnectionTimeout(), null);
                            }
                            catch (ArgumentNullException)
                            {
                                return new(Errors.General.ArgumentNullException(), null);
                            }
                            catch (Exception e)
                            {
                                return new(Errors.General.ErrorGetById("LocationV2"), null);
                            }
                        }
                    default:
                        {
                            return new(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
        }

        public async Task<Tuple<Error, IEnumerable<CustomDto.CustomLocationDto>>> GetLastNodeTicketLocationByIdAsync(string hierarchyId)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");
            if ((dataContextType == null) || (dataContextType.Name?.Length == 0) || (dataContextType.Name?.Length == 0))
                return new(Errors.General.ConnectionTimeout(), null);
            else
            {
                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            try
                            {
                                DevelopmentModel.LocationV2 parentEntity;
                                List<DevelopmentModel.LocationV2> descendantEntityList = new();
                                List<DevelopmentModel.LocationV2> lastModeDescendantEntityList = new();

                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();
                                using (DiasFacilityManagementSqlServerContext)
                                {
                                    parentEntity = await Task.Run(() =>
                                    DiasFacilityManagementSqlServerContext.LocationV2s.AsQueryable().Where(x => x.HierarchyId.ToString() == hierarchyId && (x.IsActive.HasValue && x.IsDeleted.HasValue) && (!x.IsDeleted.Value) && x.IsActive == true)
                                    .First<DevelopmentModel.LocationV2>());


                                    //şimdi bunun varsa altındaki nodelara bakarak en alt node'u bulmaya çalışalım                               
                                    descendantEntityList = await Task.Run(() =>
                                                                DiasFacilityManagementSqlServerContext.LocationV2s.AsQueryable()
                                                                .Where(subNode => (subNode.HierarchyId.IsDescendantOf(parentEntity.HierarchyId))
                                                                    && (subNode != parentEntity) && (subNode.IsActive.HasValue && subNode.IsDeleted.HasValue)
                                                                        && (!subNode.IsDeleted.Value) && subNode.IsActive == true)
                                                                            .ToList<DevelopmentModel.LocationV2>());

                                    if (descendantEntityList.Count == 0)
                                    {
                                        lastModeDescendantEntityList.Add(parentEntity);

                                    }
                                    else
                                    {
                                        foreach (DevelopmentModel.LocationV2 item in descendantEntityList)
                                        {
                                            parentEntity = item;
                                            List<DevelopmentModel.LocationV2> subDescendantEntityList = new();

                                            subDescendantEntityList = await Task.Run(() =>
                                                               DiasFacilityManagementSqlServerContext.LocationV2s.AsQueryable()
                                                               .Where(subNode => (subNode.HierarchyId.IsDescendantOf(parentEntity.HierarchyId))
                                                                   && (subNode != parentEntity) && (subNode.IsActive.HasValue && subNode.IsDeleted.HasValue)
                                                                       && (!subNode.IsDeleted.Value) && subNode.IsActive == true)
                                                                        .ToList<DevelopmentModel.LocationV2>());


                                            if (subDescendantEntityList.Count == 0)
                                            {
                                                lastModeDescendantEntityList.Add(parentEntity);
                                            }
                                        }
                                    }
                                }

                                List<CustomDto.CustomLocationDto> convertedDto = DtoMapper_DiasFacilityManagementSqlServer_Development_Custom.Map<List<DevelopmentModel.LocationV2>, List<CustomDto.CustomLocationDto>>(lastModeDescendantEntityList);

                                return new(Errors.General.GetListSuccess("LocationV2"), convertedDto);
                            }
                            catch (SqlException e) when ((e.Number == -1) || (e.Number == -2) || (e.Number == 53))
                            {
                                return new(Errors.General.ConnectionTimeout(), null);
                            }
                            catch (ArgumentNullException)
                            {
                                return new(Errors.General.ArgumentNullException(), null);
                            }
                            catch (Exception ex)
                            {
                                return new(Errors.General.ErrorGetById("TicketReasonCategoryV2"), null);
                            }
                        }
                    default:
                        {
                            return new(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
        }

        public async Task<Tuple<Error, CustomDto.CustomLocationDto>> GetLocationByNfcCodeAsync(string nfcCode)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");
            if ((dataContextType == null) || (dataContextType.Name?.Length == 0) || (dataContextType.Name?.Length == 0))
                return new(Errors.General.NotFoundDatabaseServer(), null);
            else
            {
                DevelopmentDto.LocationV2Dto returnDto = new();
                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            try
                            {
                                DevelopmentModel.LocationV2 sonucEntity;
                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();
                                using (DiasFacilityManagementSqlServerContext)
                                {
                                    sonucEntity = await Task.Run(() =>
                                    DiasFacilityManagementSqlServerContext.LocationV2s.AsQueryable().Where(x => x.NFC_Code == nfcCode && (x.IsActive.HasValue && x.IsDeleted.HasValue) && (!x.IsDeleted.Value) && x.IsActive == true)
                                    .FirstOrDefault<DevelopmentModel.LocationV2>());
                                }
                                CustomDto.CustomLocationDto convertedDto = DtoMapper_DiasFacilityManagementSqlServer_Development_Custom.Map<DevelopmentModel.LocationV2, CustomDto.CustomLocationDto>(sonucEntity);

                                return new(Errors.General.SuccessGetById("LocationV2"), convertedDto);
                            }
                            catch (SqlException e) when ((e.Number == -1) || (e.Number == -2) || (e.Number == 53))
                            {
                                return new(Errors.General.ConnectionTimeout(), null);
                            }
                            catch (ArgumentNullException)
                            {
                                return new(Errors.General.ArgumentNullException(), null);
                            }
                            catch (Exception e)
                            {
                                return new(Errors.General.ErrorGetById("LocationV2"), null);
                            }
                        }
                    default:
                        {
                            return new(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
        }

        public async Task<Tuple<Error, IEnumerable<CustomDto.CustomLocationDto>>> GetNodesTicketLocationByIdWithinLevelAsync(BusinessLogicRequest request)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");
            if ((dataContextType == null) || (dataContextType.Name?.Length == 0) || (dataContextType.Name?.Length == 0))
                return new(Errors.General.ConnectionTimeout(), null);
            else
            {
                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            try
                            {
                                DevelopmentModel.LocationV2 parentEntity;
                                List<DevelopmentModel.LocationV2> descendantEntityList = new();
                                List<CustomDto.CustomLocationDto> resultList = new();


                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();
                                using (DiasFacilityManagementSqlServerContext)
                                {
                                    if ((request == null) || (request.RequestDtosTypes == null) || (request.RequestDtosJsons == null) ||
                                         (request.RequestDtosTypes.Count < 1) || (request.RequestDtosJsons.Count < 1) ||
                                               (!Type.Equals(request.RequestDtosTypes[0], typeof(CustomSubLocationRequestDto))))
                                    {
                                        return new Tuple<Error, IEnumerable<CustomDto.CustomLocationDto>>(Errors.General.RequestNull("Location"), null);
                                    }

                                    CustomSubLocationRequestDto castedDto = JsonConvert.DeserializeObject<CustomSubLocationRequestDto>(request.RequestDtosJsons[0]);

                                    if ((castedDto != null) && (!(String.IsNullOrEmpty(castedDto.HierarchyId))) && (castedDto.Level > 0))
                                    {
                                        parentEntity = await Task.Run(() =>
                                        DiasFacilityManagementSqlServerContext.LocationV2s.AsQueryable().Where(x => x.HierarchyId.ToString() == castedDto.HierarchyId && (x.IsActive.HasValue && x.IsDeleted.HasValue) && (!x.IsDeleted.Value) && x.IsActive == true)
                                        .First<DevelopmentModel.LocationV2>());

                                        for (int i = 0; i < castedDto.Level; i++)
                                        {
                                            //şimdi bunun varsa altındaki nodelara bakalım                 
                                            descendantEntityList = await Task.Run(() =>
                                                                        DiasFacilityManagementSqlServerContext.LocationV2s.AsQueryable()
                                                                        .Where(subNode => (subNode.HierarchyId.IsDescendantOf(parentEntity.HierarchyId))
                                                                            && (subNode != parentEntity) && (subNode.IsActive.HasValue && subNode.IsDeleted.HasValue)
                                                                                && (!subNode.IsDeleted.Value) && subNode.IsActive == true)
                                                                                    .ToList<DevelopmentModel.LocationV2>());

                                            if (descendantEntityList.Count == 0)
                                            {
                                                break;
                                            }
                                            else
                                            {
                                                foreach (DevelopmentModel.LocationV2 item in descendantEntityList)
                                                {
                                                    CustomDto.CustomLocationDto itemInnerDto =
                                                        DtoMapper_DiasFacilityManagementSqlServer_Development_Custom.Map<DevelopmentModel.LocationV2, CustomDto.CustomLocationDto>(item);

                                                    itemInnerDto.RelativeLevel = i + 1;

                                                    resultList.Add(itemInnerDto);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        return new Tuple<Error, IEnumerable<CustomDto.CustomLocationDto>>(Errors.General.RequestNull("Location"), null);
                                    }
                                }

                                return new(Errors.General.GetListSuccess("LocationV2"), resultList);
                            }
                            catch (SqlException e) when ((e.Number == -1) || (e.Number == -2) || (e.Number == 53))
                            {
                                return new(Errors.General.ConnectionTimeout(), null);
                            }
                            catch (ArgumentNullException)
                            {
                                return new(Errors.General.ArgumentNullException(), null);
                            }
                            catch (Exception ex)
                            {
                                return new(Errors.General.ErrorGetById("TicketReasonCategoryV2"), null);
                            }
                        }
                    default:
                        {
                            return new(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
        }

        public async Task<Tuple<Error, IEnumerable<CustomDto.CustomLocationDto>>> GetNodesTicketLocationByIdWithinLevelMobileAsync(BusinessLogicRequest request)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");
            if ((dataContextType == null) || (dataContextType.Name?.Length == 0) || (dataContextType.Name?.Length == 0))
                return new(Errors.General.ConnectionTimeout(), null);
            else
            {
                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            try
                            {
                                DevelopmentModel.LocationV2 parentEntity;
                                List<DevelopmentModel.LocationV2> descendantEntityList = new();
                                List<CustomDto.CustomLocationDto> resultList = new();


                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();
                                using (DiasFacilityManagementSqlServerContext)
                                {
                                    if ((request == null) || (request.RequestDtosJsons == null) || (request.RequestDtosJsons.Count < 1))
                                    {
                                        return new Tuple<Error, IEnumerable<CustomDto.CustomLocationDto>>(Errors.General.RequestNull("Location"), null);
                                    }

                                    CustomSubLocationRequestDto castedDto = JsonConvert.DeserializeObject<CustomSubLocationRequestDto>(request.RequestDtosJsons[0]);

                                    if ((castedDto != null) && (!(String.IsNullOrEmpty(castedDto.HierarchyId))) && (castedDto.Level > 0))
                                    {
                                        parentEntity = await Task.Run(() =>
                                        DiasFacilityManagementSqlServerContext.LocationV2s.AsQueryable().Where(x => x.HierarchyId.ToString() == castedDto.HierarchyId && (x.IsActive.HasValue && x.IsDeleted.HasValue) && (!x.IsDeleted.Value) && x.IsActive == true)
                                        .First<DevelopmentModel.LocationV2>());

                                        short levelOfParent = parentEntity.HierarchyId.GetLevel();

                                        for (int i = 0; i < castedDto.Level; i++)
                                        {
                                            //şimdi bunun varsa altındaki nodelara bakalım                 
                                            descendantEntityList = await Task.Run(() =>
                                                                        DiasFacilityManagementSqlServerContext.LocationV2s.AsQueryable()
                                                                        .Where(subNode => (subNode.HierarchyId.IsDescendantOf(parentEntity.HierarchyId))
                                                                            && (subNode != parentEntity) && (subNode.HierarchyId.GetLevel() == (levelOfParent + (i + 1))) && (subNode.IsActive.HasValue && subNode.IsDeleted.HasValue)
                                                                                && (!subNode.IsDeleted.Value) && subNode.IsActive == true)
                                                                                    .ToList<DevelopmentModel.LocationV2>());

                                            if (descendantEntityList.Count == 0)
                                            {
                                                break;
                                            }
                                            else
                                            {
                                                foreach (DevelopmentModel.LocationV2 item in descendantEntityList)
                                                {
                                                    CustomDto.CustomLocationDto itemInnerDto =
                                                        DtoMapper_DiasFacilityManagementSqlServer_Development_Custom.Map<DevelopmentModel.LocationV2, CustomDto.CustomLocationDto>(item);

                                                    itemInnerDto.RelativeLevel = i + 1;

                                                    resultList.Add(itemInnerDto);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        return new Tuple<Error, IEnumerable<CustomDto.CustomLocationDto>>(Errors.General.RequestNull("Location"), null);
                                    }
                                }

                                return new(Errors.General.GetListSuccess("LocationV2"), resultList);
                            }
                            catch (SqlException e) when ((e.Number == -1) || (e.Number == -2) || (e.Number == 53))
                            {
                                return new(Errors.General.ConnectionTimeout(), null);
                            }
                            catch (ArgumentNullException)
                            {
                                return new(Errors.General.ArgumentNullException(), null);
                            }
                            catch (Exception ex)
                            {
                                return new(Errors.General.ErrorGetById("TicketReasonCategoryV2"), null);
                            }
                        }
                    default:
                        {
                            return new(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
        }

        public async Task<Tuple<Error, CustomDto.CustomLocationDto>> UpdateV2Async(BusinessLogicRequest request)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");

            if ((dataContextType == null) || (dataContextType.Name?.Length == 0) || (dataContextType.Name?.Length == 0))
                return new(Errors.General.ConnectionTimeout(), null);
            else
            {
                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            try
                            {                              
                                DevelopmentModel.LocationV2 updatedEntity = new();
                                CustomLocationDto castedDto = new();
                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();
                                DevelopmentModel.LocationV2 sonucEntity;

                                using (DiasFacilityManagementSqlServerContext)
                                {
                                    if ((request == null) || (request.RequestDtosTypes == null) || (request.RequestDtosJsons == null) ||
                                         (request.RequestDtosTypes.Count < 1) || (request.RequestDtosJsons.Count < 1) ||
                                               (!Type.Equals(request.RequestDtosTypes[0], typeof(CustomLocationDto))))
                                    {
                                        return new Tuple<Error, CustomDto.CustomLocationDto>(Errors.General.RequestNull("Location"), null);
                                    }

                                    castedDto = JsonConvert.DeserializeObject<CustomLocationDto>(request.RequestDtosJsons[0]);

                                    //Hiyerarşi id automapper da problem çıkardığı için veritabanından direk güncellenecek entityi alalım
                                    sonucEntity = await Task.Run(() =>
                                        DiasFacilityManagementSqlServerContext.LocationV2s.AsQueryable().Where(x => (x.HierarchyId.ToString() == castedDto.HierarchyId) &&
                                                                        (x.IsActive.HasValue && x.IsDeleted.HasValue) && (!x.IsDeleted.Value) && x.IsActive == true)
                                                                            .FirstOrDefault<DevelopmentModel.LocationV2>());

                                    //test
                                    //sonucEntity = await Task.Run(() =>
                                    //    DiasFacilityManagementSqlServerContext.LocationV2s.AsQueryable().Where(x => x.HierarchyId.ToString() == "/4/1/63/" && (x.IsActive.HasValue && x.IsDeleted.HasValue) && (!x.IsDeleted.Value) && x.IsActive == true)
                                    //    .FirstOrDefault<DevelopmentModel.LocationV2>());

                                    if (sonucEntity != null)
                                    {
                                        sonucEntity.LocationName = castedDto.LocationOriginalName;
                                        sonucEntity.LocationNumber = castedDto.LocationNumber;
                                        sonucEntity.LocationDescription = castedDto.LocationDescription;
                                        sonucEntity.NFC_Code = castedDto.NFC_Code;

                                        //test
                                        //sonucEntity.LocationName = "test1";
                                        //sonucEntity.LocationNumber = "testNumber1";

                                        if (sonucEntity.LocationDescription == null)
                                        {
                                            sonucEntity.LocationDescription = "";
                                        }

                                        await Task.Run(() => DiasFacilityManagementSqlServerContext.Update(sonucEntity));
                                        await Task.Run(() => _unitOfWork_EF.CompleteAsync(typeof(DevelopmentContext.DiasFacilityManagementSqlServer).Name, DiasFacilityManagementSqlServerContext));
                                    }
                                    else
                                    {
                                        return new Tuple<Error, CustomDto.CustomLocationDto>(Errors.General.ErrorUpdate("Location"), null);
                                    }                                    
                                }

                                return new(Errors.General.SuccessUpdate("Location"), castedDto);
                            }
                            catch (SqlException e) when ((e.Number == -1) || (e.Number == -2) || (e.Number == 53))
                            {
                                return new(Errors.General.ConnectionTimeout(), null);
                            }
                            catch (ArgumentNullException)
                            {
                                return new(Errors.General.ArgumentNullException(), null);
                            }
                            catch (Exception)
                            {
                                return new(Errors.General.ErrorUpdate("Location"), null);
                            }
                        }
                    default:
                        {
                            return new(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
        }

        public async Task<Tuple<Error, CustomDto.CustomLocationDto>> DeleteV2Async(BusinessLogicRequest request)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");

            if ((dataContextType == null) || (dataContextType.Name?.Length == 0) || (dataContextType.Name?.Length == 0))
                return new(Errors.General.ConnectionTimeout(), null);
            else
            {
                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            try
                            {
                                DevelopmentModel.LocationV2 updatedEntity = new();
                                CustomLocationDto castedDto = new();
                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();
                                DevelopmentModel.LocationV2 sonucEntity;

                                using (DiasFacilityManagementSqlServerContext)
                                {
                                    if ((request == null) || (request.RequestDtosTypes == null) || (request.RequestDtosJsons == null) ||
                                         (request.RequestDtosTypes.Count < 1) || (request.RequestDtosJsons.Count < 1) ||
                                               (!Type.Equals(request.RequestDtosTypes[0], typeof(CustomLocationDto))))
                                    {
                                        return new Tuple<Error, CustomDto.CustomLocationDto>(Errors.General.RequestNull("Location"), null);
                                    }

                                    castedDto = JsonConvert.DeserializeObject<CustomLocationDto>(request.RequestDtosJsons[0]);

                                    //Hiyerarşi id automapper da problem çıkardığı için veritabanından direk silinecek entityi alalım
                                    sonucEntity = await Task.Run(() =>
                                        DiasFacilityManagementSqlServerContext.LocationV2s.AsQueryable().Where(x => (x.HierarchyId.ToString() == castedDto.HierarchyId) &&
                                                                        (x.IsActive.HasValue && x.IsDeleted.HasValue) && (!x.IsDeleted.Value) && x.IsActive == true)
                                                                            .FirstOrDefault<DevelopmentModel.LocationV2>());

                                    //test
                                    //sonucEntity = await Task.Run(() =>
                                    //    DiasFacilityManagementSqlServerContext.LocationV2s.AsQueryable().Where(x => x.HierarchyId.ToString() == "/4/1/18/" && (x.IsActive.HasValue && x.IsDeleted.HasValue) && (!x.IsDeleted.Value) && x.IsActive == true)
                                    //    .FirstOrDefault<DevelopmentModel.LocationV2>());

                                    if (sonucEntity != null)
                                    {
                                        //silineceklerin altındakileri de çekelim(kendisi dahil)
                                        //IsActive ve IsDeleted kontrolü ileri değişiklerde tehlikeli o yüzden katmayalım
                                        List<DevelopmentModel.LocationV2> currentRecordListBelowDeletedIncludeItself = await Task.Run(() =>
                                                                        DiasFacilityManagementSqlServerContext.LocationV2s.AsQueryable().Where(x => x.HierarchyId.IsDescendantOf(sonucEntity.HierarchyId)).
                                                                            ToList<DevelopmentModel.LocationV2>());

                                        //Şimdi hepsini IsDeleted yapalım
                                        foreach (DevelopmentModel.LocationV2 item in currentRecordListBelowDeletedIncludeItself)
                                        {
                                            item.IsDeleted = true;
                                            await Task.Run(() => DiasFacilityManagementSqlServerContext.Update(item));
                                        }

                                        await Task.Run(() => _unitOfWork_EF.CompleteAsync(typeof(DevelopmentContext.DiasFacilityManagementSqlServer).Name, DiasFacilityManagementSqlServerContext));
                                    }
                                    else
                                    {
                                        return new Tuple<Error, CustomDto.CustomLocationDto>(Errors.General.ErrorDelete("Location"), null);
                                    }
                                }

                                return new(Errors.General.SuccessDelete("Location"), castedDto);
                            }
                            catch (SqlException e) when ((e.Number == -1) || (e.Number == -2) || (e.Number == 53))
                            {
                                return new(Errors.General.ConnectionTimeout(), null);
                            }
                            catch (ArgumentNullException)
                            {
                                return new(Errors.General.ArgumentNullException(), null);
                            }
                            catch (Exception)
                            {
                                return new(Errors.General.ErrorDelete("Location"), null);
                            }
                        }
                    default:
                        {
                            return new(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
        }


        public async Task<Tuple<Error, CustomDto.CustomLocationDto>> InsertLocationV2WithinParentHierarchyId(BusinessLogicRequest request)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");

            if ((dataContextType == null) || (dataContextType.Name?.Length == 0) || (dataContextType.Name?.Length == 0))
                return new(Errors.General.ConnectionTimeout(), null);
            else
            {
                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            try
                            {
                                DevelopmentModel.LocationV2 parentEntity;
                                DevelopmentModel.LocationV2 childEntity;
                                DevelopmentModel.LocationV2 addedEntity = new();
                                CustomDto.CustomLocationDto result = new();
                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();

                                using (DiasFacilityManagementSqlServerContext)
                                {
                                    if ((request == null) || (request.RequestDtosTypes == null) || (request.RequestDtosJsons == null) ||
                                         (request.RequestDtosTypes.Count < 1) || (request.RequestDtosJsons.Count < 1) ||
                                               (!Type.Equals(request.RequestDtosTypes[0], typeof(CustomLocationDto))))
                                    {
                                        return new Tuple<Error, CustomDto.CustomLocationDto>(Errors.General.RequestNull("Location"), null);
                                    }

                                    //test
                                    //CustomLocationDto test = new() { LocationOriginalName = "test3", LocationNumber = "testNumber3", ParentHierarchyFromUI = null };
                                    //string testJson =  JsonConvert.SerializeObject(test);
                                    //CustomLocationDto castedDto = JsonConvert.DeserializeObject<CustomLocationDto>(testJson);

                                    CustomLocationDto castedDto = JsonConvert.DeserializeObject<CustomLocationDto>(request.RequestDtosJsons[0]);

                                    if ((castedDto != null) && (!(String.IsNullOrEmpty(castedDto.LocationOriginalName))) && 
                                            (!(String.IsNullOrEmpty(castedDto.LocationNumber))))                                  
                                    {
                                        //en tepeye eklenecek
                                        if(String.IsNullOrEmpty(castedDto.ParentHierarchyFromUI))
                                        {
                                            //TODO: En tepeye eklemede mevcut mahal tablosunda çok uzun sürüyor, efektif bir yol bulana kadar iptal edildi.
                                            //Efektif bir yol bulunduğunda altdaki return kaldırılıp, commentler geri alanacak
                                            return new(Errors.General.ErrorInsert("Location"), null);

                                            ////En tepeye eklendiği vakit tüm mahaller ağaçta aşağı kayacaktır
                                            ////Önce kök ile yeni ekleneni yer değiştireceğiz
                                            ////Burada biraz katakuli yollara sapacağız
                                            ////Çünkü mevcut tabloda hiyerarşiyi bozamayız ancak kodda bozabiliriz
                                            ////kodda hiyerarşiyi tekrar dizayn edip tabloya basacağız

                                            ////root hiyerarşi idsini al
                                            //HierarchyId rootHiearchyId = HierarchyId.GetRoot();

                                            ////şimdi yeni ekleneni root yapalım
                                            //addedEntity = DtoMapper_DiasFacilityManagementSqlServer_Development_Custom.
                                            //                        Map<CustomDto.CustomLocationDto, DevelopmentModel.LocationV2>(castedDto);

                                            //addedEntity.HierarchyId = rootHiearchyId;

                                            ////eski olacak root'uda onun altına alalım
                                            ////güncel hiyerarşi idsini hesaplayalım
                                            //HierarchyId newHierarchyIdOldRoot =  addedEntity.HierarchyId.GetDescendant(null, null);

                                            ////altına almadan eski olacak rootun altındakileri çekelim(kendisi hariç)
                                            ////IsActive ve IsDeleted kontrolü ileri değişiklerde tehlikeli o yüzden katmayalım
                                            //List<DevelopmentModel.LocationV2> currentRecordListBelowOldRoot = await Task.Run(() =>
                                            //            DiasFacilityManagementSqlServerContext.LocationV2s.AsQueryable().Where(x => x.HierarchyId.IsDescendantOf(rootHiearchyId) && (x.HierarchyId != rootHiearchyId)).
                                            //                ToList<DevelopmentModel.LocationV2>());

                                            ////şimdi eski rootu alalım
                                            //DevelopmentModel.LocationV2 oldRootRecord = await Task.Run(() =>
                                            //            DiasFacilityManagementSqlServerContext.LocationV2s.AsQueryable().Where(x => x.HierarchyId == rootHiearchyId).Single());

                                            ////eski roota güncel hiyerarşi değerini atayalım
                                            //oldRootRecord.HierarchyId = newHierarchyIdOldRoot;
                                            //oldRootRecord.OldHierarchyId = rootHiearchyId;

                                            ////şimdi eski rootun altındaki childlerin hiyerarşi idsini güncelleyelim
                                            //foreach (DevelopmentModel.LocationV2 item in currentRecordListBelowOldRoot)
                                            //{
                                            //    item.OldHierarchyId = item.HierarchyId;
                                            //    item.HierarchyId = item.HierarchyId.GetReparentedValue(rootHiearchyId, newHierarchyIdOldRoot);
                                            //    await Task.Run(() => DiasFacilityManagementSqlServerContext.Update(item));
                                            //}

                                            ////Son olarak description null ise boş string yapalım(non null veritabanında)
                                            //if (addedEntity.LocationDescription == null)
                                            //{
                                            //    addedEntity.LocationDescription = "";
                                            //}

                                            ////Tüm tabloyu güncelleyelim
                                            //await Task.Run(() => DiasFacilityManagementSqlServerContext.AddAsync(addedEntity));
                                            //await Task.Run(() => DiasFacilityManagementSqlServerContext.Update(oldRootRecord));
                                            //await Task.Run(() => _unitOfWork_EF.CompleteAsync(typeof(DevelopmentContext.DiasFacilityManagementSqlServer).Name, DiasFacilityManagementSqlServerContext));
                                        }
                                        else//verilen parentin altına eklenecek
                                        {
                                            //IsActive ve IsDeleted kontrolü ileri değişiklerde tehlikeli o yüzden katmayalım
                                            parentEntity = await Task.Run(() =>
                                                DiasFacilityManagementSqlServerContext.LocationV2s.AsQueryable().Where(x => x.HierarchyId.ToString() == castedDto.ParentHierarchyFromUI)
                                                .First<DevelopmentModel.LocationV2>());

                                            HierarchyId parentHierarchyId = parentEntity.HierarchyId;                                           

                                              //Bu parentin altındaki tüm childlerin(varsa) en sonundakini al
                                              //IsActive ve IsDeleted kontrolü ileri değişiklerde tehlikeli o yüzden katmayalım
                                              childEntity = await Task.Run(() =>
                                                DiasFacilityManagementSqlServerContext.LocationV2s.AsQueryable().Where(x => x.HierarchyId.GetAncestor(1) == parentHierarchyId)
                                                    .OrderByDescending(x => x.HierarchyId).FirstOrDefault());

                                            //Eğer child yoksa eklenecek bu öğe ilk childdir
                                            HierarchyId lastChildHierarchyId;
                                            if (childEntity == null)
                                            {
                                                lastChildHierarchyId = null;
                                            }
                                            else//değilse son childin yanına eklenecektir
                                            {
                                                lastChildHierarchyId = childEntity.HierarchyId;
                                            }

                                            HierarchyId addedHierarchyId = parentHierarchyId.GetDescendant(lastChildHierarchyId, null);
                                            addedEntity = DtoMapper_DiasFacilityManagementSqlServer_Development_Custom.
                                                                                        Map<CustomDto.CustomLocationDto, DevelopmentModel.LocationV2>(castedDto);

                                            addedEntity.HierarchyId = addedHierarchyId;

                                            //Son olarak description null ise boş string yapalım(non null veritabanında)
                                            if(addedEntity.LocationDescription == null)
                                            {
                                                addedEntity.LocationDescription = "";
                                            }

                                            await Task.Run(() => DiasFacilityManagementSqlServerContext.AddAsync(addedEntity));
                                            await Task.Run(() => _unitOfWork_EF.CompleteAsync(typeof(DevelopmentContext.DiasFacilityManagementSqlServer).Name, DiasFacilityManagementSqlServerContext));
                                        }
                                    }
                                    else
                                    {
                                        return new Tuple<Error, CustomDto.CustomLocationDto>(Errors.General.ErrorInsert("Location"), null);
                                    }
                                }

                                result = DtoMapper_DiasFacilityManagementSqlServer_Development_Custom.
                                                                            Map<DevelopmentModel.LocationV2, CustomDto.CustomLocationDto>(addedEntity);

                                return new(Errors.General.SuccessInsert("Location"), result);
                            }
                            catch (SqlException e) when ((e.Number == -1) || (e.Number == -2) || (e.Number == 53))
                            {
                                return new(Errors.General.ConnectionTimeout(), null);
                            }
                            catch (ArgumentNullException)
                            {
                                return new(Errors.General.ArgumentNullException(), null);
                            }
                            catch (Exception)
                            {
                                return new(Errors.General.ErrorInsert("Location"), null);
                            }
                        }
                    default:
                        {
                            return new(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
        }

    }
}
