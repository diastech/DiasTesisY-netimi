using DiasShared.Errors;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
namespace DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement
{
    public interface IMenuPageV2DtoRepository
    {
        public Task<Tuple<Error, IEnumerable<DevelopmentDto.MenuPageV2Dto>>> GetAllAsync();
        public Task<Tuple<Error, DevelopmentDto.MenuPageV2Dto>> DeleteFromIntAsync(int id);
        public Task<Tuple<Error, DevelopmentDto.MenuPageV2Dto>> GetByIdFromIntAsync(int id);
        public Task<Tuple<Error, DevelopmentDto.MenuPageV2Dto>> InsertAsync(DevelopmentDto.MenuPageV2Dto insertedDto);
        public Task<Tuple<Error, DevelopmentDto.MenuPageV2Dto>> UpdateAsync(DevelopmentDto.MenuPageV2Dto updatedDto);
    }
}
