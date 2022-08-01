using System;
using System.Collections.Generic;

#nullable disable

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagementTemplate.SqlServer.Development.Models
{
    public partial class VwTicketLocation1
    {
        public int TicketId { get; set; }
        public string Location { get; set; }
        public string LocationName { get; set; }
        public string LocationHierarchy { get; set; }
    }
}
