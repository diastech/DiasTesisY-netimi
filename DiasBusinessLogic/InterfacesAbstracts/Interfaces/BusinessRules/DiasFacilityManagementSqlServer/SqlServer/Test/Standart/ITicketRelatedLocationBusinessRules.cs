using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard;


namespace DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Test.Standart
{
    public interface ITicketRelatedLocationBusinessRules
    {
        public Task<Tuple<ErrorCodes, List<DevelopmentDto.TicketRelatedLocationDto>>> GetByTicketId(int ticketId);
        public Task<Tuple<ErrorCodes, DevelopmentDto.TicketRelatedLocationDto>> AddAsync(DevelopmentDto.TicketRelatedLocationDto ticketRelatedLocationDto);
        public Task<Tuple<ErrorCodes, DevelopmentDto.TicketRelatedLocationDto>> DeleteAsync(DevelopmentDto.TicketRelatedLocationDto ticketRelatedLocationDto);
    }
}
