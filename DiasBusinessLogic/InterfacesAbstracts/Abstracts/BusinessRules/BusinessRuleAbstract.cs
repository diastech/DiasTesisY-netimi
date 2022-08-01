using DiasBusinessLogic.Shared.SimpleInjector;
using SimpleInjector;

namespace DiasBusinessLogic.InterfacesAbstracts.Abstracts.BusinessRules
{
    public abstract class BusinessRuleAbstract
    {
        protected Container AutoMapperDI_Container => DI_Container.ContainerObj;
    }
}
