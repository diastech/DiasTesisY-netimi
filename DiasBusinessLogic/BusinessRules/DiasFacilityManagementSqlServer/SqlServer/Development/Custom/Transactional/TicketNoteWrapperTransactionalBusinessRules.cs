using DiasBusinessLogic.InterfacesAbstracts.Abstracts.BusinessRules;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Base.SqlServer.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionalInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Custom;
using DevelopmentUserInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Standart;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.UnitOfWork;
using DiasBusinessLogic.AutoMapper.Configuration;
using CustomDevelopmentTicketProfile = DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.CustomProfiles.Development;
using DiasBusinessLogic.Shared.Configuration;
using DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.Configuration;
using DiasShared.Errors;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using DiasShared.Services.Communication.BusinessLogicMessage;
using DevelopmentContext = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using Microsoft.EntityFrameworkCore.Storage;
using DiasBusinessLogic.Shared.Error;
using Newtonsoft.Json;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;

namespace DiasBusinessLogic.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Custom
{
    public class TicketNoteWrapperTransactionalBusinessRules : BusinessRuleAbstract, TransactionalInterface.ITicketNoteWrapperTransactionalBusinessRules, IBaseTicketNoteWrapperTransactionalBusinessRules
    {        
        private readonly DevelopmentUserInterface.ITicketNoteBusinessRules _ticketNoteBusinessRules;
        private readonly DevelopmentUserInterface.IAttachmentBusinessRules _attachmentBusinessRules;
        private readonly IUnitOfWork_EF _unitOfWork_EF;

        private static AutoMapperProfileMapper<CustomDevelopmentTicketProfile.CustomTicketNoteProfile> DtoMapper_DiasFacilityManagementSqlServer_Development
            => new(new(DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer")));

        public TicketNoteWrapperTransactionalBusinessRules() : this(
            DI_ServiceLocator.Current.Get<DevelopmentUserInterface.ITicketNoteBusinessRules>(),
            DI_ServiceLocator.Current.Get<DevelopmentUserInterface.IAttachmentBusinessRules>(),
            DI_ServiceLocator.Current.Get<IUnitOfWork_EF>())
        {

        }
        private TicketNoteWrapperTransactionalBusinessRules(
            DevelopmentUserInterface.ITicketNoteBusinessRules ticketNoteBusinessRules,
            DevelopmentUserInterface.IAttachmentBusinessRules attachmentBusinessRules,

            IUnitOfWork_EF unitOfWork_EF)
        {
            _ticketNoteBusinessRules = ticketNoteBusinessRules;
            _attachmentBusinessRules = attachmentBusinessRules;
            _unitOfWork_EF = unitOfWork_EF;
        }

        #region WebClient
        public async Task<Tuple<Error, CustomTicketNoteDto>> AddTicketNoteWrapperAsync(BusinessLogicRequest request)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new Tuple<Error, CustomTicketNoteDto>(Errors.General.NotFoundDatabaseServer(), null);
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
                                    bool isSaved = true;

                                    if ((request == null) || (request.RequestDtosJsons == null) || (request.RequestDtosJsons.Count < 1))
                                    {
                                        return new Tuple<Error, CustomTicketNoteDto>(Errors.General.RequestNull("TicketNote"), null);
                                    }

                                    CustomTicketNoteDto castedDto = JsonConvert.DeserializeObject<CustomTicketNoteDto>(request.RequestDtosJsons[0]);

                                    if (castedDto == null)
                                    {
                                        return new Tuple<Error, CustomTicketNoteDto>(Errors.General.MappingError("TicketNote"), null);
                                    }

                                    TicketNoteDto ticketDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<CustomTicketNoteDto, TicketNoteDto>(castedDto);

                                    Tuple<Error, TicketNoteDto> resultAddedTicketNote = await _ticketNoteBusinessRules.AddAsync(ticketDto);

                                    if((resultAddedTicketNote.Item1.BusinessOperationSucceed == true) && (resultAddedTicketNote.Item2 != null))
                                    {
                                        foreach (var attachment in castedDto.Attachments)
                                        {
                                            attachment.TicketId = castedDto.TicketId;
                                            attachment.TicketNoteId = resultAddedTicketNote.Item2.Id;
                                            attachment.AddedByUserId = resultAddedTicketNote.Item2.AddedByUserId;
                                            Tuple<Error, AttachmentDto> resultAddedAttachemnt = await _attachmentBusinessRules.AddAsync(attachment);

                                            if ((resultAddedAttachemnt.Item1.BusinessOperationSucceed == true) && (resultAddedAttachemnt.Item2 != null))
                                            {
                                                isSaved = true;
                                            }
                                            else
                                            {
                                                isSaved = false;
                                                transaction.Rollback();

                                                return new Tuple<Error, CustomTicketNoteDto>(resultAddedAttachemnt.Item1, null);
                                            }
                                        }
                                        if(isSaved == true)
                                        {
                                            transaction.Commit();
                                            return new Tuple<Error, CustomTicketNoteDto>(resultAddedTicketNote.Item1, DtoMapper_DiasFacilityManagementSqlServer_Development.Map<TicketNoteDto, CustomTicketNoteDto>(resultAddedTicketNote.Item2));
                                        }
                                    }

                                    else
                                    {
                                        transaction.Rollback();

                                        return new Tuple<Error, CustomTicketNoteDto>(resultAddedTicketNote.Item1, null);
                                    }
                                }

                                catch (Exception e)
                                {
                                    transaction.Rollback();

                                    return new Tuple<Error, CustomTicketNoteDto>(Errors.General.ErrorInsert("TicketNote"), null);
                                }
                            }
                            break;
                        }

                    default:
                        {
                            return new Tuple<Error, CustomTicketNoteDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
            return new Tuple<Error, CustomTicketNoteDto>(Errors.General.NotFoundDatabaseServer(), null);
        }

        public Task<Tuple<Error, CustomTicketNoteDto>> DeleteTicketNoteWrapperAsync(BusinessLogicRequest request)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
