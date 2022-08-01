using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.BaseDto;
using System;
using System.Collections.Generic;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard
{
    public class TicketRelatedLocationDto : BaseDevelopmentStandartDto
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public int TicketLocationId { get; set; }

        public UserDto AddedByUser { get; set; }
        public UserDto LastModifiedByUser { get; set; }
        public TicketDto Ticket { get; set; }
        public LocationV2Dto TicketLocation { get; set; }
        public List<PeriodicTicketDto> PeriodicTickets { get; set; }
    }
}
