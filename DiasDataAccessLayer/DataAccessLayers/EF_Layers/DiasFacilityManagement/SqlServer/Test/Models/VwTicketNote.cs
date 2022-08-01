using System;
using System.Collections.Generic;

#nullable disable

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Test.Models
{
    public partial class VwTicketNote
    {
        public int Id { get; set; }
        public string NoteText { get; set; }
        public string NotesAddedUser { get; set; }
        public DateTime? AddedTime { get; set; }
        public int TicketId { get; set; }
        public string TicketNoteAttachments { get; set; }
    }
}
