namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard
{
    public class ViewTicketStateLevelDto
    {
        public int Id { get; set; }
        public int TicketStateSourceId { get; set; }
        public string TicketStateSource { get; set; }
        public int TicketStateDestinationId { get; set; }
        public string TicketStateDestination { get; set; }
    }
}
