using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Base.SqlServer.Custom;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Custom;
using DiasBusinessLogic.Shared.Error;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
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
    public class TicketNoteWrapperDtoRepository : ITicketNoteWrapperDtoRepository
    {
        private IBaseTicketNoteWrapperBusinessRules _baseTicketNoteWrapperBusinessRules;
        private IBaseTicketNoteWrapperTransactionalBusinessRules _baseTicketNoteWrapperTransactionalBusinessRules;
        private ApplicationBusinessLogicEnvironment _applicationBusinessLogicEnvironment;
        private bool businessLogicContainerStatus { get; set; }

        public TicketNoteWrapperDtoRepository(IBaseTicketNoteWrapperBusinessRules baseTicketNoteWrapperBusinessRules,
            IBaseTicketNoteWrapperTransactionalBusinessRules baseTicketNoteWrapperTransactionalBusinessRules,
            ApplicationBusinessLogicEnvironment applicationBusinessLogicEnvironment
            )
        {
            businessLogicContainerStatus = SimpleInjectorContainerOperations.VerifyContainer();
            if (businessLogicContainerStatus)
            {
                _baseTicketNoteWrapperBusinessRules = baseTicketNoteWrapperBusinessRules;
                _baseTicketNoteWrapperTransactionalBusinessRules = baseTicketNoteWrapperTransactionalBusinessRules;
            }

        }
        #region WebClient
        #region Development
        public async Task<Tuple<Error, IEnumerable<CustomTicketNoteDto>>> GetTicketNoteByTicketIdAsync(int Id)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            ITicketNoteWrapperBusinessRules businessRule =
                                (ITicketNoteWrapperBusinessRules)_baseTicketNoteWrapperBusinessRules;

                            return await businessRule.GetTicketNotesByTicketId(Id);
                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            return new Tuple<Error, IEnumerable<CustomTicketNoteDto>>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, IEnumerable<CustomTicketNoteDto>>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    default:
                        {
                            return new Tuple<Error, IEnumerable<CustomTicketNoteDto>>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, IEnumerable<CustomTicketNoteDto>>(Errors.General.GeneralServerError(), null);
            }
        }

        public async Task<Tuple<Error, CustomTicketNoteDto>> AddTicketNoteWrapperAsync(BusinessLogicRequest request)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            ITicketNoteWrapperTransactionalBusinessRules businessRule =
                                (ITicketNoteWrapperTransactionalBusinessRules)_baseTicketNoteWrapperTransactionalBusinessRules;
                            return await businessRule.AddTicketNoteWrapperAsync(request);
                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            return new Tuple<Error, CustomTicketNoteDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, CustomTicketNoteDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    default:
                        {
                            return new Tuple<Error, CustomTicketNoteDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, CustomTicketNoteDto>(Errors.General.GeneralServerError(), null);
            }
        }

        public Task<Tuple<Error, CustomTicketNoteDto>> DeleteTicketNoteWrapperAsync(BusinessLogicRequest request)
        {
            throw new NotImplementedException();
        }
        #endregion
        #endregion
    }
}
