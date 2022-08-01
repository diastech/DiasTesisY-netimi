namespace DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.Configuration
{
    public interface IDI_ServiceLocator
    {
        public T Get<T>() where T : class;

        public void VerifyContainer();

    }
}
