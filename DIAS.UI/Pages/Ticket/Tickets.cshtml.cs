using System;
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
using DevExtreme.AspNet.Mvc;
using DIAS_UI;
using DIAS_UI.Helpers;
using DiasShared.Operations.EnumOperations;
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
                            //var result = httpClient.GetAsync("http://localhost:33400/LocationWrapper/GetAll").Result;
                            var result = httpClient.PostAsJsonAsync(request.ApiUrl, request).Result;
                            var response = result.Content.ReadAsStringAsync().Result;
                            BusinessLogicResponse businessLogicRequestResponse = JsonConvert.DeserializeObject<BusinessLogicResponse>(response);
                            var locationList = businessLogicRequestResponse.RelevantDto.ToString();
                            var locations = JsonConvert.DeserializeObject<List<DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom.CustomLocationDto>>(locationList);
                            Locations = locations;
                        }
                    }
                }
            }            
        }
        [ViewData]
        public StandartDTO.UserDto UserViewData { get; set; }
        [ViewData]
        public List<CustomDTO.CustomLocationDto> Locations { get; set; }
        [BindProperty]
        public IFormFile NotesFile { get; set; }
        public List<CustomDTO.CustomTicketDto> customTicketDtos { get; set; }
        public IActionResult OnGetGridData(DataSourceLoadOptions loadOptions)
        {

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
                            HttpClient httpClient = new();
                            httpClient.BaseAddress = new Uri("http://localhost:33400/");
                            DevExpressRequest gridRequest = null;
                            if ((loadOptions.Filter != null) || (loadOptions.Skip != 0))
                            {
                                DataSourceLoadOptions clonedDataSourceLoadOptions = new();
                                loadOptions.CopyProperties(((object)(clonedDataSourceLoadOptions)));

                                gridRequest = new DevExpressRequest()
                                {
                                    AdditionalDevExpressParamJson = JsonConvert.SerializeObject(clonedDataSourceLoadOptions),                                   
                                    RequestOptions = new((DataSourceLoadOptions)(clonedDataSourceLoadOptions)) 
                                }; 

                                //gridRequest.RequestOptions.DataSourceLoadOption.Filter = new string[3];

                                //for (int i = 0; i < 3; i++)
                                //{
                                //    gridRequest.RequestOptions.DataSourceLoadOption.Filter[i] = i.ToString();
                                //}


                                 
                            }

                            BusinessLogicRequest request = new()
                            {
                                ApiUrl = new Uri(httpClient.BaseAddress, "TicketWrapper/GetAll"),
                                DevExpressRequestObj = gridRequest,
                                RequestDomain = RemoteIncomingDomains.DiasTesisYonetimWebClient
                            };

                            var result = httpClient.PostAsJsonAsync(request.ApiUrl, request).Result;
                            var data = result.Content.ReadAsStringAsync().Result;
                            BusinessLogicResponse businessLogicRequestResponse = JsonConvert.DeserializeObject<BusinessLogicResponse>(data);
                            var response = JsonConvert.DeserializeObject(data);

                            if (businessLogicRequestResponse.ErrorObj.BusinessOperationSucceed == true)
                            {
                                _notyfService.Success($"{businessLogicRequestResponse.ErrorObj.DisplayMessage}");
                            }
                            else
                            {
                                _notyfService.Error($"{businessLogicRequestResponse.ErrorObj.DisplayMessage}");
                                return new JsonResult(DataSourceLoader.Load(new List<CustomDTO.CustomTicketDto>(), loadOptions));
                            }

                            var myList = businessLogicRequestResponse.RelevantDto.ToString();
                            listModel = JsonConvert.DeserializeObject<List<CustomDTO.CustomTicketDto>>(myList);

                            foreach (var item in listModel)
                            {
                                item.TicketPriorityGridId = item.TicketPriorityId;
                                item.TicketPriorityGridName = item.TicketPriority.Name;                                
                                item.NotesCount = item.TicketNotes.Count;
                                item.AttachemntsCount = item.Attachments.Count;
                                item.RelatedLocationsCount = item.TicketRelatedLocations.Count;
                                item.DateTimeResolutionTimeString = item.ExpectedResolutionTime.ToString();
                                item.DateTimeResponseTimeString = item.ExpectedResponseTime.ToString();

                                var sonuc = "";

                                for (var i = 0; i < (10 - item.Id.ToString().Length); i++)
                                {
                                    sonuc += "0";
                                }

                                var ticketCode = item.LocationNameGetByCodeId + sonuc + item.Id.ToString();
                                item.TicketCode = ticketCode;
                            }
                            var user = SessionHelper.GetObjectFromJson(HttpContext.Session, "user");
                            var userDto = JsonConvert.DeserializeObject<StandartDTO.UserDto>(user);
                            int loginUserId = Convert.ToInt32(userDto.Id); //Ayşe Yücel
                            //listModel.Where(x => x.AddedByUser.Id == loginUserId.ToString());
                            _cache.Set("customTicketDtos", listModel);                            
                            return new JsonResult(DataSourceLoader.Load(listModel, loadOptions));
                        }
                    }
                }
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
                            Newtonsoft.Json.JsonSerializer jsonWriter = new()
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            };
                            var options = new JsonSerializerOptions();
                            options.Converters.Add(new CustomJsonConverterForType());
                            List<StandartDTO.TicketRelatedLocationDto> list = new();
                            string[] hierarchy = model.TicketRelatedLocationHierarchyId[0].Split(",");
                            foreach (var item in hierarchy)
                            {
                                BusinessLogicRequest request2 = new()
                                {
                                    ApiUrl = new Uri(httpClient.BaseAddress, "LocationWrapper/GetById?hierarchyId=" + item),
                                    RequestDomain = RemoteIncomingDomains.DiasTesisYonetimWebClient
                                };                                
                                var resultLocation = httpClient.PostAsJsonAsync(request2.ApiUrl, request2, options).Result;
                                var locationModel = resultLocation.Content.ReadAsStringAsync().Result;
                                BusinessLogicResponse businessLogicRequestResponseLocation = JsonConvert.DeserializeObject<BusinessLogicResponse>(locationModel);
                                var responseLocation = JsonConvert.DeserializeObject<DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard.LocationV2Dto>(businessLogicRequestResponseLocation.OptionalJsonResult);
                                StandartDTO.TicketRelatedLocationDto ticketRelatedLocationDto = new();
                                ticketRelatedLocationDto.TicketLocationId = responseLocation.Id;
                                ticketRelatedLocationDto.AddedByUserId = model.AddedByUserId;
                                list.Add(ticketRelatedLocationDto);
                            }
                            model.TicketRelatedLocations = list;
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
                                return new BadRequestResult();
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

                            Newtonsoft.Json.JsonSerializer jsonWriter = new()
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            };
                            var options = new JsonSerializerOptions();
                            options.Converters.Add(new CustomJsonConverterForType());
                            var match = Regex.Match(model.TicketReasonHierarchyId, @"\/[^\/]*\/", RegexOptions.RightToLeft);
                            var ticketId = Convert.ToInt32(match.Value.Replace("/", ""));
                            model.TicketAssignedUserId = 2;
                            model.TicketReasonId = ticketId;
                            BusinessLogicRequest request2 = new()
                            {
                                ApiUrl = new Uri(httpClient.BaseAddress, "TicketWrapper/GetById/" + model.Id),
                                RequestDomain = RemoteIncomingDomains.DiasTesisYonetimWebClient
                            };
                            var result = httpClient.PostAsJsonAsync(request2.ApiUrl, request2, options).Result;
                            var response = result.Content.ReadAsStringAsync().Result;
                            BusinessLogicResponse businessLogicRequestResponse = JsonConvert.DeserializeObject<BusinessLogicResponse>(response);
                            var ticket = businessLogicRequestResponse.RelevantDto.ToString();
                            var ticketResult = JsonConvert.DeserializeObject<CustomDTO.CustomTicketDto>(ticket);
                            List<StandartDTO.TicketRelatedLocationDto> list = new();
                            string[] hierarchy = model.TicketRelatedLocationHierarchyId[0].Split(",");
                            foreach (var item in hierarchy)
                            {
                                BusinessLogicRequest request3 = new()
                                {
                                    ApiUrl = new Uri(httpClient.BaseAddress, "LocationWrapper/GetById?hierarchyId=" + item),
                                    RequestDomain = RemoteIncomingDomains.DiasTesisYonetimWebClient
                                };                                
                                var resultLocation = httpClient.PostAsJsonAsync(request3.ApiUrl, request3, options).Result;
                                var locationModel = resultLocation.Content.ReadAsStringAsync().Result;
                                BusinessLogicResponse businessLogicRequestResponseLocation = JsonConvert.DeserializeObject<BusinessLogicResponse>(locationModel);
                                var responseLocation = JsonConvert.DeserializeObject<DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard.LocationV2Dto>(businessLogicRequestResponseLocation.OptionalJsonResult);
                                StandartDTO.TicketRelatedLocationDto ticketRelatedLocationDto = new();
                                ticketRelatedLocationDto.TicketLocationId = responseLocation.Id;
                                ticketRelatedLocationDto.AddedByUserId = model.AddedByUserId;
                                list.Add(ticketRelatedLocationDto);
                            }
                            model.TicketRelatedLocations = list;                            
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

                            var user = SessionHelper.GetObjectFromJson(HttpContext.Session, "user");
                            var userDto = JsonConvert.DeserializeObject<StandartDTO.UserDto>(user);
                            int loginUserId = Convert.ToInt32(userDto.Id);

                            BusinessLogicRequest request = new()
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
                            var resultUpdate = httpClient.PostAsJsonAsync(request.ApiUrl, request, options).Result;
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
                                return new BadRequestResult();
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
        public async Task<IActionResult> OnGetTicketAssignmentGroupData()
        {
            HttpClient httpClient = new();
            httpClient.BaseAddress = new Uri("http://localhost:33400/");
            List<CustomTicketDto> customTicketDto = new();
            return null;
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
                            var a = list.Where(x => x.Id == ticketId);
                            List<int> b = new();
                            List<StandartDTO.TicketStateTransitionFlowDto> ticketStateFlowDtos = new();
                            List<StandartDTO.TicketStateTransitionFlowDto> ticketStateFlowDtosDuplicatesIncluded = new();
                            List<StandartDTO.TicketStateTransitionFlowDto> cacheTicketStateTransitionFlowDtos = new();
                            List<CustomDTO.CustomTicketStateFlowDto> customTicketStateFlowDto = new();
                            CustomDTO.CustomTicketDto ticketWorkingOn = new();
                            foreach (var item in list.Where(x => x.Id == ticketId))
                            {
                                ticketWorkingOn = item;
                                ticketStateDtos.Add(item.TicketStatus.TicketStateTransitionSourceTicketStates[0].DestinationTicketState);
                            }

                            foreach (var item in a)
                            {
                                b = item.TicketStatus.TicketStateTransitionSourceTicketStates.Select(x => x.Id).ToList();
                            }

                            HttpClient httpClient = new();
                            httpClient.BaseAddress = new Uri("http://localhost:33400/");
                            BusinessLogicRequest request = new()
                            {
                                ApiUrl = new Uri(httpClient.BaseAddress, "TicketStateFlowWrapper/GetAll"),
                                RequestDomain = RemoteIncomingDomains.DiasTesisYonetimWebClient
                            };

                            var result = httpClient.PostAsJsonAsync(request.ApiUrl, request).Result;
                            var response = result.Content.ReadAsStringAsync().Result;
                            BusinessLogicResponse businessLogicRequestResponse = JsonConvert.DeserializeObject<BusinessLogicResponse>(response);
                            var ticketStateFlowList = businessLogicRequestResponse.RelevantDto.ToString();
                            var ticketStateFlowLists = JsonConvert.DeserializeObject<List<CustomDTO.CustomTicketStateFlowDto>>(ticketStateFlowList);
                            foreach (var item in ticketStateFlowLists)
                            {

                                foreach (var item2 in item.TicketStateTransitionFlowByTicketStateFlows)
                                {
                                    cacheTicketStateTransitionFlowDtos.Add(item2);
                                    foreach (var item3 in b)
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

                                            if (loginUserId == ticketWorkingOn.TicketReportedUserId)
                                            {
                                                if (((item2.TicketStateFlowId.GetEnumValue<TicketStateFlowEnum>() == TicketStateFlowEnum.ASSIGNTOUSER) && (item2.Id == 1)) ||
                                                     ((item2.TicketStateFlowId.GetEnumValue<TicketStateFlowEnum>() == TicketStateFlowEnum.ASSIGNTOGROUP) && (item2.Id == 2)) ||
                                                       ((item2.TicketStateFlowId.GetEnumValue<TicketStateFlowEnum>() == TicketStateFlowEnum.SUSPEND) && (item2.Id == 22)) ||
                                                         ((item2.TicketStateFlowId.GetEnumValue<TicketStateFlowEnum>() == TicketStateFlowEnum.ASSIGNTOUSER) && (item2.Id == 23)) ||
                                                           ((item2.TicketStateFlowId.GetEnumValue<TicketStateFlowEnum>() == TicketStateFlowEnum.CLOSE) && (item2.Id == 9)) ||
                                                            ((item2.TicketStateFlowId.GetEnumValue<TicketStateFlowEnum>() == TicketStateFlowEnum.REOPEN) && (item2.Id == 14)))
                                                {
                                                    ticketStateFlowDtosDuplicatesIncluded.Add(item2);
                                                }
                                            } 

                                            //Yetkiler birleşebileceği için else kullanmıyoruz
                                            if ((loginUserId == ticketWorkingOn.AddedByUserId) || (loginUserId == (ticketWorkingOn.LastModifiedByUserId ?? -1)) || (loginUserId == ticketWorkingOn.TicketOwnerUserId))
                                            {
                                                if (((item2.TicketStateFlowId.GetEnumValue<TicketStateFlowEnum>() == TicketStateFlowEnum.ASSIGNTOUSER) && (item2.Id == 1)) ||
                                                     ((item2.TicketStateFlowId.GetEnumValue<TicketStateFlowEnum>() == TicketStateFlowEnum.ASSIGNTOGROUP) && (item2.Id == 2)) ||
                                                      ((item2.TicketStateFlowId.GetEnumValue<TicketStateFlowEnum>() == TicketStateFlowEnum.SUSPEND) && (item2.Id == 22)) ||
                                                        ((item2.TicketStateFlowId.GetEnumValue<TicketStateFlowEnum>() == TicketStateFlowEnum.ASSIGNTOUSER) && (item2.Id == 23)))
                                                {
                                                    ticketStateFlowDtosDuplicatesIncluded.Add(item2);
                                                }
                                            }

                                            //Yetkiler birleşebileceği için else kullanmıyoruz
                                            if (loginUserId == ticketWorkingOn.TicketAssignedUserId)
                                            {
                                                if (((item2.TicketStateFlowId.GetEnumValue<TicketStateFlowEnum>() == TicketStateFlowEnum.REJECT) && (item2.Id == 3)) ||
                                                      ((item2.TicketStateFlowId.GetEnumValue<TicketStateFlowEnum>() == TicketStateFlowEnum.WAIT) && (item2.Id == 25)) ||
                                                        ((item2.TicketStateFlowId.GetEnumValue<TicketStateFlowEnum>() == TicketStateFlowEnum.CANCEL) && (item2.Id == 19)) ||
                                                          ((item2.TicketStateFlowId.GetEnumValue<TicketStateFlowEnum>() == TicketStateFlowEnum.ASSIGNTOUSER) && (item2.Id == 5)) ||
                                                           ((item2.TicketStateFlowId.GetEnumValue<TicketStateFlowEnum>() == TicketStateFlowEnum.ASSIGNTOGROUP) && (item2.Id == 6)) ||
                                                             ((item2.TicketStateFlowId.GetEnumValue<TicketStateFlowEnum>() == TicketStateFlowEnum.STARTTOWORKING) && (item2.Id == 4)) ||
                                                              ((item2.TicketStateFlowId.GetEnumValue<TicketStateFlowEnum>() == TicketStateFlowEnum.RESOLVE) && (item2.Id == 7)) ||
                                                               ((item2.TicketStateFlowId.GetEnumValue<TicketStateFlowEnum>() == TicketStateFlowEnum.STARTTOWORKING) && (item2.Id == 8)) ||
                                                                 ((item2.TicketStateFlowId.GetEnumValue<TicketStateFlowEnum>() == TicketStateFlowEnum.ASSIGNTOUSER) && (item2.Id == 20)) ||
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
                                                       ((item2.TicketStateFlowId.GetEnumValue<TicketStateFlowEnum>() == TicketStateFlowEnum.CANCEL) && (item2.Id == 19)) ||
                                                         ((item2.TicketStateFlowId.GetEnumValue<TicketStateFlowEnum>() == TicketStateFlowEnum.ASSIGNTOUSER) && (item2.Id == 5)) ||
                                                           ((item2.TicketStateFlowId.GetEnumValue<TicketStateFlowEnum>() == TicketStateFlowEnum.ASSIGNTOGROUP) && (item2.Id == 6)) ||
                                                             ((item2.TicketStateFlowId.GetEnumValue<TicketStateFlowEnum>() == TicketStateFlowEnum.ASSIGNTOUSER) && (item2.Id == 20)) ||
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
        public async Task<IActionResult> OnGetTicketDataById(int ticketId)
        {
            var controllersActionsJson = SessionHelper.GetObjectFromJson(HttpContext.Session, "userInfo");
            var controllersActions = JsonConvert.DeserializeObject<List<Tuple<ApiControllerDescription, List<string>>>>(controllersActionsJson);
            foreach (var controllerAction in controllersActions)
            {
                if (controllerAction.Item1.ToString() == "TicketWrapper")
                {
                    foreach (var action in controllerAction.Item2)
                    {
                        if (action == "GetById")
                        {
                            HttpClient httpClient = new();
                            httpClient.BaseAddress = new Uri("http://localhost:33400/");
                            BusinessLogicRequest request = new()
                            {
                                ApiUrl = new Uri(httpClient.BaseAddress, "TicketWrapper/GetById/" + ticketId),
                                RequestDomain = RemoteIncomingDomains.DiasTesisYonetimWebClient
                            };
                            Newtonsoft.Json.JsonSerializer jsonWriter = new()
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            };

                            var options = new JsonSerializerOptions();
                            options.Converters.Add(new CustomJsonConverterForType());

                            var result = httpClient.PostAsJsonAsync(request.ApiUrl, request).Result;                            
                            var response = result.Content.ReadAsStringAsync().Result;
                            BusinessLogicResponse businessLogicRequestResponse = JsonConvert.DeserializeObject<BusinessLogicResponse>(response);
                            var ticketDetail = businessLogicRequestResponse.RelevantDto.ToString();
                            var ticketDetailResult = JsonConvert.DeserializeObject<CustomDTO.CustomTicketDto>(ticketDetail);
                            List<CustomDTO.CustomTicketDto> listDetail = new();
                            listDetail.Add(ticketDetailResult);

                            //TODO : lokasyon adları çağrılcak
                            //TODO : Refactor, recursive olacak, seviye sayısını bilemeyebiliriz
                            foreach (var item in listDetail)
                            {
                                foreach (var item2 in item.TicketRelatedLocations)
                                {
                                    string Id = item2.TicketLocation.HierarchyId;
                                    var a = Id.Substring(0, Id.Length - 2);
                                    BusinessLogicRequest request2 = new()
                                    {
                                        ApiUrl = new Uri(httpClient.BaseAddress, "LocationWrapper/GetById?hierarchyId=" + a),
                                        RequestDomain = RemoteIncomingDomains.DiasTesisYonetimWebClient
                                    };                                    
                                    var result2 = httpClient.PostAsJsonAsync(request2.ApiUrl, request2).Result;
                                    var response2 = result2.Content.ReadAsStringAsync().Result;
                                    BusinessLogicResponse businessLogicRequestResponse2 = JsonConvert.DeserializeObject<BusinessLogicResponse>(response2);
                                    var locationList2 = businessLogicRequestResponse2.RelevantDto.ToString();
                                    var locations2 = JsonConvert.DeserializeObject<CustomDTO.CustomLocationDto>(locationList2);

                                    if (locations2.ParentHierarchy != null)
                                    {
                                        BusinessLogicRequest request3 = new()
                                        {
                                            ApiUrl = new Uri(httpClient.BaseAddress, "LocationWrapper/GetById?hierarchyId=" + locations2.ParentHierarchy),
                                            RequestDomain = RemoteIncomingDomains.DiasTesisYonetimWebClient
                                        };
                                        var result3 = httpClient.PostAsJsonAsync(request3.ApiUrl, request3).Result;
                                        var response3 = result3.Content.ReadAsStringAsync().Result;
                                        BusinessLogicResponse businessLogicRequestResponse3 = JsonConvert.DeserializeObject<BusinessLogicResponse>(response3);
                                        var locationList3 = businessLogicRequestResponse3.RelevantDto.ToString();
                                        var locations3 = JsonConvert.DeserializeObject<CustomDTO.CustomLocationDto>(locationList3);

                                        if (locations3.ParentHierarchy != null)
                                        {
                                            BusinessLogicRequest request4 = new()
                                            {
                                                ApiUrl = new Uri(httpClient.BaseAddress, "LocationWrapper/GetById?hierarchyId=" + locations3.ParentHierarchy),
                                                RequestDomain = RemoteIncomingDomains.DiasTesisYonetimWebClient
                                            };
                                            var result4 = httpClient.PostAsJsonAsync(request4.ApiUrl, request4).Result;
                                            var response4 = result4.Content.ReadAsStringAsync().Result;
                                            BusinessLogicResponse businessLogicRequestResponse4 = JsonConvert.DeserializeObject<BusinessLogicResponse>(response4);
                                            var locationList4 = businessLogicRequestResponse4.RelevantDto.ToString();
                                            var locations4 = JsonConvert.DeserializeObject<CustomDTO.CustomLocationDto>(locationList4);

                                            item2.TicketLocation.LocationName = locations4.LocationName + "-" + locations3.LocationName + "- " + locations2.LocationName + " - " + item2.TicketLocation.LocationName;
                                        }
                                        else
                                        {
                                            item2.TicketLocation.LocationName = locations3.LocationName + "- " + locations2.LocationName + " - " + item2.TicketLocation.LocationName;
                                        }
                                        
                                    }
                                    else
                                    {
                                        item2.TicketLocation.LocationName = locations2.LocationName + " - " + item2.TicketLocation.LocationName;
                                    }
                                }
                            }
                            return new JsonResult(listDetail);
                        }
                    }
                }
            }
            _notyfService.Error("İş Emri Detay Bilgilerini görme yetkiniz yoktur.");
            return new JsonResult(new List<CustomDTO.CustomTicketDto>());
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
                var base64EncodedBytes = System.Convert.FromBase64String(result);
                var realBase64 = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);

                return Content(realBase64);
            }
            catch (Exception e)
            {

                throw;
            }

        }
        public async Task<IActionResult> OnGetFileNoteAttachments(int Id, int attachmentId)
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
                            var result = Convert.ToBase64String(attc.FileData);
                            var base64EncodedBytes = System.Convert.FromBase64String(result);
                            realBase64 = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
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
                            //foreach (var item in ticketNotes)
                            //{
                            //    item.FirstLastName = item.AddedByUser.FirstName + " " + item.AddedByUser.LastName;
                            //}

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
                            //foreach (var item in attachs)
                            //{
                            //    item.FirstLastName = item.AddedByUser.FirstName + " " + item.AddedByUser.LastName;
                            //}

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
                                    attachment.AttachmentDescription = "denemeSonradanNote" + DateTime.Now.ToString("ddMMyyyy");
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

                            Newtonsoft.Json.JsonSerializer jsonWriter = new()
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            };
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
                                    attachment.AttachmentDescription = "denemeSonradan" + DateTime.Now.ToString("ddMMyyyy");

                                    //using (var fileStream = System.IO.File.Create(Path.Combine(path, newAttachmentFileName)))
                                    //{
                                    //    await item.CopyToAsync(fileStream);
                                    //}
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

                            Newtonsoft.Json.JsonSerializer jsonWriter = new()
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            };
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
        public async Task<IActionResult> OnPostChangeTicketState(int Id, int ticketStateId, int userId, int ticketId)
        {

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
                            List<CustomDTO.CustomTicketDto> list = new();
                            list = (List<CustomDTO.CustomTicketDto>)tickets;
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
                                if (item.TicketStateTransitionId == ticketStateId && item.TicketStateFlowId == Id)
                                {
                                    ticketStateTransitionFlowDto = item;
                                }
                            }
                            foreach (var item in list.Where(x=>x.Id == ticketId))
                            {
                                model = item;
                                model.TicketStatusId = item.TicketStatus.TicketStateTransitionSourceTicketStates[0].DestinationTicketStateId;
                            }


                            if (model.TicketAssignedUserId != 0)
                            {
                                model.TicketAssignedUserId = userId;
                            }
                            
                            HttpClient httpClient = new();
                            httpClient.BaseAddress = new Uri("http://localhost:33400/");

                            Newtonsoft.Json.JsonSerializer jsonWriter = new()
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            };
                            var options = new JsonSerializerOptions();
                            options.Converters.Add(new CustomJsonConverterForType());

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

        public async Task<IActionResult> OnPostFilter(StandartDTO.FilterDto model)
        {
            //saat dışında ki tarihi almak için 
            var ResoulitionedTime = model.ResoulitionedTime.Date;
            var TicketResponseTimeTargetedStart = model.TicketResponseTimeTargetedStart.Date;
            var TicketResponseTimeTargetedEnd = model.TicketResponseTimeTargetedEnd.Date;
            var TicketResolutionTimeTargetedStart = model.TicketResolutionTimeTargetedStart.Date;
            var TicketResolutionTimeTargetedEnd = model.TicketResolutionTimeTargetedEnd.Date;
            var ResponsedTime = model.ResponsedTime.Date;
            var TicketClosedTime = model.TicketClosedTime.Date;
            var TicketDateTicketStart = model.TicketDateTicketStart.Date;
            var TicketDateTicketEnd = model.TicketDateTicketEnd.Date;

            HttpClient httpClient = new();
            httpClient.BaseAddress = new Uri("http://localhost:33400/");
            Newtonsoft.Json.JsonSerializer jsonWriter = new()
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            List<int> ticketReasonIds = new();

            foreach (var item in model.TicketReasonIds)
            {
                var match = Regex.Match(item, @"\/[^\/]*\/", RegexOptions.RightToLeft);
                ticketReasonIds.Add(Convert.ToInt32(match.Value.Replace("/", "")));
            }

            var options = new JsonSerializerOptions();
            options.Converters.Add(new CustomJsonConverterForType());
            List<int> list = new();
            string[] hierarchy = model.FilterLocationIds[0].Split(",");

            foreach (var item in hierarchy)
            {
                BusinessLogicRequest request2 = new()
                {
                    ApiUrl = new Uri(httpClient.BaseAddress, "LocationWrapper/GetById?hierarchyId=" + item),
                    RequestDomain = RemoteIncomingDomains.DiasTesisYonetimWebClient
                };
                var resultLocation = httpClient.PostAsJsonAsync(request2.ApiUrl, request2, options).Result;
                var locationModel = resultLocation.Content.ReadAsStringAsync().Result;
                BusinessLogicResponse businessLogicRequestResponseLocation = JsonConvert.DeserializeObject<BusinessLogicResponse>(locationModel);
                var responseLocation = JsonConvert.DeserializeObject<StandartDTO.LocationV2Dto>(businessLogicRequestResponseLocation.OptionalJsonResult);                                
                list.Add(responseLocation.Id);
            }

            model.ListTicketReasonId = ticketReasonIds;
            model.ListLocationId = list;

            return null;
        }
    }
}
