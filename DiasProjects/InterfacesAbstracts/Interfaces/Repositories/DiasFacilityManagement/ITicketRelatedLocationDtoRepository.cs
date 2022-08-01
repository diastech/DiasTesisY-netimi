using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
namespace DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement
{
    public interface ITicketRelatedLocationDtoRepository
    {
        public Task<Tuple<ErrorCodes, IEnumerable<DevelopmentDto.TicketRelatedLocationDto>>> GetAllAsync();
        public Task<Tuple<ErrorCodes, DevelopmentDto.TicketRelatedLocationDto>> DeleteFromIntAsync(int id);
        public Task<Tuple<ErrorCodes, DevelopmentDto.TicketRelatedLocationDto>> GetByIdFromIntAsync(int id);
        public Task<Tuple<ErrorCodes, DevelopmentDto.TicketRelatedLocationDto>> InsertAsync(DevelopmentDto.TicketRelatedLocationDto insertedDto);
        public Task<Tuple<ErrorCodes, DevelopmentDto.TicketRelatedLocationDto>> UpdateAsync(DevelopmentDto.TicketRelatedLocationDto updatedDto);
    }
}
