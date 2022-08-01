using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.BaseModel;
using DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFM.SqlServer.Development.Models;
using System.Collections.Generic;


#nullable disable

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models
{
    public partial class TicketPriority : DevelopmentBaseEntity
    {
        public TicketPriority()
        {
            TicketAddedByUsers = new HashSet<Ticket>();
            PeriodicTickets = new HashSet<PeriodicTicket>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }

        public virtual User AddedByUser { get; set; }
        
        public virtual User LastModifiedByUser { get; set; }

        public virtual ICollection<Ticket> TicketAddedByUsers { get; set; }
        public virtual ICollection<PeriodicTicket> PeriodicTickets { get; set; }
    }
}

