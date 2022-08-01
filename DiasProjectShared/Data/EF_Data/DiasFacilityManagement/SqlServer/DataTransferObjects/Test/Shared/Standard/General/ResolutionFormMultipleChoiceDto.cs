using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.BaseDto;
using System;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard
{
    public class ResolutionFormMultipleChoiceDto : BaseDevelopmentStandartDto
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public int TicketFormId { get; set; }
        public bool IsMandatory { get; set; }
        public string Option1Text { get; set; }
        public string Option2Text { get; set; }
        public string Option3Text { get; set; }
        public string Option4Text { get; set; }
        public string Option5Text { get; set; }
    }
}
