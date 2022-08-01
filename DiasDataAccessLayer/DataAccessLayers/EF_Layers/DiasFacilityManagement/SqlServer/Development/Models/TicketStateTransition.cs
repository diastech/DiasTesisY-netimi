using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.BaseModel;
using System;
using DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFM.SqlServer.Development.Models;
using System.Collections.Generic;


#nullable disable

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models
{
    public partial class TicketStateTransition : DevelopmentBaseEntity
    {
        public TicketStateTransition()
        {
            TicketStateTransitionFlowByTicketStateTransitions = new HashSet<TicketStateTransitionFlow>();
        }

        public int Id { get; set; }
        public int SourceTicketStateId { get; set; }
        public int DestinationTicketStateId { get; set; }

        public virtual User AddedByUser { get; set; }
        public virtual TicketState DestinationTicketState { get; set; }
        public virtual User LastModifiedByUser { get; set; }
        public virtual TicketState SourceTicketState { get; set; }

        public virtual ICollection<TicketStateTransitionFlow> TicketStateTransitionFlowByTicketStateTransitions { get; set; }
    }
}
