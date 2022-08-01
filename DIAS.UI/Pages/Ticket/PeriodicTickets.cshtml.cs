using AspNetCoreHero.ToastNotification.Abstractions;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using DIAS_UI.Helpers;
using DiasShared.Operations.JsonOperation.Converters;
using DiasShared.Operations.ReflectionOperations;
using DiasShared.Services.Communication.BusinessLogicMessage;
using DiasShared.Services.Communication.BusinessLogicMessage.ThirdPartyLibrary.DevExpress;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static DiasShared.Enums.Standart.AuthorizationEnums;
using static DiasShared.Enums.Standart.RemoteCommunicationEnums;
using CustomDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;
using StandartDTO = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using CustomDTO = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using Microsoft.Extensions.Caching.Memory;
using DevExtreme.AspNet.Data.ResponseModel;

namespace DIAS.UI.Pages.Ticket
{
    public class CustomPeriodicTicketDto : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly INotyfService _notyfService;
        private readonly IMemoryCache _cache;
        public CustomPeriodicTicketDto(IConfiguration configuration, INotyfService notyfService,IMemoryCache cache)
        {
            _configuration = configuration;
            _notyfService = notyfService;
            _cache = cache;
        }

        public async void OnGet()
        {
            var dproducts = SessionHelper.GetObjectFromJson(HttpContext.Session, "userInfo");
            var controllersActions = JsonConvert.DeserializeObject<List<Tuple<ApiControllerDescription, List<string>>>>(dproducts);
            foreach (var item in controllersActions)
            {
                if (item.Item1.ToString() == "LocationWrapper")
                {
                    foreach (var item2 in item.Item2)
                    {
                        if (item2 == "GetAll")
                        {
                            DataSourceLoadOptions loadOptions = new DataSourceLoadOptions();
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
                            var locations = JsonConvert.DeserializeObject<List<DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom.CustomLocationDto>>(locationList);
                            Locations = locations;
                        }
                    }
                }
                if (item.Item1.ToString() == "TicketReasonCategoryWrapper")
                {
                    foreach (var action in item.Item2)
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
                            Reasons = reasonCategories;
                        }
                    }
                }

