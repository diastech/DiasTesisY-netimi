using System;
using System.Collections.Generic;

#nullable disable

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagementTemplate.SqlServer.Development.Models
{
    public partial class VwAssigmentGroupEmployee
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? AssignmentGroupId { get; set; }
        public int? EmployeeUserId { get; set; }
    }
}
