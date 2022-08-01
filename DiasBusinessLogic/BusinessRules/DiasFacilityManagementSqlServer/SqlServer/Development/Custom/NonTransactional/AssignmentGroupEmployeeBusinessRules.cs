using DiasBusinessLogic.InterfacesAbstracts.Abstracts.BusinessRules;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.BaseBusinessRules.SqlServer.Standart;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using DiasShared.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionalInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Custom;
using DevelopmentUserInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Standart;
using DiasBusinessLogic.AutoMapper.Configuration;
using StdDevelopmentTicketProfile = DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Development;
using DiasBusinessLogic.Shared.Configuration;
using DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.Configuration;
using DiasBusinessLogic.Shared.Error;
using DevelopmentContext = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;

namespace DiasBusinessLogic.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Custom.NonTransactional
{
    public class AssignmentGroupEmployeeBusinessRules : BusinessRuleAbstract, TransactionalInterface.IAssignmentGroupEmployee, IBaseAssignmentGroupEmployeeBusinessRules
    {
        private readonly DevelopmentUserInterface.IAssignmentGroupEmployeeBusinessRules _assignmentGroupEmployeeBusinessRules;
        private static AutoMapperProfileMapper<StdDevelopmentTicketProfile.AssignmentGroupEmployeeProfile> DtoMapper_DiasFacilityManagementSqlServer_Development
            => new(new(DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer")));
        public AssignmentGroupEmployeeBusinessRules() : this(DI_ServiceLocator.Current.Get<DevelopmentUserInterface.IAssignmentGroupEmployeeBusinessRules>())
        {
        }
        private AssignmentGroupEmployeeBusinessRules(DevelopmentUserInterface.IAssignmentGroupEmployeeBusinessRules assignmentGroupEmployeeBusinessRules)
        {
            _assignmentGroupEmployeeBusinessRules = assignmentGroupEmployeeBusinessRules;
        }

        public async Task<Tuple<Error, IEnumerable<AssignmentGroupEmployeeDto>>> GetAll()
        {
            Type dataContextType = DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer");

            if ((dataContextType == null) || (dataContextType.Name == null) || (dataContextType.Name == ""))
            {
                return new Tuple<Error, IEnumerable<AssignmentGroupEmployeeDto>>(Errors.General.NotFoundDatabaseServer(), null);
            }
            else
            {
                switch (dataContextType.Name)
                {
                    case "DiasFacilityManagementSqlServer":
                        {
                            DevelopmentContext.DiasFacilityManagementSqlServer DiasFacilityManagementSqlServerContext = new();

                            Tuple<Error, IEnumerable<DevelopmentDto.AssignmentGroupEmployeeDto>> resultGetAssignmentGroupEmployeeList = await _assignmentGroupEmployeeBusinessRules.GetAllAssignmentGroupEmployeeAsync();

                            try
                            {
                                if ((resultGetAssignmentGroupEmployeeList.Item1.BusinessOperationSucceed == true) && (resultGetAssignmentGroupEmployeeList.Item2 != null))
                                {
                                    return new Tuple<Error, IEnumerable<AssignmentGroupEmployeeDto>>(resultGetAssignmentGroupEmployeeList.Item1, resultGetAssignmentGroupEmployeeList.Item2);
                                }
                                else
                                {
                                    return new Tuple<Error, IEnumerable<AssignmentGroupEmployeeDto>>(resultGetAssignmentGroupEmployeeList.Item1, null);
                                }
                            }
                            catch (Exception e)
                            {
                                throw;
                            }
                        }
                    default:
                        {
                            return new Tuple<Error, IEnumerable<AssignmentGroupEmployeeDto>>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
        }
    }
}
