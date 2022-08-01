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
using DiasBusinessLogic.Shared.Error;
using Microsoft.EntityFrameworkCore.Storage;
using DevelopmentContext = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using Newtonsoft.Json;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using System.IO;

namespace DiasBusinessLogic.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Custom
{
    public class AttachmentWrapperTransactionalBusinessRules : BusinessRuleAbstract, TransactionalInterface.IAttachmentWrapperTransactionalBusinessRules, IBaseAttachmentWrapperTransactionalBusinessRules
    {
        private readonly DevelopmentUserInterface.IAttachmentBusinessRules _attachmentBusinessRules;
        private readonly IUnitOfWork_EF _unitOfWork_EF;

        private static AutoMapperProfileMapper<CustomDevelopmentTicketProfile.CustomAttachmentProfile> DtoMapper_DiasFacilityManagementSqlServer_Development
            => new(new(DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer")));

        public AttachmentWrapperTransactionalBusinessRules() : this(
            DI_ServiceLocator.Current.Get<DevelopmentUserInterface.IAttachmentBusinessRules>(),
            DI_ServiceLocator.Current.Get<IUnitOfWork_EF>())
        {

        }
        private AttachmentWrapperTransactionalBusinessRules(
            DevelopmentUserInterface.IAttachmentBusinessRules attachmentBusinessRules,

            IUnitOfWork_EF unitOfWork_EF)
        {
            _attachmentBusinessRules = attachmentBusinessRules;
            _unitOfWork_EF = unitOfWork_EF;
        }

        public async Task<Tuple<Error, CustomAttachmentDto>> AddAttachmentWrapperAsync(BusinessLogicRequest request)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new Tuple<Error, CustomAttachmentDto>(Errors.General.NotFoundDatabaseServer(), null);
            }
            else
            {
                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();
                            Tuple<Error, AttachmentDto> resultAddedAttachemnt = new Tuple<Error, AttachmentDto>(Errors.General.None(), new());

                            using (IDbContextTransaction transaction = DiasFacilityManagementSqlServerContext.Database.BeginTransaction())
                            {
                                try
                                {
                                    bool isSaved = true;

                                    if ((request == null) || (request.RequestDtosJsons == null) || (request.RequestDtosJsons.Count < 1))
                                    {
                                        return new Tuple<Error, CustomAttachmentDto>(Errors.General.RequestNull("Attachment"), null);
                                    }

                                    CustomAttachmentDto castedDto = JsonConvert.DeserializeObject<CustomAttachmentDto>(request.RequestDtosJsons[0]);

                                    if (castedDto == null)
                                    {
                                        return new Tuple<Error, CustomAttachmentDto>(Errors.General.MappingError("Attachment"), null);
                                    }
                                 
                                    foreach (var attachment in castedDto.Attachments)
                                    {
                                      
                                        attachment.TicketId = castedDto.TicketId;
                                        attachment.AddedByUserId = castedDto.AddedByUserId;

                                        FileInfo fi = new FileInfo(attachment.FolderName);
                                        attachment.FolderName = "Ticket_" + attachment.TicketId.ToString() +
                                                                     "_Added_" + attachment.AddedByUserId.ToString() +
                                                                         "_" + "attch_" + DateTime.Now.Ticks.ToString() + fi.Extension;

                                        resultAddedAttachemnt = await _attachmentBusinessRules.AddAsync(attachment);

                                        if ((resultAddedAttachemnt.Item1.BusinessOperationSucceed == true) && (resultAddedAttachemnt.Item2 != null))
                                        {
                                            isSaved = true;
                                        }
                                        else
                                        {
                                            isSaved = false;
                                            transaction.Rollback();

                                            return new Tuple<Error, CustomAttachmentDto>(resultAddedAttachemnt.Item1, null);
                                        }
                                    }
                                    if (isSaved == true)
                                    {
                                        transaction.Commit();
                                        return new Tuple<Error, CustomAttachmentDto>(resultAddedAttachemnt.Item1, DtoMapper_DiasFacilityManagementSqlServer_Development.Map<AttachmentDto, CustomAttachmentDto>(resultAddedAttachemnt.Item2));
                                    }

                                }

                                catch (Exception e)
                                {
                                    transaction.Rollback();

                                    return new Tuple<Error, CustomAttachmentDto>(Errors.General.ErrorInsert("Attachment"), null);
                                }
                            }
                            break;
                        }

                    default:
                        {
                            return new Tuple<Error, CustomAttachmentDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
            return new Tuple<Error, CustomAttachmentDto>(Errors.General.NotFoundDatabaseServer(), null);
        }

        public async Task<Tuple<Error, CustomAttachmentDto>> DeleteAttachmentWrapperAsync(BusinessLogicRequest request)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new Tuple<Error, CustomAttachmentDto>(Errors.General.NotFoundDatabaseServer(), null);
            }
            else
            {
                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();
                            Tuple<Error, AttachmentDto> resultAddedAttachemnt = new Tuple<Error, AttachmentDto>(Errors.General.None(), new());

                            using (IDbContextTransaction transaction = DiasFacilityManagementSqlServerContext.Database.BeginTransaction())
                            {
                                try
                                {
                                    bool isSaved = true;

                                    if ((request == null) || (request.RequestDtosJsons == null) || (request.RequestDtosJsons.Count < 1))
                                    {
                                        return new Tuple<Error, CustomAttachmentDto>(Errors.General.RequestNull("Attachment"), null);
                                    }

                                    CustomAttachmentDto castedDto = JsonConvert.DeserializeObject<CustomAttachmentDto>(request.RequestDtosJsons[0]);

                                    if (castedDto == null)
                                    {
                                        return new Tuple<Error, CustomAttachmentDto>(Errors.General.MappingError("Attachment"), null);
                                    }

                                    resultAddedAttachemnt = await _attachmentBusinessRules.DeleteAsync(castedDto);

                                    if ((resultAddedAttachemnt.Item1.BusinessOperationSucceed == true) && (resultAddedAttachemnt.Item2 != null))
                                    {
                                        transaction.Commit();
                                        return new Tuple<Error, CustomAttachmentDto>(resultAddedAttachemnt.Item1, DtoMapper_DiasFacilityManagementSqlServer_Development.Map<AttachmentDto, CustomAttachmentDto>(resultAddedAttachemnt.Item2));
                                    }
                                    else
                                    {
                                        transaction.Rollback();
                                        return new Tuple<Error, CustomAttachmentDto>(resultAddedAttachemnt.Item1, null);
                                    }                                    
                                }
                                catch (Exception e)
                                {
                                    transaction.Rollback();

                                    return new Tuple<Error, CustomAttachmentDto>(Errors.General.ErrorInsert("Attachment"), null);
                                }
                            }                            
                        }

                    default:
                        {
                            return new Tuple<Error, CustomAttachmentDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }            
        }
    }
}
