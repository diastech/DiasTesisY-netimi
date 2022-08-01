using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.BaseDto;
using System;


namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard
{
    public class ResolutionFormQuestionTypeDto : BaseDevelopmentStandartDto
    {
        public int Id { get; set; }
        public string QuestionType { get; set; }
    }
}
