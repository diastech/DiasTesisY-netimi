using DiasShared.Operations.JsonOperation.Converters;
using DiasShared.Operations.JwtOperations;
using DiasWebApi.Shared.Configuration;
using DiasWebApi.Shared.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace DiasWebApi
{
    public class Startup   
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        #region Core

        //Çalýþma zamaný çaðrýlacak metod
        public void ConfigureServices(IServiceCollection services)
        {
            //Cors konfigürasyonu
            Startup.ConfigureCors(ref services);

            //Jwt konfigürasyonu
            Startup.ConfigureJwt(ref services, Configuration);

            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                    options.JsonSerializerOptions.WriteIndented = true;
                    //TODO : Bunu canlýda kullanýyorsak muhakkak Cors'un hazýr ve aktive olmasý gerekmektedir
                    options.JsonSerializerOptions.Converters.Add(new CustomJsonConverterForType());
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DiasProjects", Version = "v1" });
                c.CustomSchemaIds(type => type.ToString());
            });

            //Custom extension metodlar
            services.AddBusinessLogicBusinessRulesServiceScopes();

            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
                options.SuppressAsyncSuffixInActionNames = false;
            });
        }

        // Çalýþma zamaný çaðrýlacak metod. Http istek pipeline'ý konfigüre etmek için kullanýlýr.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DiasWebApi V2"));
            }

            //app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("ApiCorsPolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }

        #endregion Core

        #region CoreHelper

        public static void ConfigureCors(ref IServiceCollection services)
        {
            //Cors engelini kaldýrmak için
            //TODO : Mobilde bunu nasýl uygulayabiliriz
            //services.AddCors(options =>
            //{
            //    options.AddPolicy(MyAllowSpecificOrigins,
            //    builder =>
            //    {
            //        //Dias Tesis Yönetim Web Client
            //        builder.WithOrigins(RemoteCommunicationHelper.GetRemoteIncomingCommunicationUrlViaConfiguration
            //                            (RemoteIncomingDomains.DiasTesisYonetimWebClient).AbsoluteUri);
            //    });
            //});

            //TODO: bunu konfigürasyondan alacak
            services.AddCors(options => options.AddPolicy("ApiCorsPolicy", builder =>
            {
                builder
                    //.WithOrigins("https://localhost:44341")
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));
        }

        public static void ConfigureJwt(ref IServiceCollection services, IConfiguration Configuration)
        { 
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Events = new()
                {
                    OnTokenValidated = context =>
                    {
                        return Task.CompletedTask;
                    },

                    OnAuthenticationFailed = context =>
                    {
                        context.NoResult();
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";

                        string response =
                            JsonConvert.SerializeObject("The access token provided is not valid.");

                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                            response =
                                JsonConvert.SerializeObject("The access token provided has expired.");
                        }

                        context.Response.WriteAsync(response);
                        return Task.CompletedTask;
                    },

                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        return Task.CompletedTask;
                    }
                };
                //TODO : Canlý'da true olacak
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(StaticJwtData.GetSecretKeyByBytes()),
                    //TODO : Canlýya geçirmeden true olmanýn yollarýný ara
                    ValidateIssuer = false,
                    //TODO : Canlýya geçirmeden true olmanýn yollarýný ara
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            //TODO : Azure kýsýmlarýna gelince bakalým buna
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)//        .AddMicrosoftIdentityWebApi(Configuration.GetSection("AzureAd"));
        }

        #endregion CoreHelper
    }
}
