using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using DiasShared.Operations.JsonOperation.Converters;
using DiasShared.Operations.ReflectionOperations;
using DiasShared.Services.Communication.BusinessLogicMessage;
using DiasShared.Services.Communication.BusinessLogicMessage.ThirdPartyLibrary.DevExpress;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
using static DiasShared.Enums.Standart.RemoteCommunicationEnums;
using CustomDTO = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Custom;
using StandartDTO = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard;



namespace DIAS.UI.Pages.Ticket.Test
{
    public class CustomBasicTicketDto : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CustomBasicTicketDto(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> OnGetGridData(DataSourceLoadOptions loadOptions)
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
                ApiUrl = new Uri(httpClient.BaseAddress, "BasicTicketWrapper/GetAll"),
                DevExpressRequestObj = gridRequest,
                RequestDomain = RemoteIncomingDomains.DiasTesisYonetimWebClient
            };
            var result = httpClient.PostAsJsonAsync(request.ApiUrl, request).Result;
            var data = result.Content.ReadAsStringAsync().Result;
            BusinessLogicResponse businessLogicRequestResponse = JsonConvert.DeserializeObject<BusinessLogicResponse>(data);
            var dataList = businessLogicRequestResponse.RelevantDto.ToString();
            var listModel = JsonConvert.DeserializeObject<List<CustomDTO.CustomBasicTicketDto>>(dataList);
            foreach (var item in listModel)
            {
                item.FirstLastName = item.AddedByUser.FirstMiddleName + " " + item.AddedByUser.LastName;
            }

            return new JsonResult(DataSourceLoader.Load(listModel, loadOptions));
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

        public async Task<IActionResult> OnPostGridRow(CustomDTO.CustomBasicTicketDto model)
        {
            
            HttpClient httpClient = new();
            httpClient.BaseAddress = new Uri("http://localhost:33400/");

            Newtonsoft.Json.JsonSerializer jsonWriter = new ()
            {
                NullValueHandling = NullValueHandling.Ignore
            };

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
            var response = JsonConvert.DeserializeObject<CustomDTO.CustomPeriodicTicketDto>(businessLogicRequestResponse.OptionalJsonResult);
            return new OkResult();
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
