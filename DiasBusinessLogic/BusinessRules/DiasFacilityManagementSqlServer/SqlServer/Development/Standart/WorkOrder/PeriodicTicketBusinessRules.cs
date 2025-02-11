﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiasBusinessLogic.InterfacesAbstracts.Abstracts.BusinessRules;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.BaseBusinessRules.SqlServer.Standart;
using DevelopmentPeriodicTicketInterface =  DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Standart;
using Microsoft.Data.SqlClient;
using DiasBusinessLogic.AutoMapper.Configuration;
using DevelopmentPeriodicTicketProfile = DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Development;
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
using DevExtreme.AspNet.Data;
using DiasShared.Services.Communication.BusinessLogicMessage.ThirdPartyLibrary.DevExpress;
using DiasBusinessLogic.Shared.Error;
using DiasBusinessLogic.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Standart.Helper;
using DiasShared.Errors;
using DiasShared.Classes.Dto;
using DevExtreme.AspNet.Data.ResponseModel;

namespace DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.SqlServer.Development.Standart
{
    public class PeriodicTicketBusinessRules : BusinessRuleAbstract, DevelopmentPeriodicTicketInterface.IPeriodicTicketBusinessRules, IBasePeriodicTicketBusinessRules
    {
        private static AutoMapperProfileMapper<DevelopmentPeriodicTicketProfile.PeriodicTicketProfile> DtoMapper_DiasFacilityManagementSqlServer_Development
            => new(new(DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer")));

        private readonly IUnitOfWork_EF _unitOfWork_EF;

        public PeriodicTicketBusinessRules() : this(DI_ServiceLocator.Current.Get<IUnitOfWork_EF>())
        {
        }
        private PeriodicTicketBusinessRules(IUnitOfWork_EF unitOfWork_EF)
        {
            _unitOfWork_EF = unitOfWork_EF;
        }

        public async Task<Tuple<Error, DevExpressLoadResultDto<List<DevelopmentDto.PeriodicTicketDto>>>> GetAllPeriodicTicketsAsync(DevExpressRequest devExpressRequestObj)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");

            if ((dataContextType == null) || (dataContextType.Name?.Length == 0) || (dataContextType.Name?.Length == 0))
                return new(Errors.General.NotFoundDatabaseServer(), null);
            else
            {
                List<DevelopmentDto.PeriodicTicketDto> returnDtoList = new();
                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            try
                            {
                                List<DevelopmentModel.PeriodicTicket> sonucEntityList;
                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();
                                LoadResult returnedLoadResult = null;
                                DevExpressLoadResultDto<List<PeriodicTicketDto>> returnLoadResultDto;


                                using (DiasFacilityManagementSqlServerContext)
                                {
                                    //Şimdilik sadece grid requestleri dinliyoruz
                                    if ((devExpressRequestObj != null) && (devExpressRequestObj.RequestOptions != null) &&
                                          (devExpressRequestObj.RequestOptions.DataSourceLoadOption != null) &&
                                          (((devExpressRequestObj.RequestOptions.DataSourceLoadOption.Filter != null) &&
                                              (devExpressRequestObj.RequestOptions.DataSourceLoadOption.Filter.Count > 0)) ||
                                               (devExpressRequestObj.RequestOptions.DataSourceLoadOption.Take > 0)))
                                    {
                                        devExpressRequestObj.RequestOptions.DataSourceLoadOption.Sort =
                                            new SortingInfo[1] { new SortingInfo() { Desc = true, Selector = "AddedTime" } };

                                        LoadResult filteredOrAndPaginatedResult = await DataSourceLoader.LoadAsync<DevelopmentModel.PeriodicTicket>
                                                                                    (DiasFacilityManagementSqlServerContext.PeriodicTickets.AsQueryable().Where(x => x.IsDeleted == false && x.IsActive == true).Include(x => x.AddedByUser).OrderBy(c => c.AddedTime),devExpressRequestObj.RequestOptions.DataSourceLoadOption);

                                        sonucEntityList = filteredOrAndPaginatedResult.data.Cast<DevelopmentModel.PeriodicTicket>().ToList();
                                        returnedLoadResult = new LoadResult() { totalCount = filteredOrAndPaginatedResult.totalCount };
                                    }
                                    else
                                    {
                                        sonucEntityList = await Task.Run(() =>
                                                DiasFacilityManagementSqlServerContext.PeriodicTickets.AsQueryable().Where(x => (x.IsActive.HasValue && x.IsDeleted.HasValue) && (!x.IsDeleted.Value) && x.IsActive == true).Include(x=>x.AddedByUser)
                                                .AsNoTracking<DevelopmentModel.PeriodicTicket>().ToList<DevelopmentModel.PeriodicTicket>());
                                    }
                                }

                                returnDtoList = DtoMapper_DiasFacilityManagementSqlServer_Development.
                                        Map<List<DevelopmentModel.PeriodicTicket>, List<DevelopmentDto.PeriodicTicketDto>>(sonucEntityList);

                                returnLoadResultDto = new(returnDtoList, returnedLoadResult);

                                return new(Errors.General.GetListSuccess("PeriodicTicket"), returnLoadResultDto);

                                //return new(Errors.General.GetListSuccess("PeriodicTicket"), returnDtoList);
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
                                return new(Errors.General.GridListError("PeriodicTicket"), null);
                            }
                        }
                    default:
                        {
                            return new(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
        }

        public async Task<Tuple<Error, DevelopmentDto.PeriodicTicketDto>> GetPeriodicTicketByIdAsync(int Id)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");
            if ((dataContextType == null) || (dataContextType.Name?.Length == 0) || (dataContextType.Name?.Length == 0))

                return new(Errors.General.ConnectionTimeout(), null);
            else
            {
                DevelopmentDto.PeriodicTicketDto returnDto = new();
                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            try
                            {
                                DevelopmentModel.PeriodicTicket sonucEntity;
                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();
                                using (DiasFacilityManagementSqlServerContext)
                                {
                                    sonucEntity = await Task.Run(() =>
                                    DiasFacilityManagementSqlServerContext.PeriodicTickets.AsQueryable().Where(x => x.Id == Id && (x.IsActive.HasValue && x.IsDeleted.HasValue) && (!x.IsDeleted.Value) && x.IsActive == true).IncludeMultiple(x => x.TicketReason)
                                    .FirstOrDefault<DevelopmentModel.PeriodicTicket>());
                                }
                                DevelopmentDto.PeriodicTicketDto convertedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<DevelopmentModel.PeriodicTicket, DevelopmentDto.PeriodicTicketDto>(sonucEntity);
                                returnDto = convertedDto;

                                return new(Errors.General.SuccessGetById("PeriodicTicket"), returnDto);
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
                                return new(Errors.General.ErrorGetById("PeriodicTicket"), null);
                            }
                        }
                    default:
                        {
                            return new(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
        }

        public async Task<Tuple<Error, PeriodicTicketDto>> AddAsync(PeriodicTicketDto periodicTicketDto)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new(Errors.General.ConnectionTimeout(), null);
            }
            else
            {
                DevelopmentDto.PeriodicTicketDto returnDto = new();

                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            try
                            {
                                DevelopmentModel.PeriodicTicket sonucEntity;
                                PeriodicTicketDto addedDto;

                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();

                                PeriodicTicket convertedEntity = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<PeriodicTicketDto, PeriodicTicket>(periodicTicketDto);

                                await Task.Run(() => DiasFacilityManagementSqlServerContext.AddAsync(convertedEntity));
                                await Task.Run(() => _unitOfWork_EF.CompleteAsync(typeof(DevelopmentContext.DiasFacilityManagementSqlServer).Name, DiasFacilityManagementSqlServerContext));

                                addedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<PeriodicTicket, PeriodicTicketDto>(convertedEntity);


                                return new(Errors.General.SuccessInsert("PeriodicTicket"), addedDto);
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
                                return new(Errors.General.ErrorInsert("PeriodicTicket"), null);
                            }
                        }

                    default:
                        {
                            return new(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
        }

        public async Task<Tuple<Error, PeriodicTicketDto>> UpdateAsync(PeriodicTicketDto periodicTicketDto)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new(Errors.General.ConnectionTimeout(), null);
            }
            else
            {
                DevelopmentDto.PeriodicTicketDto returnDto = new();

                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            try
                            {
                                DevelopmentModel.PeriodicTicket sonucEntity;
                                PeriodicTicketDto updatedDto;

                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();

                                PeriodicTicket convertedEntity = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<PeriodicTicketDto, PeriodicTicket>(periodicTicketDto);

                                await Task.Run(() => DiasFacilityManagementSqlServerContext.Update(convertedEntity));

                                updatedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<PeriodicTicket, PeriodicTicketDto>(convertedEntity);


                                return new(Errors.General.SuccessUpdate("PeriodicTicket"), updatedDto);
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
                                return new(Errors.General.ErrorUpdate("PeriodicTicket"), null);
                            }
                        }

                    default:
                        {
                            return new(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
        }

        public async Task<Tuple<Error, PeriodicTicketDto>> DeleteAsync(PeriodicTicketDto periodicTicketDto)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new(Errors.General.ConnectionTimeout(), null);
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
                                DevelopmentModel.PeriodicTicket sonucEntity;
                                PeriodicTicketDto updatedDto;

                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();
                                periodicTicketDto.IsActive = false;

                                PeriodicTicket convertedEntity = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<PeriodicTicketDto, PeriodicTicket>(periodicTicketDto);

                                await Task.Run(() => DiasFacilityManagementSqlServerContext.Update(convertedEntity));

                                updatedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<PeriodicTicket, PeriodicTicketDto>(convertedEntity);


                                return new(Errors.General.SuccessDelete("PeriodicTicket"), updatedDto);
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
                                return new(Errors.General.ErrorDelete("PeriodicTicket"), null);
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

