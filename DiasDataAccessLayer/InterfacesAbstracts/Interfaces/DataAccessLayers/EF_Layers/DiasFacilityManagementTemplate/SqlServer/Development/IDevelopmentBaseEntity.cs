using System;

namespace DiasDataAccessLayer.InterfacesAbstracts.Interfaces.DataAccessLayers.EF_Layers.DiasFacilityManagementTemplate.SqlServer.Development
{
    public interface IDevelopmentBaseEntity
    { 
        public int AddedByUserId { get; set; }
        public DateTime AddedTime { get; set; }
        public int? LastModifiedByUserId { get; set; }
        public DateTime? LastModifiedTime { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
