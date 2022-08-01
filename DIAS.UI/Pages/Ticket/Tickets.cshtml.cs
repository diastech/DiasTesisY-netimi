using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using DIAS.UI.Helpers;
using DIAS_UI.Helpers;
using DiasShared.Data.JsonData.InputOutput.Filters.Web.DevExpress.Development;
using DiasShared.Errors;
using DiasShared.Operations.EnumOperations;
using DiasShared.Operations.FilteringOperation.DevExpress.Web;
using DiasShared.Operations.JsonOperation.Converters;
using DiasShared.Operations.ReflectionOperations;
using DiasShared.Services.Communication.BusinessLogicMessage;
using DiasShared.Services.Communication.BusinessLogicMessage.ThirdPartyLibrary.DevExpress;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static DiasShared.Enums.Standart.AuthorizationEnums;
using static DiasShared.Enums.Standart.RemoteCommunicationEnums;
using static DiasShared.Enums.Standart.TicketEnums;
using CustomDTO = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using StandartDTO = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;

namespace DIAS.UI.Pages.Ticket
{
    public class CustomTicketDto : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private IHostingEnvironment _environment;
        private readonly INotyfService _notyfService;
        private readonly IMemoryCache _cache;

        public CustomTicketDto(IConfiguration configuration, IWebHostEnvironment webHostEnvironment, INotyfService notyfService, IHostingEnvironment environment, IMemoryCache cache)
        {
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
            _notyfService = notyfService;
            _environment = environment;
            _cache = cache;
        }

        [ViewData]
        public StandartDTO.UserDto UserViewData { get; set; }

        [ViewData]
        public string Title { get; set; }

        [ViewData]
        public List<CustomDTO.CustomLocationDto> Locations { get; set; }
        [ViewData]
        public List<StandartDTO.TicketPriorityDto> TicketPriorities { get; set; }

        [BindProperty]
        public IFormFile NotesFile { get; set; }

