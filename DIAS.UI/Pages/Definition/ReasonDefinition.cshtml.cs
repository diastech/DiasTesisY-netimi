using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using DIAS_UI.Helpers;
using DiasShared.Services.Communication.BusinessLogicMessage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using static DiasShared.Enums.Standart.AuthorizationEnums;
using static DiasShared.Enums.Standart.RemoteCommunicationEnums;
using CustomDTO = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;

namespace DIAS.UI.Pages.Definition
{
    public class ReasonDefinitionModel : PageModel
    {
        public async Task<IActionResult> OnGetReasonData(DataSourceLoadOptions loadOptions)
        {
            var controllersActionsJson = SessionHelper.GetObjectFromJson(HttpContext.Session, "userInfo");
            var controllersActions = JsonConvert.DeserializeObject<List<Tuple<ApiControllerDescription, List<string>>>>(controllersActionsJson);
            foreach (var controllerAction in controllersActions)
            {
                if (controllerAction.Item1.ToString() == "TicketReasonCategoryWrapper")
                {
                    foreach (var action in controllerAction.Item2)
                    {
                        if (action == "GetAll")
                        {
                            HttpClient httpClient = new();
                            httpClient.BaseAddress = new Uri("http://localhost:33400/");
                            BusinessLogicRequest request = new()
                            {
                                ApiUrl = new Uri(httpClient.BaseAddress, "TicketReasonCategoryWrapper/GetAll"),
                                RequestDomain = RemoteIncomingDomains.DiasTesisYonetimWebClient
                            };
                            var result = httpClient.PostAsJsonAsync(request.ApiUrl, request).Result;
                            var response = result.Content.ReadAsStringAsync().Result;
                            BusinessLogicResponse businessLogicRequestResponse = JsonConvert.DeserializeObject<BusinessLogicResponse>(response);
                            var reasonCategoryList = businessLogicRequestResponse.RelevantDto.ToString();
                            var reasonCategories = JsonConvert.DeserializeObject<List<CustomDTO.CustomTicketReasonCategoryDto>>(reasonCategoryList);
                            foreach (var item in reasonCategories)
                            {
                                if(item.CategoryNameToUI == null)
                                {
                                    item.CategoryNameToUI = item.CategoryName;
                                }
                            }

                            return new JsonResult(DataSourceLoader.Load(reasonCategories, loadOptions));
                        }
                    }
                }
            }
            return new JsonResult(DataSourceLoader.Load(new List<CustomDTO.CustomTicketReasonCategoryDto>(), loadOptions));
        }

        public async Task<IActionResult> OnPostInsertReason(CustomDTO.CustomTicketReasonCategoryDto model)
        {
            return null;
        }
        public async Task<IActionResult> OnPutUpdateReason(CustomDTO.CustomTicketReasonCategoryDto model)
        {
            return null;
        }
    }
}
