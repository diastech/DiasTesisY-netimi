using DiasShared.Errors;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DIMDevDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;

namespace DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement
{
    public interface IAuthenticationTestRepository
    {
        public Task<Tuple<Error, IEnumerable<DIMDevDto.CompanyRoleClaimDto>>> TestAddCompanyRoleClaimAsync();
    }
}
