using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Test.BaseModel;
using System;
using System.Collections.Generic;

#nullable disable

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Test.Models
{
    public partial class Ticket : DevelopmentBaseEntity
    {
        public Ticket()
        {
            Attachments = new HashSet<Attachment>();
            ResolutionFormQuestionAnswers = new HashSet<ResolutionFormQuestionAnswer>();
            TicketAuditHistories = new HashSet<TicketAuditHistory>();
            TicketNotes = new HashSet<TicketNote>();
            TicketRelatedLocations = new HashSet<TicketRelatedLocation>();
        }

        public int Id { get; set; }
        public int TicketReportedUserId { get; set; }
        public string TicketOwnerUserId { get; set; }
        public string TicketDescription { get; set; }
        public DateTime TicketOpenedTime { get; set; }
        public int TicketStatusId { get; set; }
        public int? PeriodicTicketId { get; set; }
        public int? TicketAssignedUserId { get; set; }
        public int? TickedAssignedAssignmentGroupId { get; set; }
        public int TicketReasonId { get; set; }
        public int TicketPriority { get; set; }
        public int? BasicTicketId { get; set; }

        public virtual User AddedByUser { get; set; }
        public virtual BasicTicket BasicTicket { get; set; }
        public virtual User LastModifiedByUser { get; set; }
        public virtual AssignmentGroup TickedAssignedAssignmentGroup { get; set; }
        public virtual User TicketAssignedUser { get; set; }
        public virtual TicketReason TicketReason { get; set; }
        public virtual TicketState TicketStatus { get; set; }
        public virtual ICollection<Attachment> Attachments { get; set; }
        public virtual ICollection<ResolutionFormQuestionAnswer> ResolutionFormQuestionAnswers { get; set; }
        public virtual ICollection<TicketAuditHistory> TicketAuditHistories { get; set; }
        public virtual ICollection<TicketNote> TicketNotes { get; set; }
        public virtual ICollection<TicketRelatedLocation> TicketRelatedLocations { get; set; }
    }
}
