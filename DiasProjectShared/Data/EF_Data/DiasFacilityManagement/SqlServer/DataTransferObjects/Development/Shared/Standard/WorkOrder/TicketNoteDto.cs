using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.BaseDto;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard
{
    public class TicketNoteDto : BaseDevelopmentStandartDto
    {
        #region MyRegion
        public int Id { get; set; }
        public string NoteText { get; set; }
        public int TicketId { get; set; }
        public IFormFile NotesFile { get; set; }
        #endregion

        #region outofDataBase
        public List<AttachmentDto> Attachments { get; set; }
        public UserDto AddedByUser { get; set; }
        public string FirstLastName {get;set;}

        #endregion

    }
}
