using DiasShared.Errors;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
namespace DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement
{
    public interface IUserMenuPageDtoRepository
    {
        public Task<Tuple<Error, IEnumerable<DevelopmentDto.UserMenuPageDto>>> GetAllAsync();
        public Task<Tuple<Error, DevelopmentDto.UserMenuPageDto>> DeleteFromIntAsync(int id);
        public Task<Tuple<Error, DevelopmentDto.UserMenuPageDto>> GetByIdFromIntAsync(int id);
        public Task<Tuple<Error, DevelopmentDto.UserMenuPageDto>> InsertAsync(DevelopmentDto.UserMenuPageDto insertedDto);
        public Task<Tuple<Error, DevelopmentDto.UserMenuPageDto>> UpdateAsync(DevelopmentDto.UserMenuPageDto updatedDto);
    }
}
