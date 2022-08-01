using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Test.BaseModel;
using System;
using System.Collections.Generic;

#nullable disable

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Test.Models
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
