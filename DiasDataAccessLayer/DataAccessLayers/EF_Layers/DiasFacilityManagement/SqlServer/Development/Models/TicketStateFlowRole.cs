using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.BaseModel;
using DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFM.SqlServer.Development.Models;

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models
{
    public class TicketStateFlowRole : DevelopmentBaseEntity
    {
        public int Id { get; set; }

        public int TicketStateRoleId { get; set; }

        public int TicketStateTransitionFlowId { get; set; }

        public bool PermissionGranted { get; set; } = false;

        public virtual User AddedByUser { get; set; }
        public virtual User LastModifiedByUser { get; set; }
        public virtual TicketStateTransitionFlow TicketStateTransitionFlowByTicketStateRole { get; set; }
        public virtual TicketStateRole TicketStateRoleByUser { get; set; }
    }
}
