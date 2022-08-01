using DiasBusinessLogic.InterfacesAbstracts.Abstracts.BusinessRules;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Base.SqlServer.Custom;
using DiasDataAccessLayer.Enums;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionalInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Custom;
using DevelopmentUserInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Standart;
using DevelopmentContext = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using CustomDevelopmentTicketProfile = DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.CustomProfiles.Development;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.UnitOfWork;
using DiasBusinessLogic.AutoMapper.Configuration;
using DiasBusinessLogic.Shared.Configuration;
using DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.Configuration;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using Microsoft.EntityFrameworkCore.Storage;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using DiasShared.Services.Communication.BusinessLogicMessage;
using Newtonsoft.Json;
using DiasShared.Errors;
using DiasBusinessLogic.Shared.Error;

namespace DiasBusinessLogic.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Custom
{
    

    public class BasicTicketWrapperTransactionalBusinessRules : BusinessRuleAbstract, TransactionalInterface.IBasicTicketWrapperTransactionalBusinessRules, IBaseBasicTicketWrapperTransactionalBusinessRules
    {
        private readonly DevelopmentUserInterface.IBasicTicketBusinessRules _basicTicketBusinessRules;
        private readonly DevelopmentUserInterface.IAttachmentBusinessRules _attachmentBusinessRules;
        private readonly IUnitOfWork_EF _unitOfWork_EF;
        private static AutoMapperProfileMapper<CustomDevelopmentTicketProfile.CustomBasicTicketProfile> DtoMapper_DiasFacilityManagementSqlServer_Development
            => new(new(DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer")));

        public BasicTicketWrapperTransactionalBusinessRules() : this(
            DI_ServiceLocator.Current.Get<DevelopmentUserInterface.IBasicTicketBusinessRules>(),
            DI_ServiceLocator.Current.Get<DevelopmentUserInterface.IAttachmentBusinessRules>(),
            DI_ServiceLocator.Current.Get<IUnitOfWork_EF>())
        {

        }

        public BasicTicketWrapperTransactionalBusinessRules(DevelopmentUserInterface.IBasicTicketBusinessRules basicTicketBusinessRules,
            DevelopmentUserInterface.IAttachmentBusinessRules attachmentBusinessRules,
            IUnitOfWork_EF unitOfWork_EF)
        {
            _basicTicketBusinessRules = basicTicketBusinessRules;
            _attachmentBusinessRules = attachmentBusinessRules;
            _unitOfWork_EF = unitOfWork_EF;
        }
        public async Task<Tuple<Error, CustomBasicTicketDto>> AddBasicTicketWrapperAsync(BusinessLogicRequest request)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {                
                return new Tuple<Error, CustomBasicTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
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
                                                (!Type.Equals(request.RequestDtosTypes[0], typeof(CustomBasicTicketDto))))
                                    {
                                        return new Tuple<Error, CustomBasicTicketDto>(Errors.General.RequestNull("BasicTicketDto"), null);
                                    }
                                    CustomBasicTicketDto castedDto = JsonConvert.DeserializeObject<CustomBasicTicketDto>(request.RequestDtosJsons[0]);

                                    if (castedDto == null)
                                    {
                                        return new Tuple<Error, CustomBasicTicketDto>(Errors.General.MappingError("BasicTicketDto"), null);
                                    }

                                    BasicTicketDto basicTicketDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<CustomBasicTicketDto, BasicTicketDto>(castedDto);
                                    List<AttachmentDto> attachmentDtos = basicTicketDto.Attachments;
                                    BasicTicketDto basicTicket = basicTicketDto;
                                    basicTicket.Attachments = null;
                                    CustomBasicTicketDto convertedDto = new();
                                    basicTicket.StateId = 9;
                                    Tuple<Error, BasicTicketDto> resultAddedBasicTicket = await _basicTicketBusinessRules.AddAsync(basicTicket);

                                    if ((resultAddedBasicTicket.Item1.BusinessOperationSucceed == true) && (resultAddedBasicTicket.Item2 != null))
                                    {
                                        foreach (var item in attachmentDtos)
                                        {
                                            item.BasicTicketId = resultAddedBasicTicket.Item2.Id;
                                            item.AddedByUserId = resultAddedBasicTicket.Item2.AddedByUserId;
                                            Tuple<Error, AttachmentDto> resultAddedAttachment = await _attachmentBusinessRules.AddAsync(item);

                                            if ((resultAddedBasicTicket.Item1.BusinessOperationSucceed == true) && (resultAddedBasicTicket.Item2 != null))
                                            {
                                                transaction.Commit();

                                                resultAddedBasicTicket.Item2.Attachments.Add(resultAddedAttachment.Item2);
                                                convertedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<BasicTicketDto, CustomBasicTicketDto>(resultAddedBasicTicket.Item2);
                                                return new Tuple<Error, CustomBasicTicketDto>(Errors.General.SuccessInsert("BasicTicketDto"), convertedDto);
                                            }

                                            else
                                            {
                                                transaction.Rollback();

                                                return new Tuple<Error, CustomBasicTicketDto>(resultAddedAttachment.Item1, null);
                                            }
                                        }
                                        
                                    }
                                    else
                                    {
                                        transaction.Rollback();

                                        return new Tuple<Error, CustomBasicTicketDto>(Errors.General.ErrorInsert("BasicTicketDto"), null);
                                    }


                                    return new Tuple<Error, CustomBasicTicketDto>(Errors.General.ErrorInsert("BasicTicketDto"), null);
                                }

                                catch (Exception e)
                                {
                                    transaction.Rollback();

                                    return new Tuple<Error, CustomBasicTicketDto>(Errors.General.ErrorInsert("BasicTicketDto"), null);
                                }
                            }
                            
                        }

                    default:
                        {
                            return new Tuple<Error, CustomBasicTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
        }

        public Task<Tuple<Error, CustomBasicTicketDto>> DeleteBasicTicketWrapperAsync(CustomBasicTicketDto customBasicTicketDto)
        {
            throw new NotImplementedException();
        }

        public Task<Tuple<Error, CustomBasicTicketDto>> UpdateBasicTicketWrapperAsync(CustomBasicTicketDto customBasicTicketDto)
        {
            throw new NotImplementedException();
        }
    }
}
