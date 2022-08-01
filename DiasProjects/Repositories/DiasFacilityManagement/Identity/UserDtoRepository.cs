using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.BaseBusinessRules.SqlServer.Standart;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using DevelopmentCustomDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using DiasWebApi.Shared.Operations.BusinessLogicOperations.SimpleInjectorOperations;
using System;
using System.Threading.Tasks;
using static DiasWebApi.Shared.Enums.WebApiApplicationEnums;
using DevelopmentBL_Interface =  DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Standart;
using DevelopmentRepositoryInterface =  DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Generic.DiasFacilityManagementSqlServer.Development;
using System.Collections.Generic;
using DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Development;
using TestDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard;
using TestAutomapper = DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Test;
using GenericInterfaceTest = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Generic.DiasFacilityManagementSqlServer.Test;
using DiasShared.Errors;
using DiasBusinessLogic.Shared.Error;
using DiasShared.Services.Communication.BusinessLogicMessage;
using Newtonsoft.Json;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.BaseBusinessRules.SqlServer.Custom;
using DevelopmentBL_InterfaceCustom = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Custom;
using DiasShared.Enums.Standart;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using DiasWebApi.Shared.Configuration;
using static DiasShared.Enums.Standart.CacheEnums;
using DiasShared.Operations.EnumOperations;
using DiasWebApi.Shared.Operations.RedisCacheOperation;
using DiasWebApi.Shared.Constants;

namespace DiasWebApi.Repositories.DiasFacilityManagement
{
    public class UserDtoRepository : DevelopmentRepositoryInterface.IUserDtoRepository
    {
        private IBaseUserBusinessRules _baseUserBusinessRules;
        private IBaseUserAssignmentGroupWrapperBusinessRules _baseUserAssignmentGroupWrapperBusinessRules;

        private ApplicationBusinessLogicEnvironment _applicationBusinessLogicEnvironment;
        private IGenericStandartBusinessRules<UserDto, UserProfile> _genericStandartBusinessRules;
        private GenericInterfaceTest.IGenericStandartBusinessRules<TestDto.UserDto, TestAutomapper.UserProfile> _genericStandartBusinessRulesTest;
        private bool businessLogicContainerStatus { get; set; }
        private IDistributedCache _redisCache;
        private string redisCacheKeyHeader;


        public UserDtoRepository(
            IBaseUserBusinessRules baseUserBusinessRules,
            IBaseUserAssignmentGroupWrapperBusinessRules baseUserAssignmentGroupWrapperBusinessRules,
            ApplicationBusinessLogicEnvironment applicationBusinessLogicEnvironment,
            IGenericStandartBusinessRules<UserDto, UserProfile> genericStandartBusinessRules,
            GenericInterfaceTest.IGenericStandartBusinessRules<TestDto.UserDto, TestAutomapper.UserProfile> genericStandartBusinessRulesTest,
            IDistributedCache redisCache)
        {
            //BL Simple Injector Container'ını kontrol et
            businessLogicContainerStatus = SimpleInjectorContainerOperations.VerifyContainer();

            if (businessLogicContainerStatus)
            {
                _baseUserBusinessRules = baseUserBusinessRules;
                _baseUserAssignmentGroupWrapperBusinessRules = baseUserAssignmentGroupWrapperBusinessRules;
                _applicationBusinessLogicEnvironment = applicationBusinessLogicEnvironment;
            }

            _genericStandartBusinessRules = genericStandartBusinessRules;
            _genericStandartBusinessRulesTest = genericStandartBusinessRulesTest;

            //redis ayarları
            _redisCache = redisCache;
            IConfiguration webApiConfigurationSettings = ConfigurationHelper.GetConfig();
            redisCacheKeyHeader = ConfigurationHelper.GetRedisKeyHeader(webApiConfigurationSettings);
        }


        //TODO : Farklı environmentler için parametreyi nasıl değiştirebiliriz?
        //TODO : Hata kodları özelleştirilecek
        public async Task<Tuple<Error, IEnumerable<UserDto>>> GetAllAsync()
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
                                    RedisDtoDefinitions.UserDto.GetDisplayOrValueFromEnum<RedisDtoDefinitions>();

                                IEnumerable<UserDto> usersFromCache = null;
                                try
                                {
                                    usersFromCache = await RedisCacheOperations<UserDto>.GetFromCacheViaList(_redisCache, redisKey);
                                }
                                catch (Exception)
                                {
                                }

