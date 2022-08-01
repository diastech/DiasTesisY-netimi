
using DiasShared.Operations.JsonOperation.Converters;
using DiasWebApi.Shared.Configuration;
using DiasWebApi.Shared.Extensions;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Newtonsoft.Json;
using Microsoft.IdentityModel.Tokens;
using DiasShared.Operations.JwtOperations;
using NLog.Web;
using NLog;
using NLog.Config;
using DiasBusinessLogic.Shared.Functions.LogFunctions;

NLog.Logger nLogger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
nLogger.Debug("Program ba�lat�ld�");

LoggingConfiguration nLoggerConf = NLog.LogManager.Configuration;
try
{

    var builder = WebApplication.CreateBuilder(args);

    #region Core
    builder.Services.AddCors(options => options.AddPolicy("ApiCorsPolicy", builder =>
    {
        builder
            //.WithOrigins("https://localhost:44341")
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    }));


    builder.Services.AddAuthentication(x =>
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
                //TODO : Canl�'da true olacak
                x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(StaticJwtData.GetSecretKeyByBytes()),
                    //TODO : Canl�ya ge�irmeden true olman�n yollar�n� ara
                    ValidateIssuer = false,
                    //TODO : Canl�ya ge�irmeden true olman�n yollar�n� ara
                    ValidateAudience = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });

    //TODO : Azure k�s�mlar�na gelince bakal�m buna
    //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)//        .AddMicrosoftIdentityWebApi(Configuration.GetSection("AzureAd"));


    builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = null;
            options.JsonSerializerOptions.WriteIndented = true;
        //TODO : Bunu canl�da kullan�yorsak muhakkak Cors'un haz�r ve aktive olmas� gerekmektedir
        options.JsonSerializerOptions.Converters.Add(new CustomJsonConverterForType());
        });

    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "DiasProjects", Version = "v1" });
        c.CustomSchemaIds(type => type.ToString());
    });

    //Custom extension metodlar
    builder.Services.AddBusinessLogicBusinessRulesServiceScopes();

    builder.Services.AddMvc(options =>
    {
        options.EnableEndpointRouting = false;
        options.SuppressAsyncSuffixInActionNames = false;
    });

    IConfiguration webApiConfigurationSettings = ConfigurationHelper.GetConfig();

    //NLog register et
    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
    builder.Host.UseNLog();
    NLogConfigurations.ProduceNLogConfiguration(nLogger, nLoggerConf, ConfigurationHelper.GetNLogEnvironment(webApiConfigurationSettings));


    //RedisCache servisini register et   
    string redisUrl = ConfigurationHelper.GetRedisUrl(webApiConfigurationSettings);
    // Null d�nd�rd���nde cacheleme redis taraf�nda de�il in memory olur
    if (!(String.IsNullOrEmpty(redisUrl)))
    {
        builder.Services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = redisUrl;
        });
    }
    #endregion

    var app = builder.Build();

    if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
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
    app.Run();
}
catch (Exception exception)
{
    //NLog: ba�lang�� hatalar�
    nLogger.Error(exception, "Hata �retildi, program durdu");
    throw;
}
finally
{
    //Kapat�lmadan �nce nlogu temizle
    NLog.LogManager.Shutdown();
}


