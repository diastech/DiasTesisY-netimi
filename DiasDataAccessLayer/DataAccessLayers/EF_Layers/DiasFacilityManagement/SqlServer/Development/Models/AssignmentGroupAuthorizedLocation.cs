using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.BaseModel;
using System;
using System.Collections.Generic;
using DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFM.SqlServer.Development.Models;


#nullable disable

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models
{
    public partial class AssignmentGroupAuthorizedLocation : DevelopmentBaseEntity
    {
        public int Id { get; set; }
        public int AssignmentGroupId { get; set; }
        public int LocationId { get; set; }

        public virtual User AddedByUser { get; set; }
        public virtual AssignmentGroup AssignmentGroup { get; set; }
        public virtual User LastModifiedByUser { get; set; }
        public virtual LocationV2 Location { get; set; }
    }
}
