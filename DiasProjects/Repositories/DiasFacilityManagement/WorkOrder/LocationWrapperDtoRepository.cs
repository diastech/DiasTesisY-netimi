using DiasDataAccessLayer.Enums;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using static DiasWebApi.Shared.Enums.WebApiApplicationEnums;
using DevelopmentRepositoryInterface = DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement;
using BusinessWrapperInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Custom;
using DiasWebApi.Shared.Operations.BusinessLogicOperations.SimpleInjectorOperations;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Generic.DiasFacilityManagementSqlServer.Development;
using DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.CustomProfiles.Development;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Base.SqlServer.Custom;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Custom;
using TestDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Custom;
using BusinessWrapperTestInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Test.Custom;
using DiasShared.Errors;
using DiasBusinessLogic.Shared.Error;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using DiasWebApi.Shared.Configuration;
using DiasWebApi.Shared.Operations.RedisCacheOperation;
using static DiasShared.Enums.Standart.CacheEnums;
using DiasShared.Operations.EnumOperations;
using DiasWebApi.Shared.Constants;

namespace DiasWebApi.Repositories.DiasFacilityManagement
{
    public class LocationWrapperDtoRepository : ILocationWrapperDtoRepository
    {
        private IBaseLocationWrapperBusinessRules _baseLocationWrapperBusinessRules;
        private ApplicationBusinessLogicEnvironment _applicationBusinessLogicEnvironment;
        private IDistributedCache _redisCache;
        private string redisCacheKeyHeader;

        private bool businessLogicContainerStatus { get; set; }
        public LocationWrapperDtoRepository(IBaseLocationWrapperBusinessRules baseLocationWrapperBusinessRules,
                                              ApplicationBusinessLogicEnvironment applicationBusinessLogicEnvironment,
                                               IDistributedCache redisCache)
        {
            businessLogicContainerStatus = SimpleInjectorContainerOperations.VerifyContainer();

            if (businessLogicContainerStatus)
            {
                _baseLocationWrapperBusinessRules = baseLocationWrapperBusinessRules;
            }

            //redis ayarları
            _redisCache = redisCache;
            IConfiguration webApiConfigurationSettings = ConfigurationHelper.GetConfig();
            redisCacheKeyHeader = ConfigurationHelper.GetRedisKeyHeader(webApiConfigurationSettings);
        }

        #region WebClient
        #region Development
        public async Task<Tuple<Error, IEnumerable<CustomLocationDto>>> GetAllLocationsWrapperAsync()
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            //redis cache de varsa iş kuralına gitmeden al
                            string redisKey = String.Empty;                           
                            if (!(String.IsNullOrEmpty(redisCacheKeyHeader)))
                            {
                                redisKey = redisCacheKeyHeader + 
                                    RedisDtoDefinitions.CustomLocationDto.GetDisplayOrValueFromEnum<RedisDtoDefinitions>();

                                IEnumerable<CustomLocationDto> locationsFromCache = null;
                                try
                                {
                                    locationsFromCache = await RedisCacheOperations<CustomLocationDto>.GetFromCacheViaList(_redisCache, redisKey);
                                }
                                catch(Exception)
                                {                                 
                                }

                                //cache dolu ise döndür değilse aşağıda iş kuralına git
                                if (locationsFromCache != null)
                                {
                                    return new Tuple<Error, IEnumerable<CustomLocationDto>>(Errors.General.GetListSuccess("LocationV2"), locationsFromCache);
                                }
                            }                           

                            ILocationWrapperBusinessRules businessRule =
                                (ILocationWrapperBusinessRules)_baseLocationWrapperBusinessRules;

                            Tuple <Error, IEnumerable<CustomLocationDto>> businessRuleResult = await businessRule.GetAllLocationsWrapperAsync();

                            if(businessRuleResult.Item1.BusinessOperationSucceed && (!(String.IsNullOrEmpty(redisCacheKeyHeader))))
                            {
                                //Cachi dolduralım
                                try
                                {
                                    await RedisCacheOperations<CustomLocationDto>.SetCacheList(_redisCache, redisKey,
                                                    businessRuleResult.Item2, RedisCacheConstants.LocationV2RedisCacheEntryOptions);
                                }
                                catch (Exception)
                                {
                                }
                            }

                            return businessRuleResult;
                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            return new Tuple<Error, IEnumerable<CustomLocationDto>>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, IEnumerable<CustomLocationDto>>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    default:
                        {
                            return new Tuple<Error, IEnumerable<CustomLocationDto>>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, IEnumerable<CustomLocationDto>>(Errors.General.GeneralServerError(), null);
            }
        }

