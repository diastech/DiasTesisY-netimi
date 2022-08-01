using System;
using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagementTemplate.SqlServer.Development.BaseModel;
using NetTopologySuite.Geometries;

#nullable disable

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagementTemplate.SqlServer.Development.Models
{
    public partial class TicketPriority : DevelopmentBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //new { ID = 1, Text = "Kritik" }
        //new { ID = 2, Text = "Yüksek Öncelikli" }
        //new { ID = 3, Text = "Normal" }
        //new { ID = 4, Text = "Düşük Öncelikli" }
    }
}
