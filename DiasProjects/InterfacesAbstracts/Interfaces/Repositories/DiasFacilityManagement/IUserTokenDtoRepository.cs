using DiasShared.Errors;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;

namespace DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement
{
    public interface IUserTokenDtoRepository
    {
        public Task<Tuple<Error, DevelopmentDto.UserTokenDto>> GetByIdFromIntAsync(int id);
        public Task<Tuple<Error, IEnumerable<DevelopmentDto.UserTokenDto>>> GetAllAsync();
        public Task<Tuple<Error, DevelopmentDto.UserTokenDto>> InsertAsync(DevelopmentDto.UserTokenDto userTokenDto);
        public Task<Tuple<Error, DevelopmentDto.UserTokenDto>> UpdateAsync(DevelopmentDto.UserTokenDto userTokenDto);
        public Task<Tuple<Error, DevelopmentDto.UserTokenDto>> DeleteFromIntAsync(int id);

        public Task<Tuple<Error, DevelopmentDto.UserTokenDto>> GetAllUserTokensByUserIdAsync(int userId);

    }
}
