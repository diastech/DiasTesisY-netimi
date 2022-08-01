using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
namespace DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement
{
    public interface ILocationDtoRepository
    {
        public Task<Tuple<ErrorCodes, IEnumerable<DevelopmentDto.LocationDto>>> GetAllAsync();
        public Task<Tuple<ErrorCodes, DevelopmentDto.LocationDto>> DeleteFromIntAsync(int id);
        public Task<Tuple<ErrorCodes, DevelopmentDto.LocationDto>> GetByIdFromIntAsync(int id);
        public Task<Tuple<ErrorCodes, DevelopmentDto.LocationDto>> InsertAsync(DevelopmentDto.LocationDto insertedDto);
        public Task<Tuple<ErrorCodes, DevelopmentDto.LocationDto>> UpdateAsync(DevelopmentDto.LocationDto updatedDto);
    }
}
