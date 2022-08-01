using Microsoft.Extensions.Configuration;
using System;
using static DiasShared.Enums.Standart.RemoteCommunicationEnums;
using static DiasWebApi.Shared.Enums.WebApiApplicationEnums;
using WebApi = DiasWebApi.Shared.Configuration;

namespace DiasWebApi.Shared.Configuration
{
    public static class RemoteCommunicationHelper
    {
        //Konfigurasyon dosyasına bakarak environmenta ve seçili uzak domaine göre domainin bağlantı Urlsini döndürür
        //Hata vermez, hata durumunda boş Url döndürür

        public static Uri GetRemoteIncomingCommunicationUrlViaConfiguration(RemoteIncomingDomains remoteIncomingDomain)
        {
            IConfiguration configurationSettings = WebApi.ConfigurationHelper.GetConfig();

            IConfiguration configurationSettingsPlatformDependent = WebApi.ConfigurationHelper.GetConfigPlatformDependent();

            ApplicationWorkingEnvironment appWorkingEnv = WebApi.ConfigurationHelper.GetWorkingEnvironment(configurationSettingsPlatformDependent);

            switch (appWorkingEnv)
            {
                case ApplicationWorkingEnvironment.Development:
                    {
                        switch (remoteIncomingDomain)
                        {                           
                            case RemoteIncomingDomains.DiasTesisYonetimWebClient:
                                {
                                    string remoteDomainUrlStr = configurationSettings.GetSection("RemoteDomains").
                                                                                       GetSection("Incoming").GetSection("DevelopmentWorkingEnvironment").
                                                                                        GetSection(RemoteIncomingDomains.DiasTesisYonetimWebClient.ToString()).Value;

                                    if (remoteDomainUrlStr != null)
                                    {
                                        return new Uri(remoteDomainUrlStr, UriKind.Absolute);
                                    }
                                    else
                                    {
                                        return new Uri(String.Empty);
                                    }
                                }

                            default:
                                {
                                    return new Uri(String.Empty);
                                }
                        }
                    }

                case ApplicationWorkingEnvironment.Live:
                    {
                        switch (remoteIncomingDomain)
                        {                           
                            case RemoteIncomingDomains.DiasTesisYonetimWebClient:
                                {
                                    string remoteDomainUrlStr = configurationSettings.GetSection("RemoteDomains").
                                                                                       GetSection("Incoming").GetSection("LiveWorkingEnvironment").
                                                                                        GetSection(RemoteIncomingDomains.DiasTesisYonetimWebClient.ToString()).Value;

                                    if (remoteDomainUrlStr != null)
                                    {
                                        return new Uri(remoteDomainUrlStr, UriKind.Absolute);
                                    }
                                    else
                                    {
                                        return new Uri(String.Empty);
                                    }
                                }

                            default:
                                {
                                    return new Uri(String.Empty);
                                }
                        }
                    }

                default:
                    {
                        return new Uri(String.Empty);
                    }
            }
        }

        public static Uri GetRemoteOutgoingCommunicationUrlViaConfiguration(RemoteOutgoingDomains remoteOutgoingDomain)
        {
            IConfiguration configurationSettings = WebApi.ConfigurationHelper.GetConfig();

            IConfiguration configurationSettingsPlatformDependent = WebApi.ConfigurationHelper.GetConfigPlatformDependent();

            ApplicationWorkingEnvironment appWorkingEnv = WebApi.ConfigurationHelper.GetWorkingEnvironment(configurationSettingsPlatformDependent);

            switch (appWorkingEnv)
            {
                case ApplicationWorkingEnvironment.Development:
                    {
                        switch (remoteOutgoingDomain)
                        {
                            case RemoteOutgoingDomains.M_Files:
                                {
                                    string remoteDomainUrlStr = configurationSettings.GetSection("RemoteDomains").
                                                                                       GetSection("Outgoing").GetSection("DevelopmentWorkingEnvironment").
                                                                                          GetSection(RemoteOutgoingDomains.M_Files.ToString()).Value;

                                    if (remoteDomainUrlStr != null)
                                    {
                                        return new Uri(remoteDomainUrlStr, UriKind.Absolute);
                                    }
                                    else
                                    {
                                        return new Uri(String.Empty);
                                    }
                                }

                            default:
                                {
                                    return new Uri(String.Empty);
                                }
                        }
                    }

                case ApplicationWorkingEnvironment.Live:
                    {
                        switch (remoteOutgoingDomain)
                        {
                            case RemoteOutgoingDomains.M_Files:
                                {
                                    string remoteDomainUrlStr = configurationSettings.GetSection("RemoteDomains").
                                                                                       GetSection("Outgoing").GetSection("LiveWorkingEnvironment").
                                                                                          GetSection(RemoteOutgoingDomains.M_Files.ToString()).Value;

                                    if (remoteDomainUrlStr != null)
                                    {
                                        return new Uri(remoteDomainUrlStr, UriKind.Absolute);
                                    }
                                    else
                                    {
                                        return new Uri(String.Empty);
                                    }
                                }

                            default:
                                {
                                    return new Uri(String.Empty);
                                }
                        }
                    }

                default:
                    {
                        return new Uri(String.Empty);
                    }
            }
        }

    }
}
