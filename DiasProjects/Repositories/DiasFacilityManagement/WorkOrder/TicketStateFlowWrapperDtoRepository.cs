using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Base.SqlServer.Custom;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Custom;
using DiasBusinessLogic.Shared.Error;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using DiasShared.Errors;
using DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement;
using DiasWebApi.Shared.Operations.BusinessLogicOperations.SimpleInjectorOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DiasWebApi.Shared.Enums.WebApiApplicationEnums;

namespace DiasWebApi.Repositories.DiasFacilityManagement
{
    public class TicketStateFlowWrapperDtoRepository : ITicketStateFlowWrapperDtoRepository
    {
        private IBaseTicketStateFlowWrapperBusinessRules _baseTicketStateFlowWrapperBusinessRules;

        private ApplicationBusinessLogicEnvironment _applicationBusinessLogicEnvironment;
        private bool businessLogicContainerStatus { get; set; }
        public TicketStateFlowWrapperDtoRepository(IBaseTicketStateFlowWrapperBusinessRules baseTicketStateFlowWrapperBusinessRules,            
            ApplicationBusinessLogicEnvironment applicationBusinessLogicEnvironment
            )
        {
            businessLogicContainerStatus = SimpleInjectorContainerOperations.VerifyContainer();
            if (businessLogicContainerStatus)
            {
                _baseTicketStateFlowWrapperBusinessRules = baseTicketStateFlowWrapperBusinessRules;                
            }

        }

        #region WebClient
        #region Development
        public async Task<Tuple<Error, IEnumerable<CustomTicketStateFlowDto>>> GetAllTicketStateFlowdWrapperAsync()
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            ITicketStateFlowWrapperBusinessRules businessRule =
                                (ITicketStateFlowWrapperBusinessRules)_baseTicketStateFlowWrapperBusinessRules;

                            return await businessRule.GetAllTicketStateFlowWrapperAsync();
                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            return new Tuple<Error, IEnumerable<CustomTicketStateFlowDto>>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, IEnumerable<CustomTicketStateFlowDto>>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    default:
                        {
                            return new Tuple<Error, IEnumerable<CustomTicketStateFlowDto>>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, IEnumerable<CustomTicketStateFlowDto>>(Errors.General.GeneralServerError(), null);
            }
        }
        #endregion
        #endregion
    }
}
