using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.BaseModel;
using DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFM.SqlServer.Development.Models;
using System.Collections.Generic;

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models
{
    public class TicketStateTransitionFlow : DevelopmentBaseEntity
    {
        public TicketStateTransitionFlow()
        {
            TicketStateTransitionFlowByTicketStateFlowRoles = new HashSet<TicketStateFlowRole>();
        }

        public int Id { get; set; }

        public int TicketStateTransitionId { get; set; }

        public int TicketStateFlowId { get; set; }

        public virtual User AddedByUser { get; set; }
        public virtual User LastModifiedByUser { get; set; }

        public virtual TicketStateFlow TicketStateFlowByTicketStateTransitionFlow { get; set; }
        public virtual TicketStateTransition TicketStateTransitionByTicketStateTransitionFlow { get; set; }

        public virtual ICollection<TicketStateFlowRole> TicketStateTransitionFlowByTicketStateFlowRoles { get; set; }
    }
}
