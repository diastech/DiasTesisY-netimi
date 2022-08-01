using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.BaseDto;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard
{
    public class ResolutionFormAnswerDto : BaseDevelopmentStandartDto
    {
        public int Id { get; set; }
        public string Answer { get; set; }
        public bool? YesOrNo { get; set; }
        public int ResolutionFormQuestionId { get; set; }
        public int ResolutionFormId { get; set; }
    }
}
