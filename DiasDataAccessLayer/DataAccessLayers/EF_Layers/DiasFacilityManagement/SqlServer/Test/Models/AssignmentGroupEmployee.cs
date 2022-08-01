using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Test.BaseModel;
using System;

#nullable disable

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Test.Models
{
    public partial class AssignmentGroupEmployee : DevelopmentBaseEntity
    {
        public int Id { get; set; }
        public int AssignmentGroupId { get; set; }
        public int EmployeeUserId { get; set; }

        public virtual User AddedByUser { get; set; }
        public virtual AssignmentGroup AssignmentGroup { get; set; }
        public virtual User EmployeeUser { get; set; }
        public virtual User LastModifiedByUser { get; set; }
    }
}
