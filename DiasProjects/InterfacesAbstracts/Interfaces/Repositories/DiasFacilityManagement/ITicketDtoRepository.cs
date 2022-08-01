using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
namespace DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement
{
    public interface ITicketDtoRepository
    {
        public Task<Tuple<ErrorCodes, IEnumerable<DevelopmentDto.TicketDto>>> GetAllAsync();
        public Task<Tuple<ErrorCodes, IEnumerable<DevelopmentDto.TicketDto>>> GetAllTicketsByBasicTicketIdAsync(int ticketId);
        public Task<Tuple<ErrorCodes, DevelopmentDto.TicketDto>> DeleteFromIntAsync(int id);
        public Task<Tuple<ErrorCodes, DevelopmentDto.TicketDto>> GetByIdFromIntAsync(int id);
        public Task<Tuple<ErrorCodes, DevelopmentDto.TicketDto>> InsertAsync(DevelopmentDto.TicketDto insertedDto);
        public Task<Tuple<ErrorCodes, DevelopmentDto.TicketDto>> UpdateAsync(DevelopmentDto.TicketDto updatedDto);
        public Task<Tuple<ErrorCodes, DevelopmentDto.TicketDto>> UpdateTicketStateAsync(DevelopmentDto.TicketDto resource);
    }
}
