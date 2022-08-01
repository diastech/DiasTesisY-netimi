using System;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard
{
    public class ViewTicketNoteDto
    {
        public int Id { get; set; }
        public string NoteText { get; set; }
        public string NotesAddedUser { get; set; }
        public DateTime? AddedTime { get; set; }
        public int TicketId { get; set; }
        public string TicketNoteAttachments { get; set; }
    }
}
