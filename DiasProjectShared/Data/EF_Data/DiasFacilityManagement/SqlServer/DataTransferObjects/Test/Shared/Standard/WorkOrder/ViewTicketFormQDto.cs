using System;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard
{
    public class ViewTicketFormQDto
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
