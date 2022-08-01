using DiasBusinessLogic.InterfacesAbstracts.Abstracts.BusinessRules;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Base.SqlServer.Custom;
using TransactionalInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevelopmentUserInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Standart;
using DevelopmentContext = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.Configuration;
using DiasDataAccessLayer.Enums;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DiasBusinessLogic.Shared.Configuration;
using Microsoft.EntityFrameworkCore.Storage;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Custom;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.UnitOfWork;
using CustomDevelopmentTicketProfile = DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.CustomProfiles.Development;
using DiasBusinessLogic.AutoMapper.Configuration;
using DiasShared.Services.Communication.BusinessLogicMessage;
using Newtonsoft.Json;
using DiasShared.Errors;
using DiasBusinessLogic.Shared.Error;
using static DiasShared.Enums.Standart.TicketEnums;

namespace DiasBusinessLogic.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Custom
{
    public class TicketWrapperTransactionalBusinessRules : BusinessRuleAbstract, TransactionalInterface.ITicketWrapperTransactionalBusinessRules, IBaseTicketWrapperTransactionalBusinessRules
    {
        private readonly DevelopmentUserInterface.ITicketBusinessRules _ticketBusinessRules;
        private readonly DevelopmentUserInterface.ITicketNoteBusinessRules _ticketNoteBusinessRules;
        private readonly DevelopmentUserInterface.IAttachmentBusinessRules _attachmentBusinessRules;
        private readonly DevelopmentUserInterface.ITicketRelatedLocationBusinessRules _ticketRelatedLocationBusinessRules;
        private readonly DevelopmentUserInterface.IBasicTicketBusinessRules _basicTicketBusinessRules;
        private readonly IUnitOfWork_EF _unitOfWork_EF;

