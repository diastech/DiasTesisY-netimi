using System;
using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Test.BaseModel;
using NetTopologySuite.Geometries;

#nullable disable

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Test.Models
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
