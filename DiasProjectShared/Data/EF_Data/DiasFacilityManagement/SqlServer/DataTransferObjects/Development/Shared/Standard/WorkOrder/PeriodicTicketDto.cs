using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.BaseDto;
using System;
using System.Collections.Generic;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard
{
    public class PeriodicTicketDto : BaseDevelopmentStandartDto
    {
        public int Id { get; set; }
        public string PeriodicName { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public DateTime FirstCareTime { get; set; }
        public int TicketPriorityId { get; set; }
        public int? LocationId { get; set; }
        public string Description { get; set; }
        public int TicketReasonId { get; set; }
        public string PeriodFrequency { get; set; }
        public int TickedAssignedAssignmentGroupId { get; set; }
        public bool ActiveStatus { get; set; }

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
