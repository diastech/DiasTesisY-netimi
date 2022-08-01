using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagementTemplate.SqlServer.Development.BaseModel;
using DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFMTemplate.SqlServer.Development.Models;
using System;
using System.Collections.Generic;

#nullable disable

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagementTemplate.SqlServer.Development.Models
{
    public partial class TicketReason : DevelopmentBaseEntity
    {
        public TicketReason()
        {
            AssignmentGroups = new HashSet<AssignmentGroup>();
            PeriodicTickets = new HashSet<PeriodicTicket>();
            ResolutionFormV2s = new HashSet<ResolutionFormV2>();
            ResolutionForms = new HashSet<ResolutionForm>();
            Tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public string ReasonName { get; set; }
        public string ReasonDescription { get; set; }
        public int ResponseTime { get; set; }
        public int ResolutionTime { get; set; }
        public int TicketReasonCategoryId { get; set; }
        public int Code { get; set; }

        public virtual User AddedByUser { get; set; }
        public virtual User LastModifiedByUser { get; set; }
        public virtual TicketReasonCategoryV2 TicketReasonCategory { get; set; }
        public virtual ICollection<AssignmentGroup> AssignmentGroups { get; set; }
        public virtual ICollection<PeriodicTicket> PeriodicTickets { get; set; }
        public virtual ICollection<ResolutionFormV2> ResolutionFormV2s { get; set; }
        public virtual ICollection<ResolutionForm> ResolutionForms { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
