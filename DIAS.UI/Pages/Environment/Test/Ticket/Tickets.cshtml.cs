using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using DiasShared.Operations.JsonOperation.Converters;
using DiasShared.Operations.ReflectionOperations;
using DiasShared.Services.Communication.BusinessLogicMessage;
using DiasShared.Services.Communication.BusinessLogicMessage.ThirdPartyLibrary.DevExpress;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using static DiasShared.Enums.Standart.RemoteCommunicationEnums;
using CustomDTO = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Custom;
using StandartDTO = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard;


namespace DIAS.UI.Pages.Ticket.Test
{
    public class CustomTicketDto : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CustomTicketDto(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult OnGetGridData(DataSourceLoadOptions loadOptions)
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

            var myList = businessLogicRequestResponse.RelevantDto.ToString();
            var listModel = JsonConvert.DeserializeObject<List<CustomDTO.CustomTicketDto>>(myList);
            foreach (var item in listModel)
            {
                item.FirstLastName = item.AddedByUser.FirstMiddleName + " " +item.AddedByUser.LastName;
            }
            return new JsonResult(DataSourceLoader.Load(listModel, loadOptions));
        }

        public async Task<IActionResult> OnPostGridRow(CustomDTO.CustomTicketDto model)
        {
            var match = Regex.Match(model.TicketReasonHierarchyId, @"\/[^\/]*\/", RegexOptions.RightToLeft);
            model.TicketReasonId = Convert.ToInt32(match.Value.Replace("/", ""));
            model.TicketAssignedUserId = 2;
            HttpClient httpClient = new();
            httpClient.BaseAddress = new Uri("http://localhost:33400/");           

            var options = new JsonSerializerOptions();
            options.Converters.Add(new CustomJsonConverterForType());

            List<StandartDTO.TicketRelatedLocationDto> list = new();

            string[] hierarchy = model.TicketRelatedLocationHierarchyId[0].Split(",");

            foreach (var item in hierarchy)
            {
                var resultLocation = httpClient.GetAsync("LocationWrapper/GetById?hierarchyId=" + item).Result;
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
            var newAttachmentFileName = $"N_{DateTime.Now.ToString("ddMMyyyy")}_{model.AttachmentsFile.FileName}";
            using (var fileStream = System.IO.File.Create(Path.Combine(path, newAttachmentFileName)))
            {
                await model.AttachmentsFile.CopyToAsync(fileStream);
            }

            if (string.IsNullOrWhiteSpace(_webHostEnvironment.WebRootPath))
            {
                _webHostEnvironment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "ticketNotes");
            }
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            var newNotesFileName = $"N_{DateTime.Now.ToString("ddMMyyyy")}_{model.NotesFile.FileName}";
            using (var fileStream = System.IO.File.Create(Path.Combine(path, newNotesFileName)))
            {
                await model.NotesFile.CopyToAsync(fileStream);
            }
            List<StandartDTO.AttachmentDto> attachmentNoteDto = new ();


            StandartDTO.AttachmentDto attachmentNote = new StandartDTO.AttachmentDto
            {
                FolderName = newNotesFileName,
                AttachmentDescription = "Added via AddBasicTicketAttachment Method",
            };
            attachmentNoteDto.Add(attachmentNote);

            model.NotesAttachment = attachmentNoteDto;
            model.NotesFile = null;

            List<StandartDTO.AttachmentDto> attachmentDto = new ();


            StandartDTO.AttachmentDto attachment = new StandartDTO.AttachmentDto
            {
                FolderName = newAttachmentFileName,
                AttachmentDescription = "Added via AddBasicTicketAttachment Method",
            };
            attachmentDto.Add(attachment);

            model.Attachments = attachmentDto;
            model.AttachmentsFile = null;
            model.TicketRelatedLocationHierarchyId = null;
            model.TicketReasonHierarchyId = null;

            BusinessLogicRequest request = new ()
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
            var response = JsonConvert.DeserializeObject<CustomDTO.CustomPeriodicTicketDto>(businessLogicRequestResponse.OptionalJsonResult);

            return new OkResult();
        }

