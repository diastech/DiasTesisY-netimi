using System;
using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Test.BaseModel;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Test.Models
{
    public partial class MenuPageV2 : DevelopmentBaseEntity
    {
        public int Id { get; set; }
        public HierarchyId HierarchyId { get; set; }
        public HierarchyId OldHierarchyId { get; set; }
        public string MenuText { get; set; }
        public string UrlPath { get; set; }
        public string MenuIcon { get; set; }
        public bool ExpandOnStart { get; set; }
        public string MenuImagePath { get; set; }

        public virtual User AddedByUser { get; set; }
        public virtual User LastModifiedByUser { get; set; }
    }
}
