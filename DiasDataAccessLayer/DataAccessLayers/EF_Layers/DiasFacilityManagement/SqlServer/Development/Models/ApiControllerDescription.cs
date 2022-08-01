using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.BaseModel;
using System.Collections.Generic;
using DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFM.SqlServer.Development.Models;


namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models
{
    public class ApiControllerDescription : DevelopmentBaseEntity
    {
        public ApiControllerDescription()
        {
            ApiActionsAddedByAdmins = new HashSet<ApiActionDescription>();
            CompanyRoleClaimsAddedByAdmins = new HashSet<CompanyRoleClaim>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<ApiActionDescription> ApiActionsAddedByAdmins { get; set; }

        public virtual ICollection<CompanyRoleClaim> CompanyRoleClaimsAddedByAdmins { get; set; }

        public virtual User AddedByUser { get; set; }

        public virtual User LastModifiedByUser { get; set; }

    }
}
