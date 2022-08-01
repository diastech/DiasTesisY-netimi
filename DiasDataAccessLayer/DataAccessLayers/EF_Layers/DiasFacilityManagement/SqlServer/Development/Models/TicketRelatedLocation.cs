using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.BaseModel;
using System;
using System.Collections.Generic;
using DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFM.SqlServer.Development.Models;


#nullable disable

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models
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
