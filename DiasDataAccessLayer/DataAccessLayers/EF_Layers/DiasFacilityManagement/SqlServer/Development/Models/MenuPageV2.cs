using System;
using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.BaseModel;
using Microsoft.EntityFrameworkCore;
using DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFM.SqlServer.Development.Models;
using System.Collections.Generic;


#nullable disable

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models
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