        private static AutoMapperProfileMapper<CustomDevelopmentTicketProfile.CustomTicketProfile> DtoMapper_DiasFacilityManagementSqlServer_Development
            => new(new(DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer")));

        private static AutoMapperProfileMapper<CustomDevelopmentTicketProfile.CustomMobileTicketProfile> DtoMapperMobile_DiasFacilityManagementSqlServer_Development
            => new(new(DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer")));



        public TicketWrapperTransactionalBusinessRules() : this(
            DI_ServiceLocator.Current.Get<DevelopmentUserInterface.ITicketBusinessRules>(),
            DI_ServiceLocator.Current.Get<DevelopmentUserInterface.ITicketNoteBusinessRules>(),
            DI_ServiceLocator.Current.Get<DevelopmentUserInterface.IAttachmentBusinessRules>(),
            DI_ServiceLocator.Current.Get<DevelopmentUserInterface.ITicketRelatedLocationBusinessRules>(),
            DI_ServiceLocator.Current.Get<DevelopmentUserInterface.IBasicTicketBusinessRules>(),
            DI_ServiceLocator.Current.Get<IUnitOfWork_EF>())
        {

        }
        private TicketWrapperTransactionalBusinessRules(DevelopmentUserInterface.ITicketBusinessRules ticketBusinessRules,
            DevelopmentUserInterface.ITicketNoteBusinessRules ticketNoteBusinessRules,
            DevelopmentUserInterface.IAttachmentBusinessRules attachmentBusinessRules,
            DevelopmentUserInterface.ITicketRelatedLocationBusinessRules ticketRelatedLocationBusinessRules,
            DevelopmentUserInterface.IBasicTicketBusinessRules basicTicketBusinessRules,

            IUnitOfWork_EF unitOfWork_EF)
        {
            _ticketBusinessRules = ticketBusinessRules;
            _ticketNoteBusinessRules = ticketNoteBusinessRules;
            _attachmentBusinessRules = attachmentBusinessRules;
            _ticketRelatedLocationBusinessRules = ticketRelatedLocationBusinessRules;
            _basicTicketBusinessRules = basicTicketBusinessRules;
            _unitOfWork_EF = unitOfWork_EF;
        }

        #region WebClient

        public async Task<Tuple<Error, CustomTicketDto>> AddTicketWrapperAsync(BusinessLogicRequest request)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new Tuple<Error, CustomTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
            }
            else
            {
                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();

                            using (IDbContextTransaction transaction = DiasFacilityManagementSqlServerContext.Database.BeginTransaction())
                            {
                                try
                                {

                                    if ((request == null) || (request.RequestDtosTypes == null) || (request.RequestDtosJsons == null) ||
                                          (request.RequestDtosTypes.Count < 1) || (request.RequestDtosJsons.Count < 1) ||
                                                (!Type.Equals(request.RequestDtosTypes[0], typeof(CustomTicketDto))))
                                    {
                                        return new Tuple<Error, CustomTicketDto>(Errors.General.RequestNull("Ticket"), null);
                                    }
                                    CustomTicketDto castedDto = JsonConvert.DeserializeObject<CustomTicketDto>(request.RequestDtosJsons[0]);

                                    if (castedDto == null)
                                    {
                                        return new Tuple<Error, CustomTicketDto>(Errors.General.MappingError("Ticket"), null);
                                    }

                                    TicketDto ticketDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<CustomTicketDto, TicketDto>(castedDto);
                                    Tuple<Error, TicketDto> resultAddedTicket = await _ticketBusinessRules.AddAsync(ticketDto);
                                    ticketDto = resultAddedTicket.Item2;
                                    TicketNoteDto ticketNoteDto = new();
                                    TicketRelatedLocationDto ticketRelatedLocationDto = new();
                                    Tuple<Error, AttachmentDto> resultAddedAttachemnt = new Tuple<Error, AttachmentDto>(Errors.General.None(), new());
                                    Tuple<Error, TicketRelatedLocationDto> resultAddedTicketRelatedLocation = new Tuple<Error, TicketRelatedLocationDto>(Errors.General.None(), new());

                                    if ((resultAddedTicket.Item1.BusinessOperationSucceed == true) && (resultAddedTicket.Item2 != null))
                                    {
                                        var sonuc = "";

                                        for (var i = 0; i < (10 - resultAddedTicket.Item2.Id.ToString().Length); i++)
                                        {
                                            sonuc += "0";
                                        }

                                        //TODO: konfigürasyondan al
                                        ticketDto.LocationCodeId = 1;
                                        var ticketCode = ticketDto.LocationNameGetByCodeId + sonuc + resultAddedTicket.Item2.Id.ToString();
                                        resultAddedTicket.Item2.TicketCode = ticketCode;

                                        var updatedTicket = await _ticketBusinessRules.UpdateAsync(resultAddedTicket.Item2);

                                        if ((updatedTicket.Item1.BusinessOperationSucceed == true) && (updatedTicket.Item2 != null))
                                        {
                                            if (castedDto.Attachments != null)
                                            {
                                                foreach (var attachment in castedDto.Attachments)
                                                {
                                                    attachment.TicketId = resultAddedTicket.Item2.Id;
                                                    attachment.AddedByUserId = resultAddedTicket.Item2.AddedByUserId;
                                                    resultAddedAttachemnt = await _attachmentBusinessRules.AddAsync(attachment);
                                                    if ((resultAddedAttachemnt.Item1.BusinessOperationSucceed == false) && (resultAddedAttachemnt.Item2 == null))
                                                    {
                                                        transaction.Rollback();

                                                        return new Tuple<Error, CustomTicketDto>(resultAddedAttachemnt.Item1, null);
                                                    }
                                                }
                                            }
                                            foreach (var item in castedDto.TicketRelatedLocations)
                                            {
                                                ticketRelatedLocationDto.TicketId = resultAddedTicket.Item2.Id;
                                                ticketRelatedLocationDto.TicketLocationId = item.TicketLocationId;
                                                ticketRelatedLocationDto.AddedByUserId = resultAddedTicket.Item2.AddedByUserId;
                                                resultAddedTicketRelatedLocation = await _ticketRelatedLocationBusinessRules.AddAsync(ticketRelatedLocationDto);

                                                if ((resultAddedTicketRelatedLocation.Item1.BusinessOperationSucceed == false) && (resultAddedTicketRelatedLocation.Item2 == null))
                                                {
                                                    transaction.Rollback();

                                                    return new Tuple<Error, CustomTicketDto>(resultAddedTicketRelatedLocation.Item1, null);
                                                }
                                            }
                                        }   
                                    }

                                    else
                                    {
                                        transaction.Rollback();

                                        return new Tuple<Error, CustomTicketDto>(resultAddedTicket.Item1, null);
                                    }

                                    transaction.Commit();

                                    CustomTicketDto convertedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<TicketDto, CustomTicketDto>(ticketDto);

                                    return new Tuple<Error, CustomTicketDto>(Errors.General.SuccessInsert("Ticket"), convertedDto);
                                }

                                catch (Exception e)
                                {
                                    transaction.Rollback();

                                    return new Tuple<Error, CustomTicketDto>(Errors.General.ErrorInsert("Ticket"), null);

                                }
                            }

                        }

                    default:
                        {
                            return new Tuple<Error, CustomTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
        }

        public async Task<Tuple<Error, CustomTicketDto>> UpdateTicketWrapperAsync(BusinessLogicRequest request)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new Tuple<Error, CustomTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
            }
            else
            {
                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();

                            using (IDbContextTransaction transaction = DiasFacilityManagementSqlServerContext.Database.BeginTransaction())
                            {
                                try
                                {
                                    if ((request == null) || (request.RequestDtosTypes == null) || (request.RequestDtosJsons == null) ||
                                          (request.RequestDtosTypes.Count < 1) || (request.RequestDtosJsons.Count < 1) ||
                                                (!Type.Equals(request.RequestDtosTypes[0], typeof(CustomTicketDto))))
                                    {
                                        return new Tuple<Error, CustomTicketDto>(Errors.General.RequestNull("Ticket"), null);
                                    }
                                    CustomTicketDto castedDto = JsonConvert.DeserializeObject<CustomTicketDto>(request.RequestDtosJsons[0]);

                                    if (castedDto == null)
                                    {
                                        return new Tuple<Error, CustomTicketDto>(Errors.General.MappingError("Ticket"), null);
                                    }

                                    TicketDto ticketDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<CustomTicketDto, TicketDto>(castedDto);

                                    //Kayıtlı Related Locationları siliyoruz
                                    var ticketLocations = await _ticketRelatedLocationBusinessRules.GetByTicketId(castedDto.Id);

                                    foreach (var item in ticketLocations.Item2)
                                    {
                                        Tuple<Error, TicketRelatedLocationDto> resultDeletedLocation = await _ticketRelatedLocationBusinessRules.DeleteAsync(item);
                                        if ((resultDeletedLocation.Item1.BusinessOperationSucceed == false) && (resultDeletedLocation.Item2 == null))
                                        {
                                            transaction.Rollback();

                                            return new Tuple<Error, CustomTicketDto>(resultDeletedLocation.Item1, null);
                                        }
                                    }

                                    Tuple<Error, TicketDto> resultUpdatedTicket = await _ticketBusinessRules.UpdateAsync(ticketDto);
                                    TicketNoteDto ticketNoteDto = new();
                                    TicketRelatedLocationDto ticketRelatedLocationDto = new();
                                    List<Tuple<Error, AttachmentDto>> attachemntResultAddedList = new();
                                    Tuple<Error, TicketRelatedLocationDto> resultAddedTicketRelatedLocation = new Tuple<Error, TicketRelatedLocationDto>(Errors.General.None(), new());


                                    if ((resultUpdatedTicket.Item1.BusinessOperationSucceed == true) && (resultUpdatedTicket.Item2 != null))
                                    {
                                        if (castedDto.NotesAttachment != null)
                                        {
                                            ticketNoteDto.TicketId = resultUpdatedTicket.Item2.Id;
                                            ticketNoteDto.NoteText = castedDto.NoteText;
                                            ticketNoteDto.AddedByUserId = resultUpdatedTicket.Item2.AddedByUserId;
                                            Tuple<Error, TicketNoteDto> resultAddedTicketNote = await _ticketNoteBusinessRules.AddAsync(ticketNoteDto);
                                            foreach (var noteAttachment in castedDto.NotesAttachment)
                                            {
                                                noteAttachment.TicketId = resultUpdatedTicket.Item2.Id;
                                                noteAttachment.AddedByUserId = resultUpdatedTicket.Item2.AddedByUserId;
                                                noteAttachment.TicketNoteId = resultAddedTicketNote.Item2.Id;
                                                Tuple<Error, AttachmentDto> resultAddedAttachemntNote = await _attachmentBusinessRules.AddAsync(noteAttachment);
                                            }
                                        }

                                        if (castedDto.Attachments != null)
                                        {
                                            foreach (var attachment in castedDto.Attachments)
                                            {
                                                attachment.TicketId = resultUpdatedTicket.Item2.Id;
                                                attachment.AddedByUserId = resultUpdatedTicket.Item2.AddedByUserId;
                                                Tuple<Error, AttachmentDto> resultAddedAttachemnt = await _attachmentBusinessRules.AddAsync(attachment);
                                            }
                                        }

                                        foreach (var item in castedDto.TicketRelatedLocations)
                                        {
                                            ticketRelatedLocationDto.TicketId = resultUpdatedTicket.Item2.Id;
                                            ticketRelatedLocationDto.TicketLocationId = item.TicketLocationId;
                                            ticketRelatedLocationDto.AddedByUserId = resultUpdatedTicket.Item2.AddedByUserId;
                                            resultAddedTicketRelatedLocation = await _ticketRelatedLocationBusinessRules.AddAsync(ticketRelatedLocationDto);

                                            if ((resultAddedTicketRelatedLocation.Item1.BusinessOperationSucceed == false) && (resultAddedTicketRelatedLocation.Item2 == null))
                                            {
                                                transaction.Rollback();

                                                return new Tuple<Error, CustomTicketDto>(resultAddedTicketRelatedLocation.Item1, null);
                                            }
                                        }
                                        transaction.Commit();

                                        CustomTicketDto convertedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<TicketDto, CustomTicketDto>(ticketDto);

                                        return new Tuple<Error, CustomTicketDto>(Errors.General.SuccessUpdate("Ticket"), convertedDto);
                                    }
                                    else
                                    {
                                        transaction.Rollback();

                                        return new Tuple<Error, CustomTicketDto>(resultUpdatedTicket.Item1, null);
                                    }
                                }

                                catch (Exception)
                                {
                                    transaction.Rollback();

                                    return new Tuple<Error, CustomTicketDto>(Errors.General.ErrorUpdate("Ticket"), null);
                                }
                            }
                            break;
                        }

                    default:
                        {
                            return new Tuple<Error, CustomTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
        }

        public async Task<Tuple<Error, CustomTicketDto>> DeleteTicketWrapperAsync(BusinessLogicRequest request)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new Tuple<Error, CustomTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
            }
            else
            {
                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();

                            using (IDbContextTransaction transaction = DiasFacilityManagementSqlServerContext.Database.BeginTransaction())
                            {
                                try
                                {
                                    if ((request == null) || (request.RequestDtosTypes == null) || (request.RequestDtosJsons == null) ||
                                          (request.RequestDtosTypes.Count < 1) || (request.RequestDtosJsons.Count < 1) ||
                                                (!Type.Equals(request.RequestDtosTypes[0], typeof(CustomTicketDto))))
                                    {
                                        return new Tuple<Error, CustomTicketDto>(Errors.General.RequestNull("Ticket"), null);
                                    }
                                    CustomTicketDto castedDto = JsonConvert.DeserializeObject<CustomTicketDto>(request.RequestDtosJsons[0]);

                                    if (castedDto == null)
                                    {
                                        return new Tuple<Error, CustomTicketDto>(Errors.General.MappingError("Ticket"), null);
                                    }


                                    TicketDto ticketDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<CustomTicketDto, TicketDto>(castedDto);

                                    Tuple<Error, TicketDto> resultDeletedTicket = await _ticketBusinessRules.DeleteAsync(ticketDto);

                                    List<TicketNoteDto> ticketNotesDto = new();
                                    List<Tuple<Error, AttachmentDto>> attachemntResultAddedList = new();

                                    if ((resultDeletedTicket.Item1.BusinessOperationSucceed == true) && (resultDeletedTicket.Item2 != null))
                                    {

                                        foreach (var item in ticketDto.TicketNotes)
                                        {
                                            if (item.TicketId == 0)
                                            {
                                                item.TicketId = resultDeletedTicket.Item2.Id;
                                                ticketNotesDto.Add(item);
                                            }
                                        }
                                        Tuple<Error, List<TicketNoteDto>> resultDeletedTicketNote = await _ticketNoteBusinessRules.DeleteAsync(ticketNotesDto);

                                        if ((resultDeletedTicketNote.Item1.BusinessOperationSucceed == true) && (resultDeletedTicketNote.Item2 != null))
                                        {
                                            bool control = true;

                                            foreach (var attachment in attachemntResultAddedList)
                                            {

                                                if ((attachment.Item1.BusinessOperationSucceed == false) && (attachment.Item2 == null))
                                                {
                                                    control = false;
                                                }
                                                if (control == false)
                                                {
                                                    transaction.Rollback();

                                                    return new Tuple<Error, CustomTicketDto>(attachment.Item1, null);
                                                }
                                                ticketDto.Attachments.Add(attachment.Item2);
                                            }
                                            if (control == true)
                                            {
                                                await Task.Run(() => _unitOfWork_EF.CompleteAsync(typeof(DevelopmentContext.DiasFacilityManagementSqlServer).Name, DiasFacilityManagementSqlServerContext));
                                                transaction.Commit();

                                                CustomTicketDto convertedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<TicketDto, CustomTicketDto>(ticketDto);

                                                return new Tuple<Error, CustomTicketDto>(Errors.General.SuccessUpdate("Ticket"), convertedDto);
                                            }

                                        }
                                        else
                                        {
                                            transaction.Rollback();

                                            return new Tuple<Error, CustomTicketDto>(resultDeletedTicketNote.Item1, null);
                                        }

                                    }
                                    else
                                    {
                                        transaction.Rollback();

                                        return new Tuple<Error, CustomTicketDto>(resultDeletedTicket.Item1, null);
                                    }
                                }

                                catch (Exception)
                                {
                                    transaction.Rollback();

                                    return new Tuple<Error, CustomTicketDto>(Errors.General.ErrorUpdate("Ticket"), null);
                                }
                            }
                            break;
                        }

                    default:
                        {
                            return new Tuple<Error, CustomTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
            return new Tuple<Error, CustomTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
        }

        public async Task<Tuple<Error, CustomTicketDto>> AddTicketWithFastTicketWrapperAsync(CustomTicketDto customTicketDto)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new Tuple<Error, CustomTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
            }
            else
            {
                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();

                            using (IDbContextTransaction transaction = DiasFacilityManagementSqlServerContext.Database.BeginTransaction())
                            {
                                try
                                {
                                    TicketDto ticketDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<CustomTicketDto, TicketDto>(customTicketDto);
                                    Tuple<Error, TicketDto> resultAddedTicket = await _ticketBusinessRules.AddAsync(ticketDto);
                                    ticketDto = resultAddedTicket.Item2;
                                    TicketRelatedLocationDto ticketRelatedLocationDto = new();
                                    List<Tuple<Error, AttachmentDto>> attachemntResultAddedList = new();
                                    Tuple<Error, TicketRelatedLocationDto> resultAddedTicketRelatedLocation = new Tuple<Error, TicketRelatedLocationDto>(Errors.General.None(), new());

                                    if ((resultAddedTicket.Item1.BusinessOperationSucceed == true) && (resultAddedTicket.Item2 != null))
                                    {


                                        foreach (var attachment in customTicketDto.Attachments)
                                        {
                                            attachment.TicketId = resultAddedTicket.Item2.Id;
                                            attachment.AddedByUserId = resultAddedTicket.Item2.AddedByUserId;
                                            Tuple<Error, AttachmentDto> resultAddedAttachemnt = await _attachmentBusinessRules.UpdateAsync(attachment);

                                            if ((resultAddedAttachemnt.Item1.BusinessOperationSucceed == true) && (resultAddedAttachemnt.Item2 != null))
                                            {

                                                foreach (var item in customTicketDto.TicketRelatedLocations)
                                                {
                                                    ticketRelatedLocationDto.TicketId = resultAddedTicket.Item2.Id;
                                                    ticketRelatedLocationDto.TicketLocationId = item.TicketLocationId;
                                                    ticketRelatedLocationDto.AddedByUserId = resultAddedTicket.Item2.AddedByUserId;
                                                    resultAddedTicketRelatedLocation = await _ticketRelatedLocationBusinessRules.AddAsync(ticketRelatedLocationDto);

                                                    if ((resultAddedTicketRelatedLocation.Item1.BusinessOperationSucceed == false) && (resultAddedTicketRelatedLocation.Item2 == null))
                                                    {
                                                        transaction.Rollback();

                                                        return new Tuple<Error, CustomTicketDto>(resultAddedTicketRelatedLocation.Item1, null);
                                                    }
                                                }

                                                Tuple<Error, BasicTicketDto> resultBasicticket = await _basicTicketBusinessRules.GetBasicTicketByIdAsync(customTicketDto.BasicTicketId ?? 0);
                                                transaction.Commit();

                                                CustomTicketDto convertedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<TicketDto, CustomTicketDto>(ticketDto);

                                                return new Tuple<Error, CustomTicketDto>(Errors.General.SuccessInsert("Ticket"), convertedDto);
                                            }
                                            else
                                            {
                                                transaction.Rollback();

                                                return new Tuple<Error, CustomTicketDto>(resultAddedAttachemnt.Item1, null);
                                            }

                                        }
                                    }


                                }

                                catch (Exception e)
                                {
                                    transaction.Rollback();

                                    return new Tuple<Error, CustomTicketDto>(Errors.General.ErrorInsert("Ticket"), null);
                                }
                            }
                            break;
                        }

