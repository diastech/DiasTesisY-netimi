using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
namespace DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement
{
    public interface ITicketAuditHistoryDtoRepository
    {
        public Task<Tuple<ErrorCodes, IEnumerable<DevelopmentDto.TicketAuditHistoryDto>>> GetAllTicketAuditHistoryByTicketIdAsync(int ticketId);
        public Task<Tuple<ErrorCodes, IEnumerable<DevelopmentDto.TicketAuditHistoryDto>>> GetAllAsync();
        public Task<Tuple<ErrorCodes, DevelopmentDto.TicketAuditHistoryDto>> DeleteFromIntAsync(int id);
        public Task<Tuple<ErrorCodes, DevelopmentDto.TicketAuditHistoryDto>> GetByIdFromIntAsync(int id);
        public Task<Tuple<ErrorCodes, DevelopmentDto.TicketAuditHistoryDto>> InsertAsync(DevelopmentDto.TicketAuditHistoryDto insertedDto);
        public Task<Tuple<ErrorCodes, DevelopmentDto.TicketAuditHistoryDto>> UpdateAsync(DevelopmentDto.TicketAuditHistoryDto updatedDto);

    }
}
