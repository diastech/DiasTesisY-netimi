using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using DIManagementStdDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;

using Newtonsoft.Json;
using DiasShared.Services.Communication.BusinessLogicMessage;

namespace DIAS_UI.Pages.Account.Test
{
    public class UserDto : PageModel
    {
        private readonly IConfiguration _configuration;
        public UserDto(IConfiguration configuration) {
            _configuration = configuration;
        }
        [BindProperty]
        public string Password { get; set; }
        public string Msg { get; set; }
        [BindProperty]
        public string EmailAddress { get; set; }

        #region Sign In method.
        public async Task<ActionResult> OnPostAsync()
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                var result = httpClient.PostAsync($"http://localhost:33400/Authentication/Login?username={EmailAddress}&password={Password}",new StringContent(JsonConvert.SerializeObject(string.Empty), Encoding.UTF8, "application/json")).Result;
                var json = result.Content.ReadAsStringAsync().Result;
                BusinessLogicResponse businessLogicRequestResponse = JsonConvert.DeserializeObject<BusinessLogicResponse>(json);
                var user = businessLogicRequestResponse.RelevantDto.ToString();
                var response = JsonConvert.DeserializeObject<DIManagementStdDto.UserDto>(user);
                var authClaims = new List<Claim> {
                    new Claim("Username", response.UserName),
                    new Claim("Email", response.Email),
                    new Claim("UserId", response.Id),
                    new Claim("Token", string.Empty),
                };
                authClaims.Add(new Claim(ClaimTypes.Role, "Test"));
                var claimsIdentity = new ClaimsIdentity(authClaims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal();
                principal.AddIdentity(claimsIdentity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                string lang = _configuration.GetValue<string>("DefaultSystemLang");
                    if (!string.IsNullOrEmpty(Request.Cookies["userLang"]))
                        lang = Request.Cookies["userLang"];
                    return Redirect("~/?culture="+ lang);
            }
            catch (Exception ex) {
                throw;
            }
        }
        #endregion
    }
}
