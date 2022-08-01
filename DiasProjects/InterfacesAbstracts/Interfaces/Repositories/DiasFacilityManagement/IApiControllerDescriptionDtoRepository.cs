using DiasShared.Errors;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;

namespace DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement
{
    public interface IApiControllerDescriptionDtoRepository
    {
        public Task<Tuple<Error, IEnumerable<DevelopmentDto.ApiControllerDescriptionDto>>> GetAllAsync();
        public Task<Tuple<Error, DevelopmentDto.ApiControllerDescriptionDto>> DeleteFromIntAsync(int id);
        public Task<Tuple<Error, DevelopmentDto.ApiControllerDescriptionDto>> GetByIdFromIntAsync(int id);
        public Task<Tuple<Error, DevelopmentDto.ApiControllerDescriptionDto>> InsertAsync(DevelopmentDto.ApiControllerDescriptionDto insertedDto);
        public Task<Tuple<Error, DevelopmentDto.ApiControllerDescriptionDto>> UpdateAsync(DevelopmentDto.ApiControllerDescriptionDto updatedDto);

    }
}