        public List<CustomDTO.CustomTicketDto> customTicketDtos { get; set; }

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
                            _cache.Set("locations", Locations);
                            _cache.Set("loadOptionsTake", 10);


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
        //int loadOptionsTakeValue = 10;
        public async Task<IActionResult> OnGetGridData(DataSourceLoadOptions loadOptions)
       {
            if (loadOptions.Filter != null)
            {
                for (int i = 0; i < loadOptions.Filter.Count; i++)
                {
                    
                    if (!String.IsNullOrEmpty(loadOptions.Filter[i].ToString()))
                    {
                        if(loadOptions.Filter.Count == 21)
                        {
                            var a = JsonConvert.DeserializeObject<IList>(loadOptions.Filter[i].ToString());
                            if (a[0].ToString() == "filter")
                            {
                                TicketBasePageFilterDto ticketBasePageFilterDto = new();
                                return await OnPostFilter(ticketBasePageFilterDto, loadOptions);
                            }
                            else
                            {
                                string b = Title;
                                if (loadOptions.Take == 0)
                                {
                                    var loadoptionsTake = _cache.Get("loadOptionsTake");
                                    int loadoptionsTakeValue = (int)loadoptionsTake;
                                    loadOptions.Take = loadoptionsTakeValue;
                                }
                                if (loadOptions.Take >= 10)
                                {
                                    _cache.Set("loadOptionsTake", loadOptions.Take);
                                }
                                //return BadRequest();
                                var controllersActionsJson = SessionHelper.GetObjectFromJson(HttpContext.Session, "userInfo");
                                var controllersActions = JsonConvert.DeserializeObject<List<Tuple<ApiControllerDescription, List<string>>>>(controllersActionsJson);

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
                        }
                        else
                        {
                            string b = Title;
                            if (loadOptions.Take == 0)
                            {
                                var loadoptionsTake = _cache.Get("loadOptionsTake");
                                int loadoptionsTakeValue = (int)loadoptionsTake;
                                loadOptions.Take = loadoptionsTakeValue;
                            }
                            if (loadOptions.Take >= 10)
                            {
                                _cache.Set("loadOptionsTake", loadOptions.Take);
                            }
                            //return BadRequest();
                            var controllersActionsJson = SessionHelper.GetObjectFromJson(HttpContext.Session, "userInfo");
                            var controllersActions = JsonConvert.DeserializeObject<List<Tuple<ApiControllerDescription, List<string>>>>(controllersActionsJson);

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
                        
                        
                    }

                }
            }

            else
            {
                string b = Title;
                if (loadOptions.Take == 0)
                {
                    var loadoptionsTake = _cache.Get("loadOptionsTake");
                    int loadoptionsTakeValue = (int)loadoptionsTake;
                    loadOptions.Take = loadoptionsTakeValue;
                }
                if (loadOptions.Take >= 10)
                {
                    _cache.Set("loadOptionsTake", loadOptions.Take);
                }
                //return BadRequest();
                var controllersActionsJson = SessionHelper.GetObjectFromJson(HttpContext.Session, "userInfo");
                var controllersActions = JsonConvert.DeserializeObject<List<Tuple<ApiControllerDescription, List<string>>>>(controllersActionsJson);

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


            return new JsonResult(DataSourceLoader.Load(new List<CustomDTO.CustomTicketDto>(), loadOptions));
        }

        public async Task<IActionResult> OnPostGridRow(CustomDTO.CustomTicketDto model)
        {
            var controllersActionsJson = SessionHelper.GetObjectFromJson(HttpContext.Session, "userInfo");
            var controllersActions = JsonConvert.DeserializeObject<List<Tuple<ApiControllerDescription, List<string>>>>(controllersActionsJson);
            foreach (var controllerAction in controllersActions)
            {
                if (controllerAction.Item1.ToString() == "TicketWrapper")
                {
                    foreach (var action in controllerAction.Item2)
                    {
                        if (action == "Insert")
                        {
                            model.TicketOpenedTime = model.TicketOpenedTime.AddHours(3);
                            model.ExpectedResolutionTime = model.ExpectedResolutionTime.AddHours(3);
                            model.ExpectedResponseTime = model.ExpectedResponseTime.AddHours(3);
                            var match = Regex.Match(model.TicketReasonHierarchyId, @"\/[^\/]*\/", RegexOptions.RightToLeft);
                            model.TicketReasonId = Convert.ToInt32(match.Value.Replace("/", ""));

                            HttpClient httpClient = new();
                            httpClient.BaseAddress = new Uri("http://localhost:33400/");
                            var options = new JsonSerializerOptions();
                            options.Converters.Add(new CustomJsonConverterForType());

                            List<StandartDTO.TicketRelatedLocationDto> relatedLocationList = new();
                            string[] hierarchy = model.TicketRelatedLocationHierarchyId[0].Split(",");

                            foreach (var item in hierarchy)
                            {
                                BusinessLogicRequest request2 = new()
                                {
                                    ApiUrl = new Uri(httpClient.BaseAddress, "LocationWrapper/GetById"),
                                    RequestDomain = RemoteIncomingDomains.DiasTesisYonetimWebClient,
                                    AdditionalParamTypes = new() { typeof(string) },
                                    AdditionalParamJsons = new() { JsonConvert.SerializeObject(item) }
                                };
                                var resultLocation = httpClient.PostAsJsonAsync(request2.ApiUrl, request2, options).Result;
                                var locationModel = resultLocation.Content.ReadAsStringAsync().Result;
                                BusinessLogicResponse businessLogicRequestResponseLocation = JsonConvert.DeserializeObject<BusinessLogicResponse>(locationModel);
                                var responseLocation = JsonConvert.DeserializeObject<StandartDTO.LocationV2Dto>(businessLogicRequestResponseLocation.OptionalJsonResult);
                                StandartDTO.TicketRelatedLocationDto ticketRelatedLocationDto = new();
                                ticketRelatedLocationDto.TicketLocationId = responseLocation.Id;
                                ticketRelatedLocationDto.AddedByUserId = model.AddedByUserId;
                                relatedLocationList.Add(ticketRelatedLocationDto);
                            }
                            model.TicketRelatedLocations = relatedLocationList;
                            if (string.IsNullOrWhiteSpace(_webHostEnvironment.WebRootPath))
                            {
                                _webHostEnvironment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "ticketNotes");
                            }
                            var path = Path.Combine(_webHostEnvironment.WebRootPath, "fileUploads");
                            if (!Directory.Exists(path))
                                Directory.CreateDirectory(path);
                            List<StandartDTO.AttachmentDto> attachmentDtos = new();
                            if (model.AttachmentsFile != null)
                            {
                                foreach (var item in model.AttachmentsFile)
                                {
                                    StandartDTO.AttachmentDto attachment = new();
                                    var newAttachmentFileName = $"N_{DateTime.Now.ToString("ddMMyyyy")}_{item.FileName}";
                                    attachment.FolderName = newAttachmentFileName;
                                    attachment.FileType = item.ContentType;
                                    attachment.AttachmentDescription = "deneme" + DateTime.Now.ToString("ddMMyyyy");

                                    using (var ms = new MemoryStream())
                                    {
                                        item.CopyTo(ms);
                                        var fileBytes = ms.ToArray();
                                        string s = Convert.ToBase64String(fileBytes);
                                        byte[] buffer = Encoding.ASCII.GetBytes(s);
                                        attachment.FileData = buffer;
                                    }
                                    attachmentDtos.Add(attachment);
                                }
                            }
                            if (model.NotesFile != null)
                            {
                                if (!Directory.Exists(path))
                                    Directory.CreateDirectory(path);
                                var newNotesFileName = $"N_{DateTime.Now.ToString("ddMMyyyy")}_{model.NotesFile.FileName}";
                                using (var fileStream = System.IO.File.Create(Path.Combine(path, newNotesFileName)))
                                {
                                    await model.NotesFile.CopyToAsync(fileStream);
                                }
                                List<StandartDTO.AttachmentDto> attachmentNoteDto = new();
                                StandartDTO.AttachmentDto attachmentNote = new StandartDTO.AttachmentDto
                                {
                                    FolderName = newNotesFileName,
                                    AttachmentDescription = "Added via AddBasicTicketAttachment Method",
                                };
                                attachmentNoteDto.Add(attachmentNote);

                                model.NotesAttachment = attachmentNoteDto;
                            }
                            model.NotesFile = null;
                            model.Attachments = attachmentDtos;
                            model.AttachmentsFile = null;
                            model.TicketRelatedLocationHierarchyId = null;
                            model.TicketReasonHierarchyId = null;

                            if (model.TicketAssignedUserId != null)
                            {
                                model.TicketStatusId = 2;
                            }
                            else
                            {
                                model.TicketStatusId = 1;
                            }

                            var user = SessionHelper.GetObjectFromJson(HttpContext.Session, "user");
                            var userDto = JsonConvert.DeserializeObject<StandartDTO.UserDto>(user);
                            int loginUserId = Convert.ToInt32(userDto.Id);

                            model.AddedByUserId = loginUserId;
                            BusinessLogicRequest request = new()
                            {
                                ApiUrl = new Uri(httpClient.BaseAddress, "TicketWrapper/Insert"),
                                RequestDomain = RemoteIncomingDomains.DiasTesisYonetimWebClient,
                                RequestDtosTypes = new List<Type>() { typeof(CustomDTO.CustomTicketDto) },
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
                            var response = JsonConvert.DeserializeObject<CustomDTO.CustomTicketDto>(businessLogicRequestResponse.OptionalJsonResult);
                            if (businessLogicRequestResponse.ErrorObj.BusinessOperationSucceed == true)
                            {
                                _notyfService.Success($"{businessLogicRequestResponse.ErrorObj.DisplayMessage}");
                            }
                            else
                            {
                                _notyfService.Error($"{businessLogicRequestResponse.ErrorObj.DisplayMessage}");
                                return new OkResult();
                            }
                            return new OkResult();
                        }
                    }
                }
            }
            _notyfService.Error("İş Emri Ekleme Yetkiniz bulunmamaktadır.");
            return new OkResult();
        }

        public async Task<IActionResult> OnPutGridRowUpdate(CustomDTO.CustomTicketDto model)
        {
            var controllersActionsJson = SessionHelper.GetObjectFromJson(HttpContext.Session, "userInfo");
            var controllersActions = JsonConvert.DeserializeObject<List<Tuple<ApiControllerDescription, List<string>>>>(controllersActionsJson);
            foreach (var controllerAction in controllersActions)
            {
                if (controllerAction.Item1.ToString() == "TicketWrapper")
                {
                    foreach (var action in controllerAction.Item2)
                    {
                        if (action == "Update")
                        {
                            model.TicketOpenedTime = model.TicketOpenedTime.AddHours(3);
                            model.ExpectedResolutionTime = model.ExpectedResolutionTime.AddHours(3);
                            model.ExpectedResponseTime = model.ExpectedResponseTime.AddHours(3);
                            HttpClient httpClient = new();
                            httpClient.BaseAddress = new Uri("http://localhost:33400/");

                            var options = new JsonSerializerOptions();
                            options.Converters.Add(new CustomJsonConverterForType());
                            var match = Regex.Match(model.TicketReasonHierarchyId, @"\/[^\/]*\/", RegexOptions.RightToLeft);
                            var ticketId = Convert.ToInt32(match.Value.Replace("/", ""));
                            model.TicketReasonId = ticketId;
                            BusinessLogicRequest requestGetById = new()
                            {
                                ApiUrl = new Uri(httpClient.BaseAddress, "TicketWrapper/GetById/" + model.Id),
                                RequestDomain = RemoteIncomingDomains.DiasTesisYonetimWebClient
                            };
                            var result = httpClient.PostAsJsonAsync(requestGetById.ApiUrl, requestGetById, options).Result;
                            var response = result.Content.ReadAsStringAsync().Result;
                            BusinessLogicResponse businessLogicRequestResponse = JsonConvert.DeserializeObject<BusinessLogicResponse>(response);
                            var ticket = businessLogicRequestResponse.RelevantDto.ToString();
                            var ticketResult = JsonConvert.DeserializeObject<CustomDTO.CustomTicketDto>(ticket);
                            List<StandartDTO.TicketRelatedLocationDto> relatedLocationlist = new();
                            string[] hierarchy = model.TicketRelatedLocationHierarchyId[0].Split(",");
                            foreach (var item in hierarchy)
                            {
                                BusinessLogicRequest request3 = new()
                                {
                                    ApiUrl = new Uri(httpClient.BaseAddress, "LocationWrapper/GetById"),
                                    RequestDomain = RemoteIncomingDomains.DiasTesisYonetimWebClient,
                                    AdditionalParamTypes = new() { typeof(string) },
                                    AdditionalParamJsons = new() { JsonConvert.SerializeObject(item) }
                                };
                                var resultLocation = httpClient.PostAsJsonAsync(request3.ApiUrl, request3, options).Result;
                                var locationModel = resultLocation.Content.ReadAsStringAsync().Result;
                                BusinessLogicResponse businessLogicRequestResponseLocation = JsonConvert.DeserializeObject<BusinessLogicResponse>(locationModel);
                                var responseLocation = JsonConvert.DeserializeObject<DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard.LocationV2Dto>(businessLogicRequestResponseLocation.OptionalJsonResult);
                                StandartDTO.TicketRelatedLocationDto ticketRelatedLocationDto = new();
                                ticketRelatedLocationDto.TicketLocationId = responseLocation.Id;
                                ticketRelatedLocationDto.AddedByUserId = model.AddedByUserId;
                                relatedLocationlist.Add(ticketRelatedLocationDto);
                            }
                            model.TicketRelatedLocations = relatedLocationlist;
                            var path = Path.Combine(_webHostEnvironment.WebRootPath, "fileUploads");
                            var newNotesFileName = "";
                            if (model.NotesFile != null)
                            {
                                if (string.IsNullOrWhiteSpace(_webHostEnvironment.WebRootPath))
                                {
                                    _webHostEnvironment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "ticketNotes");
                                }
                                if (!Directory.Exists(path))
                                    Directory.CreateDirectory(path);
                                newNotesFileName = $"N_{DateTime.Now.ToString("ddMMyyyy")}_{model.NotesFile.FileName}";
                                using (var fileStream = System.IO.File.Create(Path.Combine(path, newNotesFileName)))
                                {
                                    await model.NotesFile.CopyToAsync(fileStream);
                                }
                                List<StandartDTO.AttachmentDto> attachmentNoteDto = new();
                                StandartDTO.AttachmentDto attachmentNote = new StandartDTO.AttachmentDto
                                {
                                    FolderName = newNotesFileName,
                                    AttachmentDescription = "Added via AddBasicTicketAttachment Method",
                                };
                                attachmentNoteDto.Add(attachmentNote);
                                model.NotesAttachment = attachmentNoteDto;
                            }
                            model.AttachmentsFile = null;
                            model.NotesFile = null;
                            model.TicketRelatedLocationHierarchyId = null;
                            model.TicketReasonHierarchyId = null;

                            if (model.TicketAssignedUserId != null)
                            {
                                model.TicketStatusId = 2;
                            }

                            var user = SessionHelper.GetObjectFromJson(HttpContext.Session, "user");
                            var userDto = JsonConvert.DeserializeObject<StandartDTO.UserDto>(user);
                            int loginUserId = Convert.ToInt32(userDto.Id);
                            model.LastModifiedByUserId = loginUserId;

                            BusinessLogicRequest requestUpdate = new()
                            {
                                ApiUrl = new Uri(httpClient.BaseAddress, "TicketWrapper/Update"),
                                RequestDomain = RemoteIncomingDomains.DiasTesisYonetimWebClient,
                                RequestDtosTypes = new List<Type>() { typeof(CustomDTO.CustomTicketDto) },
                                RequestDtosJsons = new List<string>() {
                                    JsonConvert.SerializeObject(model,
                                          Formatting.None,
                                            new JsonSerializerSettings {
                                                NullValueHandling = NullValueHandling.Ignore
                                            })}
                            };
                            var resultUpdate = httpClient.PostAsJsonAsync(requestUpdate.ApiUrl, requestUpdate, options).Result;
                            var resultUpdateModel = resultUpdate.Content.ReadAsStringAsync().Result;
                            BusinessLogicResponse businessLogicRequestResponseUpdate = JsonConvert.DeserializeObject<BusinessLogicResponse>(resultUpdateModel);
                            var responseUpdate = JsonConvert.DeserializeObject<CustomDTO.CustomTicketDto>(businessLogicRequestResponseUpdate.OptionalJsonResult);
                            if (businessLogicRequestResponseUpdate.ErrorObj.BusinessOperationSucceed == true)
                            {
                                _notyfService.Success($"{businessLogicRequestResponseUpdate.ErrorObj.DisplayMessage}");
                            }
                            else
                            {
                                _notyfService.Error($"{businessLogicRequestResponse.ErrorObj.DisplayMessage}");
                                return new OkResult();
                            }
                            return new OkResult();
                        }
                    }
                }
            }
            _notyfService.Error("İş Emri Güncelleme Yetkiniz Bulunmamaktadır.");
            return new JsonResult(new CustomDTO.CustomTicketDto());
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
                            Locations = locations;
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
                            var myEntry = _cache.Get("employeOfAsgGroup");
                            List<StandartDTO.AssignmentGroupEmployeeDto> list = new();
                            list = (List<StandartDTO.AssignmentGroupEmployeeDto>)myEntry;

                            HttpClient httpClient = new();
                            var result = httpClient.GetAsync("http://localhost:33400/Users/GetAllUsers").Result;
                            var response = result.Content.ReadAsStringAsync().Result;
                            BusinessLogicResponse businessLogicRequestResponse = JsonConvert.DeserializeObject<BusinessLogicResponse>(response);
                            var userList = businessLogicRequestResponse.RelevantDto.ToString();
                            var users = JsonConvert.DeserializeObject<List<StandartDTO.UserDto>>(userList);

                            if (list != null)
                            {
                                var filtered = users.Where(x => list.Any(y => y.EmployeeUserId.ToString() == x.Id)).ToList();
                                _cache.Remove("employeOfAsgGroup");
                                return new JsonResult(DataSourceLoader.Load(filtered, loadOptions));
                            }

                            return new JsonResult(DataSourceLoader.Load(users, loadOptions));
                        }
                    }
                }
            }
            return new JsonResult(DataSourceLoader.Load(new List<StandartDTO.UserDto>(), loadOptions));
        }

        public async Task<IActionResult> OnGetTicketAssignmentGroupData(DataSourceLoadOptions loadOptions)
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
            List<StandartDTO.AssignmentGroupEmployeeDto> assignmentGroupEmployeeModelList = new();
            BusinessLogicRequest requestEmployee = new()
            {
                ApiUrl = new Uri(httpClient.BaseAddress, "AssignmentGroupEmployee/GetAll"),
                RequestDomain = RemoteIncomingDomains.DiasTesisYonetimWebClient
            };
            var resultEmployee = httpClient.PostAsJsonAsync(requestEmployee.ApiUrl, requestEmployee).Result;
            var responseAssignedGroupEmployee = resultEmployee.Content.ReadAsStringAsync().Result;
            BusinessLogicResponse businessLogicRequestResponseEmployee = JsonConvert.DeserializeObject<BusinessLogicResponse>(responseAssignedGroupEmployee);
            var assignmentGroupEmployees = businessLogicRequestResponseEmployee.RelevantDto.ToString();
            var assignmentGroupEmployeeList = JsonConvert.DeserializeObject<List<StandartDTO.AssignmentGroupEmployeeDto>>(assignmentGroupEmployees);

            foreach (var assignmentGroup in assignmentListCategories)
            {
                StandartDTO.AssignmentGroupEmployeeDto assignmentGroupEmployee = new StandartDTO.AssignmentGroupEmployeeDto();
                assignmentGroupEmployee.Id = assignmentGroup.Id;
                assignmentGroupEmployee.HierarchyId = assignmentGroup.Id.ToString();
                assignmentGroupEmployee.Name = assignmentGroup.GroupName;
                assignmentGroupEmployeeModelList.Add(assignmentGroupEmployee);
                foreach (var assignmentGroupEmployeeItem in assignmentGroupEmployeeList)
                {
                    if (assignmentGroup.Id == assignmentGroupEmployeeItem.AssignmentGroupId)
                    {
                        StandartDTO.AssignmentGroupEmployeeDto kalem = new StandartDTO.AssignmentGroupEmployeeDto();
                        kalem.Id = assignmentGroupEmployeeItem.Id;
                        kalem.ParentHierarcyId = assignmentGroupEmployee.HierarchyId;
                        kalem.HierarchyId = assignmentGroupEmployee.HierarchyId + "_" + assignmentGroupEmployeeItem.Id;
                        kalem.Name = assignmentGroupEmployeeItem.EmployeeUser.FirstName + " " + assignmentGroupEmployeeItem.EmployeeUser.LastName;
                        kalem.EmployeeUserId = assignmentGroupEmployeeItem.EmployeeUserId;
                        kalem.EmployeeUser = null;
                        assignmentGroupEmployeeModelList.Add(kalem);
                    }
                }
            }
            _cache.Set("ticketassignGrpEmp", assignmentGroupEmployeeModelList);

            return new JsonResult(DataSourceLoader.Load(assignmentGroupEmployeeModelList, loadOptions));
        }

        public async Task<IActionResult> OnGetAspEmplooyeByAsgGroupId(int asgGroupId)
        {
            HttpClient httpClient = new();
            httpClient.BaseAddress = new Uri("http://localhost:33400/");
            BusinessLogicRequest requestEmployee = new()
            {
                ApiUrl = new Uri(httpClient.BaseAddress, "AssignmentGroupEmployee/GetById/" + asgGroupId),
                RequestDomain = RemoteIncomingDomains.DiasTesisYonetimWebClient,
            };
            var resultEmployee = httpClient.PostAsJsonAsync(requestEmployee.ApiUrl, requestEmployee).Result;
            var responseAssignedGroupEmployee = resultEmployee.Content.ReadAsStringAsync().Result;
            BusinessLogicResponse businessLogicRequestResponseEmployee = JsonConvert.DeserializeObject<BusinessLogicResponse>(responseAssignedGroupEmployee);
            var assignmentGroupEmployees = businessLogicRequestResponseEmployee.RelevantDto.ToString();
            var assignmentGroupEmployeeList = JsonConvert.DeserializeObject<List<StandartDTO.AssignmentGroupEmployeeDto>>(assignmentGroupEmployees);
            _cache.Set("employeOfAsgGroup", assignmentGroupEmployeeList);
            return new JsonResult(assignmentGroupEmployeeList);
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

                            _cache.Set("ticketStates", ticketStates);
                            return new JsonResult(DataSourceLoader.Load(ticketStates, loadOptions));
                        }
                    }
                }
            }
            return new JsonResult(DataSourceLoader.Load(new List<StandartDTO.TicketStateDto>(), loadOptions));
        }

        public async Task<IActionResult> OnGetTicketStateDataOption(DataSourceLoadOptions loadOptions, int ticketId)
        {
            var controllersActionsJson = SessionHelper.GetObjectFromJson(HttpContext.Session, "userInfo");
            var controllersActions = JsonConvert.DeserializeObject<List<Tuple<ApiControllerDescription, List<string>>>>(controllersActionsJson);
            foreach (var controllerAction in controllersActions)
            {
                if (controllerAction.Item1.ToString() == "TicketStateFlowWrapper")
                {
                    foreach (var action in controllerAction.Item2)
                    {
                        if (action == "GetAll")
                        {

                            var myEntry = _cache.Get("customTicketDtos");
                            List<StandartDTO.TicketStateDto> ticketStateDtos = new();
                            List<CustomDTO.CustomTicketDto> list = new();
                            list = (List<CustomDTO.CustomTicketDto>)myEntry;
                            List<int> listInt = new();
                            List<StandartDTO.TicketStateTransitionFlowDto> ticketStateFlowDtos = new();
                            List<StandartDTO.TicketStateTransitionFlowDto> ticketStateFlowDtosDuplicatesIncluded = new();
                            List<StandartDTO.TicketStateTransitionFlowDto> cacheTicketStateTransitionFlowDtos = new();
                            List<CustomDTO.CustomTicketStateFlowDto> customTicketStateFlowDto = new();
                            CustomDTO.CustomTicketDto ticketWorkingOn = new();

                            HttpClient httpClient2 = new();
                            httpClient2.BaseAddress = new Uri("http://localhost:33400/");
                            BusinessLogicRequest requestGetById = new()
                            {
                                ApiUrl = new Uri(httpClient2.BaseAddress, "TicketWrapper/GetByWithStatusId/" + ticketId),
                                RequestDomain = RemoteIncomingDomains.DiasTesisYonetimWebClient
                            };

                            var resultGetById = httpClient2.PostAsJsonAsync(requestGetById.ApiUrl, requestGetById).Result;
                            var responseGetById = resultGetById.Content.ReadAsStringAsync().Result;
                            BusinessLogicResponse businessLogicRequestResponseGetById = JsonConvert.DeserializeObject<BusinessLogicResponse>(responseGetById);
                            var ticket = businessLogicRequestResponseGetById.RelevantDto.ToString();
                            var ticketDto = JsonConvert.DeserializeObject<CustomDTO.CustomTicketDto>(ticket);

                            ticketWorkingOn = ticketDto;
                            listInt = ticketDto.TicketStatus.TicketStateTransitionSourceTicketStates.Select(x => x.Id).ToList();
                            HttpClient httpClient = new();
                            httpClient.BaseAddress = new Uri("http://localhost:33400/");
                            BusinessLogicRequest requestGetAll = new()
                            {
                                ApiUrl = new Uri(httpClient.BaseAddress, "TicketStateFlowWrapper/GetAll"),
                                RequestDomain = RemoteIncomingDomains.DiasTesisYonetimWebClient
                            };

                            var resultGetAll = httpClient.PostAsJsonAsync(requestGetAll.ApiUrl, requestGetAll).Result;
                            var responseGetAll = resultGetAll.Content.ReadAsStringAsync().Result;
                            BusinessLogicResponse businessLogicRequestResponseGetAll = JsonConvert.DeserializeObject<BusinessLogicResponse>(responseGetAll);
                            var ticketStateFlowList = businessLogicRequestResponseGetAll.RelevantDto.ToString();
                            var ticketStateFlowLists = JsonConvert.DeserializeObject<List<CustomDTO.CustomTicketStateFlowDto>>(ticketStateFlowList);
                            foreach (var item in ticketStateFlowLists)
                            {
                                foreach (var item2 in item.TicketStateTransitionFlowByTicketStateFlows)
                                {
                                    cacheTicketStateTransitionFlowDtos.Add(item2);
                                    foreach (var item3 in listInt)
                                    {
                                        if (item2.TicketStateTransitionId == item3)
                                        {
                                            //ticketStateFlowDtos.Add(item2);

                                            //Ticket yetkileri ile ikinci kez süzelim
                                            //Burada ekleyen ve/veya son düzenleyen kullanıcı bilgisi, login olan kullanıcı bilgisi ile karşılaştırılıyor
                                            //TODO: Buradaki iş kuralı veritabanında olduğundan ve dinamik olabildiğinden ileride bu iş kuralını veritabanından dinamik halde çekilecek hale getirelim
                                            //TODO : TicketStateTransitionEnum tanımlandıktan sonra item2.Id yerine enum kullanacağız


                                            var user = SessionHelper.GetObjectFromJson(HttpContext.Session, "user");
                                            var userDto = JsonConvert.DeserializeObject<StandartDTO.UserDto>(user);
                                            int loginUserId = Convert.ToInt32(userDto.Id); //Ayşe Yücel

                                            //TODO : ANONYMOUSTICKETREPORTER sisteme eklenebilir hale geldiğinde ANONYMOUSTICKETREPORTER kontrolü yapılacak

                                            //iş emri ne personele ne gruba atanmışsa veya sadece gruba atanmışşa herkes o iş emrini başkasına veya kendisine yönlendirebilsin.
                                            if (((!(ticketWorkingOn.TickedAssignedAssignmentGroupId.HasValue)) && ((!(ticketWorkingOn.TicketAssignedUserId.HasValue)))) ||
                                                  (((ticketWorkingOn.TickedAssignedAssignmentGroupId.HasValue) && ((!(ticketWorkingOn.TicketAssignedUserId.HasValue))))))
                                            {
                                                if (((item2.TicketStateFlowId.GetEnumValue<TicketStateFlowEnum>() == TicketStateFlowEnum.ASSIGNTOGROUPORUSER) && (item2.Id == 1)) ||
                                                     ((item2.TicketStateFlowId.GetEnumValue<TicketStateFlowEnum>() == TicketStateFlowEnum.ASSIGNTOGROUP) && (item2.Id == 2)) ||
                                                       ((item2.TicketStateFlowId.GetEnumValue<TicketStateFlowEnum>() == TicketStateFlowEnum.ASSIGNTOGROUPORUSER) && (item2.Id == 20)) ||
                                                         ((item2.TicketStateFlowId.GetEnumValue<TicketStateFlowEnum>() == TicketStateFlowEnum.ASSIGNTOGROUP) && (item2.Id == 21)))

                                                {
                                                    ticketStateFlowDtosDuplicatesIncluded.Add(item2);
                                                }
                                            }


                                            //if (loginUserId == ticketWorkingOn.TicketReportedUserId)
                                            //{
                                            //    if (((item2.TicketStateFlowId.GetEnumValue<TicketStateFlowEnum>() == TicketStateFlowEnum.ASSIGNTOGROUPORUSER) && (item2.Id == 1)) ||
                                            //         ((item2.TicketStateFlowId.GetEnumValue<TicketStateFlowEnum>() == TicketStateFlowEnum.ASSIGNTOGROUP) && (item2.Id == 2)) ||
                                            //           ((item2.TicketStateFlowId.GetEnumValue<TicketStateFlowEnum>() == TicketStateFlowEnum.SUSPEND) && (item2.Id == 22)) ||
                                            //             ((item2.TicketStateFlowId.GetEnumValue<TicketStateFlowEnum>() == TicketStateFlowEnum.ASSIGNTOGROUPORUSER) && (item2.Id == 23)) ||
                                            //               ((item2.TicketStateFlowId.GetEnumValue<TicketStateFlowEnum>() == TicketStateFlowEnum.CLOSE) && (item2.Id == 9)) ||
                                            //                ((item2.TicketStateFlowId.GetEnumValue<TicketStateFlowEnum>() == TicketStateFlowEnum.REOPEN) && (item2.Id == 14)) ||
                                            //                    ((item2.TicketStateFlowId.GetEnumValue<TicketStateFlowEnum>() == TicketStateFlowEnum.CANCEL) && (item2.Id == 19)))
                                            //    {
                                            //        ticketStateFlowDtosDuplicatesIncluded.Add(item2);
                                            //    }
                                            //}

                                            //Yetkiler birleşebileceği için else kullanmıyoruz
                                            if (loginUserId == ticketWorkingOn.AddedByUserId)
                                            {
                                                if (((item2.TicketStateFlowId.GetEnumValue<TicketStateFlowEnum>() == TicketStateFlowEnum.ASSIGNTOGROUPORUSER) && (item2.Id == 1)) ||
                                                     ((item2.TicketStateFlowId.GetEnumValue<TicketStateFlowEnum>() == TicketStateFlowEnum.ASSIGNTOGROUP) && (item2.Id == 2)) ||
                                                      ((item2.TicketStateFlowId.GetEnumValue<TicketStateFlowEnum>() == TicketStateFlowEnum.SUSPEND) && (item2.Id == 22)) ||
                                                        ((item2.TicketStateFlowId.GetEnumValue<TicketStateFlowEnum>() == TicketStateFlowEnum.ASSIGNTOGROUPORUSER) && (item2.Id == 23)) ||
                                                          ((item2.TicketStateFlowId.GetEnumValue<TicketStateFlowEnum>() == TicketStateFlowEnum.CLOSE) && (item2.Id == 9)) ||
                                                           ((item2.TicketStateFlowId.GetEnumValue<TicketStateFlowEnum>() == TicketStateFlowEnum.REOPEN) && (item2.Id ==14)))
                                                {
                                                    ticketStateFlowDtosDuplicatesIncluded.Add(item2);
                                                }
                                            }

                                            //Yetkiler birleşebileceği için else kullanmıyoruz
                                            if ((ticketWorkingOn.TicketAssignedUserId.HasValue) && (loginUserId == ticketWorkingOn.TicketAssignedUserId))
                                            {
                                                if (((item2.TicketStateFlowId.GetEnumValue<TicketStateFlowEnum>() == TicketStateFlowEnum.REJECT) && (item2.Id == 3)) ||
                                                      ((item2.TicketStateFlowId.GetEnumValue<TicketStateFlowEnum>() == TicketStateFlowEnum.WAIT) && (item2.Id == 25)) ||
                                                       ((item2.TicketStateFlowId.GetEnumValue<TicketStateFlowEnum>() == TicketStateFlowEnum.WAIT) && (item2.Id == 12)) ||
                                                        ((item2.TicketStateFlowId.GetEnumValue<TicketStateFlowEnum>() == TicketStateFlowEnum.CANCEL) && (item2.Id == 19)) ||
                                                         ((item2.TicketStateFlowId.GetEnumValue<TicketStateFlowEnum>() == TicketStateFlowEnum.CANCEL) && (item2.Id == 15)) ||
                                                          ((item2.TicketStateFlowId.GetEnumValue<TicketStateFlowEnum>() == TicketStateFlowEnum.CANCEL) && (item2.Id == 18)) ||
                                                          ((item2.TicketStateFlowId.GetEnumValue<TicketStateFlowEnum>() == TicketStateFlowEnum.ASSIGNTOGROUPORUSER) && (item2.Id == 5)) ||
                                                           ((item2.TicketStateFlowId.GetEnumValue<TicketStateFlowEnum>() == TicketStateFlowEnum.ASSIGNTOGROUP) && (item2.Id == 6)) ||
                                                             ((item2.TicketStateFlowId.GetEnumValue<TicketStateFlowEnum>() == TicketStateFlowEnum.STARTTOWORKING) && (item2.Id == 4)) ||
                                                              ((item2.TicketStateFlowId.GetEnumValue<TicketStateFlowEnum>() == TicketStateFlowEnum.RESOLVE) && (item2.Id == 7)) ||
                                                               ((item2.TicketStateFlowId.GetEnumValue<TicketStateFlowEnum>() == TicketStateFlowEnum.STARTTOWORKING) && (item2.Id == 8)) ||
                                                                 ((item2.TicketStateFlowId.GetEnumValue<TicketStateFlowEnum>() == TicketStateFlowEnum.STARTTOWORKING) && (item2.Id == 13)) ||
                                                                 ((item2.TicketStateFlowId.GetEnumValue<TicketStateFlowEnum>() == TicketStateFlowEnum.ASSIGNTOGROUPORUSER) && (item2.Id == 20)) ||
                                                                    ((item2.TicketStateFlowId.GetEnumValue<TicketStateFlowEnum>() == TicketStateFlowEnum.ASSIGNTOGROUP) && (item2.Id == 21)))
                                                {
                                                    ticketStateFlowDtosDuplicatesIncluded.Add(item2);
                                                }
                                            }

                                            //Yetkiler birleşebileceği için else kullanmıyoruz
                                            //ticket da atama grubu var mı?
                                            if (ticketWorkingOn.TickedAssignedAssignmentGroupId.HasValue && (ticketWorkingOn.TickedAssignedAssignmentGroup != null) &&
                                                (loginUserId == ticketWorkingOn.TickedAssignedAssignmentGroup.GroupManagerUserId))
                                            {
                                                if (((item2.TicketStateFlowId.GetEnumValue<TicketStateFlowEnum>() == TicketStateFlowEnum.REJECT) && (item2.Id == 3)) ||
                                                     ((item2.TicketStateFlowId.GetEnumValue<TicketStateFlowEnum>() == TicketStateFlowEnum.WAIT) && (item2.Id == 25)) ||
                                                       ((item2.TicketStateFlowId.GetEnumValue<TicketStateFlowEnum>() == TicketStateFlowEnum.ASSIGNTOGROUPORUSER) && (item2.Id == 5)) ||
                                                         ((item2.TicketStateFlowId.GetEnumValue<TicketStateFlowEnum>() == TicketStateFlowEnum.ASSIGNTOGROUP) && (item2.Id == 6)) ||
                                                           ((item2.TicketStateFlowId.GetEnumValue<TicketStateFlowEnum>() == TicketStateFlowEnum.ASSIGNTOGROUPORUSER) && (item2.Id == 20)) ||
                                                              ((item2.TicketStateFlowId.GetEnumValue<TicketStateFlowEnum>() == TicketStateFlowEnum.ASSIGNTOGROUP) && (item2.Id == 21)))
                                                {
                                                    ticketStateFlowDtosDuplicatesIncluded.Add(item2);
                                                }
                                            }
                                        }

                                        //Duplicate stateleri temizleyelim
                                        if ((ticketStateFlowDtosDuplicatesIncluded != null) && (ticketStateFlowDtosDuplicatesIncluded.Count > 0))
                                        {
                                            ticketStateFlowDtos = ticketStateFlowDtosDuplicatesIncluded.GroupBy(x => x.TicketStateFlowId).Select(x => x.First()).ToList();
                                        }
                                    }
                                }
                            }

                            foreach (var item in ticketStateFlowDtos)
                            {
                                customTicketStateFlowDto.Add(ticketStateFlowLists.Where(x => x.Id == item.TicketStateFlowId).FirstOrDefault());

                            }
                            _cache.Set("cacheTicketStateTransitionFlow", cacheTicketStateTransitionFlowDtos);
                            return new JsonResult(DataSourceLoader.Load(customTicketStateFlowDto, loadOptions));
                        }
                    }
                }
            }
            return new JsonResult(DataSourceLoader.Load(new List<CustomDTO.CustomTicketStateFlowDto>(), loadOptions));
        }

        public async Task<IActionResult> OnGetTicketPriorityData(DataSourceLoadOptions loadOptions)
        {
            var controllersActionsJson = SessionHelper.GetObjectFromJson(HttpContext.Session, "userInfo");
            var controllersActions = JsonConvert.DeserializeObject<List<Tuple<ApiControllerDescription, List<string>>>>(controllersActionsJson);
            foreach (var controllerAction in controllersActions)
            {
                if (controllerAction.Item1.ToString() == "TicketPriority")
                {
                    foreach (var action in controllerAction.Item2)
                    {
                        if (action == "GetAll")
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
                    }
                }
            }
            return new JsonResult(DataSourceLoader.Load(new List<StandartDTO.TicketPriorityDto>(), loadOptions));
        }

        public async Task<IActionResult> OnGetFileAttachments(int Id)
        {
            try
            {
                var Attachments = _cache.Get("attachments");
                var attachmentList = (List<StandartDTO.AttachmentDto>)Attachments;
                var attachment = attachmentList.Where(x => x.Id == Id).SingleOrDefault();
                var result = Convert.ToBase64String(attachment.FileData);
                //var base64EncodedBytes = System.Convert.FromBase64String(result);
                //var realBase64 = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);

                return Content(result);
            }
            catch (Exception e)
            {

                throw;
            }

        }

        public async Task<IActionResult> OnGetFileNoteAttachment(int Id, int attachmentId)
        {
            try
            {
                string realBase64 = "";
                var cacheNotes = _cache.Get("ticketNotes");
                var noteList = (List<StandartDTO.TicketNoteDto>)cacheNotes;

                foreach (var item in noteList)
                {
                    foreach (var attc in item.Attachments)
                    {
                        if (attc.Id == attachmentId)
                        {
                            //Todo : Mobilden eklenmiş olan dosya kontrolü yapılmaktadır;
                            if (attc.AttachmentDescription == null)
                            {
                                var result = Convert.ToBase64String(attc.FileData);
                                return Content(result);
                            }
                            realBase64 = System.Text.Encoding.UTF8.GetString(attc.FileData);
                        }
                    }
                }

                return Content(realBase64);
            }
            catch (Exception e)
            {

                throw;
            }

        }

        public IActionResult OnGetTicketNote(DataSourceLoadOptions loadOptions, int ticketId)
        {
            var controllersActionsJson = SessionHelper.GetObjectFromJson(HttpContext.Session, "userInfo");
            var controllersActions = JsonConvert.DeserializeObject<List<Tuple<ApiControllerDescription, List<string>>>>(controllersActionsJson);
            foreach (var controllerAction in controllersActions)
            {
                if (controllerAction.Item1.ToString() == "TicketNoteWrapper")
                {
                    foreach (var action in controllerAction.Item2)
                    {
                        if (action == "GetById")
                        {
                            _cache.Remove("ticketNotes");
                            if (ticketId == 0)
                            {
                                return BadRequest();
                            }
                            HttpClient httpClient = new();
                            httpClient.BaseAddress = new Uri("http://localhost:33400/");
                            BusinessLogicRequest request = new()
                            {
                                ApiUrl = new Uri(httpClient.BaseAddress, "TicketNoteWrapper/GetById/" + ticketId),
                                RequestDomain = RemoteIncomingDomains.DiasTesisYonetimWebClient
                            };
                            var result = httpClient.PostAsJsonAsync(request.ApiUrl, request).Result;
                            var response = result.Content.ReadAsStringAsync().Result;
                            BusinessLogicResponse businessLogicRequestResponse = JsonConvert.DeserializeObject<BusinessLogicResponse>(response);
                            var ticketNoteList = businessLogicRequestResponse.RelevantDto.ToString();
                            var ticketNotes = JsonConvert.DeserializeObject<List<StandartDTO.TicketNoteDto>>(ticketNoteList);

                            _cache.Set("ticketNotes", ticketNotes);
                            return new JsonResult(DataSourceLoader.Load(ticketNotes, loadOptions));
                        }
                    }
                }
            }
            return new JsonResult(DataSourceLoader.Load(new List<StandartDTO.TicketNoteDto>(), loadOptions));
        }

        public IActionResult OnGetTicketAttachment(DataSourceLoadOptions loadOptions, int ticketId)
        {
            _cache.Remove("attachments");
            var controllersActionsJson = SessionHelper.GetObjectFromJson(HttpContext.Session, "userInfo");
            var controllersActions = JsonConvert.DeserializeObject<List<Tuple<ApiControllerDescription, List<string>>>>(controllersActionsJson);
            foreach (var controllerAction in controllersActions)
            {
                if (controllerAction.Item1.ToString() == "AttachmentWrapper")
                {
                    foreach (var action in controllerAction.Item2)
                    {
                        if (action == "GetById")
                        {
                            if (ticketId == 0)
                            {
                                List<StandartDTO.AttachmentDto> attachmentDtos = new();

                                return new JsonResult(DataSourceLoader.Load(attachmentDtos, loadOptions));
                            }
                            HttpClient httpClient = new();
                            httpClient.BaseAddress = new Uri("http://localhost:33400/");
                            BusinessLogicRequest request = new()
                            {
                                ApiUrl = new Uri(httpClient.BaseAddress, "AttachmentWrapper/GetById/" + ticketId),
                                RequestDomain = RemoteIncomingDomains.DiasTesisYonetimWebClient
                            };
                            var result = httpClient.PostAsJsonAsync(request.ApiUrl, request).Result;
                            var response = result.Content.ReadAsStringAsync().Result;
                            BusinessLogicResponse businessLogicRequestResponse = JsonConvert.DeserializeObject<BusinessLogicResponse>(response);
                            var ticketNoteList = businessLogicRequestResponse.RelevantDto.ToString();
                            var ticketAttachments = JsonConvert.DeserializeObject<List<StandartDTO.AttachmentDto>>(ticketNoteList);
                            var attachs = ticketAttachments.Where(x => x.TicketNoteId == null).ToList();
                            _cache.Set("attachments", attachs);
                            return new JsonResult(DataSourceLoader.Load(attachs, loadOptions));
                        }
                    }
                }
            }
            return new JsonResult(DataSourceLoader.Load(new List<StandartDTO.AttachmentDto>(), loadOptions));
        }

        public async Task<IActionResult> OnPostAddNote()
        {
            var controllersActionsJson = SessionHelper.GetObjectFromJson(HttpContext.Session, "userInfo");
            var controllersActions = JsonConvert.DeserializeObject<List<Tuple<ApiControllerDescription, List<string>>>>(controllersActionsJson);
            foreach (var controllerAction in controllersActions)
            {
                if (controllerAction.Item1.ToString() == "TicketNoteWrapper")
                {
                    foreach (var action in controllerAction.Item2)
                    {
                        if (action == "Insert")
                        {
                            int ticketId = Convert.ToInt32(Convert.ToString(Request.Form["TicketId"]));
                            string noteText = Convert.ToString(Request.Form["NoteText"]);
                            List<StandartDTO.AttachmentDto> attachmentNoteDto = new();

                            if (ticketId == 0)
                            {
                                return BadRequest();
                            }
                            else
                            {
                                foreach (var myFile in Request.Form.Files)
                                {
                                    StandartDTO.AttachmentDto attachment = new();
                                    var newAttachmentFileName = $"N_{DateTime.Now.ToString("ddMMyyyy")}_{myFile.FileName}";
                                    attachment.FolderName = newAttachmentFileName;
                                    attachment.FileType = myFile.ContentType;
                                    attachment.AttachmentDescription = "";
                                    using (var ms = new MemoryStream())
                                    {
                                        myFile.CopyTo(ms);
                                        var fileBytes = ms.ToArray();
                                        string s = Convert.ToBase64String(fileBytes);
                                        byte[] buffer = Encoding.ASCII.GetBytes(s);
                                        attachment.FileData = buffer;
                                    }
                                    attachmentNoteDto.Add(attachment);
                                }
                            }
                            CustomDTO.CustomTicketNoteDto model = new();
                            model.Attachments = attachmentNoteDto;
                            model.TicketId = ticketId;
                            model.NoteText = noteText;
                            var user = SessionHelper.GetObjectFromJson(HttpContext.Session, "user");
                            var userDto = JsonConvert.DeserializeObject<StandartDTO.UserDto>(user);
                            int loginUserId = Convert.ToInt32(userDto.Id);

                            model.AddedByUserId = loginUserId;

                            HttpClient httpClient = new();
                            httpClient.BaseAddress = new Uri("http://localhost:33400/");

                            var options = new JsonSerializerOptions();
                            options.Converters.Add(new CustomJsonConverterForType());

                            BusinessLogicRequest request = new()
                            {
                                ApiUrl = new Uri(httpClient.BaseAddress, "TicketNoteWrapper/Insert"),
                                RequestDomain = RemoteIncomingDomains.DiasTesisYonetimWebClient,
                                RequestDtosTypes = new List<Type>() { typeof(CustomDTO.CustomTicketNoteDto) },
                                RequestDtosJsons = new List<string>() {
                    JsonConvert.SerializeObject(model,
                          Formatting.None,
                            new JsonSerializerSettings {
                                NullValueHandling = NullValueHandling.Ignore
                            })}
                            };

                            var resultUpdateState = httpClient.PostAsJsonAsync(request.ApiUrl, request, options).Result;
                            var resultUpdateStateModel = resultUpdateState.Content.ReadAsStringAsync().Result;
                            BusinessLogicResponse businessLogicRequestResponseUpdate = JsonConvert.DeserializeObject<BusinessLogicResponse>(resultUpdateStateModel);
                            if (businessLogicRequestResponseUpdate.ErrorObj.BusinessOperationSucceed == false)
                            {
                                _notyfService.Success($"{businessLogicRequestResponseUpdate.ErrorObj.DisplayMessage}");
                            }
                            if (businessLogicRequestResponseUpdate.ErrorObj.BusinessOperationSucceed == true)
                            {
                                _notyfService.Success($"{businessLogicRequestResponseUpdate.ErrorObj.DisplayMessage}");
                            }
                            var responseUpdate = JsonConvert.DeserializeObject<CustomDTO.CustomTicketDto>(businessLogicRequestResponseUpdate.OptionalJsonResult);
                            return new JsonResult(responseUpdate);
                        }
                    }
                }
            }

            _notyfService.Error("İş Emrine Not Ekleme yetkiniz yoktur.");
            return new JsonResult(new CustomDTO.CustomTicketDto());
        }

        public async Task<IActionResult> OnPostTicketNoteAttachment()
        {
            var controllersActionsJson = SessionHelper.GetObjectFromJson(HttpContext.Session, "userInfo");
            var controllersActions = JsonConvert.DeserializeObject<List<Tuple<ApiControllerDescription, List<string>>>>(controllersActionsJson);
            foreach (var controllerAction in controllersActions)
            {
                if (controllerAction.Item1.ToString() == "TicketNoteWrapper")
                {
                    foreach (var action in controllerAction.Item2)
                    {
                        if (action == "Insert")
                        {
                            int ticketId = Convert.ToInt32(Convert.ToString(Request.Form["TicketId"]));
                            int ticketNoteId = Convert.ToInt32(Convert.ToString(Request.Form["TicketNoteId"]));
                            List<StandartDTO.AttachmentDto> attachmentsDto = new();
                            if (ticketId == 0)
                            {
                                return BadRequest();
                            }
                            if (Request.Form.Files.Count == 0)
                            {
                                return BadRequest();
                            }
                            else
                            {
                                foreach (var myFile in Request.Form.Files)
                                {

                                    StandartDTO.AttachmentDto attachment = new();
                                    var newAttachmentFileName = $"N_{DateTime.Now.ToString("ddMMyyyy")}_{myFile.FileName}";
                                    attachment.FolderName = newAttachmentFileName;
                                    attachment.FileType = myFile.ContentType;
                                    attachment.AttachmentDescription = "";
                                    attachment.TicketNoteId = ticketNoteId;
                                    attachment.TicketId = ticketId;
                                    using (var ms = new MemoryStream())
                                    {
                                        myFile.CopyTo(ms);
                                        var fileBytes = ms.ToArray();
                                        string s = Convert.ToBase64String(fileBytes);
                                        byte[] buffer = Encoding.ASCII.GetBytes(s);
                                        attachment.FileData = buffer;
                                    }
                                    attachmentsDto.Add(attachment);
                                }
                            }

                            HttpClient httpClient = new();
                            httpClient.BaseAddress = new Uri("http://localhost:33400/");

                            var options = new JsonSerializerOptions();
                            options.Converters.Add(new CustomJsonConverterForType());

                            CustomDTO.CustomAttachmentDto model = new();
                            model.Attachments = attachmentsDto;
                            model.TicketId = ticketId;

                            var user = SessionHelper.GetObjectFromJson(HttpContext.Session, "user");
                            var userDto = JsonConvert.DeserializeObject<StandartDTO.UserDto>(user);
                            int loginUserId = Convert.ToInt32(userDto.Id);

                            model.AddedByUserId = loginUserId;
                            BusinessLogicRequest request = new()
                            {
                                ApiUrl = new Uri(httpClient.BaseAddress, "AttachmentWrapper/Insert"),
                                RequestDomain = RemoteIncomingDomains.DiasTesisYonetimWebClient,
                                RequestDtosTypes = new List<Type>() { typeof(CustomDTO.CustomAttachmentDto) },
                                RequestDtosJsons = new List<string>() {
                                    JsonConvert.SerializeObject(model,
                                          Formatting.None,
                                            new JsonSerializerSettings {
                                                NullValueHandling = NullValueHandling.Ignore
                                            })}
                            };
                            var resultAttachment = httpClient.PostAsJsonAsync(request.ApiUrl, request, options).Result;
                            var resultAttachmentModel = resultAttachment.Content.ReadAsStringAsync().Result;
                            BusinessLogicResponse businessLogicRequestResponseAdded = JsonConvert.DeserializeObject<BusinessLogicResponse>(resultAttachmentModel);

                            if (businessLogicRequestResponseAdded.ErrorObj.BusinessOperationSucceed == false)
                            {
                                return new BadRequestObjectResult(businessLogicRequestResponseAdded.ErrorObj.DisplayMessage);
                            }

                            var responseAdded = JsonConvert.DeserializeObject<StandartDTO.AttachmentDto>(businessLogicRequestResponseAdded.OptionalJsonResult);
                            return new OkObjectResult(businessLogicRequestResponseAdded.ErrorObj.DisplayMessage);
                        }
                    }
                }
            }

            _notyfService.Error("İş Emrine Not Ekleme yetkiniz yoktur.");
            return new JsonResult(new CustomDTO.CustomTicketDto());
        }

        public async Task<IActionResult> OnPostAddAttachment()
        {
            var controllersActionsJson = SessionHelper.GetObjectFromJson(HttpContext.Session, "userInfo");
            var controllersActions = JsonConvert.DeserializeObject<List<Tuple<ApiControllerDescription, List<string>>>>(controllersActionsJson);
            foreach (var controllerAction in controllersActions)
            {
                if (controllerAction.Item1.ToString() == "AttachmentWrapper")
                {
                    foreach (var action in controllerAction.Item2)
                    {
                        if (action == "Insert")
                        {
                            int ticketId = Convert.ToInt32(Convert.ToString(Request.Form["TicketId"]));
                            List<StandartDTO.AttachmentDto> attachmentsDto = new();
                            if (ticketId == 0)
                            {
                                return BadRequest();
                            }
                            if (Request.Form.Files.Count == 0)
                            {
                                return BadRequest();
                            }
                            else
                            {
                                foreach (var myFile in Request.Form.Files)
                                {

                                    StandartDTO.AttachmentDto attachment = new();
                                    var newAttachmentFileName = $"N_{DateTime.Now.ToString("ddMMyyyy")}_{myFile.FileName}";
                                    attachment.FolderName = newAttachmentFileName;
                                    attachment.FileType = myFile.ContentType;
                                    attachment.AttachmentDescription = "";
                                    using (var ms = new MemoryStream())
                                    {
                                        myFile.CopyTo(ms);
                                        var fileBytes = ms.ToArray();
                                        string s = Convert.ToBase64String(fileBytes);
                                        byte[] buffer = Encoding.ASCII.GetBytes(s);
                                        attachment.FileData = buffer;
                                    }
                                    attachmentsDto.Add(attachment);
                                }
                            }

                            HttpClient httpClient = new();
                            httpClient.BaseAddress = new Uri("http://localhost:33400/");

                            var options = new JsonSerializerOptions();
                            options.Converters.Add(new CustomJsonConverterForType());

                            CustomDTO.CustomAttachmentDto model = new();
                            model.Attachments = attachmentsDto;
                            model.TicketId = ticketId;

                            var user = SessionHelper.GetObjectFromJson(HttpContext.Session, "user");
                            var userDto = JsonConvert.DeserializeObject<StandartDTO.UserDto>(user);
                            int loginUserId = Convert.ToInt32(userDto.Id);

                            model.AddedByUserId = loginUserId;
                            BusinessLogicRequest request = new()
                            {
                                ApiUrl = new Uri(httpClient.BaseAddress, "AttachmentWrapper/Insert"),
                                RequestDomain = RemoteIncomingDomains.DiasTesisYonetimWebClient,
                                RequestDtosTypes = new List<Type>() { typeof(CustomDTO.CustomAttachmentDto) },
                                RequestDtosJsons = new List<string>() {
                                    JsonConvert.SerializeObject(model,
                                          Formatting.None,
                                            new JsonSerializerSettings {
                                                NullValueHandling = NullValueHandling.Ignore
                                            })}
                            };

                            var resultAttachment = httpClient.PostAsJsonAsync(request.ApiUrl, request, options).Result;
                            var resultAttachmentModel = resultAttachment.Content.ReadAsStringAsync().Result;
                            BusinessLogicResponse businessLogicRequestResponseUpdate = JsonConvert.DeserializeObject<BusinessLogicResponse>(resultAttachmentModel);
                            var responseAdded = JsonConvert.DeserializeObject<StandartDTO.AttachmentDto>(businessLogicRequestResponseUpdate.OptionalJsonResult);
                            return new JsonResult(responseAdded);
                        }
                    }
                }
            }

            _notyfService.Error("İş Emrine Dosya Ekleme yetkiniz yoktur.");
            return new JsonResult(new StandartDTO.AttachmentDto());
        }

        public async Task<IActionResult> OnPostDeleteAttachment([FromBody] CustomDTO.CustomAttachmentDto model)
        {
            var myEntry = _cache.Get("customTicketDtos");
            List<CustomDTO.CustomTicketDto> list = new();
            list = (List<CustomDTO.CustomTicketDto>)myEntry;
            var ticketDto = list.Where(x => x.Id == model.TicketId).ToList();
            var user = SessionHelper.GetObjectFromJson(HttpContext.Session, "user");
            var userDto = JsonConvert.DeserializeObject<StandartDTO.UserDto>(user);
            int loginUserId = Convert.ToInt32(userDto.Id);
            if (ticketDto[0].TicketOwnerUserId != loginUserId)
            {
                return BadRequest();
            }

            HttpClient httpClient = new();
            httpClient.BaseAddress = new Uri("http://localhost:33400/");
            var options = new JsonSerializerOptions();
            options.Converters.Add(new CustomJsonConverterForType());
            model.IsDeleted = true;
            BusinessLogicRequest request = new()
            {
                ApiUrl = new Uri(httpClient.BaseAddress, "AttachmentWrapper/Delete"),
                RequestDomain = RemoteIncomingDomains.DiasTesisYonetimWebClient,
                RequestDtosTypes = new List<Type>() { typeof(CustomDTO.CustomAttachmentDto) },
                RequestDtosJsons = new List<string>() {
                    JsonConvert.SerializeObject(model,
                          Formatting.None,
                            new JsonSerializerSettings {
                                NullValueHandling = NullValueHandling.Ignore
                            })}
            };

            var resultAttachment = httpClient.PostAsJsonAsync(request.ApiUrl, request, options).Result;
            var resultAttachmentModel = resultAttachment.Content.ReadAsStringAsync().Result;
            BusinessLogicResponse businessLogicRequestResponseDeleted = JsonConvert.DeserializeObject<BusinessLogicResponse>(resultAttachmentModel);
            if (businessLogicRequestResponseDeleted.ErrorObj.BusinessOperationSucceed == false)
            {
                return BadRequest();
            }
            var responseDeleted = JsonConvert.DeserializeObject<StandartDTO.AttachmentDto>(businessLogicRequestResponseDeleted.OptionalJsonResult);
            return new JsonResult(responseDeleted);
        }
        public async Task<IActionResult> OnPostDeleteNote([FromBody] CustomDTO.CustomTicketNoteDto model)
        {
            var myEntry = _cache.Get("customTicketDtos");
            List<CustomDTO.CustomTicketDto> list = new();
            list = (List<CustomDTO.CustomTicketDto>)myEntry;
            var ticketDto = list.Where(x => x.Id == model.TicketId).ToList();
            var user = SessionHelper.GetObjectFromJson(HttpContext.Session, "user");
            var userDto = JsonConvert.DeserializeObject<StandartDTO.UserDto>(user);
            int loginUserId = Convert.ToInt32(userDto.Id);
            if (ticketDto[0].TicketOwnerUserId != loginUserId)
            {
                return BadRequest();
            }

            HttpClient httpClient = new();
            httpClient.BaseAddress = new Uri("http://localhost:33400/");
            var options = new JsonSerializerOptions();
            options.Converters.Add(new CustomJsonConverterForType());
            model.IsDeleted = true;
            BusinessLogicRequest request = new()
            {
                ApiUrl = new Uri(httpClient.BaseAddress, "TicketNoteWrapper/Delete"),
                RequestDomain = RemoteIncomingDomains.DiasTesisYonetimWebClient,
                RequestDtosTypes = new List<Type>() { typeof(CustomDTO.CustomAttachmentDto) },
                RequestDtosJsons = new List<string>() {
                    JsonConvert.SerializeObject(model,
                          Formatting.None,
                            new JsonSerializerSettings {
                                NullValueHandling = NullValueHandling.Ignore
                            })}
            };

            var resultTicketNote = httpClient.PostAsJsonAsync(request.ApiUrl, request, options).Result;
            var resultTicketNoteModel = resultTicketNote.Content.ReadAsStringAsync().Result;
            BusinessLogicResponse businessLogicRequestResponseDeleted = JsonConvert.DeserializeObject<BusinessLogicResponse>(resultTicketNoteModel);
            if (businessLogicRequestResponseDeleted.ErrorObj.BusinessOperationSucceed == false)
            {
                return BadRequest();
            }
            var responseDeleted = JsonConvert.DeserializeObject<StandartDTO.TicketNoteDto>(businessLogicRequestResponseDeleted.OptionalJsonResult);
            return new JsonResult(responseDeleted);
        }

        //Id -> TicketStateTransition idsi
        public async Task<IActionResult> OnPostChangeTicketState(int Id, int ticketStateId, string stringUserId, int ticketId)
        {
            var myEntry = _cache.Get("ticketassignGrpEmp");
            int userId = 0;
            int asgGrpId = 0;
            if (myEntry != null)
            {
                List<StandartDTO.AssignmentGroupEmployeeDto> listAsgEmp = new();
                listAsgEmp = (List<StandartDTO.AssignmentGroupEmployeeDto>)myEntry;

                foreach (var item in listAsgEmp)
                {
                    if (item.HierarchyId == stringUserId)
                    {
                        asgGrpId = item.Id;
                        userId = item.EmployeeUserId;
                    }
                }
            }


            var controllersActionsJson = SessionHelper.GetObjectFromJson(HttpContext.Session, "userInfo");
            var controllersActions = JsonConvert.DeserializeObject<List<Tuple<ApiControllerDescription, List<string>>>>(controllersActionsJson);
            foreach (var controllerAction in controllersActions)
            {
                if (controllerAction.Item1.ToString() == "TicketWrapper")
                {
                    foreach (var action in controllerAction.Item2)
                    {
                        if (action == "UpdateState")
                        {
                            var tickets = _cache.Get("customTicketDtos");
                            List<CustomDTO.CustomTicketDto> ticketList = new();
                            ticketList = (List<CustomDTO.CustomTicketDto>)tickets;
                            CustomDTO.CustomTicketDto model = new();

                            var cacheTicketStateTransitionFlowList = _cache.Get("cacheTicketStateTransitionFlow");
                            List<StandartDTO.TicketStateTransitionFlowDto> customTicketStateFlowDtos = new();
                            customTicketStateFlowDtos = (List<StandartDTO.TicketStateTransitionFlowDto>)cacheTicketStateTransitionFlowList;
                            StandartDTO.TicketStateTransitionFlowDto ticketStateTransitionFlowDto = new();

                            var cacheTicketState = _cache.Get("ticketStates");
                            List<StandartDTO.TicketStateDto> ticketStates = new();
                            ticketStates = (List<StandartDTO.TicketStateDto>)cacheTicketState;
                            StandartDTO.TicketStateDto ticketStateDto = new();

                            foreach (var item in customTicketStateFlowDtos)
                            {
                                if (item.TicketStateFlowId == Id)
                                {
                                    if (item.TicketStateTransitionByTicketStateTransitionFlow.SourceTicketStateId == ticketStateId)
                                    {
                                        ticketStateTransitionFlowDto = item;
                                    }
                                }
                            }
                            foreach (var item in ticketList.Where(x => x.Id == ticketId))
                            {
                                model = item;
                                model.TicketStatusId = ticketStateTransitionFlowDto.TicketStateTransitionByTicketStateTransitionFlow.DestinationTicketStateId;
                            }
                            if (model.TickedAssignedAssignmentGroupId == null)
                            {
                                if (userId != 0)
                                {
                                    model.TicketAssignedUserId = userId;
                                }
                            }
                            if (model.TicketAssignedUserId == null)
                            {
                                if (asgGrpId != 0)
                                {
                                    model.TickedAssignedAssignmentGroupId = asgGrpId;
                                }
                            }

                            if (model.UserResponseTime == null)
                            {
                                if (Id == 3)
                                {
                                    model.UserResponseTime = DateTime.Now;
                                }
                            }
                            if (model.UserResolutionTime == null)
                            {
                                if (Id == 4)
                                {
                                    model.UserResolutionTime = DateTime.Now;
                                }
                            }

                            //Yeniden açılınca
                            if ((ticketStateId == 5) && (Id == 7)) 
                            {
                                model.UserResponseTime = null;
                                model.UserResolutionTime = null;                              
                                model.TicketStatusId = 2;
                            }

                            //Reddedilen işte atananı temizle
                            if ((ticketStateId == 2) && (Id == 9))
                            {                                
                                model.TicketAssignedUserId = null;
                                model.TickedAssignedAssignmentGroupId = null;                             
                            }


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
                                ApiUrl = new Uri(httpClient.BaseAddress, "TicketWrapper/UpdateState"),
                                RequestDomain = RemoteIncomingDomains.DiasTesisYonetimWebClient,
                                RequestDtosTypes = new List<Type>() { typeof(CustomDTO.CustomTicketDto) },
                                RequestDtosJsons = new List<string>() {
                                    JsonConvert.SerializeObject(model,
                                          Formatting.None,
                                            new JsonSerializerSettings {
                                                NullValueHandling = NullValueHandling.Ignore
                                            })}
                            };
                            var resultUpdateState = httpClient.PostAsJsonAsync(request.ApiUrl, request, options).Result;
                            var resultUpdateStateModel = resultUpdateState.Content.ReadAsStringAsync().Result;
                            BusinessLogicResponse businessLogicRequestResponseUpdate = JsonConvert.DeserializeObject<BusinessLogicResponse>(resultUpdateStateModel);
                            var responseUpdate = JsonConvert.DeserializeObject<CustomDTO.CustomTicketDto>(businessLogicRequestResponseUpdate.OptionalJsonResult);
                            return new JsonResult(responseUpdate);
                        }
                    }
                }
            }
            _notyfService.Error("İş Emri Durumunu güncelleme yetkiniz yoktur.");
            return new JsonResult(new CustomDTO.CustomTicketDto());

        }

        public async Task<IActionResult> OnPostFilter(TicketBasePageFilterDto model, DataSourceLoadOptions loadOptions2)
        {
            
            

            for (int i = 0; i < loadOptions2.Filter.Count; i++)
            {

                if (!String.IsNullOrEmpty(loadOptions2.Filter[i].ToString()))
                {
                    var a = JsonConvert.DeserializeObject<IList>(loadOptions2.Filter[i].ToString());
                    if(a[0].ToString() == "TicketReasonIds")
                    {
                        if (a[2] != null)
                        {
                            model.TicketReasonIds = new List<string>();
                            var aada = JsonConvert.DeserializeObject<IList>(a[2].ToString());
                            foreach (string item in aada)
                            {
                                model.TicketReasonIds.Add(item);
                            }
                        }
                    }

                    if (a[0].ToString() == "FilterLocationIds")
                    {
                        if (a[2] != null)
                        {
                            model.FilterLocationIds = new List<string>();
                            var aada = JsonConvert.DeserializeObject<IList>(a[2].ToString());
                            foreach (string item in aada)
                            {
                                model.FilterLocationIds.Add(item);
                            }
                        }
                    }

                    if (a[0].ToString() == "PriorityId")
                    {
                        if (a[2] != null)
                        {                            
                            model.PriorityId = Int32.Parse(a[2].ToString());
                        }
                    }
                    if (a[0].ToString() == "TicketStatusId")
                    {
                        if (a[2] != null)
                        {
                            model.TicketStatusId = Int32.Parse(a[2].ToString());
                        }
                    }
                    if (a[0].ToString() == "UserId")
                    {
                        if (a[2] != null)
                        {
                            model.UserId = Int32.Parse(a[2].ToString());
                        }
                    }
                    if (a[0].ToString() == "TicketDescription")
                    {
                        if (a[2] != null)
                        {
                            model.TicketDescription = a[2].ToString();
                        }
                    }
                    if (a[0].ToString() == "ReportedUser")
                    {
                        if (a[2] != null)
                        {
                            model.ReportedUser = a[2].ToString();
                        }
                    }
                    if (a[0].ToString() == "TicketCode")
                    {
                        if (a[2] != null)
                        {
                            model.TicketCode = a[2].ToString();
                        }
                    }

                    if (a[0].ToString() == "PhoneNumber")
                    {
                        if (a[2] != null)
                        {
                            model.PhoneNumber = a[2].ToString();
                        }
                    }
                    if (a[0].ToString() == "AssignedGroupId")
                    {
                        if (a[2] != null)
                        {
                            model.AssignedGroupId = Int32.Parse(a[2].ToString());
                        }
                    }
                    if (a[0].ToString() == "InsertUser")
                    {
                        if (a[2] != null)
                        {
                            model.InsertUser = Int32.Parse(a[2].ToString());
                        }
                    }
                    if (a[0].ToString() == "TicketResponseTimeTargetedStart")
                    {
                        if (a[2] != null)
                        {
                            model.TicketResponseTimeTargetedStart = DateTimeOffset.Parse(a[2].ToString()).UtcDateTime;
                        }
                        else
                        {
                            model.TicketResponseTimeTargetedStart = new DateTime();
                        }
                    }
                    if (a[0].ToString() == "TicketResponseTimeTargetedEnd")
                    {
                        if (a[2] != null)
                        {
                            model.TicketResponseTimeTargetedEnd = DateTimeOffset.Parse(a[2].ToString()).UtcDateTime;
                        }
                        else
                        {
                            model.TicketResponseTimeTargetedEnd = new DateTime();
                        }
                    }
                    if (a[0].ToString() == "TicketResolutionTimeTargetedStart")
                    {
                        if (a[2] != null)
                        {
                            model.TicketResolutionTimeTargetedStart = DateTimeOffset.Parse(a[2].ToString()).UtcDateTime;
                        }
                        else
                        {
                            model.TicketResolutionTimeTargetedStart = new DateTime();
                        }
                    }
                    if (a[0].ToString() == "TicketResolutionTimeTargetedEnd")
                    {
                        if (a[2] != null)
                        {
                            model.TicketResolutionTimeTargetedEnd = DateTimeOffset.Parse(a[2].ToString()).UtcDateTime;
                        }
                        else
                        {
                            model.TicketResolutionTimeTargetedEnd = new DateTime();
                        }
                    }
                    if (a[0].ToString() == "ResponsedTime")
                    {
                        if (a[2] != null)
                        {
                            model.ResponsedTime = DateTimeOffset.Parse(a[2].ToString()).UtcDateTime;
                        }
                        else
                        {
                            model.ResponsedTime = new DateTime();
                        }
                    }
                    if (a[0].ToString() == "ResoulitionedTime")
                    {
                        if (a[2] != null)
                        {
                            model.ResoulitionedTime = DateTimeOffset.Parse(a[2].ToString()).UtcDateTime;
                        }
                        else
                        {
                            model.ResoulitionedTime = new DateTime();
                        }
                    }
                    if (a[0].ToString() == "TicketClosedTime")
                    {
                        if (a[2] != null)
                        {
                            model.TicketClosedTime = DateTimeOffset.Parse(a[2].ToString()).UtcDateTime;
                        }
                        else
                        {
                            model.TicketClosedTime = new DateTime();
                        }
                    }
                    if (a[0].ToString() == "TicketDateTicketStart")
                    {
                        if (a[2] != null)
                        {
                            model.TicketDateTicketStart = DateTimeOffset.Parse(a[2].ToString()).UtcDateTime;
                        }
                        else
                        {
                            model.TicketDateTicketStart = new DateTime();
                        }
                    }
                    if (a[0].ToString() == "TicketDateTicketEnd")
                    {
                        if (a[2] != null)
                        {
                            model.TicketDateTicketEnd = DateTimeOffset.Parse(a[2].ToString()).UtcDateTime;
                        }
                        else
                        {
                            model.TicketDateTicketEnd = new DateTime();
                        }
                    }
                }

            }





            //Devexpress formatında olmalıdır
            DataSourceLoadOptions loadOptions = new();
            loadOptions.Skip = loadOptions2.Skip;
            loadOptions.Take = loadOptions2.Take;
            DevExpressRequest gridRequest = null;

            //saat dışında ki tarihi almak için 
            DateTime ResolutionedTime = model.ResoulitionedTime.Date;
            DateTime TicketResponseTimeTargetedStart = model.TicketResponseTimeTargetedStart.Date;
            DateTime TicketResponseTimeTargetedEnd = model.TicketResponseTimeTargetedEnd.Date;
            DateTime TicketResolutionTimeTargetedStart = model.TicketResolutionTimeTargetedStart.Date;
            DateTime TicketResolutionTimeTargetedEnd = model.TicketResolutionTimeTargetedEnd.Date;
            DateTime ResponsedTime = model.ResponsedTime.Date;
            DateTime TicketClosedTime = model.TicketClosedTime.Date;
            DateTime TicketDateTicketStart = model.TicketDateTicketStart.Date;
            DateTime TicketDateTicketEnd = model.TicketDateTicketEnd.Date;


            gridRequest = new DevExpressRequest()
            {
                RequestOptions = new((DataSourceLoadOptions)(loadOptions))
            };

            //Textboxlar
            //İş Emri Kodu

            //TODO : Location Code da muhakkak filtre ekranı içinde olmalı
            //TODO : Şimdilik standart BSHTY gidelim
            //Eğer kodun ilk parçası girilmişse onu çıkar
            //stringi sayıya çevir ve id olarak arat
            //girilmemişse stringi sayıya çevir ve id olarak arat
            //sayı olarak çevrilemiyorsa hata ver
            //Örnek : BSHTY0054 -> id = 54
            //Örnek : BSY0054 -> id = 54
            //Örnek : 054 -> id = 54
            //Örnek : A054 -> Hata
            //Örnek : 05A4 -> Hata
            if ((!(String.IsNullOrEmpty(model.TicketCode))) && (model.TicketCode != "null"))
            {
                string sanitizedTicketCode = model.TicketCode.Replace("B", "").Replace("b", "").Replace("S", "").Replace("s", "").
                                                                Replace("H", "").Replace("h", "").Replace("T", "").Replace("t", "").
                                                                  Replace("Y", "").Replace("y", "").TrimStart(new Char[] { '0' });

                //Çevirmeye çalış
                int numberTicketCode;
                bool successConversionFlag = int.TryParse(sanitizedTicketCode, out numberTicketCode);

                if (successConversionFlag)
                {
                    gridRequest.RequestOptions.DataSourceLoadOption = FilteringOperations.ProduceDevExpressFilterWithMultiFilterObject(
                                                                        gridRequest.RequestOptions.DataSourceLoadOption,
                                                                            new object[3] { "Id", "=", numberTicketCode });

                    if (gridRequest.RequestOptions.DataSourceLoadOption == null)
                    {
                        _notyfService.Error($"{new Error($"", $"", "Geçersiz iş emri kod filtresi, sistem yöneticinizle görüşün", false).DisplayMessage}");
                        return new OkResult();
                    }
                }
                else
                {
                    _notyfService.Error($"{new Error($"", $"", "Geçersiz iş emri kod filtresi, mahal ve sayı tanımlarını düzgün giriniz", false).DisplayMessage}");
                    return new OkResult();
                }
            }

            //Bildiren Kişi
            //ad ve soyad  equal yapılacak
            if ((!(String.IsNullOrEmpty(model.ReportedUser))) && (model.ReportedUser != "null")) 
            {
                //object[] multiArr = FilteringOperations.ProduceDevExpressFilterMultiDoubleArray(
                //                        new string[3] { "TicketReportedUsers.FirstName", "contains", model.ReportedUser },
                //                         new string[3] { "TicketReportedUsers.LastName", "contains", model.ReportedUser }, false);

                gridRequest.RequestOptions.DataSourceLoadOption = FilteringOperations.ProduceDevExpressFilterWithMultiFilterObject(
                                                              gridRequest.RequestOptions.DataSourceLoadOption,
                                                                  new object[3] { "TicketReportedUserNameSurname", "contains", model.ReportedUser });

                if (gridRequest.RequestOptions.DataSourceLoadOption == null)
                {
                    _notyfService.Error($"{new Error($"", $"", "Geçersiz bildiren kişi ad filtresi, sistem yöneticinizle görüşün", false).DisplayMessage}");
                    return new OkResult();
                }
            }

            //telefon numarası(bildiren kişi)
            //contains
            if ((!(String.IsNullOrEmpty(model.PhoneNumber))) && (model.PhoneNumber != "null"))
            {
                //gridRequest.RequestOptions.DataSourceLoadOption = FilteringOperations.ProduceDevExpressFilterWithMultiFilterObject(
                //                                                        gridRequest.RequestOptions.DataSourceLoadOption,
                //                                                            new object[3] { "TicketReportedUsers.MobilePhoneNumber", "contains", model.PhoneNumber });

                gridRequest.RequestOptions.DataSourceLoadOption = FilteringOperations.ProduceDevExpressFilterWithMultiFilterObject(
                                                        gridRequest.RequestOptions.DataSourceLoadOption,
                                                            new object[3] { "TicketReportedUserPhone", "contains", model.PhoneNumber });


                if (gridRequest.RequestOptions.DataSourceLoadOption == null)
                {
                    _notyfService.Error($"{new Error($"", $"", "Geçersiz bildiren kişi telefon filtresi, sistem yöneticinizle görüşün", false).DisplayMessage}");
                    return new OkResult();
                }
            }

            //Açıklama
            //contains
            if ((!(String.IsNullOrEmpty(model.TicketDescription))) && (model.TicketDescription != "null"))
            {
                gridRequest.RequestOptions.DataSourceLoadOption = FilteringOperations.ProduceDevExpressFilterWithMultiFilterObject(
                                                                        gridRequest.RequestOptions.DataSourceLoadOption,
                                                                            new object[3] { "TicketDescription", "contains", model.TicketDescription });

                if (gridRequest.RequestOptions.DataSourceLoadOption == null)
                {
                    _notyfService.Error($"{new Error($"", $"", "Geçersiz iş emri açıklama filtresi, sistem yöneticinizle görüşün", false).DisplayMessage}");
                    return new OkResult();
                }
            }

            //comboboxlar
            //Vaka Nedeni
            //equal
            if ((model.TicketReasonIds != null) && (!(String.IsNullOrEmpty(model.TicketReasonIds[0]))) && (model.TicketReasonIds[0] != "null"))
            {
                List<int> allTicketReasons = new();
                List<int> allTicketReasonsRemovedDuplicates = new();

                string[] ticketReasonHierarchyArr = model.TicketReasonIds[0].Split(",");

                Tuple<List<int>, List<int>> ticketReasonIds = GetIdsOfTicketReasonCategoriesByHierarchicalIds(ticketReasonHierarchyArr);

                //Kategori altındaki(varsa) tüm ticket reason idleri çıkar
                List<int> ticketReasoIdsOnCategories = new();

                foreach (int item in ticketReasonIds.Item1)
                {
                    List<int> ticketReasoIdsOnCategoriesInner = GetIdsOfTicketReasonOfTicketReasonCategoriesById(item);

                    if (ticketReasoIdsOnCategoriesInner != null)
                    {
                        foreach (int itemInner in ticketReasoIdsOnCategoriesInner)
                        {
                            ticketReasoIdsOnCategories.Add(itemInner);
                        }
                    }
                }

                allTicketReasons = ticketReasoIdsOnCategories;

                //Bu listeye varsa seçilmiş reasonları ekleyelim
                foreach (int item in ticketReasonIds.Item2)
                {
                    allTicketReasons.Add(item);
                }

                //Duplicate olanları ayıklayalım
                allTicketReasonsRemovedDuplicates = allTicketReasons.Distinct().ToList();

                if (allTicketReasonsRemovedDuplicates.Count == 1)
                {
                    gridRequest.RequestOptions.DataSourceLoadOption = FilteringOperations.ProduceDevExpressFilterWithMultiFilterObject(
                                                        gridRequest.RequestOptions.DataSourceLoadOption,
                                                            new object[3] { "TicketReasonId", "=", allTicketReasonsRemovedDuplicates[0] });

                    if (gridRequest.RequestOptions.DataSourceLoadOption == null)
                    {
                        _notyfService.Error($"{new Error($"", $"", "Geçersiz vaka nedeni filtresi, sistem yöneticinizle görüşün", false).DisplayMessage}");
                        return new OkResult();
                    }
                }
                else if (allTicketReasonsRemovedDuplicates.Count > 1)
                {
                    List<object[]> filterArrays = new();
                    List<bool> filterBoolArrays = new();
                    foreach (int item in allTicketReasonsRemovedDuplicates)
                    {
                        filterArrays.Add(new object[3] { "TicketReasonId", "=", item });
                    }

                    for (int i = 0; i < allTicketReasonsRemovedDuplicates.Count - 1; i++)
                    {
                        filterBoolArrays.Add(false);
                    }

                    object[] multiArr = FilteringOperations.ProduceDevExpressFilterMultiArray(filterArrays, filterBoolArrays, false);

                    object[] filterMultiArr = new object[1];
                    filterMultiArr[0] = multiArr;

                    gridRequest.RequestOptions.DataSourceLoadOption = FilteringOperations.ProduceDevExpressFilterWithSingleFilterObject(
                                        gridRequest.RequestOptions.DataSourceLoadOption, filterMultiArr);

                    if (gridRequest.RequestOptions.DataSourceLoadOption == null)
                    {
                        _notyfService.Error($"{new Error($"", $"", "Geçersiz vaka nedeni filtresi, sistem yöneticinizle görüşün", false).DisplayMessage}");
                        return new OkResult();
                    }
                }
            }

            //öncelik
            //equal
            if (model.PriorityId > 0)
            {
                gridRequest.RequestOptions.DataSourceLoadOption = FilteringOperations.ProduceDevExpressFilterWithMultiFilterObject(
                                                                    gridRequest.RequestOptions.DataSourceLoadOption,
                                                                        new object[3] { "TicketPriorityId", "=", model.PriorityId });

                if (gridRequest.RequestOptions.DataSourceLoadOption == null)
                {
                    _notyfService.Error($"{new Error($"", $"", "Geçersiz iş emri öncelik filtresi, sistem yöneticinizle görüşün", false).DisplayMessage}");
                    return new OkResult();
                }
            }

            //kayıt altına alan kişi
            if (model.InsertUser > 0)
            {
                gridRequest.RequestOptions.DataSourceLoadOption = FilteringOperations.ProduceDevExpressFilterWithMultiFilterObject(
                                                                    gridRequest.RequestOptions.DataSourceLoadOption,
                                                                        new object[3] { "AddedByUserId", "=", model.InsertUser });

                if (gridRequest.RequestOptions.DataSourceLoadOption == null)
                {
                    _notyfService.Error($"{new Error($"", $"", "Geçersiz iş emri kayıt altına alan kişi filtresi, sistem yöneticinizle görüşün", false).DisplayMessage}");
                    return new OkResult();
                }
            }

            //iş emri durumu
            //equal
            if (model.TicketStatusId > 0)
            {
                gridRequest.RequestOptions.DataSourceLoadOption = FilteringOperations.ProduceDevExpressFilterWithMultiFilterObject(
                                                                    gridRequest.RequestOptions.DataSourceLoadOption,
                                                                        new object[3] { "TicketStatusId", "=", model.TicketStatusId });

                if (gridRequest.RequestOptions.DataSourceLoadOption == null)
                {
                    _notyfService.Error($"{new Error($"", $"", "Geçersiz iş emri durumu filtresi, sistem yöneticinizle görüşün", false).DisplayMessage}");
                    return new OkResult();
                }
            }

            //sorumlu kişi
            //equal
            if (model.UserId > 0)
            {
                gridRequest.RequestOptions.DataSourceLoadOption = FilteringOperations.ProduceDevExpressFilterWithMultiFilterObject(
                                                                    gridRequest.RequestOptions.DataSourceLoadOption,
                                                                        new object[3] { "TicketAssignedUserId", "=", model.UserId });

                if (gridRequest.RequestOptions.DataSourceLoadOption == null)
                {
                    _notyfService.Error($"{new Error($"", $"", "Geçersiz iş emri sorumlu kişi filtresi, sistem yöneticinizle görüşün", false).DisplayMessage}");
                    return new OkResult();
                }
            }

            //atama grubu
            //equal
            if (model.AssignedGroupId > 0)
            {
                gridRequest.RequestOptions.DataSourceLoadOption = FilteringOperations.ProduceDevExpressFilterWithMultiFilterObject(
                                                                    gridRequest.RequestOptions.DataSourceLoadOption,
                                                                        new object[3] { "TickedAssignedAssignmentGroupId", "=", model.AssignedGroupId });

                if (gridRequest.RequestOptions.DataSourceLoadOption == null)
                {
                    _notyfService.Error($"{new Error($"", $"", "Geçersiz iş emri sorumlu kişi filtresi, sistem yöneticinizle görüşün", false).DisplayMessage}");
                    return new OkResult();
                }
            }

            //mahal
            //equal
            if ((model.FilterLocationIds != null) && (!(String.IsNullOrEmpty(model.FilterLocationIds[0]))) && (model.FilterLocationIds[0] != "null"))
            {
                List<int> allTicketLocations = new();
                List<int> allTicketLocationsRemovedDuplicates = new();
                List<int> allTicketIdsRelatedLocations = new();

                string[] ticketLocationHierarchyArr = model.FilterLocationIds[0].Split(",");

                allTicketLocations = GetIdsOfTicketLocationByHierarchicalIds(ticketLocationHierarchyArr);

                //Duplicate olanları ayıklayalım
                allTicketLocationsRemovedDuplicates = allTicketLocations.Distinct().ToList();

                //şimdi bunların hangi ticket idlere denk geldiğini bulalım
                if (allTicketLocationsRemovedDuplicates.Count > 0)
                {
                    allTicketIdsRelatedLocations = GetIdsOfTicketByRelatedLocationByIds(allTicketLocationsRemovedDuplicates);
                }

                if (allTicketIdsRelatedLocations.Count == 1)
                {
                    gridRequest.RequestOptions.DataSourceLoadOption = FilteringOperations.ProduceDevExpressFilterWithMultiFilterObject(
                                                        gridRequest.RequestOptions.DataSourceLoadOption,
                                                            new object[3] { "Id", "=", allTicketIdsRelatedLocations[0] });

                    if (gridRequest.RequestOptions.DataSourceLoadOption == null)
                    {
                        _notyfService.Error($"{new Error($"", $"", "Geçersiz mahal filtresi, sistem yöneticinizle görüşün", false).DisplayMessage}");
                        return new OkResult();
                    }
                }
                else if (allTicketIdsRelatedLocations.Count > 1)
                {
                    List<object[]> filterArrays = new();
                    List<bool> filterBoolArrays = new();
                    foreach (int item in allTicketIdsRelatedLocations)
                    {
                        filterArrays.Add(new object[3] { "Id", "=", item });
                    }

                    for (int i = 0; i < allTicketIdsRelatedLocations.Count - 1; i++)
                    {
                        filterBoolArrays.Add(false);
                    }

                    object[] multiArr = FilteringOperations.ProduceDevExpressFilterMultiArray(filterArrays, filterBoolArrays, false);

                    object[] filterMultiArr = new object[1];
                    filterMultiArr[0] = multiArr;

                    gridRequest.RequestOptions.DataSourceLoadOption = FilteringOperations.ProduceDevExpressFilterWithSingleFilterObject(
                                        gridRequest.RequestOptions.DataSourceLoadOption, filterMultiArr);

                    if (gridRequest.RequestOptions.DataSourceLoadOption == null)
                    {
                        _notyfService.Error($"{new Error($"", $"", "Geçersiz mahal filtresi, sistem yöneticinizle görüşün", false).DisplayMessage}");
                        return new OkResult();
                    }
                }
                else//O mahal(ler)de iş emri yoksa boş liste getirsin
                {
                    gridRequest.RequestOptions.DataSourceLoadOption = FilteringOperations.ProduceDevExpressFilterWithMultiFilterObject(
                                                       gridRequest.RequestOptions.DataSourceLoadOption,
                                                           new object[3] { "Id", "=", -1 });

                    if (gridRequest.RequestOptions.DataSourceLoadOption == null)
                    {
                        _notyfService.Error($"{new Error($"", $"", "Geçersiz mahal filtresi, sistem yöneticinizle görüşün", false).DisplayMessage}");
                        return new OkResult();
                    }
                }
            }

            //Datetimelar
            //Çözüm Zamanı
            //>= < +1 dakika
            if (model.ResoulitionedTime.Year > 1970)
            {
                //TODO : utc ayarlamasını sayfada yapalım
                object[] multiArr = FilteringOperations.ProduceDevExpressFilterMultiDoubleArray(
                        new string[3] { "ExpectedResolutionTime", ">=", model.ResoulitionedTime.AddHours(3).ToString("s") },
                         new string[3] { "ExpectedResolutionTime", "<", model.ResoulitionedTime.AddHours(3).AddMinutes(1).ToString("s") }, true);

                gridRequest.RequestOptions.DataSourceLoadOption = FilteringOperations.ProduceDevExpressFilterWithMultiFilterObject(
                                                    gridRequest.RequestOptions.DataSourceLoadOption, multiArr);

                if (gridRequest.RequestOptions.DataSourceLoadOption == null)
                {
                    _notyfService.Error($"{new Error($"", $"", "Geçersiz çözüm süresi filtresi, sistem yöneticinizle görüşün", false).DisplayMessage}");
                    return new OkResult();
                }
            }

            //Bildirim başlangıç tarihi
            //>=
            if (model.TicketDateTicketStart.Year > 1970)
            {
                //TODO : utc ayarlamasını sayfada yapalım
                gridRequest.RequestOptions.DataSourceLoadOption = FilteringOperations.ProduceDevExpressFilterWithMultiFilterObject(
                                                                   gridRequest.RequestOptions.DataSourceLoadOption,
                                                                       new object[3] { "TicketOpenedTime", ">=", model.TicketDateTicketStart.AddHours(3).ToString("s") });

                if (gridRequest.RequestOptions.DataSourceLoadOption == null)
                {
                    _notyfService.Error($"{new Error($"", $"", "Geçersiz iş emri bildirim tarihi başlangıç filtresi, sistem yöneticinizle görüşün", false).DisplayMessage}");
                    return new OkResult();
                }
            }

            //Bildirim bitiş tarihi
            //<=
            if (model.TicketDateTicketEnd.Year > 1970)
            {
                //TODO : utc ayarlamasını sayfada yapalım
                gridRequest.RequestOptions.DataSourceLoadOption = FilteringOperations.ProduceDevExpressFilterWithMultiFilterObject(
                                                                   gridRequest.RequestOptions.DataSourceLoadOption,
                                                                       new object[3] { "TicketOpenedTime", "<=", model.TicketDateTicketEnd.AddHours(3).ToString("s") });

                if (gridRequest.RequestOptions.DataSourceLoadOption == null)
                {
                    _notyfService.Error($"{new Error($"", $"", "Geçersiz iş emri bildirim tarihi bitiş filtresi, sistem yöneticinizle görüşün", false).DisplayMessage}");
                    return new OkResult();
                }
            }

            //Hedeflenen Müdahale Zamanı Başlangıç
            //<=
            if (model.TicketResponseTimeTargetedStart.Year > 1970)
            {
                //TODO : utc ayarlamasını sayfada yapalım
                gridRequest.RequestOptions.DataSourceLoadOption = FilteringOperations.ProduceDevExpressFilterWithMultiFilterObject(
                                                                   gridRequest.RequestOptions.DataSourceLoadOption,
                                                                       new object[3] { "ExpectedResponseTime", ">=", model.TicketResponseTimeTargetedStart.AddHours(3).ToString("s") });

                if (gridRequest.RequestOptions.DataSourceLoadOption == null)
                {
                    _notyfService.Error($"{new Error($"", $"", "Geçersiz iş emri müdahale tarihi başlangıç filtresi, sistem yöneticinizle görüşün", false).DisplayMessage}");
                    return new OkResult();
                }
            }

            //Hedeflenen Müdahale Zamanı Bitiş
            //<=
            if (model.TicketResponseTimeTargetedEnd.Year > 1970)
            {
                //TODO : utc ayarlamasını sayfada yapalım
                gridRequest.RequestOptions.DataSourceLoadOption = FilteringOperations.ProduceDevExpressFilterWithMultiFilterObject(
                                                                   gridRequest.RequestOptions.DataSourceLoadOption,
                                                                       new object[3] { "ExpectedResponseTime", "<=", model.TicketResponseTimeTargetedEnd.AddHours(3).ToString("s") });

                if (gridRequest.RequestOptions.DataSourceLoadOption == null)
                {
                    _notyfService.Error($"{new Error($"", $"", "Geçersiz iş emri müdahale tarihi bitiş filtresi, sistem yöneticinizle görüşün", false).DisplayMessage}");
                    return new OkResult();
                }
            }

            //Hedeflenen Çözüm Zamanı Başlangıç
            //<=
            if (model.TicketResolutionTimeTargetedStart.Year > 1970)
            {
                //TODO : utc ayarlamasını sayfada yapalım
                gridRequest.RequestOptions.DataSourceLoadOption = FilteringOperations.ProduceDevExpressFilterWithMultiFilterObject(
                                                                   gridRequest.RequestOptions.DataSourceLoadOption,
                                                                       new object[3] { "ExpectedResolutionTime", ">=", model.TicketResolutionTimeTargetedStart.AddHours(3).ToString("s") });

                if (gridRequest.RequestOptions.DataSourceLoadOption == null)
                {
                    _notyfService.Error($"{new Error($"", $"", "Geçersiz iş emri çözüm tarihi başlangıç filtresi, sistem yöneticinizle görüşün", false).DisplayMessage}");
                    return new OkResult();
                }
            }

            //Hedeflenen Çözüm Zamanı Bitiş
            //<=
            if (model.TicketResolutionTimeTargetedEnd.Year > 1970)
            {
                //TODO : utc ayarlamasını sayfada yapalım
                gridRequest.RequestOptions.DataSourceLoadOption = FilteringOperations.ProduceDevExpressFilterWithMultiFilterObject(
                                                                   gridRequest.RequestOptions.DataSourceLoadOption,
                                                                       new object[3] { "ExpectedResolutionTime", "<=", model.TicketResolutionTimeTargetedEnd.AddHours(3).ToString("s") });

                if (gridRequest.RequestOptions.DataSourceLoadOption == null)
                {
                    _notyfService.Error($"{new Error($"", $"", "Geçersiz iş emri çözüm tarihi bitiş filtresi, sistem yöneticinizle görüşün", false).DisplayMessage}");
                    return new OkResult();
                }
            }

            //Kapatılma Zamanı
            //>= < +1 dakika
            if (model.TicketClosedTime.Year > 1970)
            {
                //TODO : utc ayarlamasını sayfada yapalım
                object[] multiArr = FilteringOperations.ProduceDevExpressFilterMultiDoubleArray(
                        new string[3] { "UserResolutionTime", ">=", model.TicketClosedTime.AddHours(3).ToString("s") },
                         new string[3] { "UserResolutionTime", "<", model.TicketClosedTime.AddHours(3).AddMinutes(1).ToString("s") }, true);

                gridRequest.RequestOptions.DataSourceLoadOption = FilteringOperations.ProduceDevExpressFilterWithMultiFilterObject(
                                                    gridRequest.RequestOptions.DataSourceLoadOption, multiArr);

                if (gridRequest.RequestOptions.DataSourceLoadOption == null)
                {
                    _notyfService.Error($"{new Error($"", $"", "Geçersiz iş emri kapatılma tarihi filtresi, sistem yöneticinizle görüşün", false).DisplayMessage}");
                    return new OkResult();
                }
            }

            //Müdahale Edilme Zamanı
            //>= < +1 dakika
            if (model.ResponsedTime.Year > 1970)
            {
                //TODO : utc ayarlamasını sayfada yapalım
                object[] multiArr = FilteringOperations.ProduceDevExpressFilterMultiDoubleArray(
                        new string[3] { "UserResponseTime", ">=", model.ResponsedTime.AddHours(3).ToString("s") },
                         new string[3] { "UserResponseTime", "<", model.ResponsedTime.AddHours(3).AddMinutes(1).ToString("s") }, true);

                gridRequest.RequestOptions.DataSourceLoadOption = FilteringOperations.ProduceDevExpressFilterWithMultiFilterObject(
                                                    gridRequest.RequestOptions.DataSourceLoadOption, multiArr);

                if (gridRequest.RequestOptions.DataSourceLoadOption == null)
                {
                    _notyfService.Error($"{new Error($"", $"", "Geçersiz iş emri müdahale tarihi filtresi, sistem yöneticinizle görüşün", false).DisplayMessage}");
                    return new OkResult();
                }
            }
            var tokenJson = SessionHelper.GetObjectFromJson(HttpContext.Session, "token");
            var token = JsonConvert.DeserializeObject<string>(tokenJson);
            return await LoadGrid(gridRequest.RequestOptions.DataSourceLoadOption, token);
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
                    ApiUrl = new Uri(httpClient.BaseAddress, "TicketWrapper/GetAll"),
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
            int ii = -1;
            try
            {
                List<CustomDTO.CustomTicketDto> listModel = new();

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
                        return new JsonResult(DataSourceLoader.Load(new List<CustomDTO.CustomTicketDto>(), loadOptions));
                    }

                    string myList = businessLogicResponse.RelevantDto.ToString();
                    listModel = JsonConvert.DeserializeObject<List<CustomDTO.CustomTicketDto>>(myList);

                    var Locations = _cache.Get("locations");
                    List<CustomDTO.CustomLocationDto> listDeneme = new();
                    listDeneme = (List<CustomDTO.CustomLocationDto>)Locations;

                    List<CustomDTO.CustomLocationDto> locationDtos = new();
                    List<CustomDTO.CustomLocationDto> locationStepOne = new();
                    List<CustomDTO.CustomLocationDto> locationStepOneModel = new();
                    List<CustomDTO.CustomLocationDto> locationStepTwoModel = new();
                    List<CustomDTO.CustomLocationDto> locationStepThreeModel = new();
                    List<List<StandartDTO.LocationItemDto>> locationItemDtos = new();
                    List<StandartDTO.LocationItemDto> listLocationItemDtos = new();
                    //List<CustomDTO.CustomLocationDto> locationStepOne = new();
                    //List<CustomDTO.CustomLocationDto> locationStepOneModel = new();
                   
                    foreach (var item in listModel)
                    {
                        ii++;
                        if (item.TicketRelatedLocations.Count == 1)
                        {
                            foreach (var item2 in item.TicketRelatedLocations)
                            {

                                string str = item2.TicketLocation.HierarchyId;
                                char ch = '/';
                                int freq = str.Where(x => (x == ch)).Count();
                                if (freq == 5)
                                {
                                    item.Location4 = item2.TicketLocation.LocationNumber;
                                    locationStepOne = listDeneme.Where(x => x.Id == item2.TicketLocation.Id).ToList();
                                    locationStepOneModel = listDeneme.Where(x => x.HierarchyId == locationStepOne[0].ParentHierarchy).ToList();

                                    if (locationStepOneModel != null)
                                    {
                                        item.Location3 = locationStepOneModel[0].LocationDescription;
                                    }

                                    if ((locationStepOneModel != null) && (locationStepOneModel[0].ParentHierarchy != null))
                                    {
                                        locationStepTwoModel = listDeneme.Where(x => x.HierarchyId == locationStepOneModel[0].ParentHierarchy).ToList();

                                        if (locationStepTwoModel != null)
                                        {
                                            item.Location2 = locationStepTwoModel[0].LocationDescription;
                                        }
                                    }

                                    if ((locationStepTwoModel != null) && (locationStepTwoModel[0].ParentHierarchy != null))
                                    {
                                        var locationName3 = listDeneme.Where(x => x.HierarchyId == locationStepTwoModel[0].ParentHierarchy).ToList();

                                        if (locationName3 != null)
                                        {
                                            item.Location1 = locationName3[0].LocationDescription;
                                        }
                                    }
                                }
                                if (freq == 4)
                                {
                                    item.Location4 = item2.TicketLocation.LocationNumber;
                                    item.Location3 = item2.TicketLocation.LocationDescription;
                                    locationStepOne = listDeneme.Where(x => x.Id == item2.TicketLocation.Id).ToList();
                                    locationStepOneModel = listDeneme.Where(x => x.HierarchyId == locationStepOne[0].ParentHierarchy).ToList();

                                    if (locationStepOneModel != null)
                                    {
                                        item.Location2 = locationStepOneModel[0].LocationDescription;
                                    }

                                    if ((locationStepOneModel != null) && (locationStepOneModel[0].ParentHierarchy != null))
                                    {
                                        locationStepTwoModel = listDeneme.Where(x => x.HierarchyId == locationStepOneModel[0].ParentHierarchy).ToList();

                                        if (locationStepTwoModel != null)
                                        {
                                            item.Location1 = locationStepTwoModel[0].LocationDescription;
                                        }
                                    }
                                }
                                if (freq == 3)
                                {
                                    item.Location4 = item2.TicketLocation.LocationNumber;
                                    item.Location2 = item2.TicketLocation.LocationDescription;
                                    locationStepOne = listDeneme.Where(x => x.Id == item2.TicketLocation.Id).ToList();

                                    if(locationStepOne != null)
                                    {
                                        locationStepOneModel = listDeneme.Where(x => x.HierarchyId == locationStepOne[0].ParentHierarchy).ToList();

                                        if (locationStepOneModel != null)
                                        {
                                            item.Location1 = locationStepOneModel[0].LocationDescription;
                                        }
                                    }
                                }

                                if (freq == 2)
                                {
                                    item.Location4 = item2.TicketLocation.LocationNumber;
                                    item.Location1 = item2.TicketLocation.LocationDescription;
                                }

                            }
                        }
                        else
                        {
                            foreach (var item3 in item.TicketRelatedLocations)
                            {

                                StandartDTO.LocationItemDto locationItemDto = new StandartDTO.LocationItemDto();

                                locationItemDto.LocationD = item3.TicketLocation.LocationDescription;
                                locationItemDto.LocationE = item3.TicketLocation.LocationNumber;
                                locationStepOne = listDeneme.Where(x => x.Id == item3.TicketLocation.Id).ToList();
                                locationStepOneModel = listDeneme.Where(x => x.HierarchyId == locationStepOne[0].ParentHierarchy).ToList();

                                if (locationStepOneModel != null)
                                {
                                    locationItemDto.LocationC = locationStepOneModel[0].LocationDescription;
                                }

                                if (locationStepOneModel[0].HierarchyId != "/")
                                {
                                    if ((locationStepOneModel != null) && (locationStepOneModel[0].ParentHierarchy != null))
                                    {
                                        locationStepTwoModel = listDeneme.Where(x => x.HierarchyId == locationStepOneModel[0].ParentHierarchy).ToList();

                                        if (locationStepTwoModel != null)
                                        {
                                            locationItemDto.LocationB = locationStepTwoModel[0].LocationDescription;
                                        }
                                    }
                                    else
                                    {
                                        locationItemDto.LocationB = "";
                                    }

                                    if ((locationStepTwoModel != null) && (locationStepTwoModel[0].ParentHierarchy != null))
                                    {
                                        var locationName3 = listDeneme.Where(x => x.HierarchyId == locationStepTwoModel[0].ParentHierarchy).ToList();

                                        if (locationName3 != null)
                                        {
                                            locationItemDto.LocationA = locationName3[0].LocationDescription;
                                        }
                                    }
                                    else
                                    {
                                        locationItemDto.LocationA = "";
                                    }
                                }

                                
                                listLocationItemDtos.Add(locationItemDto);

                            }
                            locationItemDtos.Add(listLocationItemDtos);
                            listLocationItemDtos = new List<StandartDTO.LocationItemDto>();
                            //listLocationItemDtos.Clear();

                            item.Location4 = item.TicketRelatedLocations[0].TicketLocation.LocationNumber + "...";
                            locationStepOne = listDeneme.Where(x => x.Id == item.TicketRelatedLocations[0].TicketLocation.Id).ToList();
                            locationStepOneModel = listDeneme.Where(x => x.HierarchyId == locationStepOne[0].ParentHierarchy).ToList();

                            if (locationStepOneModel != null)
                            {
                                item.Location3 = locationStepOneModel[0].LocationDescription + "...";
                            }

                            if ((locationStepOneModel != null) && (locationStepOneModel[0].ParentHierarchy != null))
                            {
                                locationStepTwoModel = listDeneme.Where(x => x.HierarchyId == locationStepOneModel[0].ParentHierarchy).ToList();

                                if (locationStepTwoModel != null)
                                {
                                    item.Location2 = locationStepTwoModel[0].LocationDescription + "...";
                                }
                            }

                            if ((locationStepTwoModel != null) && (locationStepTwoModel[0].ParentHierarchy != null))
                            {
                                var locationName3 = listDeneme.Where(x => x.HierarchyId == locationStepTwoModel[0].ParentHierarchy).ToList();

                                if (locationName3 != null)
                                {
                                    item.Location1 = locationName3[0].LocationDescription + "...";
                                }
                            }
                        }

                        item.LocationItems = locationItemDtos;
                        locationItemDtos = new List<List<StandartDTO.LocationItemDto>>();
                        item.TicketPriorityGridId = item.TicketPriorityId;
                        item.TicketPriorityGridName = item.TicketPriority.Name;
                        item.NotesCount = item.TicketNotes.Count;
                        item.AttachemntsCount = item.Attachments.Count;
                        item.RelatedLocationsCount = item.TicketRelatedLocations.Count;
                        item.DateTimeResolutionTimeString = item.ExpectedResolutionTime.ToString();
                        item.DateTimeResponseTimeString = item.ExpectedResponseTime.ToString();

                        if (item.UserResolutionTime == null)
                        {
                            item.DateTimeResolutionedTimeString = "Çözülmedi";
                        }
                        else
                        {
                            item.DateTimeResolutionedTimeString = item.UserResolutionTime.ToString();
                        }
                        if (item.UserResponseTime == null)
                        {
                            item.DateTimeResponsedTimeString = "Müdehale Edilmedi";
                        }
                        else
                        {
                            item.DateTimeResponsedTimeString = item.UserResponseTime.ToString();
                        }

                        if (item.TickedAssignedAssignmentGroup == null)
                        {
                            item.TicketAssignedGrupString = "Seçili Grup Yok";
                        }
                        else
                        {
                            item.TicketAssignedGrupString = item.TickedAssignedAssignmentGroup.GroupName;
                        }

                        if (item.TicketAssignedUser == null)
                        {
                            item.TicketAssignedUserName = "Sorumlu Kişi";
                            item.TicketAssignedUserLastName = "Yok";
                        }
                        else
                        {
                            item.TicketAssignedUserName = item.TicketAssignedUser.FirstName;
                            item.TicketAssignedUserLastName = item.TicketAssignedUser.LastName;
                        }
                    }
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
                ii = ii + 0;
                throw;
            }
        }

        private Tuple<List<int>, List<int>> GetIdsOfTicketReasonCategoriesByHierarchicalIds(string[] hierarchicalIdArr)
        {
            if ((hierarchicalIdArr != null) && (hierarchicalIdArr.Length > 0))
            {
                HttpClient httpClient = new();
                httpClient.BaseAddress = new Uri("http://localhost:33400/");

                JsonSerializerOptions options = new JsonSerializerOptions();
                options.Converters.Add(new CustomJsonConverterForType());

                List<int> resultCategory = new();
                List<int> resultReason = new();

                for (int i = 0; i < hierarchicalIdArr.Length; i++)
                {
                    if (!(String.IsNullOrEmpty(hierarchicalIdArr[i])))
                    {
                        BusinessLogicRequest request = new()
                        {
                            ApiUrl = new Uri(httpClient.BaseAddress, "TicketReasonCategoryWrapper/GetById"),
                            RequestDomain = RemoteIncomingDomains.DiasTesisYonetimWebClient,
                            AdditionalParamTypes = new() { typeof(string) },
                            AdditionalParamJsons = new() { JsonConvert.SerializeObject(hierarchicalIdArr[i]) }
                        };

                        HttpResponseMessage resultRequest = httpClient.PostAsJsonAsync(request.ApiUrl, request, options).Result;
                        string modelStr = resultRequest.Content.ReadAsStringAsync().Result;

                        BusinessLogicResponse businessLogicResponse = JsonConvert.DeserializeObject<BusinessLogicResponse>(modelStr);

                        if (businessLogicResponse.ErrorObj.BusinessOperationSucceed)
                        {
                            StandartDTO.TicketReasonCategoryV2Dto responseDto = JsonConvert.DeserializeObject<StandartDTO.TicketReasonCategoryV2Dto>(businessLogicResponse.OptionalJsonResult);

                            //Şimdi bu kategori altındaki altında yaprağı olmayan tüm nodeları bulalım
                            BusinessLogicRequest requestInner2 = new()
                            {
                                ApiUrl = new Uri(httpClient.BaseAddress, "TicketReasonCategoryWrapper/GetByIdLastNodes"),
                                RequestDomain = RemoteIncomingDomains.DiasTesisYonetimWebClient,
                                AdditionalParamTypes = new() { typeof(string) },
                                AdditionalParamJsons = new() { JsonConvert.SerializeObject(responseDto.HierarchyId.ToString()) }
                            };

                            HttpResponseMessage resultRequestInner2 = httpClient.PostAsJsonAsync(requestInner2.ApiUrl, requestInner2, options).Result;
                            string modelStrInner2 = resultRequestInner2.Content.ReadAsStringAsync().Result;

                            BusinessLogicResponse businessLogicResponseInner2 = JsonConvert.DeserializeObject<BusinessLogicResponse>(modelStrInner2);


                            if ((businessLogicResponseInner2.ErrorObj != null) && (businessLogicResponseInner2.ErrorObj.BusinessOperationSucceed))
                            {
                                List<StandartDTO.TicketReasonCategoryV2Dto> responseDtoListInner =
                                        JsonConvert.DeserializeObject<List<StandartDTO.TicketReasonCategoryV2Dto>>(businessLogicResponseInner2.OptionalJsonResult);

                                foreach (StandartDTO.TicketReasonCategoryV2Dto item in responseDtoListInner)
                                {
                                    resultCategory.Add(item.Id);
                                }
                            }
                        }
                        else//Kategori değil reason olabilir
                        {
                            //muhtemel reason idyi çıkar                           
                            string[] idSOfhierarchicalIdArr = hierarchicalIdArr[i].Split("/");
                            if ((idSOfhierarchicalIdArr != null) && (idSOfhierarchicalIdArr.Length > 1))
                            {
                                int idOfTicketReason;
                                bool successConversionFlag = int.TryParse(idSOfhierarchicalIdArr[idSOfhierarchicalIdArr.Length - 2], out idOfTicketReason);

                                if (successConversionFlag && idOfTicketReason > 0)
                                {
                                    //bu id ile ticket reason var mı bak
                                    BusinessLogicRequest requestInner = new()
                                    {
                                        ApiUrl = new Uri(httpClient.BaseAddress, "TicketReason/GetById"),
                                        RequestDomain = RemoteIncomingDomains.DiasTesisYonetimWebClient,
                                        AdditionalParamTypes = new() { typeof(int) },
                                        AdditionalParamJsons = new() { JsonConvert.SerializeObject(idOfTicketReason) }
                                    };

                                    HttpResponseMessage resultRequestInner = httpClient.PostAsJsonAsync(requestInner.ApiUrl, requestInner, options).Result;
                                    string modelStrInner = resultRequestInner.Content.ReadAsStringAsync().Result;

                                    BusinessLogicResponse businessLogicResponseInner = JsonConvert.DeserializeObject<BusinessLogicResponse>(modelStrInner);

                                    if ((businessLogicResponseInner.ErrorObj != null) && (businessLogicResponseInner.ErrorObj.BusinessOperationSucceed))
                                    {
                                        StandartDTO.TicketReasonDto responseDto = JsonConvert.DeserializeObject<StandartDTO.TicketReasonDto>(businessLogicResponseInner.OptionalJsonResult);

                                        resultReason.Add(responseDto.Id);
                                    }
                                }
                            }
                        }
                    }
                }

                return new Tuple<List<int>, List<int>>(resultCategory, resultReason);
            }

            return null;
        }

        private List<int> GetIdsOfTicketReasonOfTicketReasonCategoriesById(int categoryId)
        {
            HttpClient httpClient = new();
            httpClient.BaseAddress = new Uri("http://localhost:33400/");

            JsonSerializerOptions options = new JsonSerializerOptions();
            options.Converters.Add(new CustomJsonConverterForType());

            List<int> resultReason = new();

            BusinessLogicRequest request = new()
            {
                ApiUrl = new Uri(httpClient.BaseAddress, "TicketReason/GetByTicketReasonCategoryId"),
                RequestDomain = RemoteIncomingDomains.DiasTesisYonetimWebClient,
                AdditionalParamTypes = new() { typeof(string) },
                AdditionalParamJsons = new() { JsonConvert.SerializeObject(categoryId) }
            };

            HttpResponseMessage resultRequest = httpClient.PostAsJsonAsync(request.ApiUrl, request, options).Result;
            string modelStr = resultRequest.Content.ReadAsStringAsync().Result;

            BusinessLogicResponse businessLogicResponse = JsonConvert.DeserializeObject<BusinessLogicResponse>(modelStr);

            if (businessLogicResponse.ErrorObj.BusinessOperationSucceed)
            {
                List<StandartDTO.TicketReasonDto> responseDto = JsonConvert.DeserializeObject<List<StandartDTO.TicketReasonDto>>(businessLogicResponse.OptionalJsonResult);

                foreach (StandartDTO.TicketReasonDto item in responseDto)
                {
                    resultReason.Add(item.Id);
                }

                return resultReason;
            }
            else
            {
                return null;
            }
        }

        private List<int> GetIdsOfTicketLocationByHierarchicalIds(string[] hierarchicalIdArr)
        {
            if ((hierarchicalIdArr != null) && (hierarchicalIdArr.Length > 0))
            {
                HttpClient httpClient = new();
                httpClient.BaseAddress = new Uri("http://localhost:33400/");

                JsonSerializerOptions options = new JsonSerializerOptions();
                options.Converters.Add(new CustomJsonConverterForType());

                List<int> result = new();

                for (int i = 0; i < hierarchicalIdArr.Length; i++)
                {
                    if (!(String.IsNullOrEmpty(hierarchicalIdArr[i])))
                    {
                        BusinessLogicRequest request = new()
                        {
                            ApiUrl = new Uri(httpClient.BaseAddress, "LocationWrapper/GetById"),
                            RequestDomain = RemoteIncomingDomains.DiasTesisYonetimWebClient,
                            AdditionalParamTypes = new() { typeof(string) },
                            AdditionalParamJsons = new() { JsonConvert.SerializeObject(hierarchicalIdArr[i]) }
                        };

                        HttpResponseMessage resultRequest = httpClient.PostAsJsonAsync(request.ApiUrl, request, options).Result;
                        string modelStr = resultRequest.Content.ReadAsStringAsync().Result;

                        BusinessLogicResponse businessLogicResponse = JsonConvert.DeserializeObject<BusinessLogicResponse>(modelStr);

                        if (businessLogicResponse.ErrorObj.BusinessOperationSucceed)
                        {
                            CustomDTO.CustomLocationDto responseDto = JsonConvert.DeserializeObject<CustomDTO.CustomLocationDto>(businessLogicResponse.OptionalJsonResult);

                            //Şimdi bu kategori altındaki altında yaprağı olmayan tüm nodeları bulalım
                            BusinessLogicRequest requestInner2 = new()
                            {
                                ApiUrl = new Uri(httpClient.BaseAddress, "LocationWrapper/GetByIdLastNodes"),
                                RequestDomain = RemoteIncomingDomains.DiasTesisYonetimWebClient,
                                AdditionalParamTypes = new() { typeof(string) },
                                AdditionalParamJsons = new() { JsonConvert.SerializeObject(responseDto.HierarchyId.ToString()) }
                            };

                            HttpResponseMessage resultRequestInner2 = httpClient.PostAsJsonAsync(requestInner2.ApiUrl, requestInner2, options).Result;
                            string modelStrInner2 = resultRequestInner2.Content.ReadAsStringAsync().Result;

                            BusinessLogicResponse businessLogicResponseInner2 = JsonConvert.DeserializeObject<BusinessLogicResponse>(modelStrInner2);


                            if ((businessLogicResponseInner2.ErrorObj != null) && (businessLogicResponseInner2.ErrorObj.BusinessOperationSucceed))
                            {
                                List<CustomDTO.CustomLocationDto> responseDtoListInner =
                                        JsonConvert.DeserializeObject<List<CustomDTO.CustomLocationDto>>(businessLogicResponseInner2.OptionalJsonResult);

                                foreach (CustomDTO.CustomLocationDto item in responseDtoListInner)
                                {
                                    result.Add(item.Id);
                                }
                            }
                        }
                    }
                }

                return result;
            }

            return null;
        }

        private List<int> GetIdsOfTicketByRelatedLocationByIds(List<int> ticketLocationIds)
        {
            if ((ticketLocationIds != null) && (ticketLocationIds.Count > 0))
            {
                HttpClient httpClient = new();
                httpClient.BaseAddress = new Uri("http://localhost:33400/");

                JsonSerializerOptions options = new JsonSerializerOptions();
                options.Converters.Add(new CustomJsonConverterForType());

                List<int> result = new();
                List<int> resultRemovedDuplicates = new();

                BusinessLogicRequest request = new()
                {
                    ApiUrl = new Uri(httpClient.BaseAddress, "TicketRelatedLocation/GetAll"),
                    RequestDomain = RemoteIncomingDomains.DiasTesisYonetimWebClient,
                };

                HttpResponseMessage resultRequest = httpClient.PostAsJsonAsync(request.ApiUrl, request, options).Result;
                string modelStr = resultRequest.Content.ReadAsStringAsync().Result;

                BusinessLogicResponse businessLogicResponse = JsonConvert.DeserializeObject<BusinessLogicResponse>(modelStr);

                if ((businessLogicResponse.ErrorObj != null) && (businessLogicResponse.ErrorObj.BusinessOperationSucceed))
                {
                    List<StandartDTO.TicketRelatedLocationDto> responseDtoListInner =
                            JsonConvert.DeserializeObject<List<StandartDTO.TicketRelatedLocationDto>>(businessLogicResponse.OptionalJsonResult);

                    foreach (int item in ticketLocationIds)
                    {
                        List<StandartDTO.TicketRelatedLocationDto> resultInner = responseDtoListInner.Where(p => p.TicketLocationId == item).ToList();

                        foreach (StandartDTO.TicketRelatedLocationDto itemInner in resultInner)
                        {
                            result.Add(itemInner.TicketId);
                        }
                    }

                    //Duplicate olanları ayıklayalım
                    resultRemovedDuplicates = result.Distinct().ToList();

                    return resultRemovedDuplicates;
                }
            }

            return null;
        }

        public IActionResult OnGetDisabledBoolenByUserRole(int ticketOwnerUserId)
        {
            var userTicketRoles = SessionHelper.GetObjectFromJson(HttpContext.Session, "userTicketRole");
            var listTicketRoleTicketPageReadOnlyAttribute = JsonConvert.DeserializeObject<List<Tuple<List<Tuple<WebTicketPageDefinitions, string>>, int>>>(userTicketRoles);
            var user = SessionHelper.GetObjectFromJson(HttpContext.Session, "user");
            var userDto = JsonConvert.DeserializeObject<StandartDTO.UserDto>(user);
            int loginUserId = Convert.ToInt32(userDto.Id);
            List<Tuple<WebTicketPageDefinitions, string>> ad = new();
            List<ControlOfDisable> listcontrolOfDisable = new();

            if (loginUserId == ticketOwnerUserId)
            {
                foreach (var item in listTicketRoleTicketPageReadOnlyAttribute)
                {
                    if (item.Item2 == 3)
                    {
                        ad = item.Item1;
                        foreach (var item2 in item.Item1)
                        {
                            ControlOfDisable controlOfDisable = new();
                            controlOfDisable.Key = item2.Item1.ToString();
                            //false göndermemizin sebebi UI da disabled özelliği false olunca görünmesine izin veriyor.
                            controlOfDisable.Value = false;
                            listcontrolOfDisable.Add(controlOfDisable);
                        }
                    }
                }
            }
            else
            {
                foreach (var item in listTicketRoleTicketPageReadOnlyAttribute)
                {
                    if (item.Item2 == 3)
                    {
                        ad = item.Item1;
                        foreach (var item2 in item.Item1)
                        {
                            ControlOfDisable controlOfDisable = new();
                            controlOfDisable.Key = item2.Item1.ToString();
                            controlOfDisable.Value = true;
                            listcontrolOfDisable.Add(controlOfDisable);
                        }
                    }
                }
            }
            return new JsonResult(listcontrolOfDisable);
        }

        public async Task<IActionResult> OnGetSurveyQuestion(DataSourceLoadOptions loadOptions)
        {

            using (StreamReader r = new StreamReader("SampleData/survey.json"))
            {
                string json = r.ReadToEnd();
                List<StandartDTO.SurveyQuestionDto> ro = JsonConvert.DeserializeObject<List<StandartDTO.SurveyQuestionDto>>(json);
                return new JsonResult(DataSourceLoader.Load(ro, loadOptions));
            }


            return new JsonResult(DataSourceLoader.Load(new List<StandartDTO.SurveyQuestionDto>(), loadOptions));
        }
    }
}
