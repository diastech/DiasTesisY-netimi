using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiasBusinessLogic.InterfacesAbstracts.Abstracts.BusinessRules;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.BaseBusinessRules.SqlServer.Standart;
using DevelopmentTicketNoteInterface =  DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Standart;
using Microsoft.Data.SqlClient;
using DiasBusinessLogic.AutoMapper.Configuration;
using DevelopmentTicketNoteProfile = DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Development;
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
using DiasShared.Errors;
using DiasBusinessLogic.Shared.Error;

namespace DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.SqlServer.Development.Standart
{
    public class TicketNoteBusinessRules : BusinessRuleAbstract, DevelopmentTicketNoteInterface.ITicketNoteBusinessRules, IBaseTicketNoteBusinessRules
    {
        private static AutoMapperProfileMapper<DevelopmentTicketNoteProfile.TicketNoteProfile> DtoMapper_DiasFacilityManagementSqlServer_Development
            => new (new (DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer")));

        private readonly IUnitOfWork_EF _unitOfWork_EF;

        public TicketNoteBusinessRules() : this(DI_ServiceLocator.Current.Get<IUnitOfWork_EF>())
        {
        }

        private TicketNoteBusinessRules(IUnitOfWork_EF unitOfWork_EF)
        {
            _unitOfWork_EF = unitOfWork_EF;
        }

        public async Task<Tuple<Error, TicketNoteDto>> AddAsync(TicketNoteDto ticketNoteDto)
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
                                DevelopmentModel.TicketNote sonucEntity;
                                TicketNoteDto addedDto;

                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();
                                ticketNoteDto.Attachments = null;                                
                                TicketNote convertedEntity = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<TicketNoteDto, TicketNote>(ticketNoteDto);
                                await Task.Run(() => DiasFacilityManagementSqlServerContext.AddAsync(convertedEntity));
                                await Task.Run(() => _unitOfWork_EF.CompleteAsync(typeof(DevelopmentContext.DiasFacilityManagementSqlServer).Name, DiasFacilityManagementSqlServerContext));
                                addedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<TicketNote, TicketNoteDto>(convertedEntity);
                                
                                return new(Errors.General.SuccessInsert("TicketNote"), addedDto);
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
                                return new(Errors.General.ErrorInsert("TicketNote"), null);
                            }
                        }

                    default:
                        {
                            return new(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
        }

        public async Task<Tuple<Error, List<TicketNoteDto>>> UpdateAsync(List<TicketNoteDto> ticketNoteDto)
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
                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();
                                DevelopmentModel.Ticket sonucEntity;
                                List<TicketNoteDto> updatedDto = new List<TicketNoteDto>();

                                foreach (var item in ticketNoteDto)
                                {
                                    TicketNote convertedEntity = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<TicketNoteDto, TicketNote>(item);
                                    await Task.Run(() => DiasFacilityManagementSqlServerContext.Update(convertedEntity));
                                    var updatedTicketNote = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<TicketNote, TicketNoteDto>(convertedEntity);
                                    updatedDto.Add(updatedTicketNote);
                                }

                                return new(Errors.General.SuccessUpdate("TicketNote"), updatedDto);
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
                                return new(Errors.General.ErrorInsert("TicketNote"), null);
                            }
                        }

