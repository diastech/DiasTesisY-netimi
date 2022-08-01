using DiasShared.Errors;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;

namespace DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Standart
{
    public interface ICompanyRoleClaimBusinessRules
    {
        public Task<Tuple<Error, IEnumerable<DevelopmentDto.CompanyRoleClaimDto>>> TestAddCompanyRoleClaimAsync();

        public Task<Tuple<Error, IEnumerable<DevelopmentDto.CompanyRoleClaimDto>>> GetCompanyRoleClaimsByCompanyRoleIdAsync(int companyRoleId);

    }
}
