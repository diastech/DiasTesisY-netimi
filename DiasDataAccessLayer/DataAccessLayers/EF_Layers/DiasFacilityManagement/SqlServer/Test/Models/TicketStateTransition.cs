using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Test.BaseModel;
using System;

#nullable disable

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Test.Models
{
    public partial class TicketStateTransition : DevelopmentBaseEntity
    {
        public int Id { get; set; }
        public int SourceTicketStateId { get; set; }
        public int DestinationTicketStateId { get; set; }

        public virtual User AddedByUser { get; set; }
        public virtual TicketState DestinationTicketState { get; set; }
        public virtual User LastModifiedByUser { get; set; }
        public virtual TicketState SourceTicketState { get; set; }
    }
}
