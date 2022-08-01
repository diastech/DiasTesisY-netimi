using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using DiasShared.Operations.ReflectionOperations;
using DiasShared.Services.Communication.BusinessLogicMessage;
using DiasShared.Services.Communication.BusinessLogicMessage.ThirdPartyLibrary.DevExpress;
using Microsoft.AspNetCore.Hosting;
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
using System.Threading.Tasks;
using static DiasShared.Enums.Standart.RemoteCommunicationEnums;
using DFMCustomDTO = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Custom;
using StandartDTO = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard;

namespace DIAS.UI.Pages.Ticket.Test
{
    public class CustomFastTicketRedirectDto : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CustomFastTicketRedirectDto(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
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
            return new JsonResult(DataSourceLoader.Load(JsonConvert.DeserializeObject<List<DFMCustomDTO.CustomBasicTicketDto>>(dataList), loadOptions));
        }
        public async Task<IActionResult> OnGetGridDataByBasicTicket(DataSourceLoadOptions loadOptions,[FromQuery] int basicTicketId)
        {
            HttpClient httpClient = new();
            httpClient.BaseAddress = new Uri("http://localhost:33400/");
            var result = httpClient.PostAsJsonAsync("TicketWrapper/GetAllTicketsByBasicTicketId/" + basicTicketId, loadOptions).Result;
            var data = result.Content.ReadAsStringAsync().Result;
            BusinessLogicResponse businessLogicRequestResponse = JsonConvert.DeserializeObject<BusinessLogicResponse>(data);
            var dataList = businessLogicRequestResponse.RelevantDto.ToString();
            return new JsonResult(DataSourceLoader.Load(JsonConvert.DeserializeObject<List<DFMCustomDTO.CustomTicketDto>>(dataList), loadOptions));
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

        public async Task<IActionResult> OnGetTicketAssignmentGroupData()
        {
            HttpClient httpClient = new();
            httpClient.BaseAddress = new Uri("http://localhost:33400/");

            return null;
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
