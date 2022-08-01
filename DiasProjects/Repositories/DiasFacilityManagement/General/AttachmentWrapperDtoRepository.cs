using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Base.SqlServer.Custom;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Custom;
using DiasBusinessLogic.Shared.Error;
using DiasDataAccessLayer.Enums;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using DiasShared.Errors;
using DiasShared.Services.Communication.BusinessLogicMessage;
using DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement;
using DiasWebApi.Shared.Operations.BusinessLogicOperations.SimpleInjectorOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DiasWebApi.Shared.Enums.WebApiApplicationEnums;

namespace DiasWebApi.Repositories.DiasFacilityManagement
{
    public class AttachmentWrapperDtoRepository : IAttachmentWrapperDtoRepository
    {
        private IBaseAttachmentWrapperBusinessRules _baseAttachmentWrapperBusinessRules;
        private IBaseAttachmentWrapperTransactionalBusinessRules _baseAttachmentWrapperTransactionalBusinessRules;
        private ApplicationBusinessLogicEnvironment _applicationBusinessLogicEnvironment;
        private bool businessLogicContainerStatus { get; set; }
        public AttachmentWrapperDtoRepository(IBaseAttachmentWrapperBusinessRules baseAttachmentWrapperBusinessRules,
            IBaseAttachmentWrapperTransactionalBusinessRules baseAttachmentWrapperTransactionalBusinessRules,
            ApplicationBusinessLogicEnvironment applicationBusinessLogicEnvironment
            )
        {
            businessLogicContainerStatus = SimpleInjectorContainerOperations.VerifyContainer();
            if (businessLogicContainerStatus)
            {
                _baseAttachmentWrapperBusinessRules = baseAttachmentWrapperBusinessRules;
                _baseAttachmentWrapperTransactionalBusinessRules = baseAttachmentWrapperTransactionalBusinessRules;
            }

        }

        public async Task<Tuple<Error, IEnumerable<CustomAttachmentDto>>> GetAttachmentsByTicketIdAsync(int Id)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            IAttachmentWrapperBusinessRules businessRule =
                                (IAttachmentWrapperBusinessRules)_baseAttachmentWrapperBusinessRules;

                            return await businessRule.GetTicketAttachmentsByTicketId(Id);
                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            return new Tuple<Error, IEnumerable<CustomAttachmentDto>>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, IEnumerable<CustomAttachmentDto>>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    default:
                        {
                            return new Tuple<Error, IEnumerable<CustomAttachmentDto>>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, IEnumerable<CustomAttachmentDto>>(Errors.General.GeneralServerError(), null);
            }
        }

        public async Task<Tuple<Error, CustomAttachmentDto>> AddAttachmentWrapperAsync(BusinessLogicRequest request)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            IAttachmentWrapperTransactionalBusinessRules businessRule =
                                (IAttachmentWrapperTransactionalBusinessRules)_baseAttachmentWrapperTransactionalBusinessRules;
                            return await businessRule.AddAttachmentWrapperAsync(request);
                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            return new Tuple<Error, CustomAttachmentDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, CustomAttachmentDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    default:
                        {
                            return new Tuple<Error, CustomAttachmentDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, CustomAttachmentDto>(Errors.General.GeneralServerError(), null);
            }
        }
        #region WebClient
        #region Development

        #endregion
        #endregion
    }
}
