using Microsoft.Extensions.Configuration;
using System;
using static DiasShared.Enums.ApplicationEnums;

namespace DiasBusinessLogic.Shared.Configuration
{
    public static class AzureStorageHelper
    {
        //Konfigurasyon dosyasına bakarak azure storage contexti döndürür, döndürülen nesne dinamiktir
        //DAL projesindeki namespacelere ve isimlendirmelerine duyarlıdır, değişiklikte burasıda değişmelidir
        //Hata vermez, hata durumunda varsa dev veya test yoksa null döndürür
        public static Type GetAzureStorageContextTypeViaConfiguration(string contextHead)
        {
            IConfiguration settings = ConfigurationHelper.GetConfig();

            AzureStorageEnvironment appAzureStorageEnv = ConfigurationHelper.GetAzureStorageEnvironment(settings);

            switch (contextHead)
            {
                case "DiasAzureStorage":
                {
                    switch (appAzureStorageEnv)
                    {
                        case AzureStorageEnvironment.Development:
                        {                                   
                            Type dataContextType =
                                Type.GetType("DiasDataAccessLayer.DataAccessLayers.StorageLayers.Cloud.Azure.Development.Models.DiasAzureStorage"
                                + ", DiasDataAccessLayer");

                            return dataContextType;
                        }

                        case AzureStorageEnvironment.Test:
                            {
                                return null;
                            }

                        case AzureStorageEnvironment.Live:
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
                    switch (appAzureStorageEnv)
                    {
                        case AzureStorageEnvironment.Development:
                        {
                            return null;
                        }

                        case AzureStorageEnvironment.Test:
                        {
                            return null;
                        }

                        case AzureStorageEnvironment.Live:
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
