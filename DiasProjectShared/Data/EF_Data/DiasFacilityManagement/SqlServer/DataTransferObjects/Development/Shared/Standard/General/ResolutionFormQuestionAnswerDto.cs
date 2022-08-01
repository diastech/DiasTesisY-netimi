using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.BaseDto;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard
{
    public class ResolutionFormQuestionAnswerDto : BaseDevelopmentStandartDto
    {
        public int Id { get; set; }
        public int ResolutionFormId { get; set; }
        public int TicketId { get; set; }
        public string QuestionText { get; set; }
        public string AnswerText { get; set; }
    }
}
