using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.BaseDto;
using System.Collections.Generic;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard
{
    public class AssignmentGroupDto : BaseDevelopmentStandartDto
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public int GroupManagerUserId { get; set; }
        public int TicketReasonId { get; set; }
        

        public UserDto AddedByUser { get; set; }
        public UserDto GroupManagerUser { get; set; }
        public UserDto LastModifiedByUser { get; set; }
        public TicketReasonDto TicketReason { get; set; }
        public List<AssignmentGroupAuthorizedLocationDto> AssignmentGroupAuthorizedLocations { get; set; }
        public virtual List<AssignmentGroupEmployeeDto> AssignmentGroupEmployees { get; set; }
        public virtual List<TicketAuditHistoryDto> TicketAuditHistoryNextAssignedAssignmentGroups { get; set; }
        public virtual List<TicketAuditHistoryDto> TicketAuditHistoryPreviousAssignedAssignmentGroups { get; set; }
        public virtual List<TicketDto> Tickets { get; set; }
    }
}
