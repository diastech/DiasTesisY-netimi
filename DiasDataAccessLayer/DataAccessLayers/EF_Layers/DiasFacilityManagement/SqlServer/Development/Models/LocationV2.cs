using System.Collections.Generic;
using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.BaseModel;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFM.SqlServer.Development.Models;


#nullable disable

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models
{
    [Index(nameof(HierarchyId), IsUnique = true, Name = "Index_Unique_Clustered_LocationV2_HierarchyId")]
    public partial class LocationV2 : DevelopmentBaseEntity
    {
        public LocationV2()
        {
            AssignmentGroupAuthorizedLocations = new HashSet<AssignmentGroupAuthorizedLocation>();
            TicketAuditHistories = new HashSet<TicketAuditHistory>();
            TicketRelatedLocations = new HashSet<TicketRelatedLocation>();
        }

        public int Id { get; set; }
        public HierarchyId HierarchyId { get; set; }
        public HierarchyId OldHierarchyId { get; set; }
        public string LocationName { get; set; }
        public string LocationNumber { get; set; }
        public string LocationDescription { get; set; }
        public Geometry LatitudeLongitude { get; set; }
        public string NFC_Code { get; set; }

        public virtual User AddedByUser { get; set; }
        public virtual User LastModifiedByUser { get; set; }
        public virtual ICollection<AssignmentGroupAuthorizedLocation> AssignmentGroupAuthorizedLocations { get; set; }
        public virtual ICollection<TicketAuditHistory> TicketAuditHistories { get; set; }
        public virtual ICollection<TicketRelatedLocation> TicketRelatedLocations { get; set; }
    }
}
