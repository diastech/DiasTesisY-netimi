using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Test.BaseModel;
using System;
using System.Collections.Generic;

#nullable disable

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Test.Models
{
    public partial class TicketRelatedLocation : DevelopmentBaseEntity
    {
        public TicketRelatedLocation()
        {
            PeriodicTickets = new HashSet<PeriodicTicket>();
        }

        public int Id { get; set; }
        public int TicketId { get; set; }
        public int TicketLocationId { get; set; }

        public virtual User AddedByUser { get; set; }
        public virtual User LastModifiedByUser { get; set; }
        public virtual Ticket Ticket { get; set; }
        public virtual LocationV2 TicketLocation { get; set; }
        public virtual ICollection<PeriodicTicket> PeriodicTickets { get; set; }
    }
}
