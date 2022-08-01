using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagementTemplate.SqlServer.Development.BaseModel;
using DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFMTemplate.SqlServer.Development.Models;

#nullable disable

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagementTemplate.SqlServer.Development.Models
{
    public partial class MenuPage : DevelopmentBaseEntity
    {
        public string Id { get; set; }
        public int HierarchicalOrder { get; set; }
        public int HierarchicalLevel { get; set; }
        public string ParentId { get; set; }
        public string MenuText { get; set; }
        public string UrlPath { get; set; }
        public string MenuIcon { get; set; }
        public bool ExpandOnStart { get; set; }
        public string MenuImagePath { get; set; }

        public virtual User AddedByUser { get; set; }
        public virtual User LastModifiedByUser { get; set; }
    }
}
