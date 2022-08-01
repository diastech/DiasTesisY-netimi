using AspNetCoreHero.ToastNotification.Abstractions;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using DIAS_UI.Helpers;
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
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static DiasShared.Enums.Standart.AuthorizationEnums;
using static DiasShared.Enums.Standart.RemoteCommunicationEnums;
using CustomDTO = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using StandartDTO = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;



namespace DIAS.UI.Pages.Ticket
{
    public class CustomBasicTicketDto : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly INotyfService _notyfService;
        private readonly IMemoryCache _cache;
        public CustomBasicTicketDto(IMemoryCache cache,IConfiguration configuration, IWebHostEnvironment webHostEnvironment, INotyfService notyfService)
        {
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
            _notyfService = notyfService;
            _cache = cache;
        }
        public async Task<IActionResult> OnGetGridData(DataSourceLoadOptions loadOptions)
        {
            //if (loadOptions.Take == 0)
            //{
            //    var loadoptionsTake = _cache.Get("loadOptionsTakeFastTicket");
            //    int loadoptionsTakeValue = (int)loadoptionsTake;
            //    loadOptions.Take = loadoptionsTakeValue;
            //}
            //if (loadOptions.Take >= 10)
            //{
            //    _cache.Set("loadOptionsTakeFastTicket", loadOptions.Take);
            //}
            //HttpClient httpClient = new();
            //httpClient.BaseAddress = new Uri("http://localhost:33400/");
            //DevExpressRequest gridRequest = null;
            //if ((loadOptions.Filter != null) || (loadOptions.Skip != 0))
            //{
            //    DataSourceLoadOptions clonedDataSourceLoadOptions = new();
            //    loadOptions.CopyProperties(((object)(clonedDataSourceLoadOptions)));
            //    gridRequest = new DevExpressRequest()
            //    {
            //        AdditionalDevExpressParamJson = JsonConvert.SerializeObject(clonedDataSourceLoadOptions),
            //        RequestOptions = new((DataSourceLoadOptions)(clonedDataSourceLoadOptions))
            //    };
            //}
            //BusinessLogicRequest request = new()
            //{
            //    ApiUrl = new Uri(httpClient.BaseAddress, "BasicTicketWrapper/GetAll"),
            //    DevExpressRequestObj = gridRequest,
            //    RequestDomain = RemoteIncomingDomains.DiasTesisYonetimWebClient
            //};
            //var result = httpClient.PostAsJsonAsync(request.ApiUrl, request).Result;
            //var data = result.Content.ReadAsStringAsync().Result;
            //BusinessLogicResponse businessLogicRequestResponse = JsonConvert.DeserializeObject<BusinessLogicResponse>(data);
            //var dataList = businessLogicRequestResponse.RelevantDto.ToString();
            //var listModel = JsonConvert.DeserializeObject<List<CustomDTO.CustomBasicTicketDto>>(dataList);

            //if (businessLogicRequestResponse.ErrorObj.BusinessOperationSucceed == true)
            //{
            //    _notyfService.Success($"{businessLogicRequestResponse.ErrorObj.DisplayMessage}");
            //}
            //else
            //{
            //    _notyfService.Error($"{businessLogicRequestResponse.ErrorObj.DisplayMessage}");
            //    return new JsonResult(DataSourceLoader.Load(new List<CustomDTO.CustomBasicTicketDto>(), loadOptions));
            //}

            //foreach (var item in listModel)
            //{
            //    item.FirstLastName = item.AddedByUser.FirstName + " " + item.AddedByUser.LastName;
            //}

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
                    ApiUrl = new Uri(httpClient.BaseAddress, "BasicTicketWrapper/GetAll"),
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
                List<CustomDTO.CustomBasicTicketDto> listModel = new();

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
                        return new JsonResult(DataSourceLoader.Load(new List<CustomDTO.CustomBasicTicketDto>(), loadOptions));
                    }

                    string myList = businessLogicResponse.RelevantDto.ToString();
                    listModel = JsonConvert.DeserializeObject<List<CustomDTO.CustomBasicTicketDto>>(myList);

                    string user = SessionHelper.GetObjectFromJson(HttpContext.Session, "user");
                    StandartDTO.UserDto userDto = JsonConvert.DeserializeObject<StandartDTO.UserDto>(user);
                    int loginUserId = Convert.ToInt32(userDto.Id);
                    //_cache.Set("customTicketDtos", listModel);

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
        public async Task<IActionResult> OnGetUserData(DataSourceLoadOptions loadOptions)
        {
            HttpClient httpClient = new();
            var result = httpClient.GetAsync("http://localhost:33400/Users/GetAllUsers").Result;
            var response = result.Content.ReadAsStringAsync().Result;
            BusinessLogicResponse businessLogicRequestResponse = JsonConvert.DeserializeObject<BusinessLogicResponse>(response);
            var userList = businessLogicRequestResponse.RelevantDto.ToString();
            var users = JsonConvert.DeserializeObject<List<StandartDTO.UserDto>>(userList);
            return new JsonResult(DataSourceLoader.Load(users, loadOptions));
        }
        public async Task<IActionResult> OnPostGridRow(CustomDTO.CustomBasicTicketDto model)
        {           
            HttpClient httpClient = new();
            httpClient.BaseAddress = new Uri("http://localhost:33400/");                      

            var options = new JsonSerializerOptions();
            options.Converters.Add(new CustomJsonConverterForType());

            if (string.IsNullOrWhiteSpace(_webHostEnvironment.WebRootPath))
            {
                _webHostEnvironment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "ticketNotes");
            }
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "fileUploads");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            var newFileName = $"N_{DateTime.Now.ToString("ddMMyyyy")}_{model.AttachmentsFile.FileName}";
            using (var fileStream = System.IO.File.Create(Path.Combine(path, newFileName)))
            {
                await model.AttachmentsFile.CopyToAsync(fileStream);
            }

            List<StandartDTO.AttachmentDto> attachmentDtos = new ();


            StandartDTO.AttachmentDto attachments = new ()
            {
                FolderName = newFileName,
                AttachmentDescription = "Added via AddBasicTicketAttachment Method",                
            };
            attachmentDtos.Add(attachments);

            model.Attachments = attachmentDtos;
            model.AttachmentsFile = null;


            BusinessLogicRequest request = new()
            {
                ApiUrl = new Uri(httpClient.BaseAddress, "BasicTicketWrapper/Insert"),
                RequestDomain = RemoteIncomingDomains.DiasTesisYonetimWebClient,
                RequestDtosTypes = new List<Type>() { typeof(CustomDTO.CustomBasicTicketDto) },
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
            var response = JsonConvert.DeserializeObject<CustomDTO.CustomBasicTicketDto>(businessLogicRequestResponse.OptionalJsonResult);

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
        public async Task<IActionResult> OnGetFile(string fileName)
        {
            if (string.IsNullOrWhiteSpace(_webHostEnvironment.WebRootPath))
            {
                _webHostEnvironment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "ticketNotes");
            }
            var path = Path.Combine(_webHostEnvironment.WebRootPath, $"fileUploads\\{fileName}");
            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));
        }
        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }
        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
    }
}
