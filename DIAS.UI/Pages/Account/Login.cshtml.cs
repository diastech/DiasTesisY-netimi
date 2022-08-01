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
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using System.Text.Json;
using DiasShared.Operations.JsonOperation.Converters;
using static DiasShared.Enums.Standart.RemoteCommunicationEnums;
using System.Net.Http.Json;

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

        public void OnGet()
        {
            //return null;
        }

        #region Sign In method.
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                //if (!ModelState.IsValid)
                //{
                //    foreach (var item in ModelState.Values)
                //    {
                //        var deneme = item.ValidationState;
                //        if (item.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                //        {
                //            _notyfService.Error($"Uygun Olmayan {item.Errors[0].ErrorMessage} girdiniz.");
                //        }
                //    }                    
                //    return new RedirectResult("https://localhost:44341/Account/Login?ReturnUrl=%2F");
                //}

                UserCredentialsDto userCredentials = new() { EmailAddress = EmailAddress, Password = Password, ClientId = (int)(RemoteIncomingDomains.DiasTesisYonetimWebClient) };
                               
                var options = new JsonSerializerOptions();
                options.Converters.Add(new CustomJsonConverterForType());

                //HttpClient httpClient = new HttpClient();
                //var result = httpClient.PostAsync($"http://localhost:33400/Authentication/Login?username={EmailAddress}&password={Password}", new StringContent(JsonConvert.SerializeObject(string.Empty), Encoding.UTF8, "application/json")).Result;


                HttpClient httpClient = new();
                httpClient.BaseAddress = new Uri("http://localhost:33400/");


                BusinessLogicRequest request = new()
                {
                    ApiUrl = new Uri(httpClient.BaseAddress, "Authentication/LoginV2"),
                    RequestDomain = RemoteIncomingDomains.DiasTesisYonetimWebClient,
                    RequestDtosTypes = new List<Type>() { typeof(UserCredentialsDto) },
                    RequestDtosJsons = new List<string>() {
                            JsonConvert.SerializeObject(userCredentials, Formatting.None,
                                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore })}
                };

                var result = httpClient.PostAsJsonAsync(request.ApiUrl, request, options).Result;


                var json = result.Content.ReadAsStringAsync().Result;
                BusinessLogicResponse businessLogicRequestResponse = JsonConvert.DeserializeObject<BusinessLogicResponse>(json);

                if (businessLogicRequestResponse.ErrorObj.BusinessOperationSucceed)
                {
                    byte[] bytes = Encoding.ASCII.GetBytes(businessLogicRequestResponse.JwtTokenStr);
                    var a = new SymmetricSecurityKey(bytes);
                    var handler = new JwtSecurityTokenHandler();
                    var jwtSecurityToken = handler.ReadJwtToken(businessLogicRequestResponse.JwtTokenStr);
                    var userClaims = jwtSecurityToken.Payload.Claims;
                    List<Claim> disabledControlPageByClaims = new();
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "token", businessLogicRequestResponse.JwtTokenStr);

                    foreach (var item in userClaims)
                    {
                        if (item.Type == "CompanyRoleMenuAuthorizationCode")
                        {
                            var userMenuClaim = AuthorizationDeserialize.AuthorizationMenuDeserializeFunc(item);
                            SessionHelper.SetObjectAsJson(HttpContext.Session, "userMenu", userMenuClaim);
                        }
                        if(item.Type == "TicketRoleTicketPageReadOnlyAttribute")
                        {
                            disabledControlPageByClaims.Add(item);
                            
                        }
                    }
                    var userTicketRole = AuthorizationDeserialize.TicketRoleTicketPageReadOnlyAttribute(disabledControlPageByClaims);
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "userTicketRole", userTicketRole);

                    var userClaimsonuc = AuthorizationDeserialize.AuthorizationDeserializeFunc(userClaims);
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "userInfo", userClaimsonuc);

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
                    //TODO: buraya login hatasý yazýlacak
                    _notyfService.Error($"{businessLogicRequestResponse.ErrorObj.Message}");
                    return new RedirectResult("https://localhost:44341/Account/Login?ReturnUrl=%2F");
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
        #endregion
        
        public async Task OnPostLogout()
        {
            //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            string lang = _configuration.GetValue<string>("DefaultSystemLang");            
            foreach (var cookie in Request.Cookies.Keys)
            {
                //if (cookie == ".AspNetCore.Session")
                Response.Cookies.Delete(cookie);
            }
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme, new AuthenticationProperties { RedirectUri = "/Account/Login" });
            //return new OkResult();
            //if (!string.IsNullOrEmpty(Request.Cookies["userLang"]))
            //    lang = Request.Cookies["userLang"];            
            //return Redirect("~/Account/Login/?culture=" + lang);
        }
    }
}
