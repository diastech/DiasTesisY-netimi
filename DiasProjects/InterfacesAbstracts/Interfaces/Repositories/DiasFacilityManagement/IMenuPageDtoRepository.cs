using DiasShared.Errors;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;

namespace DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement
{
    public interface IMenuPageDtoRepository
    {
        public Task<Tuple<Error, IEnumerable<DevelopmentDto.MenuPageDto>>> GetAllAsync();
        public Task<Tuple<Error, DevelopmentDto.MenuPageDto>> DeleteFromIntAsync(int id);
        public Task<Tuple<Error, DevelopmentDto.MenuPageDto>> GetByIdFromIntAsync(int id);
        public Task<Tuple<Error, DevelopmentDto.MenuPageDto>> InsertAsync(DevelopmentDto.MenuPageDto insertedDto);
        public Task<Tuple<Error, DevelopmentDto.MenuPageDto>> UpdateAsync(DevelopmentDto.MenuPageDto updatedDto);
    }
}
