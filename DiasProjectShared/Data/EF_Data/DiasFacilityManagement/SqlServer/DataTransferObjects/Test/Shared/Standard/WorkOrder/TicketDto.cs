using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.BaseDto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard
{
    public class TicketDto : BaseDevelopmentStandartDto
    {
        #region Properties
        public int Id { get; set; }
        public int TicketReportedUserId { get; set; }
        public string TicketOwnerUserId { get; set; }
        public string TicketDescription { get; set; }
        public DateTime TicketOpenedTime { get; set; }
        public int TicketStatusId { get; set; }
        public int? PeriodicTicketId { get; set; }
        public int? TicketAssignedUserId { get; set; }
        public int? TickedAssignedAssignmentGroupId { get; set; }
        public int TicketReasonId { get; set; }
        public int TicketPriority { get; set; }
        public int? BasicTicketId { get; set; }

        public string FirstLastName { get; set; }


        public UserDto AddedByUser { get; set; }
        public BasicTicketDto BasicTicket { get; set; }
        public UserDto LastModifiedByUser { get; set; }
        public AssignmentGroupDto TickedAssignedAssignmentGroup { get; set; }
        public UserDto TicketAssignedUser { get; set; }
        public TicketReasonDto TicketReason { get; set; }
        public TicketStateDto TicketStatus { get; set; }
        public List<AttachmentDto> Attachments { get; set; }
        public List<ResolutionFormQuestionAnswerDto> ResolutionFormQuestionAnswers { get; set; }
        public List<TicketAuditHistoryDto> TicketAuditHistories { get; set; }
        public List<TicketNoteDto> TicketNotes { get; set; }
        public List<TicketRelatedLocationDto> TicketRelatedLocations { get; set; }
        #endregion


    }
}