                    default:
                        {
                            return new Tuple<Error, CustomTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
            return new Tuple<Error, CustomTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
        }

        public async Task<Tuple<Error, CustomTicketDto>> UpdateTicketStateWrapperAsync(BusinessLogicRequest request)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new Tuple<Error, CustomTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
            }
            else
            {
                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();

                            using (IDbContextTransaction transaction = DiasFacilityManagementSqlServerContext.Database.BeginTransaction())
                            {
                                try
                                {
                                    if ((request == null) || (request.RequestDtosJsons == null) || (request.RequestDtosJsons.Count < 1))
                                    {
                                        return new Tuple<Error, CustomTicketDto>(Errors.General.RequestNull("Ticket"), null);
                                    }

                                    CustomTicketDto castedDto = JsonConvert.DeserializeObject<CustomTicketDto>(request.RequestDtosJsons[0]);

                                    if (castedDto == null)
                                    {
                                        return new Tuple<Error, CustomTicketDto>(Errors.General.MappingError("Ticket"), null);
                                    }



                                    TicketDto ticketDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<CustomTicketDto, TicketDto>(castedDto);
                                    ticketDto.TicketReason = null;
                                    ticketDto.TicketRelatedLocations = null;
                                    ticketDto.TicketStatus = null;
                                    ticketDto.AddedByUser = null;
                                    ticketDto.TickedAssignedAssignmentGroup = null;
                                    ticketDto.TicketPriority = null;
                                    ticketDto.TicketAssignedUser = null;
                                    ticketDto.TicketNotes = null;
                                    ticketDto.TicketAuditHistories = null;
                                    ticketDto.ResolutionFormQuestionAnswers = null;

                                    Tuple<Error, TicketDto> resultGetTicket = await _ticketBusinessRules.GetTicketByIdAsync(ticketDto.Id);

                                    if ((resultGetTicket.Item1.BusinessOperationSucceed) && (resultGetTicket.Item2 != null))
                                    {
                                        bool changeSlaStatus = false;

                                        //xorluyoruz, kırmızı-kırmızı ve yeşil-yeşil de(excele göre) sla statü değişikliği olmayacak
                                        if (((ticketDto.TicketStatusId == (int)TicketStatusEnum.SUSPENDED) ||
                                           (ticketDto.TicketStatusId == (int)TicketStatusEnum.SOLVED) ||
                                            (ticketDto.TicketStatusId == (int)TicketStatusEnum.CLOSED) ||
                                              (ticketDto.TicketStatusId == (int)TicketStatusEnum.REJECTED))
                                               ^
                                              ((resultGetTicket.Item2.TicketStatusId == (int)TicketStatusEnum.SUSPENDED) ||
                                                (resultGetTicket.Item2.TicketStatusId == (int)TicketStatusEnum.SOLVED) ||
                                                    (resultGetTicket.Item2.TicketStatusId == (int)TicketStatusEnum.CLOSED) ||
                                                         (resultGetTicket.Item2.TicketStatusId == (int)TicketStatusEnum.REJECTED)))
                                        {
                                            changeSlaStatus = true;
                                        }
                                        else
                                        {
                                            changeSlaStatus = false;
                                        }

                                        Tuple<Error, TicketDto> resultAddedTicket = await _ticketBusinessRules.UpdateAsync(ticketDto, changeSlaStatus);

                                        ticketDto = resultAddedTicket.Item2;

                                        if ((resultAddedTicket.Item1.BusinessOperationSucceed == true) && (resultAddedTicket.Item2 != null))
                                        {
                                            transaction.Commit();

                                            CustomTicketDto convertedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<TicketDto, CustomTicketDto>(ticketDto);

                                            return new Tuple<Error, CustomTicketDto>(Errors.General.SuccessUpdate("Ticket"), convertedDto);
                                        }
                                        else
                                        {
                                            transaction.Rollback();

                                            return new Tuple<Error, CustomTicketDto>(Errors.General.ErrorUpdate("Ticket"), null);
                                        }
                                    }
                                    else
                                    {
                                        return new Tuple<Error, CustomTicketDto>(Errors.General.ErrorUpdate("Ticket"), null);
                                    }
                                }
                                catch (Exception e)
                                {
                                    transaction.Rollback();

                                    return new Tuple<Error, CustomTicketDto>(Errors.General.ErrorUpdate("Ticket"), null);
                                }
                            }
                        }

