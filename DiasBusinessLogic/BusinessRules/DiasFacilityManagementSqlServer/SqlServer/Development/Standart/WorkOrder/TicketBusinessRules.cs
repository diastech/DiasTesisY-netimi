using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiasBusinessLogic.InterfacesAbstracts.Abstracts.BusinessRules;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.BaseBusinessRules.SqlServer.Standart;
using DevelopmentTicketInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Standart;
using Microsoft.Data.SqlClient;
using DiasBusinessLogic.AutoMapper.Configuration;
using DevelopmentTicketProfile = DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Development;
using DiasBusinessLogic.Shared.Configuration;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.UnitOfWork;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using DevelopmentModel = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using DevelopmentContext = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.Configuration;
using Microsoft.EntityFrameworkCore;
using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using DiasShared.Services.Communication.BusinessLogicMessage.ThirdPartyLibrary.DevExpress;
using DevExtreme.AspNet.Data;
using DiasBusinessLogic.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Standart.Helper;
using DiasShared.Errors;
using DiasBusinessLogic.Shared.Error;
using DevExtreme.AspNet.Data.ResponseModel;
using DiasShared.Classes.Dto;
using DevelopmentUserInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Standart;
using static DiasShared.Enums.Standart.TicketEnums;
using DiasShared.Services.Communication.BusinessLogicMessage;
using Newtonsoft.Json;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;

namespace DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.SqlServer.Development.Standart
{
    public class TicketBusinessRules : BusinessRuleAbstract, DevelopmentTicketInterface.ITicketBusinessRules, IBaseTicketBusinessRules
    {
        private readonly DevelopmentUserInterface.ITicketReasonBusinessRules _ticketReasonBusinessRules;

