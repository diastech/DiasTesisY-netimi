using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.BaseBusinessRules.SqlServer.Standart;
using DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement;
using DiasWebApi.Shared.Operations.BusinessLogicOperations.SimpleInjectorOperations;
using static DiasWebApi.Shared.Enums.WebApiApplicationEnums;

namespace DiasWebApi.Repositories.DiasFacilityManagement
{
    public class ViewTicketNoteDtoRepository : IViewTicketNoteDtoRepository
    {
        private IBaseViewTicketNoteBusinessRules _baseViewTicketNoteBusinessRules;
        private ApplicationBusinessLogicEnvironment _applicationBusinessLogicEnvironment;
        private bool businessLogicContainerStatus { get; set; }
        public ViewTicketNoteDtoRepository(IBaseViewTicketNoteBusinessRules baseViewTicketNoteBusinessRules, ApplicationBusinessLogicEnvironment applicationBusinessLogicEnvironment)
        {
            businessLogicContainerStatus = SimpleInjectorContainerOperations.VerifyContainer();

            if (businessLogicContainerStatus)
            {
                _baseViewTicketNoteBusinessRules = baseViewTicketNoteBusinessRules;
                _applicationBusinessLogicEnvironment = applicationBusinessLogicEnvironment;
            }
        }
    }
}
