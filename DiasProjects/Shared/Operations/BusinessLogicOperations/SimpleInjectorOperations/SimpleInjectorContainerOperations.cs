using WebApi =  DiasWebApi.Shared.Configuration;
using Microsoft.Extensions.Configuration;
using System;
using static DiasWebApi.Shared.Enums.WebApiApplicationEnums;
using DiasFMSQL =  DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.Configuration;

namespace DiasWebApi.Shared.Operations.BusinessLogicOperations.SimpleInjectorOperations
{
    public static class SimpleInjectorContainerOperations
    {
        public static bool VerifyContainer()
        {
            IConfiguration configurationSettings = WebApi.ConfigurationHelper.GetConfig();

            //environmental DI lar
            switch (WebApi.ConfigurationHelper.GetBusinessLogicEnvironment(configurationSettings))
            {
                case ApplicationBusinessLogicEnvironment.Development:
                    {
                        try
                        {
                            //DiasFacilityManagement
                            DiasFMSQL.DI_ServiceLocator.Current.VerifyContainer();
                        }
                        catch (Exception)
                        {
                            return false;
                        }

                        return true;                        
                    }

                case ApplicationBusinessLogicEnvironment.Test:
                    {
                        return false;
                    }

                case ApplicationBusinessLogicEnvironment.Live:
                    {
                        return false;
                    }

                default:
                    {
                        return false;
                    }
            }
        }

    }
}
