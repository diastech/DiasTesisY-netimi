using DiasShared.Errors;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DIMDevDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;

namespace DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement
{
    public interface ICompanyRoleDtoRepository
    {
        public Task<Tuple<Error, IEnumerable<DIMDevDto.CompanyRoleDto>>> GetAllAsync();
        public Task<Tuple<Error, DIMDevDto.CompanyRoleDto>> GetByIdFromIntAsync(int id);
        public Task<Tuple<Error, DIMDevDto.CompanyRoleDto>> InsertAsync(DIMDevDto.CompanyRoleDto userRoleDto);
        public Task<Tuple<Error, DIMDevDto.CompanyRoleDto>> UpdateAsync(DIMDevDto.CompanyRoleDto userRoleDto);
        public Task<Tuple<Error, DIMDevDto.CompanyRoleDto>> DeleteFromIntAsync(int id);
    }
}
