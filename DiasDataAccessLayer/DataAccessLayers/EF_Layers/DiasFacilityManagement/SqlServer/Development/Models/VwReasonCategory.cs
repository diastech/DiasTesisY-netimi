using System;
using System.Collections.Generic;

#nullable disable

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models
{
    public partial class VwReasonCategory
    {
        public int Id { get; set; }
        public string ReasonCategoryName { get; set; }
        public string ReasonCategoryDescription { get; set; }
        public string ReasonCategoryHierarchy { get; set; }
        public string ReasonCategory { get; set; }
        public string ReasonCategoryParentId { get; set; }
        public bool? IsDisabled { get; set; }
    }
}
