using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.BaseDto;
using System.Collections.Generic;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard
{
    public class TicketNoteDto : BaseDevelopmentStandartDto
    {
        public int Id { get; set; }
        public string NoteText { get; set; }
        public int TicketId { get; set; }
        public List<AttachmentDto> Attachments { get; set; }
    }
}
