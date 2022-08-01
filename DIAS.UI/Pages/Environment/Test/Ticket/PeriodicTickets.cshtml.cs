using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
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
using static DiasShared.Enums.Standart.RemoteCommunicationEnums;
using CustomDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Custom;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace DIAS.UI.Pages.Ticket.Test
{
    public class CustomPeriodicTicketDto : PageModel
    {
        private readonly IConfiguration _configuration;
        public CustomPeriodicTicketDto(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> OnGetGridData(DataSourceLoadOptions loadOptions)
        {
            HttpClient httpClient = new HttpClient();
            //TODO: Bu daha sonra konfigürasyondan alınacak
            httpClient.BaseAddress = new Uri("http://localhost:33400/");
            List<CustomDto.CustomPeriodicTicketDto> customTicketDto = new();

            DevExpressRequest gridRequest = null;

            //ilk yükleme mi?
            //TODO: Bu bypass ileride değişebilir
            if ((loadOptions.Filter != null) || (loadOptions.Skip != 0))
            {
                //ilk yükleme değil
                //DataSourceLoadOptions ı deep copy clone yap
                DataSourceLoadOptions clonedDataSourceLoadOptions = new DataSourceLoadOptions();
                loadOptions.CopyProperties(((object)(clonedDataSourceLoadOptions)));

                gridRequest = new DevExpressRequest()
                {
                    AdditionalDevExpressParamJson = JsonConvert.SerializeObject(clonedDataSourceLoadOptions),
                    RequestOptions = new((DataSourceLoadOptions)(clonedDataSourceLoadOptions))
                };
            }

            BusinessLogicRequest request = new BusinessLogicRequest()
            {
                //TODO: Bunu konfigürasyondan alacak 
                ApiUrl = new Uri(httpClient.BaseAddress, "periodicticketwrapper/GetAll"),
                DevExpressRequestObj = gridRequest,
                RequestDomain = RemoteIncomingDomains.DiasTesisYonetimWebClient               
            };

            var result = httpClient.PostAsJsonAsync(request.ApiUrl, request).Result;
            var returnModel = result.Content.ReadAsStringAsync().Result;
            BusinessLogicResponse businessLogicRequestResponse = JsonConvert.DeserializeObject<BusinessLogicResponse>(returnModel);
            var response = JsonConvert.DeserializeObject(returnModel);
            
            var myList = businessLogicRequestResponse.RelevantDto.ToString();
            var listModel = JsonConvert.DeserializeObject<List<CustomDto.CustomPeriodicTicketDto>>(myList);
            return new JsonResult(DataSourceLoader.Load(listModel, loadOptions));
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
            return new OkResult();
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

    }
}
