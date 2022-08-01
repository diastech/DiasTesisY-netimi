using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagementTemplate.SqlServer.Development.BaseModel;
using DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFMTemplate.SqlServer.Development.Models;
using System.Collections.Generic;

#nullable disable

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagementTemplate.SqlServer.Development.Models
{
    public partial class AssignmentGroup : DevelopmentBaseEntity
    {
        public AssignmentGroup()
        {
            AssignmentGroupAuthorizedLocations = new HashSet<AssignmentGroupAuthorizedLocation>();
            AssignmentGroupEmployees = new HashSet<AssignmentGroupEmployee>();
            TicketAuditHistoryNextAssignedAssignmentGroups = new HashSet<TicketAuditHistory>();
            TicketAuditHistoryPreviousAssignedAssignmentGroups = new HashSet<TicketAuditHistory>();
            Tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public string GroupName { get; set; }
        public int GroupManagerUserId { get; set; }
        public int TicketReasonId { get; set; }

        public virtual User AddedByUser { get; set; }
        public virtual User GroupManagerUser { get; set; }
        public virtual User LastModifiedByUser { get; set; }
        public virtual TicketReason TicketReason { get; set; }
        public virtual ICollection<AssignmentGroupAuthorizedLocation> AssignmentGroupAuthorizedLocations { get; set; }
        public virtual ICollection<AssignmentGroupEmployee> AssignmentGroupEmployees { get; set; }
        public virtual ICollection<TicketAuditHistory> TicketAuditHistoryNextAssignedAssignmentGroups { get; set; }
        public virtual ICollection<TicketAuditHistory> TicketAuditHistoryPreviousAssignedAssignmentGroups { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
