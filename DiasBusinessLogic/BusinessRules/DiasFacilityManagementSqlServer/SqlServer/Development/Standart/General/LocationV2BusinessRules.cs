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
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using DevelopmentModel =  DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using DevelopmentContext =  DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.Configuration;
using static DiasShared.Enums.Standart.UserEnums;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
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
                                    DiasFacilityManagementSqlServerContext.LocationV2s.AsQueryable().Where(x => (x.IsActive.HasValue && x.IsDeleted.HasValue) && (!x.IsDeleted.Value) && x.IsActive == true)
                                    .AsNoTracking<DevelopmentModel.LocationV2>().ToList<DevelopmentModel.LocationV2>());
                                }
                                var convertedDto = DtoMapper_DiasFacilityManagementSqlServer_Development_Custom.Map<List<DevelopmentModel.LocationV2>, List<CustomDto.CustomLocationDto>>(sonucEntityList);
                                var d = convertedDto.Where(x => x.Id == 2381385);
                                List<CustomDto.CustomLocationDto> locationV2s = new();
                                foreach (var item in d)
                                {
                                    locationV2s.Add(item);
                                }

                                //var b = convertedDto.Where(x=>x.LocationName=="MH1 - 1");
                                var a = convertedDto.Where(p => p.HierarchyId.StartsWith("/5/"));
                                
                                foreach (var item in a)
                                {
                                    locationV2s.Add(item);
                                }                                
                                return new(Errors.General.GetListSuccess("LocationV2"), locationV2s);
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
                                        return new Tuple<Error, IEnumerable<CustomDto.CustomLocationDto>> (Errors.General.RequestNull("Location"), null);
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
                                        return new Tuple<Error, IEnumerable<CustomDto.CustomLocationDto>> (Errors.General.RequestNull("Location"), null);
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


    }
}
