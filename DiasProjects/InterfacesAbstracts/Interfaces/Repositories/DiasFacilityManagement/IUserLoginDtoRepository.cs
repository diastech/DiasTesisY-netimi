using DiasShared.Errors;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;

namespace DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement
{
    public interface IUserLoginDtoRepository
    {
        public Task<Tuple<Error, IEnumerable<DevelopmentDto.UserLoginDto>>> GetAllAsync();
        public Task<Tuple<Error, DevelopmentDto.UserLoginDto>> DeleteFromIntAsync(int id);
        public Task<Tuple<Error, DevelopmentDto.UserLoginDto>> GetByIdFromIntAsync(int id);
        public Task<Tuple<Error, DevelopmentDto.UserLoginDto>> InsertAsync(DevelopmentDto.UserLoginDto insertedDto);
        public Task<Tuple<Error, DevelopmentDto.UserLoginDto>> UpdateAsync(DevelopmentDto.UserLoginDto updatedDto);

        public Task<Tuple<Error, DevelopmentDto.UserLoginDto>> GetAllUserLoginsByUserIdAsync(int userId);

    }
}
