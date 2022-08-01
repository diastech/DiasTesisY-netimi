using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.BaseDto;
using System.Collections.Generic;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard
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
