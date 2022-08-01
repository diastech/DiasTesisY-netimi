using DiasShared.Errors;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;


namespace DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Standart
{
    public interface ITicketRelatedLocationBusinessRules
    {
        public Task<Tuple<Error, List<DevelopmentDto.TicketRelatedLocationDto>>> GetByTicketId(int ticketId);
        public Task<Tuple<Error, DevelopmentDto.TicketRelatedLocationDto>> AddAsync(DevelopmentDto.TicketRelatedLocationDto ticketRelatedLocationDto);
        public Task<Tuple<Error, DevelopmentDto.TicketRelatedLocationDto>> DeleteAsync(DevelopmentDto.TicketRelatedLocationDto ticketRelatedLocationDto);
    }
}
