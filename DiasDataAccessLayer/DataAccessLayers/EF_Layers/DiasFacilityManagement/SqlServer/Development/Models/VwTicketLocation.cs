using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

#nullable disable

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models
{
    public partial class VwTicketLocation
    {
        public int TicketId { get; set; }
        public int Id { get; set; }
        public string LocationName { get; set; }
        public string LocationNumber { get; set; }
        public string LocationDescription { get; set; }
        public Geometry LatitudeLongitude { get; set; }
        public string LocationHierarchy { get; set; }
        public string HierarchicalParentId { get; set; }
    }
}
