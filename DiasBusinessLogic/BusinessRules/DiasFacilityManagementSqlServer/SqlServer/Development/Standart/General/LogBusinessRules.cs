using DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.Configuration;
using DiasBusinessLogic.InterfacesAbstracts.Abstracts.BusinessRules;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Base.SqlServer.Standart;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.UnitOfWork;
using DiasShared.Errors;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using DevelopmentAttachmentInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Standart;

namespace DiasBusinessLogic.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Standart.General
{
    public class LogBusinessRules : BusinessRuleAbstract, DevelopmentAttachmentInterface.ILogBusinessRules, IBaseLogBusinessRules
    {
        //private static AutoMapperProfileMapper<DevelopmentAttachmentProfile.AttachmentProfile> DtoMapper_DiasFacilityManagementSqlServer_Development
        //    => new(new(DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer")));

        private readonly ILogger<Error> _logger;
        private readonly IUnitOfWork_EF _unitOfWork_EF;

        public LogBusinessRules() : this(
            DI_ServiceLocator.Current.Get<IUnitOfWork_EF>(),
            DI_ServiceLocator.Current.Get<ILogger<Error>>())
        {
        }

        private LogBusinessRules(IUnitOfWork_EF unitOfWork_EF, ILogger<Error> logger)
        {
            _logger = logger;
            _unitOfWork_EF = unitOfWork_EF;
        }

        public Task<Tuple<Error, int>> SaveLog(Error error)
        {
            throw new NotImplementedException();
        }
    }
}
