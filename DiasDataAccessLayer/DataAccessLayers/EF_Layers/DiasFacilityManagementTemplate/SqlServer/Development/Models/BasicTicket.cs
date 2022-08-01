using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagementTemplate.SqlServer.Development.BaseModel;
using DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFMTemplate.SqlServer.Development.Models;
using System.Collections.Generic;

#nullable disable

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagementTemplate.SqlServer.Development.Models
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
