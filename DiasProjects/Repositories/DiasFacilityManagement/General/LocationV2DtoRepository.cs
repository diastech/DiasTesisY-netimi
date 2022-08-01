using DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Development;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.BaseBusinessRules.SqlServer.Standart;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Generic.DiasFacilityManagementSqlServer.Development;
using DiasDataAccessLayer.Enums;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using DiasWebApi.Shared.Operations.BusinessLogicOperations.SimpleInjectorOperations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using static DiasWebApi.Shared.Enums.WebApiApplicationEnums;
using DevelopmentRepositoryInterface = DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement;
using TestDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard;
using TestAutomapper = DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Test;
using GenericInterfaceTest = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Generic.DiasFacilityManagementSqlServer.Test;
using DevCustomDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using DiasShared.Errors;
using DiasBusinessLogic.Shared.Error;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Standart;
using DiasShared.Services.Communication.BusinessLogicMessage;
using static DiasShared.Enums.Standart.RemoteCommunicationEnums;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using DiasWebApi.Shared.Configuration;
using static DiasShared.Enums.Standart.CacheEnums;
using DiasShared.Operations.EnumOperations;
using DiasWebApi.Shared.Operations.RedisCacheOperation;
using DiasWebApi.Shared.Extensions;

namespace DiasWebApi.Repositories.DiasFacilityManagement
{
    public class LocationV2DtoRepository : DevelopmentRepositoryInterface.ILocationV2DtoRepository
    {
        private IBaseLocationV2BusinessRules _baseLocationBusinessRules;
        private ApplicationBusinessLogicEnvironment _applicationBusinessLogicEnvironment;
        private IGenericStandartBusinessRules<LocationV2Dto, LocationV2Profile> _genericStandartBusinessRules;
        private GenericInterfaceTest.IGenericStandartBusinessRules<TestDto.LocationV2Dto, TestAutomapper.LocationV2Profile> _genericStandartBusinessRulesTest;
        private IDistributedCache _redisCache;
        private string redisCacheKeyHeader;

        private bool businessLogicContainerStatus { get; set; }
        public LocationV2DtoRepository(
            IBaseLocationV2BusinessRules baseLocationBusinessRules, 
            ApplicationBusinessLogicEnvironment applicationBusinessLogicEnvironment,
            IGenericStandartBusinessRules<LocationV2Dto, LocationV2Profile> genericStandartBusinessRules,
            GenericInterfaceTest.IGenericStandartBusinessRules<TestDto.LocationV2Dto,
            TestAutomapper.LocationV2Profile> genericStandartBusinessRulesTest,
            IDistributedCache redisCache)
        {
            businessLogicContainerStatus = SimpleInjectorContainerOperations.VerifyContainer();

            if (businessLogicContainerStatus)
            {
                _baseLocationBusinessRules = baseLocationBusinessRules;
                _applicationBusinessLogicEnvironment = applicationBusinessLogicEnvironment;
            }

            _genericStandartBusinessRules = genericStandartBusinessRules;
            _genericStandartBusinessRulesTest = genericStandartBusinessRulesTest;

            //redis ayarları
            _redisCache = redisCache;
            IConfiguration webApiConfigurationSettings = ConfigurationHelper.GetConfig();
            redisCacheKeyHeader = ConfigurationHelper.GetRedisKeyHeader(webApiConfigurationSettings);

        }