        public async Task<IActionResult> OnPutGridRowUpdate(CustomDTO.CustomTicketDto model)
        {
            HttpClient httpClient = new();
            httpClient.BaseAddress = new Uri("http://localhost:33400/");
            
            var options = new JsonSerializerOptions();
            options.Converters.Add(new CustomJsonConverterForType());

            var match = Regex.Match(model.TicketReasonHierarchyId, @"\/[^\/]*\/", RegexOptions.RightToLeft);
            var ticketId = Convert.ToInt32(match.Value.Replace("/", ""));       
            model.TicketAssignedUserId = 2;
            model.TicketReasonId = ticketId;

            //ticketı Id ye göre getir.

            var result = httpClient.GetAsync("http://localhost:33400/TicketWrapper/GetById/" + model.Id).Result;
            var response = result.Content.ReadAsStringAsync().Result;
            BusinessLogicResponse businessLogicRequestResponse = JsonConvert.DeserializeObject<BusinessLogicResponse>(response);
            var ticket = businessLogicRequestResponse.RelevantDto.ToString();
            var ticketResult = JsonConvert.DeserializeObject<CustomDTO.CustomTicketDto>(ticket); 


            List<StandartDTO.TicketRelatedLocationDto> list = new();

            string[] hierarchy = model.TicketRelatedLocationHierarchyId[0].Split(",");

            foreach (var item in hierarchy)
            {
                var resultLocation = httpClient.GetAsync("LocationWrapper/GetById?hierarchyId=" + item).Result;
                var locationModel = resultLocation.Content.ReadAsStringAsync().Result;
                BusinessLogicResponse businessLogicRequestResponseLocation = JsonConvert.DeserializeObject<BusinessLogicResponse>(locationModel);
                var responseLocation = JsonConvert.DeserializeObject<DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard.LocationV2Dto>(businessLogicRequestResponseLocation.OptionalJsonResult);
                StandartDTO.TicketRelatedLocationDto ticketRelatedLocationDto = new();
                ticketRelatedLocationDto.TicketLocationId = responseLocation.Id;
                ticketRelatedLocationDto.AddedByUserId = model.AddedByUserId;
                list.Add(ticketRelatedLocationDto);
            }
            model.TicketRelatedLocations = list;

            //var commonElements = list.Where(a => ticketResult.TicketRelatedLocations.Any(x =>x.TicketLocationId != a.TicketLocationId)).ToList();

            //var firstDiffSecond = list.Where(item => !ticketResult.TicketRelatedLocations.Contains(item));


            var path = Path.Combine(_webHostEnvironment.WebRootPath, "fileUploads");
            var newAttachmentFileName = "";
            var newNotesFileName = "";

            //Attachments 
            if (model.AttachmentsFile != null)
            {
                if (string.IsNullOrWhiteSpace(_webHostEnvironment.WebRootPath))
                {
                    _webHostEnvironment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "ticketNotes");
                }
                
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                newAttachmentFileName = $"N_{DateTime.Now.ToString("ddMMyyyy")}_{model.AttachmentsFile.FileName}";
                using (var fileStream = System.IO.File.Create(Path.Combine(path, newAttachmentFileName)))
                {
                    await model.AttachmentsFile.CopyToAsync(fileStream);
                }

                List<StandartDTO.AttachmentDto> attachmentDto = new ();


                StandartDTO.AttachmentDto attachment = new ()
                {
                    FolderName = newAttachmentFileName,
                    AttachmentDescription = "Added via AddBasicTicketAttachment Method",
                };
                attachmentDto.Add(attachment);

                model.Attachments = attachmentDto;
                
            }
            

            //NoteAttachments
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

                List<StandartDTO.AttachmentDto> attachmentNoteDto = new ();

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
            return new OkResult();
        }

        public async Task<IActionResult> OnPostChangeStatusAsync(int id)
        {
            return null;
        }
        
        public async Task<IActionResult> OnGetTicketReasonData(DataSourceLoadOptions loadOptions)
        {
            HttpClient httpClient = new();
            var result = httpClient.GetAsync("http://localhost:33400/TicketReasonCategoryWrapper/GetAll").Result;
            var response = result.Content.ReadAsStringAsync().Result;
            BusinessLogicResponse businessLogicRequestResponse = JsonConvert.DeserializeObject<BusinessLogicResponse>(response);
            var reasonCategoryList = businessLogicRequestResponse.RelevantDto.ToString();
            var reasonCategories = JsonConvert.DeserializeObject<List<DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom.CustomTicketReasonCategoryDto>>(reasonCategoryList);
            return new JsonResult(DataSourceLoader.Load(reasonCategories, loadOptions));
        }

        public async Task<IActionResult> OnGetLocationData(DataSourceLoadOptions loadOptions)
        {
            HttpClient httpClient = new();
            var result = httpClient.GetAsync("http://localhost:33400/LocationWrapper/GetAll").Result;
            var response = result.Content.ReadAsStringAsync().Result;
            BusinessLogicResponse businessLogicRequestResponse = JsonConvert.DeserializeObject<BusinessLogicResponse>(response);
            var locationList = businessLogicRequestResponse.RelevantDto.ToString();
            var locations = JsonConvert.DeserializeObject<List<DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom.CustomLocationDto>>(locationList);
            return new JsonResult(DataSourceLoader.Load(locations, loadOptions));
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

        public async Task<IActionResult> OnGetTicketAssignmentGroupData()
        {
            HttpClient httpClient = new();
            httpClient.BaseAddress = new Uri("http://localhost:33400/");
            List<CustomTicketDto> customTicketDto = new();

            return null;
        }

        public async Task<IActionResult> OnGetTicketStateData(DataSourceLoadOptions loadOptions)
        {
            HttpClient httpClient = new();
            var result = httpClient.GetAsync("http://localhost:33400/TicketState/GetAll").Result;
            var response = result.Content.ReadAsStringAsync().Result;
            BusinessLogicResponse businessLogicRequestResponse = JsonConvert.DeserializeObject<BusinessLogicResponse>(response);
            var ticketStateList = businessLogicRequestResponse.RelevantDto.ToString();
            var ticketStates = JsonConvert.DeserializeObject<List<DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard.TicketStateDto>>(ticketStateList);
            return new JsonResult(DataSourceLoader.Load(ticketStates, loadOptions));
        }

        public async Task<IActionResult> OnGetTicketDataById(int ticketId)
        {
            HttpClient httpClient = new();
            var result = httpClient.GetAsync("http://localhost:33400/TicketWrapper/GetById/" + ticketId).Result;
            var response = result.Content.ReadAsStringAsync().Result;
            BusinessLogicResponse businessLogicRequestResponse = JsonConvert.DeserializeObject<BusinessLogicResponse>(response);
            var ticketDetail = businessLogicRequestResponse.RelevantDto.ToString();
            var ticketDetailResult = JsonConvert.DeserializeObject<CustomDTO.CustomTicketDto>(ticketDetail);
            List<CustomDTO.CustomTicketDto> listDetail = new ();
            listDetail.Add(ticketDetailResult);            
            return new JsonResult(listDetail);
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

        //TODO: Bunu sharedda enum yapalım, key -> enum, value -> description
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
