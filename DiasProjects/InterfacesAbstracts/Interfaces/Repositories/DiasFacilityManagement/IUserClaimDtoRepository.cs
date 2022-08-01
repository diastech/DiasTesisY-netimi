using DiasShared.Errors;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;

namespace DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement
{
    public interface IUserClaimDtoRepository
    {
        public Task<Tuple<Error, DevelopmentDto.UserClaimDto>> GetByIdFromIntAsync(int id);
        public Task<Tuple<Error, IEnumerable<DevelopmentDto.UserClaimDto>>> GetAllAsync();
        public Task<Tuple<Error, DevelopmentDto.UserClaimDto>> InsertAsync(DevelopmentDto.UserClaimDto userClaimDto);
        public Task<Tuple<Error, DevelopmentDto.UserClaimDto>> UpdateAsync(DevelopmentDto.UserClaimDto userClaimDto);
        public Task<Tuple<Error, DevelopmentDto.UserClaimDto>> DeleteFromIntAsync(int id);

        public Task<Tuple<Error, DevelopmentDto.UserClaimDto>> GetAllUserClaimsByUserIdAsync(int userId);

    }
}
