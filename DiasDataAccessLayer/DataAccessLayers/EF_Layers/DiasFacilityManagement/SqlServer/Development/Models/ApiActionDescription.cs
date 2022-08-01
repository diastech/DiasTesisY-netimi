using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.BaseModel;
using DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFM.SqlServer.Development.Models;


namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models
{
    public class ApiActionDescription : DevelopmentBaseEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public long AuthorizationCode { get; set; }

        public int ApiControllerDescriptionId { get; set; }

       

        public virtual ApiControllerDescription ParentApiController { get; set; }

        public virtual User AddedByUser { get; set; }

        public virtual User LastModifiedByUser { get; set; }
    }
}
