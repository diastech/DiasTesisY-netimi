using DiasShared.Operations.EnumOperations;
using Microsoft.Extensions.Configuration;
using System;
using static DiasWebApi.Shared.Enums.WebApiApplicationEnums;

namespace DiasWebApi.Shared.Configuration
{
    public static class ConfigurationHelper
    {
        public static IConfiguration GetConfig()
        {
            var builder = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);
            return builder.Build();
        }

        public static IConfiguration GetConfigPlatformDependent()
        {
            var builder = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appSettingsPlatformDependent.json", optional: true, reloadOnChange: true);
            return builder.Build();
        }

        public static ApplicationWorkingEnvironment GetWorkingEnvironment(IConfiguration configSettings)
        {
            if (String.IsNullOrEmpty(configSettings["WorkingEnvironment"]))
            {
                return ApplicationWorkingEnvironment.Development;
            }

            try
            {
                return configSettings["WorkingEnvironment"].GetValueFromDescription<ApplicationWorkingEnvironment>();
            }
            catch (Exception)
            {
                return ApplicationWorkingEnvironment.Development;
            }
        }

        public static ApplicationBusinessLogicEnvironment GetBusinessLogicEnvironment(IConfiguration configSettings)
        {
            if (String.IsNullOrEmpty(configSettings["BusinessLogicEnvironment"]))
            {
                return ApplicationBusinessLogicEnvironment.Development;
            }

            try
            {
                return configSettings["BusinessLogicEnvironment"].GetValueFromDescription<ApplicationBusinessLogicEnvironment>();
            }
            catch (Exception)
            {
                return ApplicationBusinessLogicEnvironment.Development;
            }
        }
    }
}
