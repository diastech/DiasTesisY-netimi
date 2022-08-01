using DiasShared.Operations.EnumOperations;
using Microsoft.Extensions.Configuration;
using NLog.Config;
using NLog.Targets;
using System;
using static DiasShared.Enums.ApplicationEnums;
using static DiasShared.Enums.Standart.LogEnums;

namespace DiasBusinessLogic.Shared.Functions.LogFunctions
{
    public static class NLogConfigurations
    {
        //karışıklığı önlemek için uygulama boyunca tek Nlog nesnesi kullanacağız
        //dinamik olarak initialize edilirken bir hata olduysa nulldur, o yüzden her zaman null kontrolü yapılmalıdır
        public static NLog.Logger nLogLogger { get; private set; }

        //karışıklığı önlemek için uygulama boyunca tek Nlog konfigürasyonu kullanacağız
        //dinamik olarak initialize edilirken bir hata olduysa nulldur, o yüzden her zaman null kontrolü yapılmalıdır
        private static LoggingConfiguration nLogConfiguration { get; set; }

        public static void  ProduceNLogConfiguration(NLog.Logger logger, LoggingConfiguration logConfig, ApplicationNLogEnvironment logEnvironment)
        {
            switch (logEnvironment)
            {
                case ApplicationNLogEnvironment.Development:
                    {
                        ProduceNLogConfigurationForDev(logger, logConfig, logEnvironment);
                        break;
                    }

                case ApplicationNLogEnvironment.Test:
                    {
                        ProduceNLogConfigurationForTest(logger, logConfig, logEnvironment);
                        break;
                    }

                case ApplicationNLogEnvironment.Live:
                    {
                        ProduceNLogConfigurationForProd(logger, logConfig, logEnvironment);
                        break;
                    }
            }

            return;
        }


        private static void ProduceNLogConfigurationForDev(NLog.Logger logger, LoggingConfiguration logConfig, ApplicationNLogEnvironment logEnvironment)
        {
            //Önce veritabanı connection stringi alalım
            string logConString = GetSqlServerConnectionStringByEnvironment(ApplicationNLogEnvironment.Development);

            if(string.IsNullOrEmpty(logConString))
            {
                return;
            }

            DatabaseTarget targetDatabase = new DatabaseTarget("devLogDatabase") {  ConnectionString = logConString,
                                                                                KeepConnection = true, OptimizeBufferReuse = true};

            ConsoleTarget logConsole = new ConsoleTarget("devLogconsole") { DetectConsoleAvailable = true, AutoFlush = true, 
                                            OptimizeBufferReuse = true, WriteBuffer = false, Layout = "${MicrosoftConsoleLayout}"};

            logConfig.AddRule(NLog.LogLevel.Debug, NLog.LogLevel.Fatal, targetDatabase);
            logConfig.AddRule(NLog.LogLevel.Debug, NLog.LogLevel.Fatal, logConsole);

            nLogConfiguration = logConfig;
            NLog.LogManager.ReconfigExistingLoggers();           
            nLogLogger = logger;

            return;
        }

        private static void ProduceNLogConfigurationForTest(NLog.Logger logger, LoggingConfiguration logConfig, ApplicationNLogEnvironment logEnvironment)
        {
            //Önce veritabanı connection stringi alalım
            string logConString = GetSqlServerConnectionStringByEnvironment(ApplicationNLogEnvironment.Test);

            if (string.IsNullOrEmpty(logConString))
            {
                return;
            }

            DatabaseTarget targetDatabase = new DatabaseTarget("devLogDatabase")
            {
                ConnectionString = logConString,
                KeepConnection = true,
                OptimizeBufferReuse = true
            };

            ConsoleTarget logConsole = new ConsoleTarget("devLogconsole")
            {
                DetectConsoleAvailable = true,
                AutoFlush = true,
                OptimizeBufferReuse = true,
                WriteBuffer = false,
                Layout = "${MicrosoftConsoleLayout}"
            };

            logConfig.AddRule(NLog.LogLevel.Warn, NLog.LogLevel.Fatal, targetDatabase);
            logConfig.AddRule(NLog.LogLevel.Warn, NLog.LogLevel.Fatal, logConsole);

            nLogConfiguration = logConfig;
            NLog.LogManager.ReconfigExistingLoggers();
            nLogLogger = logger;

            return;
        }

        private static void ProduceNLogConfigurationForProd(NLog.Logger logger, LoggingConfiguration logConfig, ApplicationNLogEnvironment logEnvironment)
        {
            //Önce veritabanı connection stringi alalım
            string logConString = GetSqlServerConnectionStringByEnvironment(ApplicationNLogEnvironment.Live);

            if (string.IsNullOrEmpty(logConString))
            {
                return;
            }

            DatabaseTarget targetDatabase = new DatabaseTarget("devLogDatabase")
            {
                ConnectionString = logConString,
                KeepConnection = true,
                OptimizeBufferReuse = true
            };

            ConsoleTarget logConsole = new ConsoleTarget("devLogconsole")
            {
                DetectConsoleAvailable = true,
                AutoFlush = true,
                OptimizeBufferReuse = true,
                WriteBuffer = false,
                Layout = "${MicrosoftConsoleLayout}"
            };

            logConfig.AddRule(NLog.LogLevel.Error, NLog.LogLevel.Fatal, targetDatabase);
            logConfig.AddRule(NLog.LogLevel.Error, NLog.LogLevel.Fatal, logConsole);

            nLogConfiguration = logConfig;
            NLog.LogManager.ReconfigExistingLoggers();
            nLogLogger = logger;

            return;
        }

        private static string GetSqlServerConnectionStringByEnvironment(ApplicationNLogEnvironment logEnvironment)
        {           
            //Dal konfigürasyonu kullanılır
            IConfiguration settings = DiasDataAccessLayer.Shared.Configuration.ConfigurationHelper.GetConfig();

            string conString = String.Empty;

            if (DiasDataAccessLayer.Shared.Configuration.ConfigurationHelper.GetDatabaseType(settings) == ApplicationDatabaseType.SqlServer)
            {
                switch (logEnvironment)
                {
                    case ApplicationNLogEnvironment.Development:
                        {
                            conString = settings.GetSection("ConnectionStrings").GetSection("DiasFacilityManagementSqlServer")
                                                    .GetSection(DatabaseEnvironment.Development.DescriptionAttr()).Value;
                            break;
                        }

                    case ApplicationNLogEnvironment.Test:
                        {
                            conString = settings.GetSection("ConnectionStrings").GetSection("DiasFacilityManagementSqlServer")
                                                    .GetSection(DatabaseEnvironment.Test.DescriptionAttr()).Value;
                            break;
                        }

                    case ApplicationNLogEnvironment.Live:
                        {
                            conString = settings.GetSection("ConnectionStrings").GetSection("DiasFacilityManagementSqlServer")
                                                    .GetSection(DatabaseEnvironment.Live.DescriptionAttr()).Value;
                            break;
                        }                   

                    default:
                        {
                            conString = settings.GetSection("ConnectionStrings").GetSection("DiasFacilityManagementSqlServer")
                                                    .GetSection(DatabaseEnvironment.Development.DescriptionAttr()).Value;
                            break;
                        }
                }
            }
            else
            {
                conString = String.Empty;
            }

            return conString;
        }
    }
}
