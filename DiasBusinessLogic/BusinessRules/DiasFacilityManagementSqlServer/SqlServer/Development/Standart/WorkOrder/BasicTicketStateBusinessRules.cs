using DiasBusinessLogic.AutoMapper.Configuration;
using DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.Configuration;
using DiasBusinessLogic.InterfacesAbstracts.Abstracts.BusinessRules;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.BaseBusinessRules.SqlServer.Standart;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.UnitOfWork;
using DiasBusinessLogic.Shared.Configuration;
using DevelopmentBasicTicketStateInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Standart;
using DevelopmentBasicTicketStateProfile = DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Development;

namespace DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.SqlServer.Development.Standart
{
    public class BasicTicketStateBusinessRules : BusinessRuleAbstract, DevelopmentBasicTicketStateInterface.IBasicTicketStateBusinessRules,IBaseBasicTicketStateBusinessRules
    {
        private static AutoMapperProfileMapper<DevelopmentBasicTicketStateProfile.BasicTicketStateProfile> DtoMapper_DiasFacilityManagementSqlServer_Development
            => new(new(DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer")));

        private readonly IUnitOfWork_EF _unitOfWork_EF;

        public BasicTicketStateBusinessRules() : this(DI_ServiceLocator.Current.Get<IUnitOfWork_EF>())
        {
        }

        private BasicTicketStateBusinessRules(IUnitOfWork_EF unitOfWork_EF)
        {
            _unitOfWork_EF = unitOfWork_EF;
        }
    }
}
