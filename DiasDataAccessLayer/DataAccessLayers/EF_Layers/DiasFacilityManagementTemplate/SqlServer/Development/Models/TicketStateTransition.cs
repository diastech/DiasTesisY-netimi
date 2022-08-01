using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagementTemplate.SqlServer.Development.BaseModel;
using DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFMTemplate.SqlServer.Development.Models;
using System;

#nullable disable

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagementTemplate.SqlServer.Development.Models
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
