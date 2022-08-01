using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.BaseModel;
using System.Collections.Generic;
using DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFM.SqlServer.Development.Models;


#nullable disable

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models
{
    public partial class TicketState : DevelopmentBaseEntity
    {
        public TicketState()
        {
            ResolutionFormV2s = new HashSet<ResolutionFormV2>();
            ResolutionForms = new HashSet<ResolutionForm>();
            TicketAuditHistoryNextTicketStates = new HashSet<TicketAuditHistory>();
            TicketAuditHistoryPreviousTicketStates = new HashSet<TicketAuditHistory>();
            TicketStateTransitionDestinationTicketStates = new HashSet<TicketStateTransition>();
            TicketStateTransitionSourceTicketStates = new HashSet<TicketStateTransition>();
            Tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }

        public virtual User AddedByUser { get; set; }
        public virtual User LastModifiedByUser { get; set; }
        public virtual ICollection<ResolutionFormV2> ResolutionFormV2s { get; set; }
        public virtual ICollection<ResolutionForm> ResolutionForms { get; set; }
        public virtual ICollection<TicketAuditHistory> TicketAuditHistoryNextTicketStates { get; set; }
        public virtual ICollection<TicketAuditHistory> TicketAuditHistoryPreviousTicketStates { get; set; }
        public virtual ICollection<TicketStateTransition> TicketStateTransitionDestinationTicketStates { get; set; }
        public virtual ICollection<TicketStateTransition> TicketStateTransitionSourceTicketStates { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