        public async Task<Tuple<BusinessLogicMessageCodes.ErrorCodes, IEnumerable<LocationV2Dto>>> GetAllAsync()
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            return await _genericStandartBusinessRules.GetAll();
                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            return new Tuple<ErrorCodes, IEnumerable<LocationV2Dto>>(ErrorCodes.UnknownError, null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<ErrorCodes, IEnumerable<LocationV2Dto>>(ErrorCodes.UnknownError, null);
                        }
                    default:
                        {
                            return new Tuple<ErrorCodes, IEnumerable<LocationV2Dto>>(ErrorCodes.UnknownError, null);
                        }
                }
            }
            else
            {
                return new Tuple<ErrorCodes, IEnumerable<LocationV2Dto>>(ErrorCodes.UnknownError, null);
            }
        }

        //DeleteV2Async kullanılmalıdır
        public async Task<Tuple<BusinessLogicMessageCodes.ErrorCodes, LocationV2Dto>> DeleteFromIntAsync(int id)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            throw new NotImplementedException();

                            // return await _genericStandartBusinessRules.DeleteFromInt(id);

                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            return new Tuple<ErrorCodes, LocationV2Dto>(ErrorCodes.UnknownError, null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<ErrorCodes, LocationV2Dto>(ErrorCodes.UnknownError, null);
                        }
                    default:
                        {
                            return new Tuple<ErrorCodes, LocationV2Dto>(ErrorCodes.UnknownError, null);
                        }
                }
            }
            else
            {
                return new Tuple<ErrorCodes, LocationV2Dto>(ErrorCodes.UnknownError, null);
            }
        }

        public async Task<Tuple<BusinessLogicMessageCodes.ErrorCodes, LocationV2Dto>> GetByIdFromIntAsync(int id)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            return await _genericStandartBusinessRules.GetByIdFromInt(id);
                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            return new Tuple<ErrorCodes, LocationV2Dto>(ErrorCodes.UnknownError, null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<ErrorCodes, LocationV2Dto>(ErrorCodes.UnknownError, null);
                        }
                    default:
                        {
                            return new Tuple<ErrorCodes, LocationV2Dto>(ErrorCodes.UnknownError, null);
                        }
                }
            }
            else
            {
                return new Tuple<ErrorCodes, LocationV2Dto>(ErrorCodes.UnknownError, null);
            }
        }

        //InsertLocationV2WithinParentHierarchyId kullanılmalıdır
        public async Task<Tuple<BusinessLogicMessageCodes.ErrorCodes, LocationV2Dto>> InsertAsync(LocationV2Dto insertedDto)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            throw  new NotImplementedException();

                            //return await _genericStandartBusinessRules.Insert(insertedDto);
                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            return new Tuple<ErrorCodes, LocationV2Dto>(ErrorCodes.UnknownError, null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<ErrorCodes, LocationV2Dto>(ErrorCodes.UnknownError, null);
                        }
                    default:
                        {
                            return new Tuple<ErrorCodes, LocationV2Dto>(ErrorCodes.UnknownError, null);
                        }
                }
            }
            else
            {
                return new Tuple<ErrorCodes, LocationV2Dto>(ErrorCodes.UnknownError, null);
            }
        }

        //UpdateV2Async kullanılmalıdır
        public async Task<Tuple<BusinessLogicMessageCodes.ErrorCodes, LocationV2Dto>> UpdateAsync(LocationV2Dto updatedDto)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {                            
                            List<string> uniqueColumns = new List<string>() { "Id" };
                            List<object> uniqueValues = new List<object>() { updatedDto.Id };

                            return await _genericStandartBusinessRules.Update(updatedDto, uniqueColumns, uniqueValues);
                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            return new Tuple<ErrorCodes, LocationV2Dto>(ErrorCodes.UnknownError, null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<ErrorCodes, LocationV2Dto>(ErrorCodes.UnknownError, null);
                        }
                    default:
                        {
                            return new Tuple<ErrorCodes, LocationV2Dto>(ErrorCodes.UnknownError, null);
                        }
                }
            }
            else
            {
                return new Tuple<ErrorCodes, LocationV2Dto>(ErrorCodes.UnknownError, null);
            }
        }

        public async Task<Tuple<Error, DevCustomDto.CustomLocationDto>> UpdateV2Async(BusinessLogicRequest request)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {                           
                            ILocationV2BusinessRules businessRule =
                                (ILocationV2BusinessRules)_baseLocationBusinessRules;

                            Tuple<Error, DevCustomDto.CustomLocationDto> businessRuleResult = await businessRule.UpdateV2Async(request);

                            if(businessRuleResult.Item1.BusinessOperationSucceed)
                            {
                                //redis cache'i temizle
                                RedisCacheOperationExtensions k = new RedisCacheOperationExtensions("127.0.0.1:6379", "*Location*");
                                
                            }

                            return businessRuleResult;
                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            return new Tuple<Error, DevCustomDto.CustomLocationDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, DevCustomDto.CustomLocationDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    default:
                        {
                            return new Tuple<Error, DevCustomDto.CustomLocationDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, DevCustomDto.CustomLocationDto>(Errors.General.GeneralServerError(), null);
            }
        }

        public async Task<Tuple<Error, DevCustomDto.CustomLocationDto>> DeleteV2Async(BusinessLogicRequest request)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            ILocationV2BusinessRules businessRule =
                                (ILocationV2BusinessRules)_baseLocationBusinessRules;

                            Tuple<Error, DevCustomDto.CustomLocationDto> businessRuleResult = await businessRule.DeleteV2Async(request);

                            if (businessRuleResult.Item1.BusinessOperationSucceed)
                            {
                                //redis cache'i temizle
                                RedisCacheOperationExtensions k = new RedisCacheOperationExtensions("127.0.0.1:6379", "*Location*");
                            }

                            return businessRuleResult;
                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            return new Tuple<Error, DevCustomDto.CustomLocationDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, DevCustomDto.CustomLocationDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    default:
                        {
                            return new Tuple<Error, DevCustomDto.CustomLocationDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, DevCustomDto.CustomLocationDto>(Errors.General.GeneralServerError(), null);
            }
        }

        public async Task<Tuple<Error, IEnumerable<DevCustomDto.CustomLocationDto>>> GetNodesTicketLocationByIdWithinLevelAsync(BusinessLogicRequest request)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            ILocationV2BusinessRules businessRule =
                                (ILocationV2BusinessRules)_baseLocationBusinessRules;

                            if (request.RequestDomain == RemoteIncomingDomains.DiasTesisYonetimWebClient)
                            {
                                return await businessRule.GetNodesTicketLocationByIdWithinLevelAsync(request);
                            }
                            else
                            {
                                return await businessRule.GetNodesTicketLocationByIdWithinLevelMobileAsync(request);
                            }

                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            return new Tuple<Error, IEnumerable<DevCustomDto.CustomLocationDto>>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, IEnumerable<DevCustomDto.CustomLocationDto>>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    default:
                        {
                            return new Tuple<Error, IEnumerable<DevCustomDto.CustomLocationDto>>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, IEnumerable<DevCustomDto.CustomLocationDto>>(Errors.General.GeneralServerError(), null);
            }
        }

        public async Task<Tuple<Error, DevCustomDto.CustomLocationDto>> InsertLocationV2WithinParentHierarchyId(BusinessLogicRequest request)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            ILocationV2BusinessRules businessRule =
                                (ILocationV2BusinessRules)_baseLocationBusinessRules;

                            Tuple<Error, DevCustomDto.CustomLocationDto> businessRuleResult = await businessRule.InsertLocationV2WithinParentHierarchyId(request);

                            if (businessRuleResult.Item1.BusinessOperationSucceed)
                            {
                                //redis cache'i temizle
                                RedisCacheOperationExtensions k = new RedisCacheOperationExtensions("127.0.0.1:6379", "*Location*");
                            }

                            return businessRuleResult;

                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            return new Tuple<Error, DevCustomDto.CustomLocationDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, DevCustomDto.CustomLocationDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    default:
                        {
                            return new Tuple<Error, DevCustomDto.CustomLocationDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, DevCustomDto.CustomLocationDto>(Errors.General.GeneralServerError(), null);
            }
        }

    }
}
