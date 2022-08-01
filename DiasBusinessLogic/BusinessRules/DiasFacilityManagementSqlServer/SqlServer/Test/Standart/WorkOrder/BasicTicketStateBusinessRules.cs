using DiasBusinessLogic.AutoMapper.Configuration;
using DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.Configuration;
using DiasBusinessLogic.InterfacesAbstracts.Abstracts.BusinessRules;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.BaseBusinessRules.SqlServer.Standart;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.UnitOfWork;
using DiasBusinessLogic.Shared.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestBasicTicketStateInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Test.Standart;
using TestBasicTicketStateProfile = DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Test;

namespace DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.SqlServer.Test.Standart
{
    public class BasicTicketStateBusinessRules : BusinessRuleAbstract, TestBasicTicketStateInterface.IBasicTicketStateBusinessRules,IBaseBasicTicketStateBusinessRules
    {
        private static AutoMapperProfileMapper<TestBasicTicketStateProfile.BasicTicketStateProfile> DtoMapper_DiasFacilityManagementSqlServer_Development
            => new(new(DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServerTest")));

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
