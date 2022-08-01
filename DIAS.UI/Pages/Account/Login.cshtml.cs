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
using AspNetCoreHero.ToastNotification.Abstractions;
using DiasShared.Attributes;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using static DiasShared.Enums.Standart.AuthorizationEnums;
using DiasShared.Operations.EnumOperations;
using DiasShared.AuthorizationOperation;
using DIAS_UI.Helpers;

namespace DIAS_UI.Pages.Account
{
    public class UserDto : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly INotyfService _notyfService;
        public UserDto(IConfiguration configuration, INotyfService notyfService)
        {
            _configuration = configuration;
            _notyfService = notyfService;
        }
        [BindProperty]
        public string Password { get; set; }
        public string Msg { get; set; }
        [BindProperty,Email]
        public string EmailAddress { get; set; }

        #region Sign In method.
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    foreach (var item in ModelState.Values)
                    {
                        var deneme = item.ValidationState;
                        if (item.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                        {
                            _notyfService.Error($"Uygun Olmayan {item.Errors[0].ErrorMessage} girdiniz.");
                        }
                    }                    
                    return new RedirectResult("https://localhost:44341/Account/Login?ReturnUrl=%2F");
                }

                HttpClient httpClient = new HttpClient();
                var result = httpClient.PostAsync($"http://localhost:33400/Authentication/Login?username={EmailAddress}&password={Password}",new StringContent(JsonConvert.SerializeObject(string.Empty), Encoding.UTF8, "application/json")).Result;
                var json = result.Content.ReadAsStringAsync().Result;
                BusinessLogicResponse businessLogicRequestResponse = JsonConvert.DeserializeObject<BusinessLogicResponse>(json);

                byte[] bytes = Encoding.ASCII.GetBytes(businessLogicRequestResponse.JwtTokenStr);
                var a = new SymmetricSecurityKey(bytes);                
                var handler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = handler.ReadJwtToken(businessLogicRequestResponse.JwtTokenStr);                
                var userClaims = jwtSecurityToken.Payload.Claims;

                foreach (var item in userClaims)
                {
                    if(item.Type == "CompanyRoleMenuAuthorizationCode")
                    {
                        var userMenuClaim = AuthorizationDeserialize.AuthorizationMenuDeserializeFunc(item);
                        SessionHelper.SetObjectAsJson(HttpContext.Session, "userMenu", userMenuClaim);
                    }
                }

                var userClaimsonuc = AuthorizationDeserialize.AuthorizationDeserializeFunc(userClaims);
                SessionHelper.SetObjectAsJson(HttpContext.Session, "userInfo", userClaimsonuc);                
                if(businessLogicRequestResponse.ErrorObj.BusinessOperationSucceed == true)
                {
                    var user = businessLogicRequestResponse.RelevantDto.ToString();
                    var response = JsonConvert.DeserializeObject<DIManagementStdDto.UserDto>(user);
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "user", response);
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
                    return Redirect("~/?culture=" + lang);
                    
                }
                else
                {
                    _notyfService.Error($"{businessLogicRequestResponse.ErrorObj.Message}");
                    return new NotFoundObjectResult("");

                }

            }
            catch (Exception)
            {
                throw;
            }
        }       
        #endregion
    }
}
