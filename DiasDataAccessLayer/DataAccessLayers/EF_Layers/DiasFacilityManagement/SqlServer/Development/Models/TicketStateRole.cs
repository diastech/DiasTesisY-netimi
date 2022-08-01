using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.BaseModel;
using DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFM.SqlServer.Development.Models;
using System.Collections.Generic;

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models
{
    public class TicketStateRole : DevelopmentBaseEntity
    {
        public TicketStateRole()
        {
            TicketStateFlowRoleByTicketStateRoles = new HashSet<TicketStateFlowRole>();
            CompanyRoleClaimByTicketStateRoles = new HashSet<CompanyRoleClaim>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string NormalizedName { get; set; }//Identity için gerekli olabilir

        public virtual User AddedByUser { get; set; }

        public virtual User LastModifiedByUser { get; set; }

        public virtual ICollection<TicketStateFlowRole> TicketStateFlowRoleByTicketStateRoles { get; set; }

        public virtual ICollection<CompanyRoleClaim> CompanyRoleClaimByTicketStateRoles { get; set; }

    }
}
