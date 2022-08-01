using System;
using System.Collections.Generic;

#nullable disable

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagementTemplate.SqlServer.Development.Models
{
    public partial class VwTicketStateLevel
    {
        public int Id { get; set; }
        public int TicketStateSourceId { get; set; }
        public string TicketStateSource { get; set; }
        public int TicketStateDestinationId { get; set; }
        public string TicketStateDestination { get; set; }
    }
}
