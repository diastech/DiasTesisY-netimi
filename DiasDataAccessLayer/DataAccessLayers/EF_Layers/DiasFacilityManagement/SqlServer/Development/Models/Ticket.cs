using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.BaseModel;
using System;
using System.Collections.Generic;
using DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFM.SqlServer.Development.Models;


#nullable disable

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models
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
        public string TicketReportedUserNameSurname { get; set; }
        public string TicketReportedUserPhone { get; set; }
        public int TicketOwnerUserId { get; set; }
        public string TicketDescription { get; set; }
        public DateTime TicketOpenedTime { get; set; }
        public int TicketStatusId { get; set; }
        public int? PeriodicTicketId { get; set; }
        public int? TicketAssignedUserId { get; set; }
        public int? TickedAssignedAssignmentGroupId { get; set; }
        public int TicketReasonId { get; set; }
        public int TicketPriorityId { get; set; } 
        public int? BasicTicketId { get; set; }
        public int? FacilityId { get; set; } = 1;
        public DateTime ExpectedResponseTime { get; set; } = DateTime.Now;
        public DateTime ExpectedResolutionTime { get; set; } = DateTime.Now;
        public string TicketCode { get; set; }
        public DateTime? UserResponseTime { get; set; }
        public DateTime? UserResolutionTime { get; set; }
        public DateTime? SlaStopTime { get; set; }


        public virtual User AddedByUser { get; set; }
        public virtual BasicTicket BasicTicket { get; set; }
        public virtual User LastModifiedByUser { get; set; }
        public virtual AssignmentGroup TickedAssignedAssignmentGroup { get; set; }
        public virtual User TicketAssignedUser { get; set; }
        //public virtual User TicketReportedUsers { get; set; }
        public virtual User TicketOwnerUsers { get; set; }
        public virtual PeriodicTicket PeriodicTickets { get; set; }
        public virtual TicketReason TicketReason { get; set; }
        public virtual TicketState TicketStatus { get; set; }
        public virtual TicketPriority TicketPriority { get; set; }
        public virtual Facility FacilityByUser { get; set; }

        public virtual ICollection<Attachment> Attachments { get; set; }
        public virtual ICollection<ResolutionFormQuestionAnswer> ResolutionFormQuestionAnswers { get; set; }
        public virtual ICollection<TicketAuditHistory> TicketAuditHistories { get; set; }
        public virtual ICollection<TicketNote> TicketNotes { get; set; }
        public virtual ICollection<TicketRelatedLocation> TicketRelatedLocations { get; set; }       
    }
}
