using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagementTemplate.SqlServer.Development.BaseModel;
using DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFMTemplate.SqlServer.Development.Models;
using System;
using System.Collections.Generic;

#nullable disable

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagementTemplate.SqlServer.Development.Models
{
    public partial class TicketNote : DevelopmentBaseEntity
    {
        public TicketNote()
        {
            Attachments = new HashSet<Attachment>();
        }

        public int Id { get; set; }
        public string NoteText { get; set; }
        public int TicketId { get; set; }

        public virtual User AddedByUser { get; set; }
        public virtual User LastModifiedByUser { get; set; }
        public virtual Ticket Ticket { get; set; }
        public virtual ICollection<Attachment> Attachments { get; set; }
    }
}