                if (item.Item1.ToString() == "TicketPriority")
                {
                    foreach (var action in item.Item2)
                    {
                        if (action == "GetAll")
                        {
                            HttpClient httpClient2 = new();
                            httpClient2.BaseAddress = new Uri("http://localhost:33400/");
                            BusinessLogicRequest request2 = new()
                            {
                                ApiUrl = new Uri(httpClient2.BaseAddress, "TicketPriority/GetAll"),
                                RequestDomain = RemoteIncomingDomains.DiasTesisYonetimWebClient
                            };
                            var result2 = httpClient2.PostAsJsonAsync(request2.ApiUrl, request2).Result;
                            var response2 = result2.Content.ReadAsStringAsync().Result;
                            BusinessLogicResponse businessLogicRequestResponse2 = JsonConvert.DeserializeObject<BusinessLogicResponse>(response2);
                            var ticketPriorityList = businessLogicRequestResponse2.RelevantDto.ToString();
                            var ticketPriorities = JsonConvert.DeserializeObject<List<StandartDTO.TicketPriorityDto>>(ticketPriorityList);
                            TicketPriorities = ticketPriorities;
                        }
                    }
                }
            }
        }

        [ViewData]
        public List<CustomDTO.CustomLocationDto> Locations { get; set; }
        [ViewData]
        public List<StandartDTO.TicketPriorityDto> TicketPriorities { get; set; }

        [ViewData]
        public List<CustomDTO.CustomTicketReasonCategoryDto> Reasons { get; set; }

        public async Task<IActionResult> OnGetGridData(DataSourceLoadOptions loadOptions)
        {

            //HttpClient httpClient = new HttpClient();
            ////TODO: Bu daha sonra konfigürasyondan alınacak
            //httpClient.BaseAddress = new Uri("http://localhost:33400/");
            //List<CustomDto.CustomPeriodicTicketDto> customTicketDto = new();

            //DevExpressRequest gridRequest = null;

            ////ilk yükleme mi?
            ////TODO: Bu bypass ileride değişebilir
            //if ((loadOptions.Filter != null) || (loadOptions.Skip != 0))
            //{
            //    //ilk yükleme değil
            //    //DataSourceLoadOptions ı deep copy clone yap
            //    DataSourceLoadOptions clonedDataSourceLoadOptions = new DataSourceLoadOptions();
            //    loadOptions.CopyProperties(((object)(clonedDataSourceLoadOptions)));

            //    gridRequest = new DevExpressRequest()
            //    {
            //        AdditionalDevExpressParamJson = JsonConvert.SerializeObject(clonedDataSourceLoadOptions),
            //        RequestOptions = new((DataSourceLoadOptions)(clonedDataSourceLoadOptions))
            //    };
            //}

            //BusinessLogicRequest request = new BusinessLogicRequest()
            //{
            //    //TODO: Bunu konfigürasyondan alacak 
            //    ApiUrl = new Uri(httpClient.BaseAddress, "Periodicticketwrapper/GetAll"),
            //    DevExpressRequestObj = gridRequest,
            //    RequestDomain = RemoteIncomingDomains.DiasTesisYonetimWebClient               
            //};

            //var result = httpClient.PostAsJsonAsync(request.ApiUrl, request).Result;
            //var returnModel = result.Content.ReadAsStringAsync().Result;
            //BusinessLogicResponse businessLogicRequestResponse = JsonConvert.DeserializeObject<BusinessLogicResponse>(returnModel);
            //var response = JsonConvert.DeserializeObject(returnModel);
            //if (businessLogicRequestResponse.ErrorObj.BusinessOperationSucceed == true)
            //{
            //    _notyfService.Success($"{businessLogicRequestResponse.ErrorObj.DisplayMessage}");
            //}
            //else
            //{
            //    _notyfService.Error($"{businessLogicRequestResponse.ErrorObj.DisplayMessage}");
            //    return new JsonResult(DataSourceLoader.Load(new List<CustomDto.CustomPeriodicTicketDto>(), loadOptions));
            //}
            //var myList = businessLogicRequestResponse.RelevantDto.ToString();
            //var listModel = JsonConvert.DeserializeObject<List<CustomDto.CustomPeriodicTicketDto>>(myList);


            //return new JsonResult(DataSourceLoader.Load(listModel, loadOptions));
            if (loadOptions.Take == 0)
            {
                var loadoptionsTake = _cache.Get("loadOptionsTakeFastTicket");
                int loadoptionsTakeValue = (int)loadoptionsTake;
                loadOptions.Take = loadoptionsTakeValue;
            }
            if (loadOptions.Take >= 10)
            {
                _cache.Set("loadOptionsTakeFastTicket", loadOptions.Take);
            }

            var controllersActionsJson = SessionHelper.GetObjectFromJson(HttpContext.Session, "userInfo");
            var controllersActions = JsonConvert.DeserializeObject<List<Tuple<ApiControllerDescription, List<string>>>>(controllersActionsJson);
            List<CustomDTO.CustomTicketDto> listModel = new();

            foreach (var controllerAction in controllersActions)
            {
                if (controllerAction.Item1.ToString() == "TicketWrapper")
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

            return new JsonResult(DataSourceLoader.Load(new List<CustomDTO.CustomTicketDto>(), loadOptions));


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
                    ApiUrl = new Uri(httpClient.BaseAddress, "PeriodicTicketWrapper/GetAll"),
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
                List<CustomDTO.CustomPeriodicTicketDto> listModel = new();

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
                        return new JsonResult(DataSourceLoader.Load(new List<CustomDTO.CustomPeriodicTicketDto>(), loadOptions));
                    }

                    string myList = businessLogicResponse.RelevantDto.ToString();
                    listModel = JsonConvert.DeserializeObject<List<CustomDTO.CustomPeriodicTicketDto>>(myList);
                    
                    string user = SessionHelper.GetObjectFromJson(HttpContext.Session, "user");
                    StandartDTO.UserDto userDto = JsonConvert.DeserializeObject<StandartDTO.UserDto>(user);
                    int loginUserId = Convert.ToInt32(userDto.Id);                                                
                    _cache.Set("customTicketDtos", listModel);

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
        public async Task<IActionResult> OnPostGridRow(CustomDto.CustomPeriodicTicketDto model)
        {
            HttpClient httpClient = new();
            httpClient.BaseAddress = new Uri("http://localhost:33400/");           

            //System.Text.Json ayarları
            var options = new JsonSerializerOptions();
            options.Converters.Add(new CustomJsonConverterForType());

            BusinessLogicRequest request = new()
            {
                //TODO: Bunu konfigürasyondan alacak 
                ApiUrl = new Uri(httpClient.BaseAddress, "PeriodicTicketWrapper/Insert"),
                RequestDomain = RemoteIncomingDomains.DiasTesisYonetimWebClient,
                //Burada Dto 2 lisini(RequestDtosTypes, RequestDtosJsons) dolduracağız
                RequestDtosTypes = new List<Type>() { typeof(CustomDto.CustomPeriodicTicketDto) },
                //Newtonsoft ile serialize eder
                RequestDtosJsons = new List<string>() {
                    JsonConvert.SerializeObject(model,
                          Formatting.None,
                            new JsonSerializerSettings {
                                NullValueHandling = NullValueHandling.Ignore
                            })}
            };

            //System.Text.Json ile gönderir!
            var result = httpClient.PostAsJsonAsync(request.ApiUrl, request, options ).Result;

            var resultModel = result.Content.ReadAsStringAsync().Result;
            BusinessLogicResponse businessLogicRequestResponse = JsonConvert.DeserializeObject<BusinessLogicResponse>(resultModel);
            var response = JsonConvert.DeserializeObject<CustomDto.CustomPeriodicTicketDto>(businessLogicRequestResponse.OptionalJsonResult);
            if (businessLogicRequestResponse.ErrorObj.BusinessOperationSucceed == true)
            {
                _notyfService.Success($"{businessLogicRequestResponse.ErrorObj.DisplayMessage}");
            }
            else
            {
                _notyfService.Error($"{businessLogicRequestResponse.ErrorObj.DisplayMessage}");
                return new BadRequestResult();
            }
            return new OkResult();
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

                            //İHTİYAÇ HALİNDE KULLANILABİLİR
                            //Locations = locations;
                            return new JsonResult(DataSourceLoader.Load(locations, loadOptions));
                        }
                    }
                }
            }
            return new JsonResult(DataSourceLoader.Load(new List<CustomDTO.CustomLocationDto>(), loadOptions));
        }

        public async Task<IActionResult> OnGetUserData(DataSourceLoadOptions loadOptions)
        {
            HttpClient httpClient = new();
            var result = httpClient.GetAsync("http://localhost:33400/Users/GetAllUsers").Result;
            var response = result.Content.ReadAsStringAsync().Result;
            BusinessLogicResponse businessLogicRequestResponse = JsonConvert.DeserializeObject<BusinessLogicResponse>(response);
            var userList = businessLogicRequestResponse.RelevantDto.ToString();
            var users = JsonConvert.DeserializeObject<List<DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard.UserDto>>(userList);
            return new JsonResult(DataSourceLoader.Load(users, loadOptions));
        }

        public async Task<IActionResult> OnGetTicketPriorityData(DataSourceLoadOptions loadOptions)
        {
            HttpClient httpClient = new();
            httpClient.BaseAddress = new Uri("http://localhost:33400/");
            BusinessLogicRequest request = new()
            {
                ApiUrl = new Uri(httpClient.BaseAddress, "TicketPriority/GetAll"),
                RequestDomain = RemoteIncomingDomains.DiasTesisYonetimWebClient
            };

            var result = httpClient.PostAsJsonAsync(request.ApiUrl, request).Result;
            var response = result.Content.ReadAsStringAsync().Result;
            BusinessLogicResponse businessLogicRequestResponse = JsonConvert.DeserializeObject<BusinessLogicResponse>(response);
            var ticketStateList = businessLogicRequestResponse.RelevantDto.ToString();
            var ticketStates = JsonConvert.DeserializeObject<List<StandartDTO.TicketPriorityDto>>(ticketStateList);
            return new JsonResult(DataSourceLoader.Load(ticketStates, loadOptions));
        }

        public async Task<IActionResult> OnGetTicketStateData(DataSourceLoadOptions loadOptions)
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
                                ApiUrl = new Uri(httpClient.BaseAddress, "TicketState/GetAll"),
                                RequestDomain = RemoteIncomingDomains.DiasTesisYonetimWebClient
                            };
                            var result = httpClient.PostAsJsonAsync(request.ApiUrl, request).Result;
                            var response = result.Content.ReadAsStringAsync().Result;
                            BusinessLogicResponse businessLogicRequestResponse = JsonConvert.DeserializeObject<BusinessLogicResponse>(response);
                            var ticketStateList = businessLogicRequestResponse.RelevantDto.ToString();
                            var ticketStates = JsonConvert.DeserializeObject<List<StandartDTO.TicketStateDto>>(ticketStateList);

                            //CACHE DE TUTMAYA GEREK OLUNCA KULLANILIR
                            //_cache.Set("ticketStates", ticketStates);
                            return new JsonResult(DataSourceLoader.Load(ticketStates, loadOptions));
                        }
                    }
                }
            }
            return new JsonResult(DataSourceLoader.Load(new List<StandartDTO.TicketStateDto>(), loadOptions));
        }

        public async Task<IActionResult> OnGetTicketAssignmentGroupDataGetAll(DataSourceLoadOptions loadOptions)
        {
            HttpClient httpClient = new();
            httpClient.BaseAddress = new Uri("http://localhost:33400/");
            BusinessLogicRequest request = new()
            {
                ApiUrl = new Uri(httpClient.BaseAddress, "AssignmentGroup/GetAll"),
                RequestDomain = RemoteIncomingDomains.DiasTesisYonetimWebClient
            };
            var result = httpClient.PostAsJsonAsync(request.ApiUrl, request).Result;
            var response = result.Content.ReadAsStringAsync().Result;
            BusinessLogicResponse businessLogicRequestResponse = JsonConvert.DeserializeObject<BusinessLogicResponse>(response);
            var assignmentGroupList = businessLogicRequestResponse.RelevantDto.ToString();
            var assignmentListCategories = JsonConvert.DeserializeObject<List<StandartDTO.AssignmentGroupDto>>(assignmentGroupList);
            return new JsonResult(DataSourceLoader.Load(assignmentListCategories, loadOptions));
        }
    }
}
