using System;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard
{
    public class ViewAssigmentGroupEmployeeDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? AssignmentGroupId { get; set; }
        public int? EmployeeUserId { get; set; }
    }
}
