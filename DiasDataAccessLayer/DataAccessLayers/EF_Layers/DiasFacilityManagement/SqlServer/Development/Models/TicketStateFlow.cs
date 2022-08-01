using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.BaseModel;
using DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFM.SqlServer.Development.Models;
using System.Collections.Generic;

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models
{
    public class TicketStateFlow : DevelopmentBaseEntity
    {
        public TicketStateFlow()
        {          
            TicketStateTransitionFlowByTicketStateFlows = new HashSet<TicketStateTransitionFlow>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string NormalizedName { get; set; }

        public virtual User AddedByUser { get; set; }
        public virtual User LastModifiedByUser { get; set; }
 
        public virtual ICollection<TicketStateTransitionFlow> TicketStateTransitionFlowByTicketStateFlows { get; set; }

    }
}
