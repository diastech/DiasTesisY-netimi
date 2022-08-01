using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Test.BaseModel;
using System;

#nullable disable

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Test.Models
{
    public partial class PeriodicTicket : DevelopmentBaseEntity
    {
        public int Id { get; set; }
        public string PeriodicName { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public int TicketPriority { get; set; }
        public int? LocationId { get; set; }
        public string PeriodFrequency { get; set; }
        public int TicketReasonId { get; set; }

        public virtual User AddedByUser { get; set; }
        public virtual User LastModifiedByUser { get; set; }
        public virtual TicketRelatedLocation Location { get; set; }
        public virtual TicketReason TicketReason { get; set; }
    }
}
