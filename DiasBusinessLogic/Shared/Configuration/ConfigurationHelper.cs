using DiasShared.Operations.EnumOperations;
using Microsoft.Extensions.Configuration;
using System;
using static DiasShared.Enums.ApplicationEnums;
using static DiasShared.Enums.Standart.CultureCodesEnums;
using static DiasShared.Enums.Standart.DateTimeCodesEnums;

namespace DiasBusinessLogic.Shared.Configuration
{
    public static class ConfigurationHelper
    {
        public static IConfiguration GetConfig()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appSettingsBusinessLogic.json", optional: true, reloadOnChange: true);
            return builder.Build();
        }

        public static DatabaseEnvironment GetDatabaseEnvironment(IConfiguration configSettings)
        {
            if (String.IsNullOrEmpty(configSettings["DatabaseEnvironment"]))
            {
                return DatabaseEnvironment.Development;
            }

            try
            {
                return configSettings["DatabaseEnvironment"].GetValueFromDescription<DatabaseEnvironment>();
            }
            catch (Exception)
            {
                return DatabaseEnvironment.Development;
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

        public static ApplicationDatabaseType GetDatabaseType(IConfiguration configSettings)
        {
            if (String.IsNullOrEmpty(configSettings["DatabaseType"]))
            {
                return ApplicationDatabaseType.SqlServer;
            }

            try
            {
                return configSettings["Environment"].GetValueFromDescription<ApplicationDatabaseType>();
            }
            catch (Exception)
            {
                return ApplicationDatabaseType.SqlServer;
            }
        }

        public static string GetEntityStringCultureCode(IConfiguration configSettings)
        {
            if (String.IsNullOrEmpty(configSettings["EntityStringCulture"]))
            {
                return CultureCodes.Turkey.DescriptionAttr<CultureCodes>();
            }

            try
            {
                CultureCodes returnEnum =  configSettings["EntityStringCulture"].GetValueFromDescription<CultureCodes>();

                return returnEnum.DescriptionAttr<CultureCodes>();
            }
            catch (Exception)
            {
                return CultureCodes.Turkey.DescriptionAttr<CultureCodes>();
            }
        }

        public static string GetEntityDateFormatCode(IConfiguration configSettings)
        {
            if (String.IsNullOrEmpty(configSettings["CRUDEntityDateFormat"]))
            {
                return DateCodes.BusinessRuleDateStandardFormat.DescriptionAttr<DateCodes>(); ;
            }

            try
            {
                DateCodes returnEnum =  configSettings["CRUDEntityDateFormat"].GetValueFromDescription<DateCodes>();

                return returnEnum.DescriptionAttr<DateCodes>();
            }
            catch (Exception)
            {
                return DateCodes.BusinessRuleDateStandardFormat.DescriptionAttr<DateCodes>();
            }
        }

        public static string GetEntityDateTimeFormatCode(IConfiguration configSettings)
        {
            if (String.IsNullOrEmpty(configSettings["CRUDEntityDateTimeFormat"]))
            {
                return DateTimeCodes.BusinessRuleDateTimeStandardFormat.DescriptionAttr<DateTimeCodes>(); ;
            }

            try
            {
                DateTimeCodes returnEnum =
                    configSettings["CRUDEntityDateTimeFormat"].GetValueFromDescription<DateTimeCodes>();

                return returnEnum.DescriptionAttr<DateTimeCodes>();
            }
            catch (Exception)
            {
                return DateTimeCodes.BusinessRuleDateTimeStandardFormat.DescriptionAttr<DateTimeCodes>(); ;
            }
        }

        public static string GetEntityTimeFormatCode(IConfiguration configSettings)
        {
            if (String.IsNullOrEmpty(configSettings["CRUDEntityTimeFormat"]))
            {
                return TimeCodes.BusinessRuleTimeStandardFormat.DescriptionAttr<TimeCodes>(); ;
            }

            try
            {
                TimeCodes returnEnum = configSettings["CRUDEntityTimeFormat"].GetValueFromDescription<TimeCodes>();

                return returnEnum.DescriptionAttr<TimeCodes>();
            }
            catch (Exception)
            {
                return TimeCodes.BusinessRuleTimeStandardFormat.DescriptionAttr<TimeCodes>(); ;
            }
        }

    }
}
