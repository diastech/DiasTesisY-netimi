using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using DiasShared.Services.Communication.BusinessLogicMessage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace DIAS.UI.Pages.TicketWrapper.Test
{
    public class CustomTicketDto : PageModel
    {
        private readonly IConfiguration _configuration;
        public CustomTicketDto(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public IActionResult OnGetGridData(DataSourceLoadOptions loadOptions)
        {
            loadOptions.AllowAsyncOverSync = true;
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:33400/");
            List<CustomTicketDto> customTicketDto = new();
            var result = httpClient.PostAsJsonAsync("ticketwrapper/getAll", loadOptions).Result;
            //var result = httpClient.PostAsync("Account/Authenticate", new StringContent(JsonConvert.SerializeObject(userModel), Encoding.UTF8, "application/json")).Result;
            var denemeModel = result.Content.ReadAsStringAsync().Result;
            BusinessLogicResponse businessLogicRequestResponse = JsonConvert.DeserializeObject<BusinessLogicResponse>(denemeModel);
            var response = JsonConvert.DeserializeObject(denemeModel);
            //burdan relevantdToyu nasýl çekecez 
            //var response = JsonConvert.DeserializeObject(denemeModel);
            //object[] arr = (object[])response;
            var myList = businessLogicRequestResponse.RelevantDto.ToString();
            var deneme22 = JsonConvert.DeserializeObject<List<DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom.CustomTicketDto>>(myList);
            



            return new JsonResult(DataSourceLoader.Load(deneme22, loadOptions));
        }

        public class CustomLoad
        {
            public bool allowX;
            public bool allowY;
        }


    }
}
