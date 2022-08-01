using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using DIAS.UI.Helpers;
using DIAS.UI.Service;
using DIAS.UI.Shared;
using DiasShared.Operations.JsonOperation.Converters;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
// Add framework services.
//services.AddDbContext<IdentityContext>();

//var builder = services.AddDefaultIdentity<ApplicationUser>();
//builder.AddRoles<IdentityRole>()
//       //.AddSignInManager<SignInManager<Users>>()
//       .AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>>()
//       .AddEntityFrameworkStores<IdentityContext>();            
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(o =>
    {
        o.LoginPath = new PathString("/Account/Login");
        //o.LogoutPath = new PathString("/Account/Login");
        //o.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
        //o.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        o.EventsType = typeof(CustomCookieAuthenticationEvents);
    }
);

builder.Services.AddScoped<CustomCookieAuthenticationEvents>();
builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

builder.Services.AddControllers()
.ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory =  // the interjection
        ModelStateValidator.ValidateModelState;
});


builder.Services.AddRazorPages()
    .AddViewLocalization()
    //.AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization()
    .AddViewOptions(options =>
    {
        options.HtmlHelperOptions.ClientValidationEnabled = false;
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.WriteIndented = true;
                    //TODO : Bunu canlýda kullanýyorsak muhakkak Cors'un hazýr ve aktive olmasý gerekmektedir
                    options.JsonSerializerOptions.Converters.Add(new CustomJsonConverterForType());
    })
    .AddRazorPagesOptions(options =>
    {
        options.Conventions.AuthorizeFolder("/");
        options.Conventions.AllowAnonymousToPage("/Shared/_Layout");
        options.Conventions.AllowAnonymousToPage("/_ViewImports");
        options.Conventions.AllowAnonymousToPage("/_ViewStart");
        options.Conventions.AllowAnonymousToPage("/Account/Login");
    })
    ;
//services.AddSession();
//builder.Services.AddDistributedMemoryCache();


builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(30);    
});
builder.Services.AddMemoryCache();

//services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var cultures = new List<CultureInfo> {
                    new CultureInfo("en"),
                    new CultureInfo("tr")
                };
    options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en", "en");
    options.SupportedCultures = cultures;
    options.SupportedUICultures = cultures;
});
builder.Services.AddSingleton<CommonLocalizationService>();
builder.Services.AddNotyf(config => { config.DurationInSeconds = 10; config.IsDismissable = true; config.Position = NotyfPosition.TopRight; });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
}
app.UseRequestLocalization(((IApplicationBuilder)app).ApplicationServices.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseSession();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseNotyf();
app.UseEndpoints(endpoints => {
    endpoints.MapRazorPages();
});
//var options = ((IApplicationBuilder)app).ApplicationServices.GetRequiredService<IOptions<RequestLocalizationOptions>>();

DIAS.UI.Helpers.HttpContextHelper.Configure(((IApplicationBuilder)app).ApplicationServices.GetRequiredService<Microsoft.AspNetCore.Http.IHttpContextAccessor>());
app.Run();