using DiasBusinessLogic.InterfacesAbstracts.Abstracts.BusinessRules;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Base.SqlServer.Custom;
using DiasDataAccessLayer.Enums;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionalInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Test.Custom;
using DevelopmentUserInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Test.Standart;
using DevelopmentContext = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Test.Models;
using CustomDevelopmentTicketProfile = DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.CustomProfiles.Test;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.UnitOfWork;
using DiasBusinessLogic.AutoMapper.Configuration;
using DiasBusinessLogic.Shared.Configuration;
using DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.Configuration;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using Microsoft.EntityFrameworkCore.Storage;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard;
using DiasShared.Services.Communication.BusinessLogicMessage;
using Newtonsoft.Json;

namespace DiasBusinessLogic.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Test.Custom
{
    

    public class BasicTicketWrapperTransactionalBusinessRules : BusinessRuleAbstract, TransactionalInterface.IBasicTicketWrapperTransactionalBusinessRules, IBaseBasicTicketWrapperTransactionalBusinessRules
    {
        private readonly DevelopmentUserInterface.IBasicTicketBusinessRules _basicTicketBusinessRules;
        private readonly DevelopmentUserInterface.IAttachmentBusinessRules _attachmentBusinessRules;
        private readonly IUnitOfWork_EF _unitOfWork_EF;
        private static AutoMapperProfileMapper<CustomDevelopmentTicketProfile.CustomBasicTicketProfile> DtoMapper_DiasFacilityManagementSqlServer_Development
            => new(new(DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServerTest")));

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
        public async Task<Tuple<BusinessLogicMessageCodes.ErrorCodes, CustomBasicTicketDto>> AddBasicTicketWrapperAsync(BusinessLogicRequest request)
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServerTest");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new Tuple<ErrorCodes, CustomBasicTicketDto>(ErrorCodes.UnknownError, null);
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
                                        return new Tuple<ErrorCodes, CustomBasicTicketDto>(ErrorCodes.UnknownError, null);
                                    }
                                    CustomBasicTicketDto castedDto = JsonConvert.DeserializeObject<CustomBasicTicketDto>(request.RequestDtosJsons[0]);

                                    if (castedDto == null)
                                    {
                                        return new Tuple<ErrorCodes, CustomBasicTicketDto>(ErrorCodes.UnknownError, null);
                                    }

                                    BasicTicketDto basicTicketDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<CustomBasicTicketDto, BasicTicketDto>(castedDto);
                                    List<AttachmentDto> attachmentDtos = basicTicketDto.Attachments;
                                    BasicTicketDto basicTicket = basicTicketDto;
                                    basicTicket.Attachments = null;
                                    CustomBasicTicketDto convertedDto = new();
                                    basicTicket.StateId = 9;
                                    Tuple<ErrorCodes, BasicTicketDto> resultAddedBasicTicket = await _basicTicketBusinessRules.AddAsync(basicTicket);

                                    if ((resultAddedBasicTicket.Item1 == ErrorCodes.None) && (resultAddedBasicTicket.Item2 != null))
                                    {
                                        foreach (var item in attachmentDtos)
                                        {
                                            item.BasicTicketId = resultAddedBasicTicket.Item2.Id;
                                            item.AddedByUserId = resultAddedBasicTicket.Item2.AddedByUserId;
                                            Tuple<ErrorCodes, AttachmentDto> resultAddedAttachment = await _attachmentBusinessRules.AddAsync(item);

                                            if ((resultAddedBasicTicket.Item1 == ErrorCodes.None) && (resultAddedBasicTicket.Item2 != null))
                                            {
                                                transaction.Commit();

                                                resultAddedBasicTicket.Item2.Attachments.Add(resultAddedAttachment.Item2);
                                                convertedDto = DtoMapper_DiasFacilityManagementSqlServer_Development.Map<BasicTicketDto, CustomBasicTicketDto>(resultAddedBasicTicket.Item2);
                                            }

                                            else
                                            {
                                                transaction.Rollback();

                                                return new Tuple<ErrorCodes, CustomBasicTicketDto>(resultAddedAttachment.Item1, null);
                                            }
                                        }
                                        
                                    }
                                    else
                                    {
                                        transaction.Rollback();

                                        return new Tuple<ErrorCodes, CustomBasicTicketDto>(resultAddedBasicTicket.Item1, null);
                                    }


                                    

                                    return new Tuple<ErrorCodes, CustomBasicTicketDto>(ErrorCodes.NonCrudError, convertedDto);
                                }

                                catch (Exception e)
                                {
                                    transaction.Rollback();

                                    return new Tuple<ErrorCodes, CustomBasicTicketDto>(ErrorCodes.UnknownError, null);
                                }
                            }
                            break;
                        }

                    default:
                        {
                            return new Tuple<ErrorCodes, CustomBasicTicketDto>(ErrorCodes.UnknownError, null);
                        }
                }
            }
            return new Tuple<ErrorCodes, CustomBasicTicketDto>(ErrorCodes.UnknownError, null);
        }

        public Task<Tuple<BusinessLogicMessageCodes.ErrorCodes, CustomBasicTicketDto>> DeleteBasicTicketWrapperAsync(CustomBasicTicketDto customBasicTicketDto)
        {
            throw new NotImplementedException();
        }

        public Task<Tuple<BusinessLogicMessageCodes.ErrorCodes, CustomBasicTicketDto>> UpdateBasicTicketWrapperAsync(CustomBasicTicketDto customBasicTicketDto)
        {
            throw new NotImplementedException();
        }
    }
}
