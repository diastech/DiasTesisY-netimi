using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.BaseDto;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using DiasShared.InterfacesAbstracts.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.BaseDto;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom
{
    public class CustomTicketDto : TicketDto, IBaseDevelopmentCustomDto
    {
        public string NoteText { get; set; }
        public string TicketReasonHierarchyId { get; set; }

        public List<string> TicketRelatedLocationHierarchyId { get; set; }
        
        //Mobilden gelen locationları almak için eklendi.
        public List<int> LocationList { get; set; }

        public List<IFormFile> AttachmentsFile { get; set; }
        public IFormFile NotesFile { get; set; }

        public List<AttachmentDto> NotesAttachment { get; set; }

        public string AttachmentId { get; set; }
        public int TicketPriorityGridId { get; set; }
        public string TicketPriorityGridName { get; set; }
        public string DateTimeResolutionTimeString { get; set; }
        public string DateTimeResponseTimeString { get; set; }
        public int NotesCount { get; set; }
        public int AttachemntsCount { get; set; }
        public int RelatedLocationsCount { get; set; }       
    }
}
