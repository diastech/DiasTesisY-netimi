using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using DIAS.UI.Helpers;
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
    public class RoleHelperModel : PageModel
    {
        public async Task<IActionResult> OnGetReasons(DataSourceLoadOptions loadOptions)
        {
            return null;
            //var controllersActionsJson = SessionHelper.GetObjectFromJson(HttpContext.Session, "userInfo");
            //var controllersActions = JsonConvert.DeserializeObject<List<Tuple<ApiControllerDescription, List<string>>>>(controllersActionsJson);
            //foreach (var controllerAction in controllersActions)
            //{
            //    if (controllerAction.Item1.ToString() == "UserDefinition")
            //    {
            //        foreach (var action in controllerAction.Item2)
            //        {
            //            if (action == "GetAll")
            //            {
            //                var tokenJson = SessionHelper.GetObjectFromJson(HttpContext.Session, "token");
            //                var token = JsonConvert.DeserializeObject<string>(tokenJson);
            //                return await LoadGrid(loadOptions, token);
            //            }
            //        }
            //    }
            //}
            //return new JsonResult(DataSourceLoader.Load(new List<UserHelperDto>(), loadOptions));
        }
        public async Task<IActionResult> OnGetSelectedReasons(DataSourceLoadOptions loadOptions)
        {
            List<CustomDTO.CustomTicketReasonCategoryDto> reasonCategories = new List<CustomDTO.CustomTicketReasonCategoryDto>()
            {
                new CustomDTO.CustomTicketReasonCategoryDto(){Id=1,CategoryName="CategoryName1" },
                new CustomDTO.CustomTicketReasonCategoryDto(){Id=2,CategoryName="CategoryName2"},
                new CustomDTO.CustomTicketReasonCategoryDto(){Id=3,CategoryName="CategoryName3" },
                new CustomDTO.CustomTicketReasonCategoryDto(){Id=4,CategoryName="CategoryName4" },
                new CustomDTO.CustomTicketReasonCategoryDto(){Id=5,CategoryName="CategoryName5" },
                new CustomDTO.CustomTicketReasonCategoryDto(){Id=6,CategoryName="CategoryName6" },
                new CustomDTO.CustomTicketReasonCategoryDto(){Id=7,CategoryName="CategoryName7" },
                new CustomDTO.CustomTicketReasonCategoryDto(){Id=8,CategoryName="CategoryName8" },
                new CustomDTO.CustomTicketReasonCategoryDto(){Id=9,CategoryName="CategoryName9" },
                new CustomDTO.CustomTicketReasonCategoryDto(){Id=10,CategoryName="CategoryName10" },
                new CustomDTO.CustomTicketReasonCategoryDto(){Id=11,CategoryName="CategoryName11" },
                new CustomDTO.CustomTicketReasonCategoryDto(){Id=12,CategoryName="CategoryName12" },
                new CustomDTO.CustomTicketReasonCategoryDto(){Id=13,CategoryName="CategoryName13" },
                new CustomDTO.CustomTicketReasonCategoryDto(){Id=14,CategoryName="CategoryName14" },
                new CustomDTO.CustomTicketReasonCategoryDto(){Id=15,CategoryName="CategoryName15" },
                new CustomDTO.CustomTicketReasonCategoryDto(){Id=16,CategoryName="CategoryName16" },
                new CustomDTO.CustomTicketReasonCategoryDto(){Id=17,CategoryName="CategoryName17" },
                new CustomDTO.CustomTicketReasonCategoryDto(){Id=18,CategoryName="CategoryName18" }
            };

            return new JsonResult(DataSourceLoader.Load(reasonCategories, loadOptions));
        }
        public async Task<IActionResult> OnGetGridData(DataSourceLoadOptions loadOptions)
        {
            //var controllersActionsJson = SessionHelper.GetObjectFromJson(HttpContext.Session, "userInfo");
            //var controllersActions = JsonConvert.DeserializeObject<List<Tuple<ApiControllerDescription, List<string>>>>(controllersActionsJson);
            //foreach (var controllerAction in controllersActions)
            //{
            //    if (controllerAction.Item1.ToString() == "UserDefinition")
            //    {
            //        foreach (var action in controllerAction.Item2)
            //        {
            //            if (action == "GetAll")
            //            {
            //                var tokenJson = SessionHelper.GetObjectFromJson(HttpContext.Session, "token");
            //                var token = JsonConvert.DeserializeObject<string>(tokenJson);
            //                return await LoadGrid(loadOptions, token);
            //            }
            //        }
            //    }
            //}
            return new JsonResult(DataSourceLoader.Load(new List<RoleHelperDto>(), loadOptions));
        }
        public async Task<IActionResult> OnGetTicketReasonData(DataSourceLoadOptions loadOptions)
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
                            return new JsonResult(DataSourceLoader.Load(reasonCategories, loadOptions));
                        }
                    }
                }
            }
            return new JsonResult(DataSourceLoader.Load(new List<CustomDTO.CustomTicketReasonCategoryDto>(), loadOptions));
        }
        public async Task<IActionResult> OnGetLocationData(DataSourceLoadOptions loadOptions)
        {
            HttpClient httpClient = new();
            httpClient.BaseAddress = new Uri("http://localhost:33400/");
            BusinessLogicRequest request = new()
            {
                ApiUrl = new Uri(httpClient.BaseAddress, "LocationWrapper/GetAll"),
                RequestDomain = RemoteIncomingDomains.DiasTesisYonetimWebClient
            };
            var result = httpClient.PostAsJsonAsync(request.ApiUrl, request).Result;
            var response = result.Content.ReadAsStringAsync().Result;
            BusinessLogicResponse businessLogicRequestResponse = JsonConvert.DeserializeObject<BusinessLogicResponse>(response);
            var locationList = businessLogicRequestResponse.RelevantDto.ToString();
            var locations = JsonConvert.DeserializeObject<List<CustomDTO.CustomLocationDto>>(locationList);
            return new JsonResult(DataSourceLoader.Load(locations, loadOptions));            
        }
        public async Task<IActionResult> OnGetSelectedLocations(DataSourceLoadOptions loadOptions)
        {
            List<CustomDTO.CustomLocationDto> locations = new List<CustomDTO.CustomLocationDto>()
            {
                new CustomDTO.CustomLocationDto(){Id=1,LocationName="LocationName1" },
                new CustomDTO.CustomLocationDto(){Id=2,LocationName="LocationName2"},
                new CustomDTO.CustomLocationDto(){Id=3,LocationName="LocationName3" },
                new CustomDTO.CustomLocationDto(){Id=4,LocationName="LocationName4" },
                new CustomDTO.CustomLocationDto(){Id=5,LocationName="LocationName5" },
                new CustomDTO.CustomLocationDto(){Id=6,LocationName="LocationName6" },
                new CustomDTO.CustomLocationDto(){Id=7,LocationName="LocationName7" },
                new CustomDTO.CustomLocationDto(){Id=8,LocationName="LocationName8" },
                new CustomDTO.CustomLocationDto(){Id=9,LocationName="LocationName9" },
                new CustomDTO.CustomLocationDto(){Id=10,LocationName="LocationName10" },
                new CustomDTO.CustomLocationDto(){Id=11,LocationName="LocationName11" },
                new CustomDTO.CustomLocationDto(){Id=12,LocationName="LocationName12" },
                new CustomDTO.CustomLocationDto(){Id=13,LocationName="LocationName13" },
                new CustomDTO.CustomLocationDto(){Id=14,LocationName="LocationName14" },
                new CustomDTO.CustomLocationDto(){Id=15,LocationName="LocationName15" },
                new CustomDTO.CustomLocationDto(){Id=16,LocationName="LocationName16" },
                new CustomDTO.CustomLocationDto(){Id=17,LocationName="LocationName17" },
                new CustomDTO.CustomLocationDto(){Id=18,LocationName="LocationName18" }
            };
            return new JsonResult(DataSourceLoader.Load(locations, loadOptions));
        }
    }
}
