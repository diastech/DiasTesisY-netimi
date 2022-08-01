using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using DIAS_UI;
using DIAS_UI.Helpers;
using DiasShared.Operations.JsonOperation.Converters;
using DiasShared.Services.Communication.BusinessLogicMessage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using static DiasShared.Enums.Standart.AuthorizationEnums;
using static DiasShared.Enums.Standart.RemoteCommunicationEnums;
using CustomDTO = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using StandartDTO = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;


namespace DIAS.UI.Pages.Definition.TicketDefinitions
{
    public class BasicTicketStateModel : PageModel
    {
        private readonly INotyfService _notyfService;

        public BasicTicketStateModel(INotyfService notyfService)
        {
            _notyfService = notyfService;
        }

        public IActionResult OnGetGridData(DataSourceLoadOptions loadOptions)
        {
            var controllersActionsJson = SessionHelper.GetObjectFromJson(HttpContext.Session, "userInfo");
            var controllersActions = JsonConvert.DeserializeObject<List<Tuple<ApiControllerDescription, List<string>>>>(controllersActionsJson);
            foreach (var controllerAction in controllersActions)
            {
                if (controllerAction.Item1.ToString() == "BasicTicketState")
                {
                    foreach (var action in controllerAction.Item2)
                    {
                        if (action == "GetAll")
                        {
                            HttpClient httpClient = new();
                            httpClient.BaseAddress = new Uri("http://localhost:33400/");
                            BusinessLogicRequest request = new()
                            {
                                ApiUrl = new Uri(httpClient.BaseAddress, "BasicTicketState/GetAll"),
                                RequestDomain = RemoteIncomingDomains.DiasTesisYonetimWebClient
                            };
                            var result = httpClient.PostAsJsonAsync(request.ApiUrl, request).Result;
                            var data = result.Content.ReadAsStringAsync().Result;
                            BusinessLogicResponse businessLogicRequestResponse = JsonConvert.DeserializeObject<BusinessLogicResponse>(data);
                            var response = JsonConvert.DeserializeObject(data);

                            var myList = businessLogicRequestResponse.RelevantDto.ToString();
                            var listModel = JsonConvert.DeserializeObject<List<StandartDTO.BasicTicketStateDto>>(myList).Where(x => x.IsActive == true && x.IsDeleted == false);
                            return new JsonResult(DataSourceLoader.Load(listModel, loadOptions));
                        }
                    }
                }
            }
            return new JsonResult(DataSourceLoader.Load(new List<StandartDTO.BasicTicketStateDto>(), loadOptions));
        }
        public async Task<IActionResult> OnPostGridRow(StandartDTO.BasicTicketStateDto model)
        {

            var controllersActionsJson = SessionHelper.GetObjectFromJson(HttpContext.Session, "userInfo");
            var controllersActions = JsonConvert.DeserializeObject<List<Tuple<ApiControllerDescription, List<string>>>>(controllersActionsJson);
            foreach (var controllerAction in controllersActions)
            {
                if (controllerAction.Item1.ToString() == "BasicTicketState")
                {
                    foreach (var action in controllerAction.Item2)
                    {
                        if (action == "Insert")
                        {
                            HttpClient httpClient = new();
                            httpClient.BaseAddress = new Uri("http://localhost:33400/");
                            var options = new JsonSerializerOptions();
                            options.Converters.Add(new CustomJsonConverterForType());
                            BusinessLogicRequest request = new()
                            {
                                ApiUrl = new Uri(httpClient.BaseAddress, "BasicTicketState/Insert"),
                                RequestDomain = RemoteIncomingDomains.DiasTesisYonetimWebClient,
                                RequestDtosTypes = new List<Type>() { typeof(StandartDTO.BasicTicketStateDto) },
                                RequestDtosJsons = new List<string>() {
                                    JsonConvert.SerializeObject(model,
                                          Formatting.None,
                                        new JsonSerializerSettings {
                                                NullValueHandling = NullValueHandling.Ignore
                            })}
                            };

                            var result = httpClient.PostAsJsonAsync(request.ApiUrl, request, options).Result;
                            var resultModel = result.Content.ReadAsStringAsync().Result;
                            BusinessLogicResponse businessLogicRequestResponse = JsonConvert.DeserializeObject<BusinessLogicResponse>(resultModel);
                            var response = JsonConvert.DeserializeObject<StandartDTO.BasicTicketStateDto>(businessLogicRequestResponse.OptionalJsonResult);
                            return new OkResult();
                        }
                    }
                }
            }
            _notyfService.Error("Hýzlý Ýþ Emri Durumu Ekleme yetkiniz yoktur.");
            return new JsonResult(new StandartDTO.TicketStateDto());
        }
        public async Task<IActionResult> OnPutGridRowUpdate(StandartDTO.BasicTicketStateDto model)
        {

            var controllersActionsJson = SessionHelper.GetObjectFromJson(HttpContext.Session, "userInfo");
            var controllersActions = JsonConvert.DeserializeObject<List<Tuple<ApiControllerDescription, List<string>>>>(controllersActionsJson);
            foreach (var controllerAction in controllersActions)
            {
                if (controllerAction.Item1.ToString() == "BasicTicketState")
                {
                    foreach (var action in controllerAction.Item2)
                    {
                        if (action == "Update")
                        {
                            HttpClient httpClient = new();
                            httpClient.BaseAddress = new Uri("http://localhost:33400/");
                            var options = new JsonSerializerOptions();
                            options.Converters.Add(new CustomJsonConverterForType());
                            BusinessLogicRequest request = new()
                            {
                                ApiUrl = new Uri(httpClient.BaseAddress, "BasicTicketState/Update"),
                                RequestDomain = RemoteIncomingDomains.DiasTesisYonetimWebClient,
                                RequestDtosTypes = new List<Type>() { typeof(StandartDTO.BasicTicketStateDto) },
                                RequestDtosJsons = new List<string>() {
                    JsonConvert.SerializeObject(model,
                          Formatting.None,
                            new JsonSerializerSettings {
                                NullValueHandling = NullValueHandling.Ignore
                            })}
                            };

                            var result = httpClient.PostAsJsonAsync(request.ApiUrl, request, options).Result;
                            var resultModel = result.Content.ReadAsStringAsync().Result;
                            BusinessLogicResponse businessLogicRequestResponse = JsonConvert.DeserializeObject<BusinessLogicResponse>(resultModel);
                            var response = JsonConvert.DeserializeObject<StandartDTO.BasicTicketStateDto>(businessLogicRequestResponse.OptionalJsonResult);
                            return new OkResult();
                        }
                    }
                }
            }
            _notyfService.Error("Basit Ýþ Emri Durumu Güncelleme yetkiniz yoktur.");
            return new JsonResult(new StandartDTO.TicketStateDto());
        }
        public async Task<IActionResult> OnDeleteGridRowDelete(StandartDTO.BasicTicketStateDto model)
        {
            var controllersActionsJson = SessionHelper.GetObjectFromJson(HttpContext.Session, "userInfo");
            var controllersActions = JsonConvert.DeserializeObject<List<Tuple<ApiControllerDescription, List<string>>>>(controllersActionsJson);
            foreach (var controllerAction in controllersActions)
            {
                if (controllerAction.Item1.ToString() == "BasicTicketState")
                {
                    foreach (var action in controllerAction.Item2)
                    {
                        if (action == "Delete")
                        {
                            HttpClient httpClient = new();
                            httpClient.BaseAddress = new Uri("http://localhost:33400/");
                            var options = new JsonSerializerOptions();
                            options.Converters.Add(new CustomJsonConverterForType());
                            BusinessLogicRequest request = new()
                            {
                                ApiUrl = new Uri(httpClient.BaseAddress, "BasicTicketState/Delete"),
                                RequestDomain = RemoteIncomingDomains.DiasTesisYonetimWebClient,
                                RequestDtosTypes = new List<Type>() { typeof(StandartDTO.BasicTicketStateDto) },
                                RequestDtosJsons = new List<string>() {
                    JsonConvert.SerializeObject(model,
                          Formatting.None,
                            new JsonSerializerSettings {
                                NullValueHandling = NullValueHandling.Ignore
                            })}
                            };

                            var result = httpClient.PostAsJsonAsync(request.ApiUrl, request, options).Result;
                            var resultModel = result.Content.ReadAsStringAsync().Result;
                            BusinessLogicResponse businessLogicRequestResponse = JsonConvert.DeserializeObject<BusinessLogicResponse>(resultModel);
                            var response = JsonConvert.DeserializeObject<StandartDTO.BasicTicketStateDto>(businessLogicRequestResponse.OptionalJsonResult);
                            return new OkResult();
                        }
                    }
                }
            }
            _notyfService.Error("Basit Ýþ Emri Silme yetkiniz yoktur.");
            return new JsonResult(new StandartDTO.TicketStateDto());
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
                            //var result = httpClient.GetAsync("http://localhost:33400/TicketReasonCategoryWrapper/GetAll").Result;
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
            var controllersActionsJson = SessionHelper.GetObjectFromJson(HttpContext.Session, "userInfo");
            var controllersActions = JsonConvert.DeserializeObject<List<Tuple<ApiControllerDescription, List<string>>>>(controllersActionsJson);
            foreach (var controllerAction in controllersActions)
            {
                if (controllerAction.Item1.ToString() == "LocationWrapper")
                {
                    foreach (var action in controllerAction.Item2)
                    {
                        if (action == "GetAll")
                        {
                            HttpClient httpClient = new();
                            httpClient.BaseAddress = new Uri("http://localhost:33400/");
                            BusinessLogicRequest request = new()
                            {
                                ApiUrl = new Uri(httpClient.BaseAddress, "LocationWrapper/GetAll"),
                                RequestDomain = RemoteIncomingDomains.DiasTesisYonetimWebClient
                            };
                            //var result = httpClient.GetAsync("http://localhost:33400/LocationWrapper/GetAll").Result;
                            var result = httpClient.PostAsJsonAsync(request.ApiUrl, request).Result;
                            var response = result.Content.ReadAsStringAsync().Result;
                            BusinessLogicResponse businessLogicRequestResponse = JsonConvert.DeserializeObject<BusinessLogicResponse>(response);
                            var locationList = businessLogicRequestResponse.RelevantDto.ToString();
                            var locations = JsonConvert.DeserializeObject<List<CustomDTO.CustomLocationDto>>(locationList);
                            return new JsonResult(DataSourceLoader.Load(locations, loadOptions));
                        }
                    }
                }
            }
            return new JsonResult(DataSourceLoader.Load(new List<CustomDTO.CustomLocationDto>(), loadOptions));
        }
        public async Task<IActionResult> OnGetUserData(DataSourceLoadOptions loadOptions)
        {
            var controllersActionsJson = SessionHelper.GetObjectFromJson(HttpContext.Session, "userInfo");
            var controllersActions = JsonConvert.DeserializeObject<List<Tuple<ApiControllerDescription, List<string>>>>(controllersActionsJson);
            foreach (var controllerAction in controllersActions)
            {
                if (controllerAction.Item1.ToString() == "Users")
                {
                    foreach (var action in controllerAction.Item2)
                    {
                        if (action == "GetAllUsers")
                        {
                            HttpClient httpClient = new();
                            var result = httpClient.GetAsync("http://localhost:33400/Users/GetAllUsers").Result;
                            var response = result.Content.ReadAsStringAsync().Result;
                            BusinessLogicResponse businessLogicRequestResponse = JsonConvert.DeserializeObject<BusinessLogicResponse>(response);
                            var userList = businessLogicRequestResponse.RelevantDto.ToString();
                            var users = JsonConvert.DeserializeObject<List<StandartDTO.UserDto>>(userList);
                            return new JsonResult(DataSourceLoader.Load(users, loadOptions));
                        }
                    }
                }
            }
            return new JsonResult(DataSourceLoader.Load(new List<StandartDTO.UserDto>(), loadOptions));
        }
        public async Task<IActionResult> OnGetBasicTicketStateData(DataSourceLoadOptions loadOptions)
        {
            var controllersActionsJson = SessionHelper.GetObjectFromJson(HttpContext.Session, "userInfo");
            var controllersActions = JsonConvert.DeserializeObject<List<Tuple<ApiControllerDescription, List<string>>>>(controllersActionsJson);
            foreach (var controllerAction in controllersActions)
            {
                if (controllerAction.Item1.ToString() == "TicketState")
                {
                    foreach (var action in controllerAction.Item2)
                    {
                        if (action == "GetAll")
                        {
                            HttpClient httpClient = new();
                            httpClient.BaseAddress = new Uri("http://localhost:33400/");
                            BusinessLogicRequest request = new()
                            {
                                ApiUrl = new Uri(httpClient.BaseAddress, "BasicTicketState/GetAll"),
                                RequestDomain = RemoteIncomingDomains.DiasTesisYonetimWebClient
                            };

                            var result = httpClient.PostAsJsonAsync(request.ApiUrl, request).Result;
                            //var result = httpClient.GetAsync("http://localhost:33400/BasicTicketState/GetAll").Result;
                            var response = result.Content.ReadAsStringAsync().Result;
                            BusinessLogicResponse businessLogicRequestResponse = JsonConvert.DeserializeObject<BusinessLogicResponse>(response);
                            var BasicTicketStateList = businessLogicRequestResponse.RelevantDto.ToString();
                            var BasicTicketStates = JsonConvert.DeserializeObject<List<StandartDTO.BasicTicketStateDto>>(BasicTicketStateList);
                            return new JsonResult(DataSourceLoader.Load(BasicTicketStates, loadOptions));
                        }
                    }
                }
            }
            return new JsonResult(DataSourceLoader.Load(new List<StandartDTO.TicketStateDto>(), loadOptions));
        }
    }
}
