using DiasShared.Errors;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;

namespace DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement
{
    public interface IApiActionDescriptionDtoRepository
    {
        public Task<Tuple<Error, IEnumerable<DevelopmentDto.ApiActionDescriptionDto>>> GetAllAsync();
        public Task<Tuple<Error, DevelopmentDto.ApiActionDescriptionDto>> DeleteFromIntAsync(int id);
        public Task<Tuple<Error, DevelopmentDto.ApiActionDescriptionDto>> GetByIdFromIntAsync(int id);
        public Task<Tuple<Error, DevelopmentDto.ApiActionDescriptionDto>> InsertAsync(DevelopmentDto.ApiActionDescriptionDto insertedDto);
        public Task<Tuple<Error, DevelopmentDto.ApiActionDescriptionDto>> UpdateAsync(DevelopmentDto.ApiActionDescriptionDto updatedDto);

    }
}
