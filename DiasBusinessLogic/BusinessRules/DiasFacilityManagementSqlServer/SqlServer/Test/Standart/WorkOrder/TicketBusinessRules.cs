using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiasBusinessLogic.InterfacesAbstracts.Abstracts.BusinessRules;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.BaseBusinessRules.SqlServer.Standart;
using DevelopmentTicketInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Test.Standart;
using Microsoft.Data.SqlClient;
using DiasBusinessLogic.AutoMapper.Configuration;
using DevelopmentTicketProfile = DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Test;
using DiasBusinessLogic.Shared.Configuration;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.UnitOfWork;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard;
using DevelopmentModel = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Test.Models;
using DevelopmentContext = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Test.Models;
using DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.Configuration;
using static DiasShared.Enums.Standart.UserEnums;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Test.Models;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard;
using System.Linq.Expressions;
using DiasShared.Services.Communication.BusinessLogicMessage.ThirdPartyLibrary.DevExpress;
using DevExtreme.AspNet.Data;
using DiasBusinessLogic.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Standart.Helper;

namespace DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.SqlServer.Test.Standart
{
    public class TicketBusinessRules : BusinessRuleAbstract, DevelopmentTicketInterface.ITicketBusinessRules, IBaseTicketBusinessRules
    {
        private static AutoMapperProfileMapper<DevelopmentTicketProfile.TicketProfile> DtoMapper_DiasFacilityManagementSqlServer_Development
            => new(new(DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServerTest")));

        private readonly IUnitOfWork_EF _unitOfWork_EF;

        public TicketBusinessRules() : this(DI_ServiceLocator.Current.Get<IUnitOfWork_EF>())
        {
        }

        private TicketBusinessRules(IUnitOfWork_EF unitOfWork_EF)
        {
            _unitOfWork_EF = unitOfWork_EF;
        }

        public async Task<Tuple<ErrorCodes, List<TicketDto>>> GetAllTicketAsync(DevExpressRequest devExpressRequestObj)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServerTest");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new(ErrorCodes.UnknownError, null);
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
                                        sonucEntityList = (await DataSourceLoader.LoadAsync<DevelopmentModel.Ticket>
                                                            (DiasFacilityManagementSqlServerContext.Tickets, devExpressRequestObj.RequestOptions.DataSourceLoadOption))
                                                                .data.Cast<DevelopmentModel.Ticket>().ToList();
                                    }
                                    else
                                    {
                                        sonucEntityList = await Task.Run(() =>
                                        DiasFacilityManagementSqlServerContext.Tickets.AsQueryable().Where(x => x.IsDeleted == false && x.IsActive == true).Include(x => x.TicketNotes)
                                        .ThenInclude(x => x.Attachments)
                                        .Include(x => x.TicketRelatedLocations.Where(y=>y.IsActive ==  true && y.IsDeleted == false)).ThenInclude(x => x.TicketLocation).Include(x => x.TicketReason).ThenInclude(x => x.TicketReasonCategory)
                                        .IncludeMultiple(z => z.TickedAssignedAssignmentGroup, x => x.TicketAssignedUser, x => x.AddedByUser, x => x.Attachments.Where(y => y.TicketNoteId == null))
                                        .AsNoTracking<DevelopmentModel.Ticket>().ToList<DevelopmentModel.Ticket>());
                                    }
                                }

                                returnDtoList = DtoMapper_DiasFacilityManagementSqlServer_Development.
                                        Map<List<DevelopmentModel.Ticket>, List<DevelopmentDto.TicketDto>>(sonucEntityList);

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

        public async Task<Tuple<ErrorCodes, TicketDto>> GetTicketByIdAsync(int Id)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServerTest");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new(ErrorCodes.UnknownError, null);
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
                                        .FirstOrDefault<DevelopmentModel.Ticket>());
                                }

                                DevelopmentDto.TicketDto convertedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<DevelopmentModel.Ticket, DevelopmentDto.TicketDto>(sonucEntity);
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

        public async Task<Tuple<ErrorCodes, List<TicketDto>>> GetAllTicketsByBasicTicketIdAsync(int Id)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServerTest");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new(ErrorCodes.UnknownError, null);
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

        public async Task<Tuple<ErrorCodes, TicketDto>> AddAsync(TicketDto ticketDto)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServerTest");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new(ErrorCodes.UnknownError, null);
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
                                convertedEntity.TicketAssignedUserId = 1;
                                convertedEntity.TicketReportedUserId = 1;
                                convertedEntity.TicketStatusId = 1;
                                await Task.Run(() => DiasFacilityManagementSqlServerContext.AddAsync(convertedEntity));
                                await Task.Run(() => _unitOfWork_EF.CompleteAsync(typeof(DevelopmentContext.DiasFacilityManagementSqlServer).Name, DiasFacilityManagementSqlServerContext));

                                addedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<Ticket, TicketDto>(convertedEntity);
                                                                
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

        public async Task<Tuple<ErrorCodes, TicketDto>> UpdateAsync(TicketDto ticketDto)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServerTest");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new(ErrorCodes.UnknownError, null);
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

                                await Task.Run(() => DiasFacilityManagementSqlServerContext.Update(convertedEntity));
                                await Task.Run(() => _unitOfWork_EF.CompleteAsync(typeof(DevelopmentContext.DiasFacilityManagementSqlServer).Name, DiasFacilityManagementSqlServerContext));

                                updatedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<Ticket, TicketDto>(convertedEntity);


                                return new(ErrorCodes.None, updatedDto);
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

        public async Task<Tuple<ErrorCodes, TicketDto>> DeleteAsync(TicketDto ticketDto)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServerTest");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new(ErrorCodes.UnknownError, null);
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


                                return new(ErrorCodes.None, updatedDto);
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

    }
}