                    default:
                        {
                            return new(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
        }

        public async Task<Tuple<Error, List<TicketNoteDto>>> DeleteAsync(List<TicketNoteDto> ticketNoteDto)
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
                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();
                                DevelopmentModel.Ticket sonucEntity;
                                List<TicketNoteDto> updatedDto = new List<TicketNoteDto>();

                                foreach (var item in ticketNoteDto)
                                {
                                    TicketNote convertedEntity = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<TicketNoteDto, TicketNote>(item);
                                    convertedEntity.IsActive = false;
                                    convertedEntity.IsDeleted = true;
                                    await Task.Run(() => DiasFacilityManagementSqlServerContext.Update(convertedEntity));
                                    var updatedTicketNote = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<TicketNote, TicketNoteDto>(convertedEntity);
                                    updatedDto.Add(updatedTicketNote);
                                }
                                
                                return new(Errors.General.SuccessDelete("TicketNote"), updatedDto);
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
                                return new(Errors.General.ErrorInsert("TicketNote"), null);
                            }
                        }

                    default:
                        {
                            return new(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
        }

        public async Task<Tuple<Error, List<TicketNoteDto>>> GetNotesByTicketId(int ticketId)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new(Errors.General.NotFoundDatabaseServer(), null);
            }
            else
            {
                List<DevelopmentDto.TicketNoteDto> returnDto = new();

                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            try
                            {
                                List<DevelopmentModel.TicketNote> sonucEntity;

                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();

                                using (DiasFacilityManagementSqlServerContext)
                                {

                                    sonucEntity = await Task.Run(() =>
                                        DiasFacilityManagementSqlServerContext.TicketNotes.AsQueryable().Where(x => x.TicketId == ticketId && x.IsActive == true && x.IsDeleted == false).Include(x=>x.Attachments)
                                        .Include(x=>x.AddedByUser)
                                    .AsNoTracking<DevelopmentModel.TicketNote>().ToList<DevelopmentModel.TicketNote>());
                                }

                                List<DevelopmentDto.TicketNoteDto> convertedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<List<DevelopmentModel.TicketNote>, List<DevelopmentDto.TicketNoteDto>>(sonucEntity);
                                returnDto = convertedDto;

                                return new(Errors.General.SuccessGetById("TicketNote"), returnDto);
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
                                return new(Errors.General.ErrorGetById("TicketNote"), null);
                            }
                        }

                    default:
                        {
                            return new(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
        }

        public async Task<Tuple<Error, int>> GetNotesCountByTicketId(int ticketId)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new(Errors.General.NotFoundDatabaseServer(), -1);
            }
            else
            {
                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            try
                            {
                                int sonuc;
                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();

                                using (DiasFacilityManagementSqlServerContext)
                                {

                                    sonuc = await Task.Run(() =>
                                        DiasFacilityManagementSqlServerContext.TicketNotes.AsQueryable().Where(x => x.TicketId == ticketId && x.IsActive == true && x.IsDeleted == false)
                                         .AsNoTracking<DevelopmentModel.TicketNote>().ToList<DevelopmentModel.TicketNote>().Count);
                                }

                                return new(Errors.General.SuccessGetById("TicketNote"), sonuc);
                            }
                            catch (SqlException e) when ((e.Number == -1) || (e.Number == -2) || (e.Number == 53))
                            {
                                return new(Errors.General.ConnectionTimeout(), -1);
                            }
                            catch (ArgumentNullException)
                            {
                                return new(Errors.General.ArgumentNullException(), -1);
                            }
                            catch (Exception)
                            {
                                return new(Errors.General.ErrorGetById("TicketNote"), -1);
                            }
                        }

                    default:
                        {
                            return new(Errors.General.NotFoundDatabaseServer(), -1);
                        }
                }
            }
        }

        public async Task<Tuple<Error, TicketNoteDto>> DeleteSingleAsync(TicketNoteDto ticketNoteDto)
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
                                DevelopmentModel.TicketNote sonucEntity;
                                TicketNoteDto addedDto;

                                DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();
                                ticketNoteDto.Attachments = null;
                                TicketNote convertedEntity = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<TicketNoteDto, TicketNote>(ticketNoteDto);
                                convertedEntity.IsDeleted = true;
                                convertedEntity.IsActive = false;
                                convertedEntity.AddedByUser = null;

                                await Task.Run(() => DiasFacilityManagementSqlServerContext.Update(convertedEntity));
                                await Task.Run(() => _unitOfWork_EF.CompleteAsync(typeof(DevelopmentContext.DiasFacilityManagementSqlServer).Name, DiasFacilityManagementSqlServerContext));
                                addedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<TicketNote, TicketNoteDto>(convertedEntity);

                                return new(Errors.General.SuccessInsert("TicketNote"), addedDto);
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
                                return new(Errors.General.ErrorInsert("TicketNote"), null);
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
