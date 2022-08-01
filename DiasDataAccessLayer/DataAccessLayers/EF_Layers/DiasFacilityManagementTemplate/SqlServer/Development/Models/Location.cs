using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagementTemplate.SqlServer.Development.BaseModel;
using DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFMTemplate.SqlServer.Development.Models;
using NetTopologySuite.Geometries;

#nullable disable

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagementTemplate.SqlServer.Development.Models
{
    public partial class Location : DevelopmentBaseEntity
    {
        public int Id { get; set; }
        public string LocationName { get; set; }
        public string LocationNumber { get; set; }
        public string LocationDescription { get; set; }
        public Geometry LatitudeLongitude { get; set; }
        public string LocationHierarchy { get; set; }
        public string HierarchicalParentId { get; set; }

        public virtual User AddedByUser { get; set; }
        public virtual User LastModifiedByUser { get; set; }
    }
}
