using System;
using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagementTemplate.SqlServer.Development.BaseModel;
using DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFMTemplate.SqlServer.Development.Models;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagementTemplate.SqlServer.Development.Models
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
