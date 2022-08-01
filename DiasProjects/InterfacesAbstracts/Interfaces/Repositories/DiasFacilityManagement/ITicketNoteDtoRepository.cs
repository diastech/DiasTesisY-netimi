using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;

namespace DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement
{
    public interface ITicketNoteDtoRepository
    {
        public Task<Tuple<ErrorCodes, IEnumerable<DevelopmentDto.TicketNoteDto>>> GetAllAsync();
        public Task<Tuple<ErrorCodes, IEnumerable<DevelopmentDto.TicketNoteDto>>> GetTicketNoteByTicketIdAsync(int ticketId);
        public Task<Tuple<ErrorCodes, DevelopmentDto.TicketNoteDto>> DeleteFromIntAsync(int id);
        public Task<Tuple<ErrorCodes, DevelopmentDto.TicketNoteDto>> GetByIdFromIntAsync(int id);
        public Task<Tuple<ErrorCodes, DevelopmentDto.TicketNoteDto>> InsertAsync(DevelopmentDto.TicketNoteDto insertedDto);
        public Task<Tuple<ErrorCodes, DevelopmentDto.TicketNoteDto>> UpdateAsync(DevelopmentDto.TicketNoteDto updatedDto);

    }
}
