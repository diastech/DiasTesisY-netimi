using DiasDataAccessLayer.Enums;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using static DiasWebApi.Shared.Enums.WebApiApplicationEnums;
using DevelopmentRepositoryInterface = DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement;
using BusinessWrapperInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Custom;
using DiasWebApi.Shared.Operations.BusinessLogicOperations.SimpleInjectorOperations;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Base.SqlServer.Custom;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Custom;
using TestDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Custom;
using BusinessWrapperTestInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Test.Custom;
using DiasShared.Errors;
using DiasBusinessLogic.Shared.Error;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using DiasWebApi.Shared.Configuration;
using static DiasShared.Enums.Standart.CacheEnums;
using DiasShared.Operations.EnumOperations;
using DiasWebApi.Shared.Operations.RedisCacheOperation;
using DiasWebApi.Shared.Constants;
using DiasShared.Services.Communication.BusinessLogicMessage;

namespace DiasWebApi.Repositories.DiasFacilityManagement 
{
    public class TicketReasonCategoryWrapperDtoRepository : ITicketReasonCategoryWrapperDtoRepository
    {
        private IBaseTicketReasonCategoryWrapperBusinessRules _baseTicketReasonCategoryWrapperBusinessRules;
        private ApplicationBusinessLogicEnvironment _applicationBusinessLogicEnvironment;
        private bool businessLogicContainerStatus { get; set; }
        private IDistributedCache _redisCache;
        private string redisCacheKeyHeader;


        #region WebClient
        #region Development
        public TicketReasonCategoryWrapperDtoRepository(IBaseTicketReasonCategoryWrapperBusinessRules baseTicketReasonCategoryWrapperBusinessRules, 
            ApplicationBusinessLogicEnvironment applicationBusinessLogicEnvironment, IDistributedCache redisCache)
        {
            businessLogicContainerStatus = SimpleInjectorContainerOperations.VerifyContainer();
            if (businessLogicContainerStatus)
            {
                _baseTicketReasonCategoryWrapperBusinessRules = baseTicketReasonCategoryWrapperBusinessRules;
            }

            //redis ayarları
            _redisCache = redisCache;
            IConfiguration webApiConfigurationSettings = ConfigurationHelper.GetConfig();
            redisCacheKeyHeader = ConfigurationHelper.GetRedisKeyHeader(webApiConfigurationSettings);
        }
        public async Task<Tuple<Error, IEnumerable<CustomTicketReasonCategoryDto>>> GetAllTicketReasonCategoriesWrapperAsync()
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
                                    RedisDtoDefinitions.CustomTicketReasonCategoryDto.GetDisplayOrValueFromEnum<RedisDtoDefinitions>();

                                IEnumerable<CustomTicketReasonCategoryDto> customTicketReasonsFromCache = null;
                                try
                                {
                                    customTicketReasonsFromCache = await RedisCacheOperations<CustomTicketReasonCategoryDto>.GetFromCacheViaList(_redisCache, redisKey);
                                }
                                catch (Exception ex )
                                {
                                    int a = 5;
                                    a++;

                                }

                                //cache dolu ise döndür değilse aşağıda iş kuralına git
                                if (customTicketReasonsFromCache != null)
                                {
                                    return new Tuple<Error, IEnumerable<CustomTicketReasonCategoryDto>>(Errors.General.GetListSuccess("TicketReasonCategoryV2"), customTicketReasonsFromCache);
                                }
                            }

                            ITicketReasonCategoryWrapperBusinessRules businessRule =
                                (ITicketReasonCategoryWrapperBusinessRules)_baseTicketReasonCategoryWrapperBusinessRules;

                            Tuple<Error, IEnumerable<CustomTicketReasonCategoryDto>> businessRuleResult =  await businessRule.GetAllTicketReasonCategoriesWrapperAsync();

                            if (businessRuleResult.Item1.BusinessOperationSucceed && (!(String.IsNullOrEmpty(redisCacheKeyHeader))))
                            {
                                //Cachi dolduralım
                                try
                                {
                                    await RedisCacheOperations<CustomTicketReasonCategoryDto>.SetCacheList(_redisCache, redisKey,
                                                    businessRuleResult.Item2, RedisCacheConstants.customTicketReasonRedisCacheEntryOptions);
                                }
                                catch (Exception)
                                {
                                }
                            }

