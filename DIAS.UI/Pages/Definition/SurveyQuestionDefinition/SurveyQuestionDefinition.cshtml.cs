using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using DIAS.UI.Helpers;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using DiasShared.Operations.EnumOperations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using static DiasShared.Enums.Standart.TicketEnums;
using StandartDTO = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;

namespace DIAS.UI.Pages.Definition
{
    public class SurveyQuestionModel : PageModel
    {
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnGetGridData(DataSourceLoadOptions loadOptions)
        {


            using (StreamReader r = new StreamReader("SampleData/survey.json"))
            {
                string json = r.ReadToEnd();
                List<StandartDTO.SurveyQuestionDto> ro = JsonConvert.DeserializeObject<List<StandartDTO.SurveyQuestionDto>>(json);
                return new JsonResult(DataSourceLoader.Load(ro, loadOptions));
            }


            return new JsonResult(DataSourceLoader.Load(new List<StandartDTO.SurveyQuestionDto>(), loadOptions));
        }
        public async Task<IActionResult> OnGetPlainDataForDragAndDrop(DataSourceLoadOptions loadOptions)
        {
            return null;
            //return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(TreeViewPlainDataForDragAndDrop.FileSystemItems, loadOptions)), "application/json");
        }

        public async Task<IActionResult> OnPostGridRow(SurveyQuestionDto model)
        {

            List<SurveyAnswerDto> listSurveyAnswerDto = new();
            List<SurveyAnswerDto> listSurveyAnswerDtoForApi = new();
            listSurveyAnswerDto = JsonConvert.DeserializeObject<List<SurveyAnswerDto>>(model.SurveyAnswerControlDtosJson);
            string[] listString = model.OrderOfTheSurveyQuestion[0].Split(",");

            foreach (var item in listString)
            {
                foreach (var surveyAnswer in listSurveyAnswerDto)
                {
                    if (surveyAnswer.SurveyAnswerControlTypeInt == (int)((SurveyAnswerControlType)Enum.Parse(typeof(SurveyAnswerControlType), item)))
                    {
                        if (!(listSurveyAnswerDtoForApi.Contains(surveyAnswer)))
                        {
                            listSurveyAnswerDtoForApi.Add(surveyAnswer);
                            break;
                        }
                    }
                }
            }

            return new OkObjectResult(listSurveyAnswerDtoForApi);
        }

        public async Task<IActionResult> OnPostUpdateForm(SurveyQuestionDto model)
        {
            try
            {
                List<SurveyAnswerDto> listSurveyAnswerDto = new();
                List<SurveyAnswerDto> listSurveyAnswerDtoForApi = new();
                listSurveyAnswerDto = JsonConvert.DeserializeObject<List<SurveyAnswerDto>>(model.SurveyAnswerControlDtosJson);
                string[] listString = model.OrderOfTheSurveyQuestion[0].Split(",");

                foreach (var item in listString)
                {
                    foreach (var surveyAnswer in listSurveyAnswerDto)
                    {
                        if (surveyAnswer.SurveyAnswerControlTypeInt == (int)((SurveyAnswerControlType)Enum.Parse(typeof(SurveyAnswerControlType), item)))
                        {
                            if (!(listSurveyAnswerDtoForApi.Contains(surveyAnswer)))
                            {
                                listSurveyAnswerDtoForApi.Add(surveyAnswer);
                                break;
                            }
                        }
                    }
                }

                return new OkObjectResult(listSurveyAnswerDtoForApi);
            }
            catch (Exception e)
            {

                throw;
            }
            
        }
    }
}