                    default:
                        {
                            return new Tuple<Error, CustomTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
        }

        #endregion WebClient

        #region Mobile

        public async Task<Tuple<Error, CustomMobileTicketDto>> AddTicketWrapperMobileAsync(BusinessLogicRequest request)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new Tuple<Error, CustomMobileTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
            }
            else
            {
                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();

                            using (IDbContextTransaction transaction = DiasFacilityManagementSqlServerContext.Database.BeginTransaction())
                            {
                                try
                                {

                                    if ((request == null) || (request.RequestDtosJsons == null) || (request.RequestDtosJsons.Count < 1))
                                    {
                                        return new Tuple<Error, CustomMobileTicketDto>(Errors.General.RequestNull("Ticket"), null);
                                    }

                                    CustomMobileTicketDto castedDto = JsonConvert.DeserializeObject<CustomMobileTicketDto>(request.RequestDtosJsons[0]);

                                    if (castedDto == null)
                                    {
                                        return new Tuple<Error, CustomMobileTicketDto>(Errors.General.MappingError("Ticket"), null);
                                    }

                                    TicketDto ticketDto = DtoMapperMobile_DiasFacilityManagementSqlServer_Development.Map<CustomMobileTicketDto, TicketDto>(castedDto);
                                    Tuple<Error, TicketDto> resultAddedTicket = await _ticketBusinessRules.AddAsync(ticketDto);

                                    ticketDto = resultAddedTicket.Item2;
                                    TicketNoteDto ticketNoteDto = new();
                                    TicketRelatedLocationDto ticketRelatedLocationDto = new();
                                    AttachmentDto attachmentDto = new();                                 
                                    Tuple<Error, AttachmentDto> attachemntResultAddedList = new Tuple<Error, AttachmentDto>(Errors.General.None(), new());
                                    Tuple<Error, TicketRelatedLocationDto> resultAddedTicketRelatedLocation = new Tuple<Error, TicketRelatedLocationDto>(Errors.General.None(), new());

                                    if ((resultAddedTicket.Item1.BusinessOperationSucceed == true) && (resultAddedTicket.Item2 != null))
                                    {
                                        var sonuc = "";

                                        for (var i = 0; i < (10 - resultAddedTicket.Item2.Id.ToString().Length); i++)
                                        {
                                            sonuc += "0";
                                        }

                                        //TODO: konfigürasyondan al
                                        ticketDto.LocationCodeId = 1;
                                        string ticketCode = ticketDto.LocationNameGetByCodeId + sonuc + resultAddedTicket.Item2.Id.ToString();
                                        resultAddedTicket.Item2.TicketCode = ticketCode;

                                        var updatedTicket = await _ticketBusinessRules.UpdateAsync(resultAddedTicket.Item2);

                                        if ((updatedTicket.Item1.BusinessOperationSucceed == true) && (updatedTicket.Item2 != null))
                                        {

                                            foreach (var item in castedDto.LocationList)
                                            {
                                                ticketRelatedLocationDto.TicketId = resultAddedTicket.Item2.Id;
                                                ticketRelatedLocationDto.TicketLocationId = item;
                                                ticketRelatedLocationDto.AddedByUserId = resultAddedTicket.Item2.AddedByUserId;
                                                resultAddedTicketRelatedLocation = await _ticketRelatedLocationBusinessRules.AddAsync(ticketRelatedLocationDto);

                                            }

                                            if ((resultAddedTicketRelatedLocation.Item1.BusinessOperationSucceed == true) && (resultAddedTicketRelatedLocation.Item2 != null))
                                            {

                                                foreach (var attachmentItem in castedDto.AttachmentsFile)
                                                {
                                                    attachmentDto.AddedByUserId = resultAddedTicket.Item2.AddedByUserId;
                                                    attachmentDto.FileData = attachmentItem.FileData;
                                                    attachmentDto.FileType = attachmentItem.FileType;
                                                    attachmentDto.AttachmentDescription = "Mobil description";
                                                    attachmentDto.FolderName = attachmentItem.FileName;
                                                    attachmentDto.TicketId = resultAddedTicket.Item2.Id;
                                                    attachemntResultAddedList = await _attachmentBusinessRules.AddAsync(attachmentDto);
                                                    if ((attachemntResultAddedList.Item1.BusinessOperationSucceed == false) && (attachemntResultAddedList.Item2 == null))
                                                    {

                                                        transaction.Rollback();

                                                        return new Tuple<Error, CustomMobileTicketDto>(resultAddedTicketRelatedLocation.Item1, null);
                                                    }

                                                }

                                            }
                                            else
                                            {
                                                transaction.Rollback();

                                                return new Tuple<Error, CustomMobileTicketDto>(resultAddedTicketRelatedLocation.Item1, null);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        transaction.Rollback();
                                        return new Tuple<Error, CustomMobileTicketDto>(Errors.General.ErrorInsert("Ticket"), null);
                                    }

                                    transaction.Commit();

                                    CustomMobileTicketDto convertedMobileDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<TicketDto, CustomMobileTicketDto>(ticketDto);

                                    return new Tuple<Error, CustomMobileTicketDto>(Errors.General.SuccessInsert("Ticket"), convertedMobileDto);
                                }
                                catch (Exception e)
                                {
                                    transaction.Rollback();
                                    return new Tuple<Error, CustomMobileTicketDto>(Errors.General.ErrorInsert("Ticket"), null);
                                }
                            }
                        }
                    default:
                        {
                            return new Tuple<Error, CustomMobileTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
        }
       
        #endregion Mobile
    }
}


