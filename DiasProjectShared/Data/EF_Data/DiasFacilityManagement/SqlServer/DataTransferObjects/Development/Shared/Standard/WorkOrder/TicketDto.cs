using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.BaseDto;
using System;
using System.Collections.Generic;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using static DiasShared.Enums.Standart.TicketEnums;
using DiasShared.Operations.EnumOperations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard
{
    public class TicketDto : BaseDevelopmentStandartDto
    {
        #region Properties
        public int Id { get; set; }
        public int TicketReportedUserId { get; set; }
        public int TicketOwnerUserId { get; set; }
        public string TicketDescription { get; set; }
        public DateTime TicketOpenedTime { get; set; }
        public int TicketStatusId { get; set; }
        public int? PeriodicTicketId { get; set; }
        public int? TicketAssignedUserId { get; set; }
        public int? TickedAssignedAssignmentGroupId { get; set; }
        public int TicketReasonId { get; set; }
        public int TicketPriorityId { get; set; }
        public int? BasicTicketId { get; set; }


        [NotMapped]
        private int? _locationCodeId { get; set; }

        public int? LocationCodeId
        {
            get
            {
                return _locationCodeId;
            }
            set
            {
                int v2 = value ?? 0;

                string codeStr = v2.GetEnumValue<LocationCodeEnum>().GetDisplayOrValueFromEnum<LocationCodeEnum>();
                LocationNameGetByCodeId = codeStr;
                _locationCodeId = value;
            }
        }

        public string TicketCode { get; set; }
        public string LocationNameGetByCodeId { get; set; }

        public string FirstLastName { get; set; }

        public string PhoneNumber { get; set; }

        public string PhoneNumberKayitAltinaAlan { get; set; }

        public DateTime ExpectedResolutionTime { get; set; }
        public DateTime ExpectedResponseTime { get; set; }

        public DateTime? UserResponseTime { get; set; }
        public DateTime? UserResolutionTime { get; set; }
        public DateTime? SlaStopTime { get; set; }

        public UserDto AddedByUser { get; set; }
        public BasicTicketDto BasicTicket { get; set; }
        public TicketPriorityDto TicketPriority { get; set; }
        public UserDto LastModifiedByUser { get; set; }
        public UserDto TicketReportedUsers { get; set; }
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
