using DiasShared.Errors;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using StandartDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;


namespace DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Standart
{
    public interface ITicketNoteBusinessRules
    {
        public Task<Tuple<Error, List<StandartDto.TicketNoteDto>>> GetNotesByTicketId(int ticketId);
        public Task<Tuple<Error, StandartDto.TicketNoteDto>> AddAsync(StandartDto.TicketNoteDto ticketNoteDto);
        public Task<Tuple<Error, List<StandartDto.TicketNoteDto>>> UpdateAsync(List<StandartDto.TicketNoteDto> ticketNoteDto);
        public Task<Tuple<Error, List<StandartDto.TicketNoteDto>>> DeleteAsync(List<StandartDto.TicketNoteDto> ticketNoteDto);
        public Task<Tuple<Error, StandartDto.TicketNoteDto>> DeleteSingleAsync(StandartDto.TicketNoteDto ticketNoteDto);
        public Task<Tuple<Error, int>> GetNotesCountByTicketId(int ticketId);
    }
}
