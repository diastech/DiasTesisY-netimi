using DiasBusinessLogic.InterfacesAbstracts.Abstracts.BusinessRules;
using DevelopmentTicketPriorityProfile = DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Development;
using DevelopmentTicketPriorityInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Standart;
using DiasBusinessLogic.AutoMapper.Configuration;
using DiasBusinessLogic.Shared.Configuration;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.UnitOfWork;
using DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.Configuration;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.BaseBusinessRules.SqlServer.Standart;

namespace DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.SqlServer.Development.Standart
{
    public class TicketPriorityBusinessRules : BusinessRuleAbstract , DevelopmentTicketPriorityInterface.ITicketPriorityBusinessRules, IBaseTicketPriorityBusinessRules
    {
        private static AutoMapperProfileMapper<DevelopmentTicketPriorityProfile.TicketPriorityProfile> DtoMapper_DiasFacilityManagementSqlServer_Development
            => new(new(DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer")));

        private readonly IUnitOfWork_EF _unitOfWork_EF;

        public TicketPriorityBusinessRules() : this(DI_ServiceLocator.Current.Get<IUnitOfWork_EF>())
        {
        }

        private TicketPriorityBusinessRules(IUnitOfWork_EF unitOfWork_EF)
        {
            _unitOfWork_EF = unitOfWork_EF;
        }
    }
}