                            return businessRuleResult;
                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            return new Tuple<Error, IEnumerable<CustomTicketReasonCategoryDto>>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, IEnumerable<CustomTicketReasonCategoryDto>>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    default:
                        {
                            return new Tuple<Error, IEnumerable<CustomTicketReasonCategoryDto>>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, IEnumerable<CustomTicketReasonCategoryDto>>(Errors.General.GeneralServerError(), null);
            }
        }

        public async Task<Tuple<Error, CustomTicketReasonCategoryDto>> GetTicketReasonCategoryWrapperByIdAsync(string hierarchyId)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            ITicketReasonCategoryWrapperBusinessRules businessRule =
                                (ITicketReasonCategoryWrapperBusinessRules)_baseTicketReasonCategoryWrapperBusinessRules;
                            return await businessRule.GetTicketReasonCategoryWrapperByIdAsync(hierarchyId);
                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            return new Tuple<Error, CustomTicketReasonCategoryDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, CustomTicketReasonCategoryDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    default:
                        {
                            return new Tuple<Error, CustomTicketReasonCategoryDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, CustomTicketReasonCategoryDto>(Errors.General.GeneralServerError(), null);
            }
        }

        public async Task<Tuple<Error, IEnumerable<CustomTicketReasonCategoryDto>>> GetTicketReasonCategoryWrapperLastNodeByIdAsync(string hierarchyId)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            ITicketReasonCategoryWrapperBusinessRules businessRule =
                                (ITicketReasonCategoryWrapperBusinessRules)_baseTicketReasonCategoryWrapperBusinessRules;

                            return await businessRule.GetTicketReasonCategoryWrapperLastNodeByIdAsync(hierarchyId);
                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            return new Tuple<Error, IEnumerable<CustomTicketReasonCategoryDto>>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, IEnumerable<CustomTicketReasonCategoryDto>>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    default:
                        {
                            return new Tuple<Error, IEnumerable<CustomTicketReasonCategoryDto>>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, IEnumerable<CustomTicketReasonCategoryDto>>(Errors.General.GeneralServerError(), null);
            }
        }

        public async Task<Tuple<Error, CustomTicketReasonCategoryDto>> InsertV2Async(BusinessLogicRequest request)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            ITicketReasonCategoryWrapperBusinessRules businessRule =
                                (ITicketReasonCategoryWrapperBusinessRules)_baseTicketReasonCategoryWrapperBusinessRules;

                            Tuple<Error, CustomTicketReasonCategoryDto> businessRuleResult = await businessRule.InsertTicketReasonOrCategoryAsync(request);

                            if (businessRuleResult.Item1.BusinessOperationSucceed)
                            {
                                //redis cache'i temizle
                                string redisKey = String.Empty;
                                if (!(String.IsNullOrEmpty(redisCacheKeyHeader)))
                                {
                                    redisKey = redisCacheKeyHeader +
                                        RedisDtoDefinitions.CustomTicketReasonCategoryDto.GetDisplayOrValueFromEnum<RedisDtoDefinitions>();

                                    try
                                    {
                                        await RedisCacheOperations<CustomTicketReasonCategoryDto>.ClearCache(_redisCache, redisKey);
                                    }
                                    catch (Exception)
                                    {
                                    }
                                }
                            }

                            return businessRuleResult;

                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            return new Tuple<Error, CustomTicketReasonCategoryDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, CustomTicketReasonCategoryDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    default:
                        {
                            return new Tuple<Error, CustomTicketReasonCategoryDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, CustomTicketReasonCategoryDto>(Errors.General.GeneralServerError(), null);
            }
        }

        #endregion

        #region Test
        public async Task<Tuple<ErrorCodes, IEnumerable<TestDto.CustomTicketReasonCategoryDto>>> GetAllTicketReasonCategoriesWrapperTestAsync()
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            return new Tuple<ErrorCodes, IEnumerable<TestDto.CustomTicketReasonCategoryDto>>(ErrorCodes.UnknownError, null);

                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            BusinessWrapperTestInterface.ITicketReasonCategoryWrapperBusinessRules businessRule =
                                (BusinessWrapperTestInterface.ITicketReasonCategoryWrapperBusinessRules)_baseTicketReasonCategoryWrapperBusinessRules;

                            return await businessRule.GetAllTicketReasonCategoriesWrapperAsync();
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<ErrorCodes, IEnumerable<TestDto.CustomTicketReasonCategoryDto>>(ErrorCodes.UnknownError, null);
                        }
                    default:
                        {
                            return new Tuple<ErrorCodes, IEnumerable<TestDto.CustomTicketReasonCategoryDto>>(ErrorCodes.UnknownError, null);
                        }
                }
            }
            else
            {
                return new Tuple<ErrorCodes, IEnumerable<TestDto.CustomTicketReasonCategoryDto>>(ErrorCodes.UnknownError, null);
            }
        }

        public async Task<Tuple<ErrorCodes, TestDto.CustomTicketReasonCategoryDto>> GetTicketReasonCategoryWrapperByIdTestAsync(string hierarchyId)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            return new Tuple<ErrorCodes, TestDto.CustomTicketReasonCategoryDto>(ErrorCodes.UnknownError, null);

                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            BusinessWrapperTestInterface.ITicketReasonCategoryWrapperBusinessRules businessRule =
                                (BusinessWrapperTestInterface.ITicketReasonCategoryWrapperBusinessRules)_baseTicketReasonCategoryWrapperBusinessRules;

                            return await businessRule.GetTicketReasonCategoryWrapperByIdAsync(hierarchyId);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<ErrorCodes, TestDto.CustomTicketReasonCategoryDto>(ErrorCodes.UnknownError, null);
                        }
                    default:
                        {
                            return new Tuple<ErrorCodes, TestDto.CustomTicketReasonCategoryDto>(ErrorCodes.UnknownError, null);
                        }
                }
            }
            else
            {
                return new Tuple<ErrorCodes, TestDto.CustomTicketReasonCategoryDto>(ErrorCodes.UnknownError, null);
            }
        }
        #endregion

        #endregion

        #region Mobile

        #endregion

    }
}