        public async Task<Tuple<Error, CustomLocationDto>> GetLocationWrapperByIdAsync(string hierarchyId)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            ILocationWrapperBusinessRules businessRule =
                                (ILocationWrapperBusinessRules)_baseLocationWrapperBusinessRules;
                            return await businessRule.GetLocationWrapperByIdAsync(hierarchyId);
                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            return new Tuple<Error, CustomLocationDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, CustomLocationDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    default:
                        {
                            return new Tuple<Error, CustomLocationDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, CustomLocationDto>(Errors.General.GeneralServerError(), null);
            }
        }

        public async Task<Tuple<Error, IEnumerable<CustomLocationDto>>> GetTicketLocationWrapperLastNodeByIdAsync(string hierarchyId)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            //redis cache de varsa iş kuralına gitmeden al
                            string redisKey = String.Empty;
                            if (!(String.IsNullOrEmpty(redisCacheKeyHeader)))
                            {
                                redisKey = redisCacheKeyHeader +
                                            RedisDtoDefinitions.CustomLocationLastNodeFind.GetDisplayOrValueFromEnum<RedisDtoDefinitions>() +
                                              "_" + hierarchyId;

                                IEnumerable<CustomLocationDto> locationsFromCache = null;

                                try
                                {
                                    locationsFromCache = await RedisCacheOperations<CustomLocationDto>.GetFromCacheViaList(_redisCache, redisKey);
                                }
                                catch (Exception)
                                {
                                }

                                //cache dolu ise döndür değilse aşağıda iş kuralına git
                                if (locationsFromCache != null)
                                {
                                    return new Tuple<Error, IEnumerable<CustomLocationDto>>(Errors.General.GetListSuccess("LocationV2"), locationsFromCache);
                                }
                            }

                            ILocationWrapperBusinessRules businessRule =
                                 (ILocationWrapperBusinessRules)_baseLocationWrapperBusinessRules;

                            Tuple<Error, IEnumerable<CustomLocationDto>> businessRuleResult = await businessRule.GetTicketLocationWrapperLastNodeByIdAsync(hierarchyId);

                            if (businessRuleResult.Item1.BusinessOperationSucceed && (!(String.IsNullOrEmpty(redisCacheKeyHeader))))
                            {
                                //Cachi dolduralım
                                try
                                {
                                    await RedisCacheOperations<CustomLocationDto>.SetCacheList(_redisCache, redisKey,
                                                    businessRuleResult.Item2, RedisCacheConstants.customLocationV2LastNodeFindRedisCacheEntryOptions);
                                }
                                catch (Exception)
                                {
                                }
                            }

                            return businessRuleResult;                           
                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            return new Tuple<Error, IEnumerable<CustomLocationDto>>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, IEnumerable<CustomLocationDto>>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    default:
                        {
                            return new Tuple<Error, IEnumerable<CustomLocationDto>>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, IEnumerable<CustomLocationDto>>(Errors.General.GeneralServerError(), null);
            }
        }

        public async Task<Tuple<Error, CustomLocationDto>> GetLocationWrapperByNfcCodeAsync(string nfcCode)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            ILocationWrapperBusinessRules businessRule =
                                (ILocationWrapperBusinessRules)_baseLocationWrapperBusinessRules;
                            return await businessRule.GetLocationWrapperByNfcCodeAsync(nfcCode);
                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            return new Tuple<Error, CustomLocationDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, CustomLocationDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    default:
                        {
                            return new Tuple<Error, CustomLocationDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, CustomLocationDto>(Errors.General.GeneralServerError(), null);
            }
        }

        #endregion

        #region Test
        public async Task<Tuple<ErrorCodes, IEnumerable<TestDto.CustomLocationDto>>> GetAllLocationsWrapperTestAsync()
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            return new Tuple<ErrorCodes, IEnumerable<TestDto.CustomLocationDto>>(ErrorCodes.UnknownError, null);

                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            BusinessWrapperTestInterface.ILocationWrapperBusinessRules businessRule =
                                (BusinessWrapperTestInterface.ILocationWrapperBusinessRules)_baseLocationWrapperBusinessRules;

                            return await businessRule.GetAllLocationsWrapperAsync();
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<ErrorCodes, IEnumerable<TestDto.CustomLocationDto>>(ErrorCodes.UnknownError, null);
                        }
                    default:
                        {
                            return new Tuple<ErrorCodes, IEnumerable<TestDto.CustomLocationDto>>(ErrorCodes.UnknownError, null);
                        }
                }
            }
            else
            {
                return new Tuple<ErrorCodes, IEnumerable<TestDto.CustomLocationDto>>(ErrorCodes.UnknownError, null);
            }
        }

        public async Task<Tuple<ErrorCodes, TestDto.CustomLocationDto>> GetLocationWrapperByIdTestAsync(string hierarchyId)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            return new Tuple<ErrorCodes, TestDto.CustomLocationDto>(ErrorCodes.UnknownError, null);

                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            BusinessWrapperTestInterface.ILocationWrapperBusinessRules businessRule =
                                (BusinessWrapperTestInterface.ILocationWrapperBusinessRules)_baseLocationWrapperBusinessRules;

                            return await businessRule.GetLocationWrapperByIdAsync(hierarchyId);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<ErrorCodes, TestDto.CustomLocationDto>(ErrorCodes.UnknownError, null);
                        }
                    default:
                        {
                            return new Tuple<ErrorCodes,TestDto.CustomLocationDto>(ErrorCodes.UnknownError, null);
                        }
                }
            }
            else
            {
                return new Tuple<ErrorCodes, TestDto.CustomLocationDto>(ErrorCodes.UnknownError, null);
            }
        }
        #endregion


        #endregion
    }
}
