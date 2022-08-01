
using System.Collections.Generic;
using static DiasShared.Enums.Standart.TicketEnums;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard
{
    public class SurveyAnswerDto
    {
        public CheckBoxDto CheckBoxDtoObj { get; set; }

        public DropDownDto DropDownDtoObj { get; set; }

        public RadioGroupDto RadioGroupDtoObj { get; set; }

        public TextBoxDto TextBoxDtoObj { get; set; }

        public int SurveyAnswerControlTypeInt { get; set; }


    }
}
