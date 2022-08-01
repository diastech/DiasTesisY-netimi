using DiasShared.Operations.EnumOperations;
using Microsoft.Extensions.Configuration;
using System;
using static DiasShared.Enums.Standart.TicketEnums;
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

        public static ApplicationRedisEnvironment GetRedisEnvironment(IConfiguration configSettings)
        {
            if (String.IsNullOrEmpty(configSettings["RedisEnvironment"]))
            {
                return ApplicationRedisEnvironment.Development;
            }

            try
            {
                return configSettings["RedisEnvironment"].GetValueFromDescription<ApplicationRedisEnvironment>();
            }
            catch (Exception)
            {
                return ApplicationRedisEnvironment.Development;
            }
        }

        public static LocationCodeEnum GetFacilityEnvironment(IConfiguration configSettings)
        {
            if (String.IsNullOrEmpty(configSettings["FacilityEnvironment"]))
            {
                return LocationCodeEnum.UNDEFINED;
            }

            try
            {
                return configSettings["FacilityEnvironment"].GetValueFromDescription<LocationCodeEnum>();
            }
            catch (Exception)
            {
                return LocationCodeEnum.UNDEFINED;
            }
        }

        /// <summary>
        /// Null döndürdüğünde cacheleme redis tarafında değil in memory olur
        /// </summary>
        public static string GetRedisUrl(IConfiguration configSettings)
        {
            string redisUrl = null;

            try
            {       
                if (String.IsNullOrEmpty(configSettings["RedisEnvironment"]))
                {
                    return null;
                }

                switch (ConfigurationHelper.GetRedisEnvironment(configSettings))
                {
                    case ApplicationRedisEnvironment.Development:
                        {
                            redisUrl = configSettings.GetSection("RedisCacheUrl").
                                                        GetSection(ApplicationRedisEnvironment.Development.DescriptionAttr()).Value;
                            break;
                        }

                    case ApplicationRedisEnvironment.Test:
                        {
                            redisUrl = configSettings.GetSection("RedisCacheUrl").
                                                        GetSection(ApplicationRedisEnvironment.Test.DescriptionAttr()).Value;
                            break;
                        }

                    case ApplicationRedisEnvironment.Live:
                        {
                            redisUrl = configSettings.GetSection("RedisCacheUrl").
                                                         GetSection(ApplicationRedisEnvironment.Live.DescriptionAttr()).Value;
                            break;
                        }

                    default:
                        {
                            return null;
                        }
                }
            }
            catch(Exception)
            {
                return null;
            }

            return redisUrl;
        }

        public static string GetRedisKeyHeader(IConfiguration configSettings)
        {
            if (String.IsNullOrEmpty(configSettings["RedisEnvironment"]))
            {
                return null;
            }

            if (String.IsNullOrEmpty(configSettings["FacilityEnvironment"]))
            {
                return null;
            }

            IConfiguration settings = ConfigurationHelper.GetConfig();
            string header = String.Empty;

            ApplicationRedisEnvironment redisEnvEnum = ConfigurationHelper.GetRedisEnvironment(settings);

            switch (redisEnvEnum)
            {
                case ApplicationRedisEnvironment.Development:                

                case ApplicationRedisEnvironment.Test:                  

                case ApplicationRedisEnvironment.Live:
                    {
                        header += Enum.GetName(typeof(ApplicationRedisEnvironment), redisEnvEnum) + "_";
                        break;
                    }                

                default:
                    {
                        return null;
                    }
            }

            LocationCodeEnum facilityEnvEnum = ConfigurationHelper.GetFacilityEnvironment(settings);

            switch (facilityEnvEnum)
            {           
                case LocationCodeEnum.BSHTY:
                    {
                        header += Enum.GetName(typeof(LocationCodeEnum), facilityEnvEnum) + "_";
                        break;

                    }

                case LocationCodeEnum.UNDEFINED:

                default:
                    {
                        return null;
                    }
            }

            return header;
        }
    }
}
