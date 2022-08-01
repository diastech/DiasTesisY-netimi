using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.BaseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard
{
    public class SurveyQuestionDto : BaseDevelopmentStandartDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string QuestionName { get; set; }
        public UserDto AddedByUser { get; set; }
        public List<SurveyAnswerDto> SurveyAnswerControlDtos { get; set; }

        //Dummy veri için Ad ve Soyad prop eklenmiştir.Silinecek AddedByUser dan alacak
        public string Ad { get; set; }
        public string SoyAd { get; set; }

        public string SurveyAnswerControlDtosJson { get; set; }
        public List<string> OrderOfTheSurveyQuestion { get; set; }

    }
}
