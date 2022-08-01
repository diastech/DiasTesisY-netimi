using DiasBusinessLogic.InterfacesAbstracts.Abstracts.BusinessRules;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Base.SqlServer.Custom;
using TransactionalInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Test.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevelopmentUserInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Test.Standart;
using DevelopmentContext = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Test.Models;
using DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.Configuration;
using DiasDataAccessLayer.Enums;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Custom;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DiasBusinessLogic.Shared.Configuration;
using Microsoft.EntityFrameworkCore.Storage;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Test.Custom;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.UnitOfWork;
using CustomDevelopmentTicketProfile = DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.CustomProfiles.Test;
using DiasBusinessLogic.AutoMapper.Configuration;
using DiasShared.Services.Communication.BusinessLogicMessage;
using Newtonsoft.Json;

namespace DiasBusinessLogic.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Test.Custom
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
            => new(new(DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServerTest")));

        private static AutoMapperProfileMapper<CustomDevelopmentTicketProfile.CustomMobileTicketProfile> DtoMapperMobile_DiasFacilityManagementSqlServer_Development
            => new(new(DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServerTest")));



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

        public async Task<Tuple<BusinessLogicMessageCodes.ErrorCodes, CustomTicketDto>> AddTicketWrapperAsync(BusinessLogicRequest request)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServerTest");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new Tuple<ErrorCodes, CustomTicketDto>(ErrorCodes.UnknownError, null);
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
                                        return new Tuple<ErrorCodes, CustomTicketDto>(ErrorCodes.UnknownError, null);
                                    }
                                    CustomTicketDto castedDto = JsonConvert.DeserializeObject<CustomTicketDto>(request.RequestDtosJsons[0]);

                                    if (castedDto == null)
                                    {
                                        return new Tuple<ErrorCodes, CustomTicketDto>(ErrorCodes.UnknownError, null);
                                    }

                                    TicketDto ticketDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<CustomTicketDto, TicketDto>(castedDto);

                                    Tuple<ErrorCodes, TicketDto> resultAddedTicket = await _ticketBusinessRules.AddAsync(ticketDto);
                                    ticketDto = resultAddedTicket.Item2;
                                    TicketNoteDto ticketNoteDto = new();
                                    TicketRelatedLocationDto ticketRelatedLocationDto = new();
                                    List<Tuple<ErrorCodes, AttachmentDto>> attachemntResultAddedList = new();
                                    Tuple<ErrorCodes, TicketRelatedLocationDto> resultAddedTicketRelatedLocation = new Tuple<ErrorCodes, TicketRelatedLocationDto>(ErrorCodes.None, new());
                                    
                                    if ((resultAddedTicket.Item1 == ErrorCodes.None) && (resultAddedTicket.Item2 != null))
                                    {
                                        ticketNoteDto.TicketId = resultAddedTicket.Item2.Id;
                                        ticketNoteDto.NoteText = castedDto.NoteText;
                                        ticketNoteDto.AddedByUserId = resultAddedTicket.Item2.AddedByUserId;

                                        Tuple<ErrorCodes, TicketNoteDto> resultAddedTicketNote = await _ticketNoteBusinessRules.AddAsync(ticketNoteDto);

                                        if ((resultAddedTicketNote.Item1 == ErrorCodes.None) && (resultAddedTicketNote.Item2 != null))
                                        {
                                            foreach (var noteAttachment in castedDto.NotesAttachment)
                                            {
                                                noteAttachment.TicketId = resultAddedTicket.Item2.Id;
                                                noteAttachment.TicketNoteId = resultAddedTicketNote.Item2.Id;
                                                noteAttachment.AddedByUserId = resultAddedTicket.Item2.AddedByUserId;
                                                Tuple<ErrorCodes, AttachmentDto> resultAddedAttachemntNote = await _attachmentBusinessRules.AddAsync(noteAttachment);

                                                if ((resultAddedAttachemntNote.Item1 == ErrorCodes.None) && (resultAddedAttachemntNote.Item2 != null))
                                                {
                                                    foreach (var attachment in castedDto.Attachments)
                                                    {
                                                        attachment.TicketId = resultAddedTicket.Item2.Id;
                                                        attachment.AddedByUserId = resultAddedTicket.Item2.AddedByUserId;
                                                        Tuple<ErrorCodes, AttachmentDto> resultAddedAttachemnt = await _attachmentBusinessRules.AddAsync(attachment);

                                                        if ((resultAddedAttachemnt.Item1 == ErrorCodes.None) && (resultAddedAttachemnt.Item2 != null))
                                                        {

                                                            foreach (var item in castedDto.TicketRelatedLocations)
                                                            {
                                                                ticketRelatedLocationDto.TicketId = resultAddedTicket.Item2.Id;
                                                                ticketRelatedLocationDto.TicketLocationId = item.TicketLocationId;
                                                                ticketRelatedLocationDto.AddedByUserId = resultAddedTicket.Item2.AddedByUserId;
                                                                resultAddedTicketRelatedLocation = await _ticketRelatedLocationBusinessRules.AddAsync(ticketRelatedLocationDto);

                                                                if ((resultAddedTicketRelatedLocation.Item1 != ErrorCodes.None) && (resultAddedTicketRelatedLocation.Item2 == null))
                                                                {
                                                                    transaction.Rollback();

                                                                    return new Tuple<ErrorCodes, CustomTicketDto>(resultAddedTicketRelatedLocation.Item1, null);
                                                                }
                                                            }

                                                            transaction.Commit();

                                                            CustomTicketDto convertedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<TicketDto, CustomTicketDto>(ticketDto);

                                                            return new Tuple<ErrorCodes, CustomTicketDto>(ErrorCodes.None, convertedDto);
                                                        }
                                                        else
                                                        {
                                                            transaction.Rollback();

                                                            return new Tuple<ErrorCodes, CustomTicketDto>(resultAddedAttachemnt.Item1, null);
                                                        }

                                                    }
                                                }
                                                else
                                                {
                                                    transaction.Rollback();

                                                    return new Tuple<ErrorCodes, CustomTicketDto>(resultAddedAttachemntNote.Item1, null);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            transaction.Rollback();

                                            return new Tuple<ErrorCodes, CustomTicketDto>(resultAddedTicket.Item1, null);
                                        }

                                    }
                                    else
                                    {
                                        transaction.Rollback();

                                        return new Tuple<ErrorCodes, CustomTicketDto>(resultAddedTicket.Item1, null);
                                    }
                                }

                                catch (Exception e)
                                {
                                    transaction.Rollback();

                                    return new Tuple<ErrorCodes, CustomTicketDto>(ErrorCodes.UnknownError, null);
                                }
                            }
                            break;
                        }

                    default:
                        {
                            return new Tuple<ErrorCodes, CustomTicketDto>(ErrorCodes.UnknownError, null);
                        }
                }
            }
            return new Tuple<ErrorCodes, CustomTicketDto>(ErrorCodes.UnknownError, null);
        }

        public async Task<Tuple<ErrorCodes, CustomTicketDto>> UpdateTicketWrapperAsync(BusinessLogicRequest request)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServerTest");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new Tuple<ErrorCodes, CustomTicketDto>(ErrorCodes.UnknownError, null);
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
                                        return new Tuple<ErrorCodes, CustomTicketDto>(ErrorCodes.UnknownError, null);
                                    }
                                    CustomTicketDto castedDto = JsonConvert.DeserializeObject<CustomTicketDto>(request.RequestDtosJsons[0]);

                                    if (castedDto == null)
                                    {
                                        return new Tuple<ErrorCodes, CustomTicketDto>(ErrorCodes.UnknownError, null);
                                    }

                                    TicketDto ticketDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<CustomTicketDto, TicketDto>(castedDto);

                                    //Kayıtlı Related Locationları siliyoruz
                                    var ticketLocations = await _ticketRelatedLocationBusinessRules.GetByTicketId(castedDto.Id);

                                    foreach (var item in ticketLocations.Item2)
                                    {
                                        Tuple<ErrorCodes, TicketRelatedLocationDto> resultDeletedLocation = await _ticketRelatedLocationBusinessRules.DeleteAsync(item);
                                        if ((resultDeletedLocation.Item1 != ErrorCodes.None) && (resultDeletedLocation.Item2 == null))
                                        {
                                            transaction.Rollback();

                                            return new Tuple<ErrorCodes, CustomTicketDto>(resultDeletedLocation.Item1, null);
                                        }
                                    }

                                    Tuple<ErrorCodes, TicketDto> resultUpdatedTicket = await _ticketBusinessRules.UpdateAsync(ticketDto);
                                    TicketNoteDto ticketNoteDto = new();
                                    TicketRelatedLocationDto ticketRelatedLocationDto = new();
                                    List<Tuple<ErrorCodes, AttachmentDto>> attachemntResultAddedList = new();
                                    Tuple<ErrorCodes, TicketRelatedLocationDto> resultAddedTicketRelatedLocation = new Tuple<ErrorCodes, TicketRelatedLocationDto>(ErrorCodes.None, new());

                                   
                                    if ((resultUpdatedTicket.Item1 == ErrorCodes.None) && (resultUpdatedTicket.Item2 != null))
                                    {
                                        if (castedDto.NotesAttachment != null)
                                        {
                                            ticketNoteDto.TicketId = resultUpdatedTicket.Item2.Id;
                                            ticketNoteDto.NoteText = castedDto.NoteText;
                                            ticketNoteDto.AddedByUserId = resultUpdatedTicket.Item2.AddedByUserId;
                                            Tuple<ErrorCodes, TicketNoteDto> resultAddedTicketNote = await _ticketNoteBusinessRules.AddAsync(ticketNoteDto);
                                            foreach (var noteAttachment in castedDto.NotesAttachment)
                                            {
                                                noteAttachment.TicketId = resultUpdatedTicket.Item2.Id;                                                
                                                noteAttachment.AddedByUserId = resultUpdatedTicket.Item2.AddedByUserId;
                                                noteAttachment.TicketNoteId = resultAddedTicketNote.Item2.Id;
                                                Tuple<ErrorCodes, AttachmentDto> resultAddedAttachemntNote = await _attachmentBusinessRules.AddAsync(noteAttachment);
                                            }
                                        }

                                        if (castedDto.Attachments != null)
                                        {
                                            foreach (var attachment in castedDto.Attachments)
                                            {
                                                attachment.TicketId = resultUpdatedTicket.Item2.Id;
                                                attachment.AddedByUserId = resultUpdatedTicket.Item2.AddedByUserId;
                                                Tuple<ErrorCodes, AttachmentDto> resultAddedAttachemnt = await _attachmentBusinessRules.AddAsync(attachment);
                                            }
                                        }

                                        foreach (var item in castedDto.TicketRelatedLocations)
                                        {                                            
                                            ticketRelatedLocationDto.TicketId = resultUpdatedTicket.Item2.Id;
                                            ticketRelatedLocationDto.TicketLocationId = item.TicketLocationId;
                                            ticketRelatedLocationDto.AddedByUserId = resultUpdatedTicket.Item2.AddedByUserId;
                                            resultAddedTicketRelatedLocation = await _ticketRelatedLocationBusinessRules.AddAsync(ticketRelatedLocationDto);

                                            if ((resultAddedTicketRelatedLocation.Item1 != ErrorCodes.None) && (resultAddedTicketRelatedLocation.Item2 == null))
                                            {
                                                transaction.Rollback();

                                                return new Tuple<ErrorCodes, CustomTicketDto>(resultAddedTicketRelatedLocation.Item1, null);
                                            }
                                        }
                                        transaction.Commit();

                                        CustomTicketDto convertedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<TicketDto, CustomTicketDto>(ticketDto);

                                        return new Tuple<ErrorCodes, CustomTicketDto>(ErrorCodes.None, convertedDto);
                                    }
                                    else
                                    {
                                        transaction.Rollback();

                                        return new Tuple<ErrorCodes, CustomTicketDto>(resultUpdatedTicket.Item1, null);
                                    }
                                }

                                catch (Exception)
                                {
                                    transaction.Rollback();

                                    return new Tuple<ErrorCodes, CustomTicketDto>(ErrorCodes.UnknownError, null);
                                }
                            }
                            break;
                        }

                    default:
                        {
                            return new Tuple<ErrorCodes, CustomTicketDto>(ErrorCodes.UnknownError, null);
                        }
                }
            }
            return new Tuple<ErrorCodes, CustomTicketDto>(ErrorCodes.UnknownError, null);
        }

        public async Task<Tuple<ErrorCodes, CustomTicketDto>> DeleteTicketWrapperAsync(BusinessLogicRequest request)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServerTest");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new Tuple<ErrorCodes, CustomTicketDto>(ErrorCodes.UnknownError, null);
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
                                        return new Tuple<ErrorCodes, CustomTicketDto>(ErrorCodes.UnknownError, null);
                                    }
                                    CustomTicketDto castedDto = JsonConvert.DeserializeObject<CustomTicketDto>(request.RequestDtosJsons[0]);

                                    if (castedDto == null)
                                    {
                                        return new Tuple<ErrorCodes, CustomTicketDto>(ErrorCodes.UnknownError, null);
                                    }


                                    TicketDto ticketDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<CustomTicketDto, TicketDto>(castedDto);
                                    
                                    Tuple<ErrorCodes, TicketDto> resultDeletedTicket = await _ticketBusinessRules.DeleteAsync(ticketDto);

                                    List<TicketNoteDto> ticketNotesDto = new();
                                    List<Tuple<ErrorCodes, AttachmentDto>> attachemntResultAddedList = new();

                                    if ((resultDeletedTicket.Item1 == ErrorCodes.None) && (resultDeletedTicket.Item2 != null))
                                    {

                                        foreach (var item in ticketDto.TicketNotes)
                                        {
                                            if (item.TicketId == 0)
                                            {
                                                item.TicketId = resultDeletedTicket.Item2.Id;
                                                ticketNotesDto.Add(item);
                                            }
                                        }
                                        Tuple<ErrorCodes, List<TicketNoteDto>> resultDeletedTicketNote = await _ticketNoteBusinessRules.DeleteAsync(ticketNotesDto);

                                        if ((resultDeletedTicketNote.Item1 == ErrorCodes.None) && (resultDeletedTicketNote.Item2 != null))
                                        {
                                            bool control = true;

                                            foreach (var attachment in attachemntResultAddedList)
                                            {

                                                if ((attachment.Item1 != ErrorCodes.None) && (attachment.Item2 == null))
                                                {
                                                    control = false;
                                                }
                                                if (control == false)
                                                {
                                                    transaction.Rollback();

                                                    return new Tuple<ErrorCodes, CustomTicketDto>(attachment.Item1, null);
                                                }
                                                ticketDto.Attachments.Add(attachment.Item2);
                                            }
                                            if (control == true)
                                            {
                                                await Task.Run(() => _unitOfWork_EF.CompleteAsync(typeof(DevelopmentContext.DiasFacilityManagementSqlServer).Name, DiasFacilityManagementSqlServerContext));
                                                transaction.Commit();

                                                CustomTicketDto convertedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<TicketDto, CustomTicketDto>(ticketDto);

                                                return new Tuple<ErrorCodes, CustomTicketDto>(ErrorCodes.None, convertedDto);
                                            }

                                        }
                                        else
                                        {
                                            transaction.Rollback();

                                            return new Tuple<ErrorCodes, CustomTicketDto>(resultDeletedTicketNote.Item1, null);
                                        }

                                    }
                                    else
                                    {
                                        transaction.Rollback();

                                        return new Tuple<ErrorCodes, CustomTicketDto>(resultDeletedTicket.Item1, null);
                                    }
                                }

                                catch (Exception)
                                {
                                    transaction.Rollback();

                                    return new Tuple<ErrorCodes, CustomTicketDto>(ErrorCodes.UnknownError, null);
                                }
                            }
                            break;
                        }

                    default:
                        {
                            return new Tuple<ErrorCodes, CustomTicketDto>(ErrorCodes.UnknownError, null);
                        }
                }
            }
            return new Tuple<ErrorCodes, CustomTicketDto>(ErrorCodes.UnknownError, null);
        }

        public async Task<Tuple<ErrorCodes, CustomTicketDto>> AddTicketWithFastTicketWrapperAsync(CustomTicketDto customTicketDto)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServerTest");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new Tuple<ErrorCodes, CustomTicketDto>(ErrorCodes.UnknownError, null);
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
                                    Tuple<ErrorCodes, TicketDto> resultAddedTicket = await _ticketBusinessRules.AddAsync(ticketDto);
                                    ticketDto = resultAddedTicket.Item2;
                                    TicketRelatedLocationDto ticketRelatedLocationDto = new();
                                    List<Tuple<ErrorCodes, AttachmentDto>> attachemntResultAddedList = new();
                                    Tuple<ErrorCodes, TicketRelatedLocationDto> resultAddedTicketRelatedLocation = new Tuple<ErrorCodes, TicketRelatedLocationDto>(ErrorCodes.None, new());

                                    if ((resultAddedTicket.Item1 == ErrorCodes.None) && (resultAddedTicket.Item2 != null))
                                    {


                                        foreach (var attachment in customTicketDto.Attachments)
                                        {
                                            attachment.TicketId = resultAddedTicket.Item2.Id;
                                            attachment.AddedByUserId = resultAddedTicket.Item2.AddedByUserId;
                                            Tuple<ErrorCodes, AttachmentDto> resultAddedAttachemnt = await _attachmentBusinessRules.UpdateAsync(attachment);

                                            if ((resultAddedAttachemnt.Item1 == ErrorCodes.None) && (resultAddedAttachemnt.Item2 != null))
                                            {

                                                foreach (var item in customTicketDto.TicketRelatedLocations)
                                                {
                                                    ticketRelatedLocationDto.TicketId = resultAddedTicket.Item2.Id;
                                                    ticketRelatedLocationDto.TicketLocationId = item.TicketLocationId;
                                                    ticketRelatedLocationDto.AddedByUserId = resultAddedTicket.Item2.AddedByUserId;
                                                    resultAddedTicketRelatedLocation = await _ticketRelatedLocationBusinessRules.AddAsync(ticketRelatedLocationDto);

                                                    if ((resultAddedTicketRelatedLocation.Item1 != ErrorCodes.None) && (resultAddedTicketRelatedLocation.Item2 == null))
                                                    {
                                                        transaction.Rollback();

                                                        return new Tuple<ErrorCodes, CustomTicketDto>(resultAddedTicketRelatedLocation.Item1, null);
                                                    }
                                                }
                                                    
                                                    Tuple<ErrorCodes, BasicTicketDto> resultBasicticket = await _basicTicketBusinessRules.GetBasicTicketByIdAsync(customTicketDto.BasicTicketId ?? 0);
                                                transaction.Commit();

                                                CustomTicketDto convertedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<TicketDto, CustomTicketDto>(ticketDto);

                                                return new Tuple<ErrorCodes, CustomTicketDto>(ErrorCodes.None, convertedDto);
                                            }
                                            else
                                            {
                                                transaction.Rollback();

                                                return new Tuple<ErrorCodes, CustomTicketDto>(resultAddedAttachemnt.Item1, null);
                                            }

                                        }
                                    }
                                    

                                }

                                catch (Exception e)
                                {
                                    transaction.Rollback();

                                    return new Tuple<ErrorCodes, CustomTicketDto>(ErrorCodes.UnknownError, null);
                                }
                            }
                            break;
                        }

                    default:
                        {
                            return new Tuple<ErrorCodes, CustomTicketDto>(ErrorCodes.UnknownError, null);
                        }
                }
            }
            return new Tuple<ErrorCodes, CustomTicketDto>(ErrorCodes.UnknownError, null);
        }

        public async Task<Tuple<ErrorCodes, CustomTicketDto>> UpdateTicketStateWrapperAsync(BusinessLogicRequest request)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new Tuple<ErrorCodes, CustomTicketDto>(ErrorCodes.UnknownError, null);
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
                                        return new Tuple<ErrorCodes, CustomTicketDto>(ErrorCodes.UnknownError, null);
                                    }
                                    CustomTicketDto castedDto = JsonConvert.DeserializeObject<CustomTicketDto>(request.RequestDtosJsons[0]);

                                    if (castedDto == null)
                                    {
                                        return new Tuple<ErrorCodes, CustomTicketDto>(ErrorCodes.UnknownError, null);
                                    }

                                    TicketDto ticketDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<CustomTicketDto, TicketDto>(castedDto);
                                    ticketDto.TicketReason = null;
                                    ticketDto.TicketRelatedLocations = null;
                                    ticketDto.TicketStatus = null;
                                    Tuple<ErrorCodes, TicketDto> resultAddedTicket = await _ticketBusinessRules.UpdateAsync(ticketDto);
                                    ticketDto = resultAddedTicket.Item2;

                                    if ((resultAddedTicket.Item1 == ErrorCodes.None) && (resultAddedTicket.Item2 != null))
                                    {
                                        transaction.Commit();

                                        CustomTicketDto convertedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<TicketDto, CustomTicketDto>(ticketDto);

                                        return new Tuple<ErrorCodes, CustomTicketDto>(ErrorCodes.None, convertedDto);
                                    }
                                    else
                                    {
                                        transaction.Rollback();

                                        return new Tuple<ErrorCodes, CustomTicketDto>(ErrorCodes.UnknownError, null);
                                    }
                                }

                                catch (Exception e)
                                {
                                    transaction.Rollback();

                                    return new Tuple<ErrorCodes, CustomTicketDto>(ErrorCodes.UnknownError, null);
                                }
                            }
                            break;
                        }

                    default:
                        {
                            return new Tuple<ErrorCodes, CustomTicketDto>(ErrorCodes.UnknownError, null);
                        }
                }
            }
            return new Tuple<ErrorCodes, CustomTicketDto>(ErrorCodes.UnknownError, null);
        }

        #endregion WebClient

        #region Mobile

        public async Task<Tuple<ErrorCodes, CustomMobileTicketDto>> AddTicketWrapperMobileAsync(BusinessLogicRequest request)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServerTest");



            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new Tuple<ErrorCodes, CustomMobileTicketDto>(ErrorCodes.UnknownError, null);
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
                                        return new Tuple<ErrorCodes, CustomMobileTicketDto>(ErrorCodes.UnknownError, null);
                                    }

                                    CustomMobileTicketDto castedDto = JsonConvert.DeserializeObject<CustomMobileTicketDto>(request.RequestDtosJsons[0]);

                                    if (castedDto == null)
                                    {
                                        return new Tuple<ErrorCodes, CustomMobileTicketDto>(ErrorCodes.UnknownError, null);
                                    }

                                    TicketDto ticketDto = DtoMapperMobile_DiasFacilityManagementSqlServer_Development.Map<CustomMobileTicketDto, TicketDto>(castedDto);
                                    Tuple<ErrorCodes, TicketDto> resultAddedTicket = await _ticketBusinessRules.AddAsync(ticketDto);

                                    ticketDto = resultAddedTicket.Item2;
                                    TicketNoteDto ticketNoteDto = new();
                                    TicketRelatedLocationDto ticketRelatedLocationDto = new();
                                    AttachmentDto attachmentDto = new();
                                    //List<Tuple<ErrorCodes, AttachmentDto>> attachemntResultAddedList = new();
                                    Tuple<ErrorCodes, AttachmentDto> attachemntResultAddedList = new Tuple<ErrorCodes, AttachmentDto>(ErrorCodes.None, new());
                                    Tuple<ErrorCodes, TicketRelatedLocationDto> resultAddedTicketRelatedLocation = new Tuple<ErrorCodes, TicketRelatedLocationDto>(ErrorCodes.None, new());



                                    if ((resultAddedTicket.Item1 == ErrorCodes.None) && (resultAddedTicket.Item2 != null))
                                    {

                                        foreach (var item in castedDto.LocationList)
                                        {
                                            ticketRelatedLocationDto.TicketId = resultAddedTicket.Item2.Id;
                                            ticketRelatedLocationDto.TicketLocationId = item;
                                            ticketRelatedLocationDto.AddedByUserId = resultAddedTicket.Item2.AddedByUserId;
                                            resultAddedTicketRelatedLocation = await _ticketRelatedLocationBusinessRules.AddAsync(ticketRelatedLocationDto);



                                            if ((resultAddedTicketRelatedLocation.Item1 == ErrorCodes.None) && (resultAddedTicketRelatedLocation.Item2 != null))
                                            {

                                                foreach (var attachmentItem in castedDto.AttachmentsFile)
                                                {
                                                    attachmentDto.AddedByUserId = 1;
                                                    attachmentDto.FileData = attachmentItem.FileData;
                                                    attachmentDto.FileType = attachmentItem.FileType;
                                                    attachmentDto.AttachmentDescription = "Mobil description";
                                                    attachmentDto.FolderName = attachmentItem.FileName;
                                                    attachmentDto.TicketId = resultAddedTicket.Item2.Id;
                                                    attachemntResultAddedList = await _attachmentBusinessRules.AddAsync(attachmentDto);
                                                    if ((attachemntResultAddedList.Item1 != ErrorCodes.None) && (attachemntResultAddedList.Item2 == null))
                                                    {

                                                        transaction.Rollback();

                                                        return new Tuple<ErrorCodes, CustomMobileTicketDto>(resultAddedTicketRelatedLocation.Item1, null);
                                                    }

                                                }

                                                transaction.Commit();

                                                CustomMobileTicketDto convertedMobileDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<TicketDto, CustomMobileTicketDto>(ticketDto);

                                                return new Tuple<ErrorCodes, CustomMobileTicketDto>(ErrorCodes.None, convertedMobileDto);

                                            }
                                        }



                                        transaction.Commit();



                                        CustomMobileTicketDto convertedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<TicketDto, CustomMobileTicketDto>(ticketDto);



                                        return new Tuple<ErrorCodes, CustomMobileTicketDto>(ErrorCodes.None, convertedDto);






                                    }
                                    else
                                    {
                                        transaction.Rollback();



                                        return new Tuple<ErrorCodes, CustomMobileTicketDto>(resultAddedTicket.Item1, null);
                                    }
                                }



                                catch (Exception e)
                                {
                                    transaction.Rollback();



                                    return new Tuple<ErrorCodes, CustomMobileTicketDto>(ErrorCodes.UnknownError, null);
                                }
                            }
                            break;
                        }



                    default:
                        {
                            return new Tuple<ErrorCodes, CustomMobileTicketDto>(ErrorCodes.UnknownError, null);
                        }
                }
            }
            return new Tuple<ErrorCodes, CustomMobileTicketDto>(ErrorCodes.UnknownError, null);
        }

        #endregion Mobile
    }
}


