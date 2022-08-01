using DiasShared.Errors;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DIMDevDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;

namespace DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement
{
    public interface ICompanyRoleUserDtoRepository
    {
        public Task<Tuple<Error, DIMDevDto.CompanyRoleUserDto>> GetByIdFromIntAsync(int id);
        public Task<Tuple<Error, IEnumerable<DIMDevDto.CompanyRoleUserDto>>> GetAllAsync();
        public Task<Tuple<Error, DIMDevDto.CompanyRoleUserDto>> InsertAsync(DIMDevDto.CompanyRoleUserDto roleDto);
        public Task<Tuple<Error, DIMDevDto.CompanyRoleUserDto>> UpdateAsync(DIMDevDto.CompanyRoleUserDto roleDto);
        public Task<Tuple<Error, DIMDevDto.CompanyRoleUserDto>> DeleteFromIntAsync(int id);

        public Task<Tuple<Error, DIMDevDto.CompanyRoleUserDto>> GetAllCompanyRolesByUserIdAsync(int userId);

    }
}
