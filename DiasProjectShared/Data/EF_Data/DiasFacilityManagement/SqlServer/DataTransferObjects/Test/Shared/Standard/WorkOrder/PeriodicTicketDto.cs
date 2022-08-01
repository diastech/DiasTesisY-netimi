using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.BaseDto;
using System;
using System.Collections.Generic;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard
{
    public class PeriodicTicketDto : BaseDevelopmentStandartDto
    {
        public int Id { get; set; }
        public string PeriodicName { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public int TicketPriority { get; set; }
        public int? LocationId { get; set; }
        public string Description { get; set; }
        public int TicketReasonId { get; set; }
        public string PeriodFrequency { get; set; }

        public UserDto AddedByUser { get; set; }
        public BasicTicketDto BasicTicket { get; set; }
        public UserDto LastModifiedByUser { get; set; }
        public AssignmentGroupDto TickedAssignedAssignmentGroup { get; set; }
        public UserDto TicketAssignedUser { get; set; }
        public TicketReasonDto TicketReason { get; set; }
        public TicketStateDto TicketStatus { get; set; }
        public List<TicketRelatedLocationDto> TicketRelatedLocations { get; set; }
    }
}
