using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.BaseDto;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard
{
    public class AttachmentDto : BaseDevelopmentStandartDto
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string AttachmentDescription { get; set; }
        public string FolderName { get; set; }
        public int? TicketNoteId { get; set; }
        public int? BasicTicketId { get; set; }
        public string FirstLastName { get; set; }

        public List<IFormFile> AttachmentsFile { get; set; }
        

        public string FileType { get; set; }
        public byte[] FileData { get; set; }
        public UserDto AddedByUser { get; set; }
        public BasicTicketDto BasicTicket { get; set; }
        public UserDto LastModifiedByUser { get; set; }
        public TicketDto Ticket { get; set; }
        public TicketNoteDto TicketNote { get; set; }
    }
}
