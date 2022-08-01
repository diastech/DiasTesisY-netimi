using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.BaseDto;
using System;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard
{
    public class TicketAuditHistoryDto : BaseDevelopmentStandartDto
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string HistoryType { get; set; }
        public DateTime HistoryAddTime { get; set; }
        public int? PreviousTicketStateId { get; set; }
        public int? NextTicketStateId { get; set; }
        public DateTime? ActivityStartTime { get; set; }
        public DateTime? ActivityEndTime { get; set; }
        public int? LocationId { get; set; }
        public int? PreviousTicketAssignedUserId { get; set; }
        public int? NextTicketAssignedUserId { get; set; }
        public int? PreviousAssignedAssignmentGroupId { get; set; }
        public int? NextAssignedAssignmentGroupId { get; set; }
    }
}
