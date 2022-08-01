
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using DiasShared.Errors;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Custom
{
    public interface IUserAssignmentGroupWrapperBusinessRules
    {
        public Task<Tuple<Error, IEnumerable<CombinedUserAndAssignmentGroupDto>>> GetAllCombinedUserAndAssigmentGroups();
    }
}
