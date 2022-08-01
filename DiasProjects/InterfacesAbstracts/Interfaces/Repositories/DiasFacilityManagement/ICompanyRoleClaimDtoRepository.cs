using DiasShared.Errors;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DIMDevDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;

namespace DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement
{
    public interface ICompanyRoleClaimDtoRepository
    {
        public Task<Tuple<Error, DIMDevDto.CompanyRoleClaimDto>> GetByIdFromIntAsync(int id);
        public Task<Tuple<Error, IEnumerable<DIMDevDto.CompanyRoleClaimDto>>> GetAllAsync();
        public Task<Tuple<Error, DIMDevDto.CompanyRoleClaimDto>> InsertAsync(DIMDevDto.CompanyRoleClaimDto roleClaimDto);
        public Task<Tuple<Error, DIMDevDto.CompanyRoleClaimDto>> UpdateAsync(DIMDevDto.CompanyRoleClaimDto roleClaimDto);
        public Task<Tuple<Error, DIMDevDto.CompanyRoleClaimDto>> DeleteFromIntAsync(int id);

        public Task<Tuple<Error, DIMDevDto.CompanyRoleClaimDto>> GetAllRoleClaimsByUserIdAsync(int userId);
    }
}
