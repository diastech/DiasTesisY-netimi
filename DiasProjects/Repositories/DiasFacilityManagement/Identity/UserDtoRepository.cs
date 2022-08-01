using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.BaseBusinessRules.SqlServer.Standart;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using DiasWebApi.Shared.Operations.BusinessLogicOperations.SimpleInjectorOperations;
using System;
using System.Threading.Tasks;
using static DiasWebApi.Shared.Enums.WebApiApplicationEnums;
using DevelopmentBL_Interface =  DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Standart;
using DevelopmentBL_InterfaceCustom = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Custom;
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
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.BaseBusinessRules.SqlServer.Custom;

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

        public UserDtoRepository(
            IBaseUserBusinessRules baseUserBusinessRules,
            IBaseUserAssignmentGroupWrapperBusinessRules baseUserAssignmentGroupWrapperBusinessRules,
            ApplicationBusinessLogicEnvironment applicationBusinessLogicEnvironment,
            IGenericStandartBusinessRules<UserDto, UserProfile> genericStandartBusinessRules,
            GenericInterfaceTest.IGenericStandartBusinessRules<TestDto.UserDto, TestAutomapper.UserProfile> genericStandartBusinessRulesTest
            )
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
                            //TODO: Buraya generic metod gelecek
                            //DevelopmentBL_Interface.IUserBusinessRules businessRule =
                            //     (DevelopmentBL_Interface.IUserBusinessRules)_baseUserBusinessRules;

                            //return await businessRule.MakeUserActiveOrPassiveByUserIdAsync(userId, userStatusId);

                            //TODO : Generic metodlar tamamlanınca alttaki satır kalkacaktır
                            //return new Tuple<ErrorCodes, DevelopmentDto.UserDto>(ErrorCodes.None, new DevelopmentDto.UserDto());

                            return await _genericStandartBusinessRules.GetAllV2();
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
                            DevelopmentBL_InterfaceCustom.IUserAssignmentGroupWrapperBusinessRules businessRule =
                                 (DevelopmentBL_InterfaceCustom.IUserAssignmentGroupWrapperBusinessRules)_baseUserAssignmentGroupWrapperBusinessRules;

                            return await businessRule.GetAllCombinedUserAndAssigmentGroups();
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