        private static AutoMapperProfileMapper<DevelopmentTicketProfile.TicketProfile> DtoMapper_DiasFacilityManagementSqlServer_Development
            => new(new(DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer")));

        private readonly IUnitOfWork_EF _unitOfWork_EF;

        public TicketBusinessRules() : this(DI_ServiceLocator.Current.Get<IUnitOfWork_EF>(),
                                               DI_ServiceLocator.Current.Get<DevelopmentUserInterface.ITicketReasonBusinessRules>())
        {
        }

        private TicketBusinessRules(IUnitOfWork_EF unitOfWork_EF, DevelopmentUserInterface.ITicketReasonBusinessRules ticketReasonBusinessRules)
        {
            _unitOfWork_EF = unitOfWork_EF;
            _ticketReasonBusinessRules = ticketReasonBusinessRules;
        }


        public async Task<Tuple<Error, DevExpressLoadResultDto<List<TicketDto>>>> GetAllTicketAsync(DevExpressRequest devExpressRequestObj,int kullaniciId,List<int> assignmentGroupId,List<int> ticketStatus, bool isAdmin = false)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new(Errors.General.NotFoundDatabaseServer(), null);
            }
            else
            {
                List<DevelopmentDto.TicketDto> returnDtoList = new();

                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            try
                            {
                                List<DevelopmentModel.Ticket> sonucEntityList;

                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();
                                LoadResult returnedLoadResult = null;
                                DevExpressLoadResultDto<List<TicketDto>> returnLoadResultDto;

                                using (DiasFacilityManagementSqlServerContext)
                                {
                                    if ((devExpressRequestObj != null) && (devExpressRequestObj.RequestOptions != null) &&      
                                          (devExpressRequestObj.RequestOptions.DataSourceLoadOption != null) &&
                                          (((devExpressRequestObj.RequestOptions.DataSourceLoadOption.Filter != null) &&
                                              (devExpressRequestObj.RequestOptions.DataSourceLoadOption.Filter.Count > 0)) ||
                                               (devExpressRequestObj.RequestOptions.DataSourceLoadOption.Take > 0)))
                                    {
                                        //TODO: Default sortlamayı konfigüre edebilir miyiz(mesela bir konfigürasyon dosyasından?)
                                        devExpressRequestObj.RequestOptions.DataSourceLoadOption.Sort =
                                            new SortingInfo[1] { new SortingInfo() { Desc = true, Selector = "AddedTime" } };

                                        //TODO:Wherelemeden tüm tabloyu includeladığımızdan performans problemi çıkar
                                        //TODO:Devexpress dökümanlarını araştırıp include'ı devExpressRequestObj.RequestOptions üzerinden yapabilir miyiz araştır
                                        LoadResult filteredOrAndPaginatedResult = await DataSourceLoader.LoadAsync<DevelopmentModel.Ticket>
                                                                                    (DiasFacilityManagementSqlServerContext.Tickets.AsQueryable().Where(x => (x.TicketOpenedTime > DateTime.Now.AddDays(-2) || (x.TicketStatusId != (int)TicketStatusEnum.CLOSED)) &&
                                                                                                                                (x.IsDeleted == false) && (x.IsActive == true))
                                                                                            .Include(x => x.TicketPriority)
                                                                                            .Include(x => x.AddedByUser)
                                                                                            .Include(x => x.LastModifiedByUser)
                                                                                            .Include(x => x.TicketRelatedLocations.
                                                                                                    Where(y => (y.IsActive == true && y.IsDeleted == false) && (y.TicketLocation != null && y.TicketLocation.IsActive == true && y.TicketLocation.IsDeleted == false)))
                                                                                                        .ThenInclude(x => x.TicketLocation)
                                                                                            .Include(x => x.TicketReason).ThenInclude(x => x.TicketReasonCategory)
                                                                                            .Include(x => x.TicketStatus)
                                                                                            .IncludeMultiple(z => z.TickedAssignedAssignmentGroup, x => x.TicketAssignedUser, x => x.AddedByUser).OrderBy(c => c.AddedTime)
                                                                                             , devExpressRequestObj.RequestOptions.DataSourceLoadOption);
                                        
                                        sonucEntityList = filteredOrAndPaginatedResult.data.Cast<DevelopmentModel.Ticket>().ToList();

                                        returnedLoadResult = new LoadResult() { totalCount = filteredOrAndPaginatedResult.totalCount };
                                    }
                                    else
                                    {
                                        sonucEntityList = await Task.Run(() =>
                                        DiasFacilityManagementSqlServerContext.Tickets.AsQueryable().Where(x => (x.TicketOpenedTime > DateTime.Now.AddDays(-2) || (x.TicketStatusId != (int)TicketStatusEnum.CLOSED)) &&
                                                                                                                                (x.IsDeleted == false) && (x.IsActive == true))
                                            .Include(x => x.TicketPriority)
                                            .Include(x => x.AddedByUser)
                                            .Include(x => x.LastModifiedByUser)
                                            .Include(x => x.TicketRelatedLocations.
                                                    Where(y => (y.IsActive == true && y.IsDeleted == false) && (y.TicketLocation != null && y.TicketLocation.IsActive == true && y.TicketLocation.IsDeleted == false)))
                                                        .ThenInclude(x => x.TicketLocation)
                                            .Include(x => x.TicketReason).ThenInclude(x => x.TicketReasonCategory)
                                            .Include(x => x.TicketStatus)
                                            .IncludeMultiple(z => z.TickedAssignedAssignmentGroup, x => x.TicketAssignedUser, x => x.AddedByUser).OrderBy(c => c.AddedTime)
                                            .AsNoTracking<DevelopmentModel.Ticket>().ToList<DevelopmentModel.Ticket>());
                                    }
                                }

                                returnDtoList = DtoMapper_DiasFacilityManagementSqlServer_Development.
                                        Map<List<DevelopmentModel.Ticket>, List<DevelopmentDto.TicketDto>>(sonucEntityList);

                                returnLoadResultDto = new(returnDtoList, returnedLoadResult);

                                return new(Errors.General.GetListSuccess("Ticket"), returnLoadResultDto);
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
                                return new(Errors.General.GridListError("Ticket"), null);
                            }
                        }

                    default:
                        {
                            return new(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
        } 

        public async Task<Tuple<Error, List<TicketDto>>> GetAllTicketsMobileAsync(DevExpressRequest devExpressRequestObj)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new(Errors.General.NotFoundDatabaseServer(), null);
            }
            else
            {
                List<DevelopmentDto.TicketDto> returnDtoList = new();

                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            try
                            {
                                List<DevelopmentModel.Ticket> sonucEntityList;

                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();

                                using (DiasFacilityManagementSqlServerContext)
                                {
                                    if ((devExpressRequestObj != null) && (devExpressRequestObj.RequestOptions != null) &&
                                          (devExpressRequestObj.RequestOptions.DataSourceLoadOption != null) &&
                                            (devExpressRequestObj.RequestOptions.DataSourceLoadOption.Filter != null) &&
                                                (devExpressRequestObj.RequestOptions.DataSourceLoadOption.Filter.Count > 0))
                                    {
                                        //TODO:Wherelemeden tüm tabloyu includeladığımızdan performans problemi çıkar
                                        //TODO:Devexpress dökümanlarını araştırıp include'ı devExpressRequestObj.RequestOptions üzerinden yapabilir miyiz araştır
                                        sonucEntityList = (await DataSourceLoader.LoadAsync<DevelopmentModel.Ticket>
                                                            (
                                                            DiasFacilityManagementSqlServerContext.Tickets.AsQueryable().Where(x => x.IsDeleted == false && x.IsActive == true)
                                                                .Include(x => x.TicketPriority)
                                                                .Include(x => x.AddedByUser)
                                                                .Include(x => x.LastModifiedByUser)
                                                                //.Include(x => x.TicketReportedUsers)
                                                                .Include(x => x.TicketRelatedLocations.
                                                                        Where(y => (y.IsActive == true && y.IsDeleted == false) && (y.TicketLocation != null && y.TicketLocation.IsActive == true && y.TicketLocation.IsDeleted == false)))
                                                                            .ThenInclude(x => x.TicketLocation)
                                                                .Include(x => x.TicketReason).ThenInclude(x => x.TicketReasonCategory)
                                                                .Include(x => x.TicketStatus).ThenInclude(x => x.TicketStateTransitionSourceTicketStates).ThenInclude(x => x.DestinationTicketState)
                                                                .IncludeMultiple(z => z.TickedAssignedAssignmentGroup, x => x.TicketAssignedUser, x => x.AddedByUser).OrderByDescending(c => c.AddedTime)
                                                                 , devExpressRequestObj.RequestOptions.DataSourceLoadOption))
                                                                   .data.Cast<DevelopmentModel.Ticket>().ToList();
                                    }
                                    else
                                    {
                                        sonucEntityList = await Task.Run(() =>
                                        DiasFacilityManagementSqlServerContext.Tickets.AsQueryable().Where(x => x.IsDeleted == false && x.IsActive == true)
                                            .Include(x => x.TicketPriority)
                                            .Include(x => x.AddedByUser)
                                            .Include(x => x.LastModifiedByUser)
                                            //.Include(x => x.TicketReportedUsers)
                                            .Include(x => x.TicketRelatedLocations.
                                                    Where(y => (y.IsActive == true && y.IsDeleted == false) && (y.TicketLocation != null && y.TicketLocation.IsActive == true && y.TicketLocation.IsDeleted == false)))
                                                        .ThenInclude(x => x.TicketLocation)
                                            .Include(x => x.TicketReason).ThenInclude(x => x.TicketReasonCategory)
                                            .Include(x => x.TicketStatus).ThenInclude(x => x.TicketStateTransitionSourceTicketStates).ThenInclude(x => x.DestinationTicketState)
                                            .IncludeMultiple(z => z.TickedAssignedAssignmentGroup, x => x.TicketAssignedUser, x => x.AddedByUser).OrderByDescending(c => c.AddedTime)
                                            .AsNoTracking<DevelopmentModel.Ticket>().ToList<DevelopmentModel.Ticket>());
                                    }
                                }

                                returnDtoList = DtoMapper_DiasFacilityManagementSqlServer_Development.
                                        Map<List<DevelopmentModel.Ticket>, List<DevelopmentDto.TicketDto>>(sonucEntityList);

                                return new(Errors.General.GetListSuccess("Ticket"), returnDtoList);
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
                                return new(Errors.General.GridListError("Ticket"), null);
                            }
                        }

                    default:
                        {
                            return new(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
        }

        public async Task<Tuple<Error, List<TicketDto>>> GetAllTicketsWithLocationFilterNonWebAsync(BusinessLogicRequest request)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new(Errors.General.NotFoundDatabaseServer(), null);
            }
            else
            {
                List<DevelopmentDto.TicketDto> returnDtoList = new();

                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            try
                            {
                                List<DevelopmentModel.Ticket> sonucEntityList;

                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();

                                if ((request == null) || (request.RequestDtosJsons == null) || (request.RequestDtosJsons.Count < 1))
                                {
                                    return new Tuple<Error, List<TicketDto>>(Errors.General.RequestNull("Ticket"), null);
                                }

                                CustomSubLocationFilterRequestDto castedDto  = JsonConvert.DeserializeObject<CustomSubLocationFilterRequestDto>(request.RequestDtosJsons[0]);
                                if (castedDto != null)
                                {
                                    string[] locationHierachyIds = castedDto.HierarchyIdArr;

                                    if ((locationHierachyIds != null) && (locationHierachyIds.Length > 0))
                                    {
                                        DevelopmentModel.LocationV2 parentLocationEntity;
                                        List<DevelopmentModel.LocationV2> descendantLocationEntityList = new();
                                        List<string> descendantLocationEntityFullHierarchyIds = new();

                                        using (DiasFacilityManagementSqlServerContext)
                                        {
                                            for (int i = 0; i < locationHierachyIds.Length; i++)
                                            {
                                                string locationHierachyId = locationHierachyIds[i];
                                                descendantLocationEntityList = new();

                                                if (!(String.IsNullOrEmpty(locationHierachyId)))
                                                {

                                                    parentLocationEntity = await Task.Run(() =>
                                                        DiasFacilityManagementSqlServerContext.LocationV2s.AsQueryable().Where(x => x.HierarchyId.ToString() == locationHierachyId && (x.IsActive.HasValue && x.IsDeleted.HasValue) && (!x.IsDeleted.Value) && x.IsActive == true)
                                                        .First<DevelopmentModel.LocationV2>());

                                                    //şimdi bunun varsa altındaki nodelara alalım(kendisi dahil)                                                    
                                                    descendantLocationEntityList = await Task.Run(() =>
                                                                                DiasFacilityManagementSqlServerContext.LocationV2s.AsQueryable()
                                                                                .Where(subNode => (subNode.HierarchyId.IsDescendantOf(parentLocationEntity.HierarchyId))
                                                                                         && (subNode.IsActive.HasValue && subNode.IsDeleted.HasValue)
                                                                                           && (!subNode.IsDeleted.Value) && subNode.IsActive == true)
                                                                                            .ToList<DevelopmentModel.LocationV2>());
                                                }
                                                else
                                                {
                                                    continue;
                                                }

                                                if(castedDto.OnTheSameLevel)
                                                {
                                                    foreach (LocationV2 item in descendantLocationEntityList)
                                                    {
                                                        descendantLocationEntityFullHierarchyIds.Add(item.HierarchyId.ToString());
                                                    }

                                                }
                                                else
                                                {
                                                    foreach (LocationV2 item in descendantLocationEntityList)
                                                    {
                                                        //unique kontrolü
                                                        if (!(descendantLocationEntityFullHierarchyIds.Contains(item.HierarchyId.ToString())))
                                                        {
                                                            descendantLocationEntityFullHierarchyIds.Add(item.HierarchyId.ToString());
                                                        }
                                                    }
                                                }
                                            }

                                            sonucEntityList = await Task.Run(() =>
                                                DiasFacilityManagementSqlServerContext.Tickets.AsQueryable().Where(x => x.IsDeleted == false && x.IsActive == true)
                                                    .Include(x => x.TicketRelatedLocations.Where(y => y.IsActive == true && y.IsDeleted == false)).ThenInclude(x => x.TicketLocation)
                                                    .Where(x => x.TicketRelatedLocations.Any(y => descendantLocationEntityFullHierarchyIds.Contains(y.TicketLocation.HierarchyId.ToString())))
                                                    .Include(x => x.TicketPriority)
                                                    .Include(x => x.AddedByUser)
                                                    .Include(x => x.LastModifiedByUser)
                                                    //.Include(x => x.TicketReportedUsers)
                                                    .Include(x => x.TicketReason).ThenInclude(x => x.TicketReasonCategory)
                                                    .Include(x => x.TicketStatus).ThenInclude(x => x.TicketStateTransitionSourceTicketStates).ThenInclude(x => x.DestinationTicketState)
                                                    .IncludeMultiple(z => z.TickedAssignedAssignmentGroup, x => x.TicketAssignedUser, x => x.AddedByUser).OrderByDescending(c => c.AddedTime)
                                                    .AsNoTracking<DevelopmentModel.Ticket>().ToList<DevelopmentModel.Ticket>());

                                            returnDtoList = DtoMapper_DiasFacilityManagementSqlServer_Development.
                                                    Map<List<DevelopmentModel.Ticket>, List<DevelopmentDto.TicketDto>>(sonucEntityList);

                                            return new(Errors.General.GetListSuccess("Ticket"), returnDtoList);

                                        }
                                    }
                                    else
                                    {
                                        return new(Errors.General.ErrorGetList("Ticket"), null);
                                    }
                                }
                                else
                                {
                                    return new(Errors.General.ErrorGetList("Ticket"), null);
                                }

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
                                return new(Errors.General.GridListError("Ticket"), null);
                            }
                            
                        }

                    default:
                        {
                            return new(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
        }

        public async Task<Tuple<Error, List<TicketDto>>> GetAllTicketWrapperWithTowerFilterNonWebAsync(BusinessLogicRequest request)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new(Errors.General.NotFoundDatabaseServer(), null);
            }
            else
            {
                List<DevelopmentDto.TicketDto> returnDtoList = new();

                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            try
                            {
                                List<DevelopmentModel.Ticket> sonucEntityList;

                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();

                                if ((request == null) || (request.RequestDtosJsons == null) || (request.RequestDtosJsons.Count < 1))
                                {
                                    return new Tuple<Error, List<TicketDto>>(Errors.General.RequestNull("Ticket"), null);
                                }

                                CustomSubLocationFilterRequestDto castedDto = JsonConvert.DeserializeObject<CustomSubLocationFilterRequestDto>(request.RequestDtosJsons[0]);
                                if (castedDto != null)
                                {
                                    string[] locationHierachyIds = castedDto.HierarchyIdArr;

                                    if ((locationHierachyIds != null) && (locationHierachyIds.Length == 1))
                                    {
                                        using (DiasFacilityManagementSqlServerContext)
                                        {
                                            string locationHierachyId = locationHierachyIds[0];

                                            if (!(String.IsNullOrEmpty(locationHierachyId)))
                                            {
                                                sonucEntityList = await Task.Run(() =>
                                                    DiasFacilityManagementSqlServerContext.Tickets.AsQueryable().Where(x => (x.TicketOpenedTime > DateTime.Now.AddDays(-2) || (x.TicketStatusId != (int)TicketStatusEnum.CLOSED)) &&
                                                                                                                                (x.IsDeleted == false) && (x.IsActive == true))
                                                        .Include(x => x.TicketRelatedLocations.
                                                                Where(y => (y.IsActive == true && y.IsDeleted == false) && (y.TicketLocation != null && y.TicketLocation.IsActive == true && y.TicketLocation.IsDeleted == false)))
                                                                    .ThenInclude(x => x.TicketLocation)
                                                        //Hiyerarşik tabloya göre alt childlar parentların stringini takip etmelidir
                                                        .Where(x => x.TicketRelatedLocations.Any(y => (y.TicketLocation.IsActive == true && y.IsDeleted == false) && (y.TicketLocation.HierarchyId.ToString().StartsWith(locationHierachyId))))
                                                        .Include(x => x.TicketPriority)
                                                        .Include(x => x.AddedByUser)
                                                        .Include(x => x.LastModifiedByUser)
                                                        //.Include(x => x.TicketReportedUsers)
                                                        .Include(x => x.TicketReason).ThenInclude(x => x.TicketReasonCategory)
                                                        .Include(x => x.TicketStatus).ThenInclude(x => x.TicketStateTransitionSourceTicketStates).ThenInclude(x => x.DestinationTicketState)
                                                        .IncludeMultiple(z => z.TickedAssignedAssignmentGroup, x => x.TicketAssignedUser, x => x.AddedByUser).OrderByDescending(c => c.AddedTime)
                                                        .AsNoTracking<DevelopmentModel.Ticket>().ToList<DevelopmentModel.Ticket>());

                                                returnDtoList = DtoMapper_DiasFacilityManagementSqlServer_Development.
                                                        Map<List<DevelopmentModel.Ticket>, List<DevelopmentDto.TicketDto>>(sonucEntityList);

                                                return new(Errors.General.GetListSuccess("Ticket"), returnDtoList);
                                            }
                                            else
                                            {
                                                return new(Errors.General.ErrorGetList("Ticket"), null);
                                            }                                          
                                        }
                                    }
                                    else
                                    {
                                        return new(Errors.General.ErrorGetList("Ticket"), null);
                                    }
                                }
                                else
                                {
                                    return new(Errors.General.ErrorGetList("Ticket"), null);
                                }

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
                                return new(Errors.General.GridListError("Ticket"), null);
                            }
                        }

                    default:
                        {
                            return new(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
        }

        public async Task<Tuple<Error, TicketDto>> GetTicketByIdAsync(int Id)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new(Errors.General.NotFoundDatabaseServer(), null);
            }
            else
            {
                DevelopmentDto.TicketDto returnDto = new();

                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            try
                            {
                                DevelopmentModel.Ticket sonucEntity;

                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();

                                using (DiasFacilityManagementSqlServerContext)
                                {

                                    sonucEntity = await Task.Run(() =>
                                        DiasFacilityManagementSqlServerContext.Tickets.AsQueryable().Where(x => x.Id == Id && x.IsActive == true && x.IsDeleted == false).Include(x => x.TicketNotes)
                                        .ThenInclude(x => x.Attachments)
                                        .Include(x => x.TicketRelatedLocations.Where(x => x.IsDeleted == false && x.IsActive == true)).ThenInclude(x => x.TicketLocation).Include(x => x.TicketReason).ThenInclude(x => x.TicketReasonCategory)
                                        .IncludeMultiple(z => z.TickedAssignedAssignmentGroup, x => x.TicketAssignedUser, x => x.AddedByUser, x => x.Attachments.Where(y => y.TicketNoteId == null))
                                        .Include(x => x.TicketStatus)
                                        //.ThenInclude(x => x.TicketStateTransitionSourceTicketStates).ThenInclude(x => x.DestinationTicketState)
                                        .FirstOrDefault<DevelopmentModel.Ticket>());
                                }

                                DevelopmentDto.TicketDto convertedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<DevelopmentModel.Ticket, DevelopmentDto.TicketDto>(sonucEntity);
                                returnDto = convertedDto;
                                return new(Errors.General.SuccessGetById("Ticket"), returnDto);
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
                                return new(Errors.General.ErrorGetById("Ticket"), null);
                            }
                        }

                    default:
                        {
                            return new(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
        }

        public async Task<Tuple<Error, List<TicketDto>>> GetAllTicketsByBasicTicketIdAsync(int Id)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new(Errors.General.NotFoundDatabaseServer(), null);
            }
            else
            {
                List<DevelopmentDto.TicketDto> returnDtoList = new();

                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            try
                            {
                                List<DevelopmentModel.Ticket> sonucEntityList;

                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();

                                using (DiasFacilityManagementSqlServerContext)
                                {
                                    sonucEntityList = await Task.Run(() =>
                                    DiasFacilityManagementSqlServerContext.Tickets.AsQueryable().Where(x => x.IsDeleted == false && x.IsActive == true && x.BasicTicketId == Id)
                                    .IncludeMultiple(x => x.TicketReason,x=>x.TickedAssignedAssignmentGroup,x=>x.TicketAssignedUser,x=>x.TicketStatus)
                                    .AsNoTracking<DevelopmentModel.Ticket>().ToList<DevelopmentModel.Ticket>());
                                }

                                returnDtoList = DtoMapper_DiasFacilityManagementSqlServer_Development.
                                        Map<List<DevelopmentModel.Ticket>, List<DevelopmentDto.TicketDto>>(sonucEntityList);

                                return new(Errors.General.SuccessGetById("Ticket"), returnDtoList);
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
                                return new(Errors.General.ErrorGetById("Ticket"), null);
                            }
                        }

                    default:
                        {
                            return new(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
        }

        public async Task<Tuple<Error, TicketDto>> AddAsync(TicketDto ticketDto)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new(Errors.General.NotFoundDatabaseServer(), null);
            }
            else
            {
                DevelopmentDto.TicketDto returnDto = new();

                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            try
                            {
                                TicketDto addedDto;

                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();

                                Ticket convertedEntity = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<TicketDto, Ticket>(ticketDto);
                                convertedEntity.TicketRelatedLocations = null;
                                convertedEntity.Attachments = null;
                                convertedEntity.FacilityId = 1;

                                //Müdahale ve çözüm sürelerini hesapla
                                int responseTime = 0;
                                int resolutionTime = 0;

                                Tuple<Error, TicketReasonDto> ticketReasonResult = await _ticketReasonBusinessRules.GetTicketReasonByIdAsync(convertedEntity.TicketReasonId);

                                if ((ticketReasonResult.Item1.BusinessOperationSucceed) && (ticketReasonResult.Item2 != null))
                                {
                                    responseTime = ticketReasonResult.Item2.ResponseTime;
                                    resolutionTime = ticketReasonResult.Item2.ResolutionTime;
                                }

                                //ExpectedResponseTime geldiğinden emin olalım 
                                if (convertedEntity.ExpectedResponseTime > DateTime.Now.AddYears(-1))
                                {
                                    convertedEntity.ExpectedResponseTime = convertedEntity.ExpectedResponseTime.AddMinutes(responseTime);
                                }
                                else
                                {
                                    convertedEntity.ExpectedResponseTime =DateTime.Now.AddMinutes(responseTime);
                                }

                                //ExpectedResolutionTime geldiğinden emin olalım 
                                if (convertedEntity.ExpectedResolutionTime > DateTime.Now.AddYears(-1))
                                {
                                    convertedEntity.ExpectedResolutionTime = convertedEntity.ExpectedResolutionTime.AddMinutes(resolutionTime);
                                }
                                else
                                {
                                    convertedEntity.ExpectedResolutionTime = DateTime.Now.AddMinutes(resolutionTime);
                                }


                                await Task.Run(() => DiasFacilityManagementSqlServerContext.AddAsync(convertedEntity));
                                await Task.Run(() => _unitOfWork_EF.CompleteAsync(typeof(DevelopmentContext.DiasFacilityManagementSqlServer).Name, DiasFacilityManagementSqlServerContext));

                                addedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<Ticket, TicketDto>(convertedEntity);

                                return new(Errors.General.SuccessInsert("Ticket"), addedDto);
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
                                return new(Errors.General.ErrorInsert("Ticket"), null);
                            }
                        }

                    default:
                        {
                            return new(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
        }

        public async Task<Tuple<Error, TicketDto>> UpdateAsync(TicketDto ticketDto, bool updateState = false)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new(Errors.General.NotFoundDatabaseServer(), null);
            }
            else
            {
                DevelopmentDto.TicketDto returnDto = new();

                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            try
                            {                         
                                TicketDto updatedDto;

                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();

                                Ticket convertedEntity = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<TicketDto, Ticket>(ticketDto);
                                convertedEntity.TicketRelatedLocations = null;
                                convertedEntity.Attachments = null;
                                convertedEntity.AddedByUser = null;
                                //convertedEntity.TicketReportedUsers = null;
                                convertedEntity.LastModifiedByUser = null;
                                convertedEntity.LastModifiedTime = DateTime.Now;

                                if (updateState)
                                {
                                    //Excele göre SLA süresini durdur veya devam ettir

                                    //Sla durdurma işaretleme
                                    //askıya alma, çözümleme, kapatma, iptal
                                    if ((convertedEntity.TicketStatusId == (int)TicketStatusEnum.SUSPENDED) ||
                                        (convertedEntity.TicketStatusId == (int)TicketStatusEnum.SOLVED) ||
                                         (convertedEntity.TicketStatusId == (int)TicketStatusEnum.CLOSED) ||
                                           (convertedEntity.TicketStatusId == (int)TicketStatusEnum.REJECTED))
                                    {
                                        convertedEntity.SlaStopTime = DateTime.Now;
                                    }
                                    else//Eğer isaretlenmiş sla durdurma varsa, müdahale ve çözüm sürelerini ileri taşı
                                    {
                                        //client muhakkak SlaStopTime değerini göndermiş olmalı
                                        if (convertedEntity.SlaStopTime.HasValue)
                                        {
                                            TimeSpan diffTime = DateTime.Now.Subtract(convertedEntity.SlaStopTime.Value);

                                            if (diffTime.Minutes > 0)
                                            {
                                                //Müdahale zamanında eğer değer varsa herhangi bir değişiklik yapmıyoruz, sadece çözüm zamanında
                                                if (!(convertedEntity.UserResponseTime.HasValue))
                                                {
                                                    convertedEntity.ExpectedResponseTime = convertedEntity.ExpectedResponseTime.AddMinutes(diffTime.Minutes);
                                                }

                                                convertedEntity.ExpectedResolutionTime = convertedEntity.ExpectedResolutionTime.AddMinutes(diffTime.Minutes);
                                            }
                                        }
                                    }
                                }

                                await Task.Run(() => DiasFacilityManagementSqlServerContext.Update(convertedEntity));
                                await Task.Run(() => _unitOfWork_EF.CompleteAsync(typeof(DevelopmentContext.DiasFacilityManagementSqlServer).Name, DiasFacilityManagementSqlServerContext));

                                updatedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<Ticket, TicketDto>(convertedEntity);

                                return new(Errors.General.SuccessUpdate("Ticket"), updatedDto);
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
                                return new(Errors.General.ErrorUpdate("Ticket"), null);
                            }
                        }

                    default:
                        {
                            return new(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
        }

        public async Task<Tuple<Error, TicketDto>> DeleteAsync(TicketDto ticketDto)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new(Errors.General.NotFoundDatabaseServer(), null);
            }
            else
            {
                DevelopmentDto.TicketDto returnDto = new();

                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            try
                            {
                                DevelopmentModel.Ticket sonucEntity;
                                TicketDto updatedDto;

                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();
                                ticketDto.IsActive = false;

                                Ticket convertedEntity = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<TicketDto, Ticket>(ticketDto);
                                
                                await Task.Run(() => DiasFacilityManagementSqlServerContext.Update(convertedEntity));

                                updatedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<Ticket, TicketDto>(convertedEntity);


                                return new(Errors.General.SuccessDelete("Ticket"), updatedDto);
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
                                return new(Errors.General.ErrorInsert("Ticket"), null);
                            }
                        }

                    default:
                        {
                            return new(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
        }

        public async Task<Tuple<Error, TicketDto>> GetTicketWithStatusByIdAsync(int Id)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new(Errors.General.NotFoundDatabaseServer(), null);
            }
            else
            {
                DevelopmentDto.TicketDto returnDto = new();

                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            try
                            {
                                DevelopmentModel.Ticket sonucEntity;

                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();

                                using (DiasFacilityManagementSqlServerContext)
                                {

                                    sonucEntity = await Task.Run(() =>
                                        DiasFacilityManagementSqlServerContext.Tickets.AsQueryable().Where(x => x.Id == Id && x.IsActive == true && x.IsDeleted == false)
                                        //.Include(x => x.TicketNotes)
                                        //.ThenInclude(x => x.Attachments)
                                        //.Include(x => x.TicketRelatedLocations.Where(x => x.IsDeleted == false && x.IsActive == true)).ThenInclude(x => x.TicketLocation).Include(x => x.TicketReason).ThenInclude(x => x.TicketReasonCategory)
                                        //.IncludeMultiple(z => z.TickedAssignedAssignmentGroup, x => x.TicketAssignedUser, x => x.AddedByUser, x => x.Attachments.Where(y => y.TicketNoteId == null))
                                        .Include(x => x.TicketStatus)
                                        .ThenInclude(x => x.TicketStateTransitionSourceTicketStates)
                                        .FirstOrDefault<DevelopmentModel.Ticket>());
                                }

                                DevelopmentDto.TicketDto convertedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<DevelopmentModel.Ticket, DevelopmentDto.TicketDto>(sonucEntity);
                                returnDto = convertedDto;
                                return new(Errors.General.SuccessGetById("Ticket"), returnDto);
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
                                return new(Errors.General.ErrorGetById("Ticket"), null);
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
