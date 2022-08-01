using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.BaseDto;


namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard
{
    public class ResolutionFormQuestionDto : BaseDevelopmentStandartDto
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public int ResolutionFormQuestionTypeId { get; set; }
    }
}
