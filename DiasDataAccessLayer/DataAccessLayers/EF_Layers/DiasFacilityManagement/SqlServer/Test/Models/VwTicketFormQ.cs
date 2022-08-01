using System;
using System.Collections.Generic;

#nullable disable

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Test.Models
{
    public partial class VwTicketFormQ
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public string QuestionText { get; set; }
        public int TicketFormId { get; set; }
        public bool? IsMandatory { get; set; }
        public string Q1 { get; set; }
        public string Q2 { get; set; }
        public string Q3 { get; set; }
        public string Q4 { get; set; }
        public string Q5 { get; set; }
    }
}
