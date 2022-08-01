using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.BaseDto;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard
{
    public class ResolutionFormDto : BaseDevelopmentStandartDto
    {
        public int Id { get; set; }
        public int? TicketReasonId { get; set; }
        public int? TicketReasonCategoryId { get; set; }
        public string FormDescription { get; set; }
        public int TicketStateId { get; set; }
        public bool? IsMandatory { get; set; }
    }
}
