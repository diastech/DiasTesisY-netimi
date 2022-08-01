using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using DIAS_UI.Helpers;
using DiasShared.Operations.JsonOperation.Converters;
using DiasShared.Services.Communication.BusinessLogicMessage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using static DiasShared.Enums.Standart.AuthorizationEnums;
using static DiasShared.Enums.Standart.RemoteCommunicationEnums;
using StandartDTO = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using CustomDTO = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;

namespace DIAS.UI.Pages.Definition
{
    public class LocationDefinitionModel : PageModel
    {
        private readonly INotyfService _notyfService;
        public LocationDefinitionModel(INotyfService notyfService)
        {
            _notyfService = notyfService;
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
        public async Task<IActionResult> OnPostInsertLocation(CustomDTO.CustomLocationDto model)
        {
            //var controllersActionsJson = SessionHelper.GetObjectFromJson(HttpContext.Session, "userInfo");
            //var controllersActions = JsonConvert.DeserializeObject<List<Tuple<ApiControllerDescription, List<string>>>>(controllersActionsJson);
            //foreach (var controllerAction in controllersActions)
            //{
            //    if (controllerAction.Item1.ToString() == "LocationDefinition")
            //    {
            //        foreach (var action in controllerAction.Item2)
            //        {
            //            if (action == "Insert")
            //            {

            //            }
            //        }
            //    }
            //}
            //_notyfService.Error("Ýþ Emrine Durumu Ekleme yetkiniz yoktur.");
            //return new JsonResult(new LocationHelperDto());
            HttpClient httpClient = new();
            httpClient.BaseAddress = new Uri("http://localhost:33400/");
            var options = new JsonSerializerOptions();
            options.Converters.Add(new CustomJsonConverterForType());

            var user = SessionHelper.GetObjectFromJson(HttpContext.Session, "user");
            var userDto = JsonConvert.DeserializeObject<StandartDTO.UserDto>(user);
            int loginUserId = Convert.ToInt32(userDto.Id);
            model.AddedByUserId = loginUserId;

            BusinessLogicRequest request = new()
            {
                ApiUrl = new Uri(httpClient.BaseAddress, "LocationV2/AddLocationV2WithinParentHierarchyId"),
                RequestDomain = RemoteIncomingDomains.DiasTesisYonetimWebClient,
                RequestDtosTypes = new List<Type>() { typeof(CustomDTO.CustomLocationDto) },
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
            if(businessLogicRequestResponse.ErrorObj.BusinessOperationSucceed != true)
            {
                _notyfService.Error(businessLogicRequestResponse.ErrorObj.Message,2);
                return new ObjectResult(businessLogicRequestResponse.ErrorObj.Message);
            }
            //var response = JsonConvert.DeserializeObject<CustomDTO.CustomLocationDto>(businessLogicRequestResponse.OptionalJsonResult);
            _notyfService.Success(businessLogicRequestResponse.ErrorObj.Message,2);
            return new OkResult();

        }
        public async Task<IActionResult> OnPutUpdateLocation(CustomDTO.CustomLocationDto model)
        {
            HttpClient httpClient = new();
            httpClient.BaseAddress = new Uri("http://localhost:33400/");
            var options = new JsonSerializerOptions();
            options.Converters.Add(new CustomJsonConverterForType());

            var user = SessionHelper.GetObjectFromJson(HttpContext.Session, "user");
            var userDto = JsonConvert.DeserializeObject<StandartDTO.UserDto>(user);
            int loginUserId = Convert.ToInt32(userDto.Id);
            model.LastModifiedByUserId = loginUserId;

            BusinessLogicRequest request = new()
            {
                ApiUrl = new Uri(httpClient.BaseAddress, "LocationV2/UpdateV2"),
                RequestDomain = RemoteIncomingDomains.DiasTesisYonetimWebClient,
                RequestDtosTypes = new List<Type>() { typeof(CustomDTO.CustomLocationDto) },
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
            if (businessLogicRequestResponse.ErrorObj.BusinessOperationSucceed != true)
            {
                _notyfService.Error(businessLogicRequestResponse.ErrorObj.Message, 2);
                return new ObjectResult(businessLogicRequestResponse.ErrorObj.Message);
            }
            //var response = JsonConvert.DeserializeObject<CustomDTO.CustomLocationDto>(businessLogicRequestResponse.OptionalJsonResult);
            _notyfService.Success(businessLogicRequestResponse.ErrorObj.Message, 2);
            return new OkResult();
        }
        public async Task<IActionResult> OnDeleteRemoveLocation(CustomDTO.CustomLocationDto model)
        {
            HttpClient httpClient = new();
            httpClient.BaseAddress = new Uri("http://localhost:33400/");
            var options = new JsonSerializerOptions();
            options.Converters.Add(new CustomJsonConverterForType());

            var user = SessionHelper.GetObjectFromJson(HttpContext.Session, "user");
            var userDto = JsonConvert.DeserializeObject<StandartDTO.UserDto>(user);
            int loginUserId = Convert.ToInt32(userDto.Id);
            model.LastModifiedByUserId = loginUserId;

            BusinessLogicRequest request = new()
            {
                ApiUrl = new Uri(httpClient.BaseAddress, "LocationV2/DeleteV2"),
                RequestDomain = RemoteIncomingDomains.DiasTesisYonetimWebClient,
                RequestDtosTypes = new List<Type>() { typeof(CustomDTO.CustomLocationDto) },
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
            if (businessLogicRequestResponse.ErrorObj.BusinessOperationSucceed != true)
            {
                _notyfService.Error(businessLogicRequestResponse.ErrorObj.Message, 2);
                return new ObjectResult(businessLogicRequestResponse.ErrorObj.Message);
            }
            //var response = JsonConvert.DeserializeObject<CustomDTO.CustomLocationDto>(businessLogicRequestResponse.OptionalJsonResult);
            _notyfService.Success(businessLogicRequestResponse.ErrorObj.Message, 2);
            return new OkResult();
        }
    }
}
