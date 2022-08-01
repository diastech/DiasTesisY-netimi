using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiasBusinessLogic.InterfacesAbstracts.Abstracts.BusinessRules;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.BaseBusinessRules.SqlServer.Standart;
using DevelopmentBasicTicketInterface =  DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Standart;
using Microsoft.Data.SqlClient;
using DiasBusinessLogic.AutoMapper.Configuration;
using DevelopmentBasicTicketProfile = DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Development;
using DiasBusinessLogic.Shared.Configuration;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.UnitOfWork;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using DevelopmentModel =  DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using DevelopmentContext =  DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.Configuration;
using Microsoft.EntityFrameworkCore;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using DiasShared.Services.Communication.BusinessLogicMessage.ThirdPartyLibrary.DevExpress;
using DevExtreme.AspNet.Data;
using DiasBusinessLogic.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Standart.Helper;
using DiasShared.Errors;
using DiasBusinessLogic.Shared.Error;

namespace DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.SqlServer.Development.Standart
{
    public class BasicTicketBusinessRules : BusinessRuleAbstract, DevelopmentBasicTicketInterface.IBasicTicketBusinessRules, IBaseBasicTicketBusinessRules
    {
        private static AutoMapperProfileMapper<DevelopmentBasicTicketProfile.BasicTicketProfile> DtoMapper_DiasFacilityManagementSqlServer_Development
            => new(new(DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer")));

        private readonly IUnitOfWork_EF _unitOfWork_EF;

        public BasicTicketBusinessRules() : this(DI_ServiceLocator.Current.Get<IUnitOfWork_EF>())
        {
        }

        private BasicTicketBusinessRules(IUnitOfWork_EF unitOfWork_EF)
        {
            _unitOfWork_EF = unitOfWork_EF;
        }
        public async Task<Tuple<Error, List<DevelopmentDto.BasicTicketDto>>> GetAllBasicTicketsAsync(DevExpressRequest devExpressRequestObj)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");
            if ((dataContextType == null) || (dataContextType.Name?.Length == 0) || (dataContextType.Name?.Length == 0))
                return new(Errors.General.NotFoundDatabaseServer(), null);
            else
            {
                List<DevelopmentDto.BasicTicketDto> returnDtoList = new();
                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            try
                            {
                                List<DevelopmentModel.BasicTicket> sonucEntityList;
                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();
                                using (DiasFacilityManagementSqlServerContext)
                                {
                                    if ((devExpressRequestObj != null) && (devExpressRequestObj.RequestOptions != null) &&
                                          (devExpressRequestObj.RequestOptions.DataSourceLoadOption != null) &&
                                            (devExpressRequestObj.RequestOptions.DataSourceLoadOption.Filter != null) &&
                                              (devExpressRequestObj.RequestOptions.DataSourceLoadOption.Filter.Count > 0))
                                    {
                                        sonucEntityList = (await DataSourceLoader.LoadAsync<DevelopmentModel.BasicTicket>
                                                            (DiasFacilityManagementSqlServerContext.BasicTickets, devExpressRequestObj.RequestOptions.DataSourceLoadOption))
                                                                .data.Cast<DevelopmentModel.BasicTicket>().ToList();
                                    }
                                    else
                                    {
                                        sonucEntityList = await Task.Run(() =>
                                        DiasFacilityManagementSqlServerContext.BasicTickets.AsQueryable().Where(x => (x.IsActive.HasValue && x.IsDeleted.HasValue) && (!x.IsDeleted.Value) && x.IsActive == true)
                                        .IncludeMultiple(x => x.Attachments,y=>y.StateOfBasicTicket,x=>x.Tickets, x => x.AddedByUser)
                                        .AsNoTracking<DevelopmentModel.BasicTicket>().ToList<DevelopmentModel.BasicTicket>());
                                    }
                                }

                                returnDtoList = DtoMapper_DiasFacilityManagementSqlServer_Development.
                                        Map<List<DevelopmentModel.BasicTicket>, List<DevelopmentDto.BasicTicketDto>>(sonucEntityList);

                                return new(Errors.General.GetListSuccess("BasicTicket"), returnDtoList);
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
                                return new(Errors.General.GridListError("BasicTicket"), null);
                            }
                        }
                    default:
                        {
                            return new(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
        }
        public async Task<Tuple<Error, DevelopmentDto.BasicTicketDto>> GetBasicTicketByIdAsync(int Id)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");
            if ((dataContextType == null) || (dataContextType.Name?.Length == 0) || (dataContextType.Name?.Length == 0))
                return new(Errors.General.NotFoundDatabaseServer(), null);
            else
            {
                DevelopmentDto.BasicTicketDto returnDto = new();
                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            try
                            {
                                DevelopmentModel.BasicTicket sonucEntity;
                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();
                                using (DiasFacilityManagementSqlServerContext)
                                {
                                    sonucEntity = await Task.Run(() =>
                                    DiasFacilityManagementSqlServerContext.BasicTickets.AsQueryable().Where(x => x.Id == Id && (x.IsActive.HasValue && x.IsDeleted.HasValue) && (!x.IsDeleted.Value) && x.IsActive == true).IncludeMultiple(x => x.Attachments)
                                    .FirstOrDefault<DevelopmentModel.BasicTicket>());
                                }
                                DevelopmentDto.BasicTicketDto convertedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<DevelopmentModel.BasicTicket, DevelopmentDto.BasicTicketDto>(sonucEntity);
                                returnDto = convertedDto;
                                return new(Errors.General.SuccessGetById("BasicTicket"), returnDto);
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
                                return new(Errors.General.ErrorGetById("BasicTicket"), null);
                            }
                        }
                    default:
                        {
                            return new(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
        }

        public async Task<Tuple<Error, BasicTicketDto>> AddAsync(BasicTicketDto basicTicketDto)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new(Errors.General.NotFoundDatabaseServer(), null);
            }
            else
            {
                DevelopmentDto.BasicTicketDto returnDto = new();

                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            try
                            {
                                DevelopmentModel.BasicTicket sonucEntity;
                                BasicTicketDto addedDto;

                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();

                                DevelopmentModel.BasicTicket convertedEntity = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<BasicTicketDto, DevelopmentModel.BasicTicket>(basicTicketDto);

                                await Task.Run(() => DiasFacilityManagementSqlServerContext.AddAsync(convertedEntity));
                                await Task.Run(() => _unitOfWork_EF.CompleteAsync(typeof(DevelopmentContext.DiasFacilityManagementSqlServer).Name, DiasFacilityManagementSqlServerContext));

                                addedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<DevelopmentModel.BasicTicket, BasicTicketDto>(convertedEntity);

                                return new(Errors.General.SuccessInsert("BasicTicket"), addedDto);
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
                                return new(Errors.General.ErrorInsert("BasicTicket"), null);
                            }
                        }

                    default:
                        {
                            return new(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
        }

        public Task<Tuple<Error, BasicTicketDto>> UpdateAsync(BasicTicketDto basicTicketDto)
        {
            throw new NotImplementedException();
        }

        public Task<Tuple<Error, BasicTicketDto>> DeleteAsync(BasicTicketDto basicTicketDto)
        {
            throw new NotImplementedException();
        }
    }
}