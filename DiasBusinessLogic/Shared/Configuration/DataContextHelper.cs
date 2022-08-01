using Microsoft.Extensions.Configuration;
using System;
using static DiasShared.Enums.ApplicationEnums;
using BusinessLogic = DiasBusinessLogic.Shared.Configuration;

namespace DiasBusinessLogic.Shared.Configuration
{
    public static class DataContextHelper
    {
        //Konfigurasyon dosyasına bakarak data contexti döndürür, döndürülen nesne dinamiktir
        //DAL projesindeki namespacelere ve isimlendirmelerine duyarlıdır, değişiklikte burasıda değişmelidir
        //Hata vermez, hata durumunda varsa dev veya test yoksa yoksa null döndürür
        public static Type GetDataContextTypeViaConfiguration(string contextHead)
        {
            IConfiguration settings = BusinessLogic.ConfigurationHelper.GetConfig();

            DatabaseEnvironment appDatabaseEnv = BusinessLogic.ConfigurationHelper.GetDatabaseEnvironment(settings);

            ApplicationDatabaseType appDatabase = BusinessLogic.ConfigurationHelper.GetDatabaseType(settings);

            switch (contextHead)
            {
                case "DiasFacilityManagementSqlServer":
                    {
                        switch (appDatabaseEnv)
                        {
                            case DatabaseEnvironment.Development:
                                {
                                    switch (appDatabase)
                                    {
                                        case ApplicationDatabaseType.SqlServer:
                                            {
                                                Type dataContextType =
                                                    Type.GetType("DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models.DiasFacilityManagementSqlServer"
                                                    + ", DiasDataAccessLayer");

                                                return dataContextType;
                                            }

                                        default:
                                            {
                                                return null;
                                            }
                                    }
                                }

                            default:
                                {
                                    switch (BusinessLogic.ConfigurationHelper.GetDatabaseType(settings))
                                    {
                                        case ApplicationDatabaseType.SqlServer:
                                            {
                                                return null;
                                            }

                                        default:
                                            {
                                                return null;
                                            }
                                    }
                                }
                        }                        
                    }

                case "DiasFacilityManagementSqlServerTest":
                    {
                        switch (appDatabaseEnv)
                        {
                            case DatabaseEnvironment.Development:
                                {
                                    switch (appDatabase)
                                    {
                                        case ApplicationDatabaseType.SqlServer:
                                            {
                                                Type dataContextType =
                                                    Type.GetType("DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Test.Models.DiasFacilityManagementSqlServer"
                                                    + ", DiasDataAccessLayer");

                                                return dataContextType;
                                            }

                                        default:
                                            {
                                                return null;
                                            }
                                    }
                                }

                            default:
                                {
                                    switch (BusinessLogic.ConfigurationHelper.GetDatabaseType(settings))
                                    {
                                        case ApplicationDatabaseType.SqlServer:
                                            {
                                                return null;
                                            }

                                        default:
                                            {
                                                return null;
                                            }
                                    }
                                }
                        }
                    }

                //Obsolete
                // TODO: Referanslar kontrol edilerek silinecek
                case "IdentityManagementSqlServer":
                    {
                        switch (appDatabaseEnv)
                        {
                            case DatabaseEnvironment.Development:
                                {
                                    switch (appDatabase)
                                    {
                                        case ApplicationDatabaseType.SqlServer:
                                            {
                                                Type dataContextType =
                                                    Type.GetType("DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement.SqlServer.Development.Models.IdentityManagementSqlServer"
                                                    + ", DiasDataAccessLayer");

                                                return dataContextType;
                                            }

                                        default:
                                            {
                                                return null;
                                            }
                                    }
                                }
                            default:
                                {
                                    switch (BusinessLogic.ConfigurationHelper.GetDatabaseType(settings))
                                    {
                                        case ApplicationDatabaseType.SqlServer:
                                            {
                                                return null;
                                            }

                                        default:
                                            {
                                                return null;
                                            }
                                    }
                                }
                        }
                    }

                //Sadece ihtiyaç olduğunda kullanılacak, bunun yerine DiasFacilityManagementSqlServer kullanılacak
                case "IdentityManagement_DFMSqlServer":
                    {
                        switch (appDatabaseEnv)
                        {
                            case DatabaseEnvironment.Development:
                                {
                                    switch (appDatabase)
                                    {
                                        case ApplicationDatabaseType.SqlServer:
                                            {
                                                Type dataContextType =
                                                    Type.GetType("DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFM.SqlServer.Development.Models.IdentityManagement_DFMSqlServer"
                                                    + ", DiasDataAccessLayer");

                                                return dataContextType;
                                            }

                                        default:
                                            {
                                                return null;
                                            }
                                    }
                                }


                            default:
                                {
                                    switch (BusinessLogic.ConfigurationHelper.GetDatabaseType(settings))
                                    {
                                        case ApplicationDatabaseType.SqlServer:
                                            {
                                                return null;
                                            }

                                        default:
                                            {
                                                return null;
                                            }
                                    }
                                }
                        }
                    }


                default:
                    {
                        switch (appDatabaseEnv)
                        {
                            case DatabaseEnvironment.Live:
                                {
                                    switch (appDatabase)
                                    {
                                        case ApplicationDatabaseType.SqlServer:
                                            {
                                                return null;
                                            }

                                        default:
                                            {
                                                return null;
                                            }
                                    }
                                }

                            default:
                                {
                                    switch (appDatabase)
                                    {
                                        case ApplicationDatabaseType.SqlServer:
                                            {
                                                return null;
                                            }

                                        default:
                                            {
                                                return null;
                                            }
                                    }
                                }
                        }
                    }
            }
        }

    }
}
