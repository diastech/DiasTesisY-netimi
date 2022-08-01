using DiasBusinessLogic.InterfacesAbstracts.Abstracts.BusinessRules;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.BaseBusinessRules.SqlServer.Custom;
using TransactionalInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Custom;
using DevelopmentUserInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Standart;
using DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.Configuration;

namespace DiasBusinessLogic.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Custom
{
    //Transactional iş kuralı birden çok senkronize iş kuralının bir arada transaction mantığı ile yapılması için tasarlanmıştır
    //Mevcut diğer iş kuralları mikro yapıda olduğundan ve transaction mantığı kullanılamayacağından böyle bir iş kuralına ihtiyaç vardır
    //Kendi içinde birden çok  iş kuralı DI interface i içerebilir
    public class UserTransactionalBusinessRules : BusinessRuleAbstract, TransactionalInterface.IUserTransactionalBusinessRules, IBaseUserTransactionalBusinessRules
    {
        //İş kuralı içinde kullanacağımız ve inject edeceğimiz diğer iş kuralı interfaceleri
        private readonly DevelopmentUserInterface.IUserBusinessRules _userBusinessRules;


        //Simple Injector Service Factory vasıtasıyla Transaction da kullanacağımız iş kurallarını inject edelim
        public UserTransactionalBusinessRules() : this(DI_ServiceLocator.Current.Get<DevelopmentUserInterface.IUserBusinessRules>())
        {
        }

        //Sadece boş kurucu metod tarafından çağrılır
        private UserTransactionalBusinessRules(DevelopmentUserInterface.IUserBusinessRules userBusinessRules)
        {
            _userBusinessRules = userBusinessRules;
        }



    }
}
