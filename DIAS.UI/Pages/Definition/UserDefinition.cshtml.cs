using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using DIAS.UI.Helpers;
using DIAS_UI.Helpers;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using DiasShared.Operations.JsonOperation.Converters;
using DiasShared.Operations.ReflectionOperations;
using DiasShared.Services.Communication.BusinessLogicMessage;
using DiasShared.Services.Communication.BusinessLogicMessage.ThirdPartyLibrary.DevExpress;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using static DiasShared.Enums.Standart.AuthorizationEnums;
using static DiasShared.Enums.Standart.RemoteCommunicationEnums;
using StandartDTO = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;

namespace DIAS.UI.Pages.Definition
{
    public class UserDefinitionModel : PageModel
    {
        private readonly INotyfService _notyfService;
        public UserDefinitionModel(INotyfService notyfService)
        {
            _notyfService = notyfService;
        }
        public async Task<IActionResult> OnGetGridData(DataSourceLoadOptions loadOptions)
        {
            var controllersActionsJson = SessionHelper.GetObjectFromJson(HttpContext.Session, "userInfo");
            var controllersActions = JsonConvert.DeserializeObject<List<Tuple<ApiControllerDescription, List<string>>>>(controllersActionsJson);
            foreach (var controllerAction in controllersActions)
            {
                if (controllerAction.Item1.ToString() == "UserDefinition")
                {
                    foreach (var action in controllerAction.Item2)
                    {
                        if (action == "GetAll")
                        {
                            var tokenJson = SessionHelper.GetObjectFromJson(HttpContext.Session, "token");
                            var token = JsonConvert.DeserializeObject<string>(tokenJson);
                            return await LoadGrid(loadOptions, token);
                        }
                    }
                }
            }
            return new JsonResult(DataSourceLoader.Load(new List<UserHelperDto>(), loadOptions));
        }
        public async Task<IActionResult> OnPostGridRow(UserHelperDto model)
        {
            var controllersActionsJson = SessionHelper.GetObjectFromJson(HttpContext.Session, "userInfo");
            var controllersActions = JsonConvert.DeserializeObject<List<Tuple<ApiControllerDescription, List<string>>>>(controllersActionsJson);
            foreach (var controllerAction in controllersActions)
            {
                if (controllerAction.Item1.ToString() == "UserDefinition")
                {
                    foreach (var action in controllerAction.Item2)
                    {
                        if (action == "Insert")
                        {
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
                                ApiUrl = new Uri(httpClient.BaseAddress, "LocationCode/Insert"),
                                RequestDomain = RemoteIncomingDomains.DiasTesisYonetimWebClient,
                                RequestDtosTypes = new List<Type>() { typeof(UserHelperDto) },
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
                            var response = JsonConvert.DeserializeObject<UserHelperDto>(businessLogicRequestResponse.OptionalJsonResult);
                            return new OkResult();
                        }
                    }
                }
            }
            _notyfService.Error("Ýþ Emrine Durumu Ekleme yetkiniz yoktur.");
            return new JsonResult(new UserHelperDto());
        }
        public async Task<IActionResult> OnPutGridRowUpdate(UserHelperDto model)
        {
            var controllersActionsJson = SessionHelper.GetObjectFromJson(HttpContext.Session, "userInfo");
            var controllersActions = JsonConvert.DeserializeObject<List<Tuple<ApiControllerDescription, List<string>>>>(controllersActionsJson);
            foreach (var controllerAction in controllersActions)
            {
                if (controllerAction.Item1.ToString() == "UserDefinition")
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
                                ApiUrl = new Uri(httpClient.BaseAddress, "TicketState/Update"),
                                RequestDomain = RemoteIncomingDomains.DiasTesisYonetimWebClient,
                                RequestDtosTypes = new List<Type>() { typeof(UserHelperDto) },
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
                            var response = JsonConvert.DeserializeObject<UserHelperDto>(businessLogicRequestResponse.OptionalJsonResult);
                            return new OkResult();
                        }
                    }
                }
            }
            _notyfService.Error("Ýþ Emrine Durumu Güncelleme yetkiniz yoktur.");
            return new JsonResult(new UserHelperDto());
        }
        public async Task<IActionResult> OnDeleteGridRowDelete(UserHelperDto model)
        {
            var controllersActionsJson = SessionHelper.GetObjectFromJson(HttpContext.Session, "userInfo");
            var controllersActions = JsonConvert.DeserializeObject<List<Tuple<ApiControllerDescription, List<string>>>>(controllersActionsJson);
            foreach (var controllerAction in controllersActions)
            {
                if (controllerAction.Item1.ToString() == "UserDefinition")
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
                                ApiUrl = new Uri(httpClient.BaseAddress, "TicketState/Delete"),
                                RequestDomain = RemoteIncomingDomains.DiasTesisYonetimWebClient,
                                RequestDtosTypes = new List<Type>() { typeof(UserHelperDto) },
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
                            var response = JsonConvert.DeserializeObject<UserHelperDto>(businessLogicRequestResponse.OptionalJsonResult);
                            return new OkResult();
                        }
                    }
                }
            }
            _notyfService.Error("Ýþ Emrine Durumu Silme yetkiniz yoktur.");
            return new JsonResult(new UserHelperDto());
        }
        public async Task<IActionResult> OnGetRoleData(DataSourceLoadOptions loadOptions)
        {

            List<RoleHelperDto> roles = new List<RoleHelperDto>()
            {
                new RoleHelperDto(){Id=1,Name="Rol1" },
                new RoleHelperDto(){Id=1,Name="Rol2" },
                new RoleHelperDto(){Id=1,Name="Rol3" }
            };



            //var controllersActionsJson = SessionHelper.GetObjectFromJson(HttpContext.Session, "userInfo");
            //var controllersActions = JsonConvert.DeserializeObject<List<Tuple<ApiControllerDescription, List<string>>>>(controllersActionsJson);
            //foreach (var controllerAction in controllersActions)
            //{
            //    if (controllerAction.Item1.ToString() == "TicketPriority")
            //    {
            //        foreach (var action in controllerAction.Item2)
            //        {
            //            if (action == "GetAll")
            //            {
            //                HttpClient httpClient = new();
            //                httpClient.BaseAddress = new Uri("http://localhost:33400/");
            //                BusinessLogicRequest request = new()
            //                {
            //                    ApiUrl = new Uri(httpClient.BaseAddress, "LocationCode/GetAll"),
            //                    RequestDomain = RemoteIncomingDomains.DiasTesisYonetimWebClient
            //                };
            //                var result = httpClient.PostAsJsonAsync(request.ApiUrl, request).Result;
            //                var response = result.Content.ReadAsStringAsync().Result;
            //                BusinessLogicResponse businessLogicRequestResponse = JsonConvert.DeserializeObject<BusinessLogicResponse>(response);
            //                var ticketStateList = businessLogicRequestResponse.RelevantDto.ToString();
            //                var ticketStates = JsonConvert.DeserializeObject<List<StandartDTO.TicketPriorityDto>>(ticketStateList);
            return new JsonResult(DataSourceLoader.Load(roles, loadOptions));
            //            }
            //        }
            //    }
            //}
            //return new JsonResult(DataSourceLoader.Load(new List<StandartDTO.TicketPriorityDto>(), loadOptions));
        }
        private async Task<IActionResult> LoadGrid(DataSourceLoadOptions loadOptions, string token)
        {
            try
            {
                HttpClient httpClient = new();
                httpClient.BaseAddress = new Uri("http://localhost:33400/");

                DevExpressRequest gridRequest = null;

                if ((loadOptions.Filter != null) || (loadOptions.Skip != 0) || (loadOptions.Take > 0))
                {
                    DataSourceLoadOptions clonedDataSourceLoadOptions = new();
                    loadOptions.CopyProperties(((object)(clonedDataSourceLoadOptions)));

                    gridRequest = new DevExpressRequest()
                    {
                        AdditionalDevExpressParamJson = JsonConvert.SerializeObject(clonedDataSourceLoadOptions),
                        RequestOptions = new((DataSourceLoadOptions)(clonedDataSourceLoadOptions))
                    };

                    gridRequest.RequestOptions.DataSourceLoadOption.RequireTotalCount = true;
                }

                BusinessLogicRequest request = new()
                {
                    ApiUrl = new Uri(httpClient.BaseAddress, "LocationCode/GetAll"),
                    DevExpressRequestObj = gridRequest,
                    RequestDomain = RemoteIncomingDomains.DiasTesisYonetimWebClient,
                    jwt = token
                };

                JsonSerializerOptions options = new JsonSerializerOptions();
                options.Converters.Add(new CustomJsonConverterForType());

                HttpResponseMessage result = await httpClient.PostAsJsonAsync(request.ApiUrl, request, options);

                if ((gridRequest == null) || (gridRequest.RequestOptions == null) || (gridRequest.RequestOptions.DataSourceLoadOption == null))
                {
                    return ReadLoadedGrid(result, loadOptions);
                }
                else
                {
                    gridRequest.RequestOptions.DataSourceLoadOption.Skip = 0;
                    return ReadLoadedGrid(result, gridRequest.RequestOptions.DataSourceLoadOption);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private IActionResult ReadLoadedGrid(HttpResponseMessage result, DataSourceLoadOptions loadOptions)
        {
            try
            {
                List<UserHelperDto> listModel = new();

                using (result)
                {
                    string data = result.Content.ReadAsStringAsync().Result;
                    BusinessLogicResponse businessLogicResponse = JsonConvert.DeserializeObject<BusinessLogicResponse>(data);

                    if (businessLogicResponse.ErrorObj.BusinessOperationSucceed == true)
                    {
                        _notyfService.Success($"{businessLogicResponse.ErrorObj.DisplayMessage}");
                    }
                    else
                    {
                        _notyfService.Error($"{businessLogicResponse.ErrorObj.DisplayMessage}");
                        return new JsonResult(DataSourceLoader.Load(new List<UserHelperDto>(), loadOptions));
                    }

                    string myList = businessLogicResponse.RelevantDto.ToString();
                    listModel = JsonConvert.DeserializeObject<List<UserHelperDto>>(myList);


                    string user = SessionHelper.GetObjectFromJson(HttpContext.Session, "user");
                    UserDto userDto = JsonConvert.DeserializeObject<UserDto>(user);
                    int loginUserId = Convert.ToInt32(userDto.Id);

                    LoadResult dsLoad = DataSourceLoader.Load(listModel, loadOptions);

                    if (businessLogicResponse.DevExpressFilteredAndOrPaginatedResultMetadata != null)
                    {
                        dsLoad.totalCount = businessLogicResponse.DevExpressFilteredAndOrPaginatedResultMetadata.totalCount;
                    }

                    return new JsonResult(dsLoad);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
