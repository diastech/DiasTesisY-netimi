using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.BaseBusinessRules.SqlServer.Standart;
using DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement;
using DiasWebApi.Shared.Operations.BusinessLogicOperations.SimpleInjectorOperations;
using static DiasWebApi.Shared.Enums.WebApiApplicationEnums;

namespace DiasWebApi.Repositories.DiasFacilityManagement
{
    public class ViewTicketStateLevelDtoRepository : IViewTicketStateLevelDtoRepository
    {
        private IBaseViewTicketStateLevelBusinessRules _baseViewTicketStateLevelBusinessRules;
        private ApplicationBusinessLogicEnvironment _applicationBusinessLogicEnvironment;
        private bool businessLogicContainerStatus { get; set; }
        public ViewTicketStateLevelDtoRepository(IBaseViewTicketStateLevelBusinessRules baseViewTicketStateLevelBusinessRules, ApplicationBusinessLogicEnvironment applicationBusinessLogicEnvironment)
        {
            businessLogicContainerStatus = SimpleInjectorContainerOperations.VerifyContainer();

            if (businessLogicContainerStatus)
            {
                _baseViewTicketStateLevelBusinessRules = baseViewTicketStateLevelBusinessRules;
                _applicationBusinessLogicEnvironment = applicationBusinessLogicEnvironment;
            }
        }
    }
}
