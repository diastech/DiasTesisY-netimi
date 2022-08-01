using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.BaseDto;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard
{
    public class ResolutionFormYesNoDto : BaseDevelopmentStandartDto
    {
        public int Id { get; set; }
        public int TicketFormId { get; set; }
        public string QuestionText { get; set; }
        public bool IsMandatory { get; set; }
    }
}