                                //cache dolu ise döndür değilse aşağıda iş kuralına git
                                if (usersFromCache != null)
                                {
                                    return new Tuple<Error, IEnumerable<UserDto>>(Errors.General.Success("generic"), usersFromCache);
                                }
                            }                         

                            Tuple<Error, IEnumerable<UserDto>> businessRuleResult = await _genericStandartBusinessRules.GetAllV2();

                            if (businessRuleResult.Item1.BusinessOperationSucceed && (!(String.IsNullOrEmpty(redisCacheKeyHeader))))
                            {
                                //Cachi dolduralım
                                try
                                {
                                    await RedisCacheOperations<UserDto>.SetCacheList(_redisCache, redisKey,
                                                    businessRuleResult.Item2, RedisCacheConstants.userRedisCacheEntryOptions);
                                }
                                catch (Exception)
                                {
                                }
                            }

                            return businessRuleResult;                            
                        }

                    //TODO: paremetre test dtosu olacak
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            return new Tuple<Error, IEnumerable<DevelopmentDto.UserDto>>(Errors.General.GeneralServerError(), null);
                        }

                    //TODO: paremetre canlı dtosu olacak
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, IEnumerable<DevelopmentDto.UserDto>>(Errors.General.GeneralServerError(), null);
                        }

                    default:
                        {
                            return new Tuple<Error, IEnumerable<DevelopmentDto.UserDto>>(Errors.General.GeneralServerError(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, IEnumerable<DevelopmentDto.UserDto>>(Errors.General.GeneralServerError(), null);
            }
        }

        //TODO : Hata kodları özelleştirilecek
        public async Task<Tuple<Error, UserDto>> DeleteFromIntAsync(int id)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            return await _genericStandartBusinessRules.DeleteFromIntV2(id);
                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            return new Tuple<Error, UserDto>(Errors.General.GeneralServerError(), null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, UserDto>(Errors.General.GeneralServerError(), null);
                        }
                    default:
                        {
                            return new Tuple<Error, UserDto>(Errors.General.GeneralServerError(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, UserDto>(Errors.General.GeneralServerError(), null);
            }
        }

        //TODO : Hata kodları özelleştirilecek
        public async Task<Tuple<Error, UserDto>> GetByIdFromIntAsync(int id)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            return await _genericStandartBusinessRules.GetByIdFromIntV2(id);
                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            return new Tuple<Error, UserDto>(Errors.General.GeneralServerError(), null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, UserDto>(Errors.General.GeneralServerError(), null);
                        }
                    default:
                        {
                            return new Tuple<Error, UserDto>(Errors.General.GeneralServerError(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, UserDto>(Errors.General.GeneralServerError(), null);
            }
        }

        //TODO : Hata kodları özelleştirilecek
        public async Task<Tuple<Error, UserDto>> InsertAsync(UserDto insertedDto)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            //Burada aynı kayıtı insert edip etmediğimizi kontrol edebiliriz
                            //Aynı kayıtı insert etmeye kalkarsak hata verir
                            //uniqueColumns dto propertysi değil entity propertysi olacak!(şimdi hepsi aynı gerçi)
                            //ileride bunu dto propertysi olacak şekilde ayarlayacağım
                            List<string> uniqueColumns = new List<string>()
                            {
                                "UserName",
                                "LastName",
                                "PhoneNumber"
                            };

                            //Tablodaki değerler uniqueColumns a göre
                            List<object> uniqueValues = new List<object>()
                            {
                                insertedDto.UserName,
                                insertedDto.LastName,
                                insertedDto.PhoneNumber
                            };

                            return await _genericStandartBusinessRules.InsertV2(insertedDto, uniqueColumns, uniqueValues);

                            //Eğer aynı kayıt kontrolu olmadan direkt insert yapacaksak  aşağısı
                            //return await _genericStandartBusinessRules.Insert(insertedDto);
                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            return new Tuple<Error, UserDto>(Errors.General.GeneralServerError(), null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, UserDto>(Errors.General.GeneralServerError(), null);
                        }
                    default:
                        {
                            return new Tuple<Error, UserDto>(Errors.General.GeneralServerError(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, UserDto>(Errors.General.GeneralServerError(), null);
            }
        }

        //TODO : Hata kodları özelleştirilecek
        public async Task<Tuple<Error, UserDto>> UpdateAsync(UserDto updatedDto)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            //Burada update edilecek satırı belirtiyoruz
                            //uniqueColumns dto propertysi değil entity propertysi olacak!(şimdi hepsi aynı gerçi)
                            //ileride bunu dto propertysi olacak şekilde ayarlayacağım
                            //ille id olmak zorunda değil diğer sütunlarda olabilir
                            List<string> uniqueColumns = new List<string>()
                            {
                                "Id"
                            };

                            //Tablodaki değerler uniqueColumns a göre
                            List<object> uniqueValues = new List<object>()
                            {
                                6
                            };

                            //Eğer basedto da (addedbyuserid vb) update edilecekse ve dtoda updateli ise metodun üçüncü parametresi true olacak(varsayılan olarak boş da bırakılabilir))
                            //Yok basedatoda update olmayacaksa false olacak
                            return await _genericStandartBusinessRules.UpdateV2(updatedDto, uniqueColumns, uniqueValues, false);
                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            return new Tuple<Error, UserDto>(Errors.General.GeneralServerError(), null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, UserDto>(Errors.General.GeneralServerError(), null);
                        }
                    default:
                        {
                            return new Tuple<Error, UserDto>(Errors.General.GeneralServerError(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, UserDto>(Errors.General.GeneralServerError(), null);
            }
        }

        //TODO : Hata kodları özelleştirilecek
        public async Task<Tuple<Error, UserDto>> Login(string email, string password)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            DevelopmentBL_Interface.IUserBusinessRules businessRule =
                                 (DevelopmentBL_Interface.IUserBusinessRules)_baseUserBusinessRules;

                            return await businessRule.Login(email,password);                            
                        }

                    //TODO: paremetre test dtosu olacak
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            return new Tuple<Error, DevelopmentDto.UserDto>(Errors.General.GeneralServerError(), null);
                        }

                    //TODO: paremetre canlı dtosu olacak
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, DevelopmentDto.UserDto>(Errors.General.GeneralServerError(), null);
                        }

                    default:
                        {
                            return new Tuple<Error, DevelopmentDto.UserDto>(Errors.General.GeneralServerError(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, DevelopmentDto.UserDto>(Errors.General.GeneralServerError(), null);
            }
        }

        //TODO : Hata kodları özelleştirilecek
        public async Task<Tuple<Error, UserDto>> LoginV2(BusinessLogicRequest request)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            if(request != null)
                            {
                                if(request.RequestDomain == RemoteCommunicationEnums.RemoteIncomingDomains.DiasTesisYonetimMobileClient)
                                {
                                    if ((request.RequestDtosJsons == null) ||
                                           (request.RequestDtosJsons.Count < 1) ||
                                             (String.IsNullOrEmpty(request.RequestDtosJsons[0])))
                                    {
                                        return new Tuple<Error, UserDto>(Errors.General.GeneralServerError(), null);
                                    }
                                }
                                else
                                {
                                    if ((request.RequestDtosTypes == null) || (request.RequestDtosJsons == null) ||
                                           (request.RequestDtosTypes.Count < 1) || (request.RequestDtosJsons.Count < 1) ||
                                             (String.IsNullOrEmpty(request.RequestDtosJsons[0])) ||
                                               (!Type.Equals(request.RequestDtosTypes[0], typeof(DevelopmentCustomDto.UserCredentialsDto))))
                                    {
                                        return new Tuple<Error, UserDto>(Errors.General.GeneralServerError(), null);
                                    }
                                }

                                DevelopmentCustomDto.UserCredentialsDto castedDto =
                                    JsonConvert.DeserializeObject<DevelopmentCustomDto.UserCredentialsDto>(request.RequestDtosJsons[0]);

                                if (castedDto == null)
                                {
                                    return new Tuple<Error, UserDto>(Errors.General.GeneralServerError(), null);
                                }

                                DevelopmentBL_Interface.IUserBusinessRules businessRule =
                                     (DevelopmentBL_Interface.IUserBusinessRules)_baseUserBusinessRules;

                                return await businessRule.LoginV2(castedDto);
                            }
                            else 
                            {
                                return new Tuple<Error, UserDto>(Errors.General.GeneralServerError(), null);
                            }
                        }

                    //TODO: paremetre test dtosu olacak
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            return new Tuple<Error, DevelopmentDto.UserDto>(Errors.General.GeneralServerError(), null);
                        }

                    //TODO: paremetre canlı dtosu olacak
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, DevelopmentDto.UserDto>(Errors.General.GeneralServerError(), null);
                        }

                    default:
                        {
                            return new Tuple<Error, DevelopmentDto.UserDto>(Errors.General.GeneralServerError(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, DevelopmentDto.UserDto>(Errors.General.GeneralServerError(), null);
            }
        }

        //TODO : Hata kodları özelleştirilecek
        public async Task<Tuple<Error, UserDto>> MakeUserActiveOrPassiveByUserIdAsync(int userId, int userStatusId)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            DevelopmentBL_Interface.IUserBusinessRules businessRule =
                                 (DevelopmentBL_Interface.IUserBusinessRules)_baseUserBusinessRules;

                            return await businessRule.MakeUserActiveOrPassiveByUserIdAsync(userId, userStatusId);
                        }

                    //TODO: paremetre test dtosu olacak
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            return new Tuple<Error, DevelopmentDto.UserDto>(Errors.General.GeneralServerError(), null);
                        }

                    //TODO: paremetre canlı dtosu olacak
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, DevelopmentDto.UserDto>(Errors.General.GeneralServerError(), null);
                        }

                    default:
                        {
                            return new Tuple<Error, DevelopmentDto.UserDto>(Errors.General.GeneralServerError(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, DevelopmentDto.UserDto>(Errors.General.GeneralServerError(), null);
            }
        }

        //TODO : Hata kodları özelleştirilecek
        public async Task<Tuple<Error, IEnumerable<CombinedUserAndAssignmentGroupDto>>> GetAllCombinedUserAndAssigmentGroups()
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
                                    RedisDtoDefinitions.CombinedUserAndAssignmentGroupDto.GetDisplayOrValueFromEnum<RedisDtoDefinitions>();

                                IEnumerable<CombinedUserAndAssignmentGroupDto> userAndAssignmentGroupFromCache = null;
                                try
                                {
                                    userAndAssignmentGroupFromCache = await RedisCacheOperations<CombinedUserAndAssignmentGroupDto>.GetFromCacheViaList(_redisCache, redisKey);
                                }
                                catch (Exception)
                                {
                                }

                                //cache dolu ise döndür değilse aşağıda iş kuralına git
                                if (userAndAssignmentGroupFromCache != null)
                                {
                                    return new Tuple<Error, IEnumerable<CombinedUserAndAssignmentGroupDto>>(Errors.General.GetListSuccess("CombinedUserAndAssignmentGroupDto"), userAndAssignmentGroupFromCache);
                                }
                            }


                            DevelopmentBL_InterfaceCustom.IUserAssignmentGroupWrapperBusinessRules businessRule =
                                 (DevelopmentBL_InterfaceCustom.IUserAssignmentGroupWrapperBusinessRules)_baseUserAssignmentGroupWrapperBusinessRules;

                            Tuple<Error, IEnumerable<CombinedUserAndAssignmentGroupDto>> businessRuleResult = await businessRule.GetAllCombinedUserAndAssigmentGroups();

                            if (businessRuleResult.Item1.BusinessOperationSucceed && (!(String.IsNullOrEmpty(redisCacheKeyHeader))))
                            {
                                //Cachi dolduralım
                                try
                                {
                                    await RedisCacheOperations<CombinedUserAndAssignmentGroupDto>.SetCacheList(_redisCache, redisKey,
                                                    businessRuleResult.Item2, RedisCacheConstants.CombinedUserAndAssignmentGroupRedisCacheEntryOptions);
                                }
                                catch (Exception)
                                {
                                }
                            }

                            return businessRuleResult;
                        }

                    //TODO: paremetre test dtosu olacak
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            return new Tuple<Error, IEnumerable<CombinedUserAndAssignmentGroupDto>>(Errors.General.GeneralServerError(), null);
                        }

                    //TODO: paremetre canlı dtosu olacak
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, IEnumerable<CombinedUserAndAssignmentGroupDto>>(Errors.General.GeneralServerError(), null);
                        }

                    default:
                        {
                            return new Tuple<Error, IEnumerable<CombinedUserAndAssignmentGroupDto>>(Errors.General.GeneralServerError(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, IEnumerable<CombinedUserAndAssignmentGroupDto>>(Errors.General.GeneralServerError(), null);
            }
        }

    }
}
