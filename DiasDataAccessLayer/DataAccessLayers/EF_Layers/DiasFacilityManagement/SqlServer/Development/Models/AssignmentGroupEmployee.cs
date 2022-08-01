using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.BaseModel;
using System;
using DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFM.SqlServer.Development.Models;


#nullable disable

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models
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
