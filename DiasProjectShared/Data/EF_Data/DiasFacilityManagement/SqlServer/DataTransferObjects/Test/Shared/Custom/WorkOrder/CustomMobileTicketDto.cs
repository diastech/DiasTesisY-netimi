using DiasShared.Classes.Dto;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.BaseDto;
using DiasShared.InterfacesAbstracts.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.BaseDto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Custom
{
    public class CustomMobileTicketDto : BaseDevelopmentStandartDto, IBaseDevelopmentCustomDto
    {
        public int? TicketReportedUserId { get; set; }

        public int? TicketOwnerUserId { get; set; }

        public string TicketDescription { get; set; }

        public DateTime? TicketOpenedTime { get; set; }

        public int? TicketStatusId { get; set; }

        public int? TicketAssignedUserId { get; set; }

        public int? TickedAssignedAssignmentGroupId { get; set; }

        public int? TicketReasonId { get; set; }

        public List<int> LocationList { get; set; }

        public int? TicketPriority { get; set; }

        public int? BasicTicketId { get; set; }

        public int? PeriodicTicketId { get; set; }
        public List<FileMetaDataDto> AttachmentsFile { get; set; }
    }    
}
