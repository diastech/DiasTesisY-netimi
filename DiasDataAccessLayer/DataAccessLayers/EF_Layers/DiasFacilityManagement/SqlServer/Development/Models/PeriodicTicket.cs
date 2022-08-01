using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.BaseModel;
using System;
using DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFM.SqlServer.Development.Models;
using System.Collections.Generic;


#nullable disable

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models
{


    public partial class PeriodicTicket : DevelopmentBaseEntity
    {

        public PeriodicTicket()
        {
            PeriodicTickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public string PeriodicName { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public int TicketPriorityId { get; set; }
        public int? LocationId { get; set; }
        public string PeriodFrequency { get; set; }
        public int TicketReasonId { get; set; }

        public virtual User AddedByUser { get; set; }
        public virtual User LastModifiedByUser { get; set; }
        public virtual TicketRelatedLocation Location { get; set; }
        public virtual TicketReason TicketReason { get; set; }
        public virtual TicketPriority TicketPriority { get; set; }
        public virtual ICollection<Ticket> PeriodicTickets { get; set; }
    }
}
