using DiasShared.Operations.EnumOperations;
using Microsoft.Extensions.Configuration;
using System;
using static DiasShared.Enums.ApplicationEnums;

namespace DiasDataAccessLayer.Shared.Configuration
{
    public static class ConfigurationHelper
    {
        public static IConfiguration GetConfig()
        {
            var builder = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appSettingsDataAccessLayer.json", optional: true, reloadOnChange: true);
            return builder.Build();
        }

        public static DatabaseEnvironment GetDatabaseEnvironment(IConfiguration configSettings)
        {
            if(String.IsNullOrEmpty(configSettings["DatabaseEnvironment"]))
            {
                return DatabaseEnvironment.Development;
            }

            try
            {
                return configSettings["DatabaseEnvironment"].GetValueFromDescription<DatabaseEnvironment>();
            }
            catch(Exception)
            {
                return DatabaseEnvironment.Development;
            }          
        }

        public static ApplicationDatabaseType GetDatabaseType(IConfiguration configSettings)
        {
            if (String.IsNullOrEmpty(configSettings["DatabaseType"]))
            {
                return ApplicationDatabaseType.SqlServer;
            }

            try
            {
                return configSettings["DatabaseType"].GetValueFromDescription<ApplicationDatabaseType>();
            }
            catch (Exception)
            {
                return ApplicationDatabaseType.SqlServer;
            }
        }

        public static AzureStorageEnvironment GetAzureStorageEnvironment(IConfiguration configSettings)
        {
            if (String.IsNullOrEmpty(configSettings["AzureStorageEnvironment"]))
            {
                return AzureStorageEnvironment.Development;
            }

            try
            {
                return configSettings["AzureStorageEnvironment"].GetValueFromDescription<AzureStorageEnvironment>();
            }
            catch (Exception)
            {
                return AzureStorageEnvironment.Development;
            }
        }
    }
}
