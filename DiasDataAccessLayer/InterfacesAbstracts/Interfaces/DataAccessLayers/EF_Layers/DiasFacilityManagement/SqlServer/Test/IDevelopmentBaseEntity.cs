using System;

namespace DiasDataAccessLayer.InterfacesAbstracts.Interfaces.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Test
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
