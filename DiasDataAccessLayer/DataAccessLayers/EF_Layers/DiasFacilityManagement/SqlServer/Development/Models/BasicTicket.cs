using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.BaseModel;
using System;
using System.Collections.Generic;
using DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFM.SqlServer.Development.Models;


#nullable disable

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models
{
    public partial class BasicTicket : DevelopmentBaseEntity
    {
        public BasicTicket()
        {
            Attachments = new HashSet<Attachment>();
            Tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public string TicketDescription { get; set; }
        public int? MobilePhoneNumber { get; set; }
        public int StateId { get; set; }

        public virtual User AddedByUser { get; set; }
        public virtual User LastModifiedByUser { get; set; }
        public virtual ICollection<Attachment> Attachments { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual BasicTicketState StateOfBasicTicket { get; set; }

    }
}
