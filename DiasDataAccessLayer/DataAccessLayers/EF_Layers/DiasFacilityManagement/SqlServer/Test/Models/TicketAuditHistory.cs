using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Test.BaseModel;
using System;

#nullable disable

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Test.Models
{
    public partial class TicketAuditHistory : DevelopmentBaseEntity
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

        public virtual User AddedByUser { get; set; }
        public virtual User LastModifiedByUser { get; set; }
        public virtual LocationV2 Location { get; set; }
        public virtual AssignmentGroup NextAssignedAssignmentGroup { get; set; }
        public virtual User NextTicketAssignedUser { get; set; }
        public virtual TicketState NextTicketState { get; set; }
        public virtual AssignmentGroup PreviousAssignedAssignmentGroup { get; set; }
        public virtual User PreviousTicketAssignedUser { get; set; }
        public virtual TicketState PreviousTicketState { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}
