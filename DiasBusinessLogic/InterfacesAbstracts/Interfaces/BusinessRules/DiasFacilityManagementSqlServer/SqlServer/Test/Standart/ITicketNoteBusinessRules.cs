using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using StandartDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard;


namespace DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Test.Standart
{
    public interface ITicketNoteBusinessRules
    {
        public Task<Tuple<ErrorCodes, StandartDto.TicketNoteDto>> AddAsync(StandartDto.TicketNoteDto ticketNoteDto);
        public Task<Tuple<ErrorCodes, List<StandartDto.TicketNoteDto>>> UpdateAsync(List<StandartDto.TicketNoteDto> ticketNoteDto);
        public Task<Tuple<ErrorCodes, List<StandartDto.TicketNoteDto>>> DeleteAsync(List<StandartDto.TicketNoteDto> ticketNoteDto);
    }
}
