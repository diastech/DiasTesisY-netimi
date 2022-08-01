using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiasBusinessLogic.InterfacesAbstracts.Abstracts.BusinessRules;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.BaseBusinessRules.SqlServer.Standart;
using DevelopmentBasicTicketInterface =  DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Test.Standart;
using Microsoft.Data.SqlClient;
using DiasBusinessLogic.AutoMapper.Configuration;
using DevelopmentBasicTicketProfile = DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Test;
using DiasBusinessLogic.Shared.Configuration;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.UnitOfWork;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard;
using DevelopmentModel =  DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Test.Models;
using DevelopmentContext =  DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Test.Models;
using DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.Configuration;
using Microsoft.EntityFrameworkCore;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard;
using DiasShared.Services.Communication.BusinessLogicMessage.ThirdPartyLibrary.DevExpress;
using DevExtreme.AspNet.Data;
using DiasBusinessLogic.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Standart.Helper;

namespace DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.SqlServer.Test.Standart
{
    public class BasicTicketBusinessRules : BusinessRuleAbstract, DevelopmentBasicTicketInterface.IBasicTicketBusinessRules, IBaseBasicTicketBusinessRules
    {
        private static AutoMapperProfileMapper<DevelopmentBasicTicketProfile.BasicTicketProfile> DtoMapper_DiasFacilityManagementSqlServer_Development
            => new(new(DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServerTest")));

        private readonly IUnitOfWork_EF _unitOfWork_EF;

        public BasicTicketBusinessRules() : this(DI_ServiceLocator.Current.Get<IUnitOfWork_EF>())
        {
        }

        private BasicTicketBusinessRules(IUnitOfWork_EF unitOfWork_EF)
        {
            _unitOfWork_EF = unitOfWork_EF;
        }
        public async Task<Tuple<ErrorCodes, List<DevelopmentDto.BasicTicketDto>>> GetAllBasicTicketsAsync(DevExpressRequest devExpressRequestObj)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServerTest");
            if ((dataContextType == null) || (dataContextType.Name?.Length == 0) || (dataContextType.Name?.Length == 0))
                return new(ErrorCodes.UnknownError, null);
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
                                        DiasFacilityManagementSqlServerContext.BasicTickets.AsQueryable().Where(x => !x.IsDeleted && x.IsActive == true)
                                        .IncludeMultiple(x => x.Attachments,y=>y.StateOfBasicTicket,x=>x.Tickets)
                                        .AsNoTracking<DevelopmentModel.BasicTicket>().ToList<DevelopmentModel.BasicTicket>());
                                    }
                                }

                                returnDtoList = DtoMapper_DiasFacilityManagementSqlServer_Development.
                                        Map<List<DevelopmentModel.BasicTicket>, List<DevelopmentDto.BasicTicketDto>>(sonucEntityList);

                                return new(ErrorCodes.None, returnDtoList);
                            }
                            catch (SqlException e) when ((e.Number == -1) || (e.Number == -2) || (e.Number == 53))
                            {
                                return new(ErrorCodes.UnknownError, null);
                            }
                            catch (ArgumentNullException)
                            {
                                return new(ErrorCodes.UnknownError, null);
                            }
                            catch (Exception e)
                            {
                                return new(ErrorCodes.UnknownError, null);
                            }
                        }
                    default:
                        {
                            return new(ErrorCodes.UnknownError, null);
                        }
                }
            }
        }
        public async Task<Tuple<ErrorCodes, DevelopmentDto.BasicTicketDto>> GetBasicTicketByIdAsync(int Id)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServerTest");
            if ((dataContextType == null) || (dataContextType.Name?.Length == 0) || (dataContextType.Name?.Length == 0))
                return new(ErrorCodes.UnknownError, null);
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
                                    DiasFacilityManagementSqlServerContext.BasicTickets.AsQueryable().Where(x => x.Id == Id && !x.IsDeleted && x.IsActive == true).IncludeMultiple(x => x.Attachments)
                                    .FirstOrDefault<DevelopmentModel.BasicTicket>());
                                }
                                DevelopmentDto.BasicTicketDto convertedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<DevelopmentModel.BasicTicket, DevelopmentDto.BasicTicketDto>(sonucEntity);
                                returnDto = convertedDto;
                                return new(ErrorCodes.None, returnDto);
                            }
                            catch (SqlException e) when ((e.Number == -1) || (e.Number == -2) || (e.Number == 53))
                            {
                                return new(ErrorCodes.UnknownError, null);
                            }
                            catch (ArgumentNullException)
                            {
                                return new(ErrorCodes.UnknownError, null);
                            }
                            catch (Exception)
                            {
                                return new(ErrorCodes.UnknownError, null);
                            }
                        }
                    default:
                        {
                            return new(ErrorCodes.UnknownError, null);
                        }
                }
            }
        }

        public async Task<Tuple<ErrorCodes, BasicTicketDto>> AddAsync(BasicTicketDto basicTicketDto)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServerTest");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new(ErrorCodes.UnknownError, null);
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


                                return new(ErrorCodes.None, addedDto);
                            }
                            catch (SqlException e) when ((e.Number == -1) || (e.Number == -2) || (e.Number == 53))
                            {
                                return new(ErrorCodes.UnknownError, null);
                            }
                            catch (ArgumentNullException)
                            {
                                return new(ErrorCodes.UnknownError, null);
                            }
                            catch (Exception e)
                            {
                                return new(ErrorCodes.UnknownErrorOnGettingEntityFromUserTable, null);
                            }
                        }

                    default:
                        {
                            return new(ErrorCodes.UnknownError, null);
                        }
                }
            }
        }

        public Task<Tuple<ErrorCodes, BasicTicketDto>> UpdateAsync(BasicTicketDto basicTicketDto)
        {
            throw new NotImplementedException();
        }

        public Task<Tuple<ErrorCodes, BasicTicketDto>> DeleteAsync(BasicTicketDto basicTicketDto)
        {
            throw new NotImplementedException();
        }
    }
}