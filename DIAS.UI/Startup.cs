using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using Microsoft.Extensions.Options;
using DIAS.UI.Service;
using DiasShared.Operations.JsonOperation.Converters;
using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using DIAS.UI.Shared;
using System;

namespace DIAS_UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            //services.AddDbContext<IdentityContext>();

            //var builder = services.AddDefaultIdentity<ApplicationUser>();
            //builder.AddRoles<IdentityRole>()
            //       //.AddSignInManager<SignInManager<Users>>()
            //       .AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>>()
            //       .AddEntityFrameworkStores<IdentityContext>();            
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(o =>
                {
                    o.LoginPath = new PathString("/Account/Login");
                    o.LogoutPath = new PathString("/Account/Login");                    
                    o.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                    o.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                }
            );

            services.AddControllers()
            .ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory =  // the interjection
                    ModelStateValidator.ValidateModelState;
            });
           

            services.AddRazorPages()
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
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });
            services.AddMemoryCache();
            
            //services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var cultures = new List<CultureInfo> {
                    new CultureInfo("en"),
                    new CultureInfo("tr")
                };
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en", "en");
                options.SupportedCultures = cultures;
                options.SupportedUICultures = cultures;
            });
            services.AddSingleton<CommonLocalizationService>();
            services.AddNotyf(config=> { config.DurationInSeconds = 10;config.IsDismissable = true;config.Position = NotyfPosition.TopRight; });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseRequestLocalization(app.ApplicationServices.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);            
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

            DIAS.UI.Helpers.HttpContextHelper.Configure(app.ApplicationServices.GetRequiredService<Microsoft.AspNetCore.Http.IHttpContextAccessor>());
        }
    }
}
