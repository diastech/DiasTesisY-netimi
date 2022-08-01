using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.BaseDto;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard
{
    public class AssignmentGroupEmployeeDto : BaseDevelopmentStandartDto
    {
        public int Id { get; set; }
        public int AssignmentGroupId { get; set; }
        public int EmployeeUserId { get; set; }
    }
}
