using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.BaseBusinessRules.SqlServer.Standart;
using DIMDevDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using DiasWebApi.Shared.Operations.BusinessLogicOperations.SimpleInjectorOperations;
using System;
using System.Threading.Tasks;
using static DiasWebApi.Shared.Enums.WebApiApplicationEnums;
using System.Collections.Generic;
using DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Generic.DiasFacilityManagementSqlServer.Development;
using DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Development;
using DiasShared.Errors;
using DiasBusinessLogic.Shared.Error;

namespace DiasWebApi.Repositories.DiasFacilityManagement
{
    public class UserClaimDtoRepository : IUserClaimDtoRepository
    {
        private IBaseUserClaimBusinessRules _baseUserClaimBusinessRules;
        private ApplicationBusinessLogicEnvironment _applicationBusinessLogicEnvironment;
        private IGenericStandartBusinessRules<DIMDevDto.UserClaimDto, UserClaimProfile> _genericStandartBusinessRules;
        private bool businessLogicContainerStatus { get; set; }
        public UserClaimDtoRepository(
            IBaseUserClaimBusinessRules baseUserClaimBusinessRules, 
            ApplicationBusinessLogicEnvironment applicationBusinessLogicEnvironment,
            IGenericStandartBusinessRules<DIMDevDto.UserClaimDto, UserClaimProfile> genericStandartBusinessRules
            )
        {
            businessLogicContainerStatus = SimpleInjectorContainerOperations.VerifyContainer();

            if (businessLogicContainerStatus)
            {
                _baseUserClaimBusinessRules = baseUserClaimBusinessRules;
                _applicationBusinessLogicEnvironment = applicationBusinessLogicEnvironment;
            }

            _genericStandartBusinessRules = genericStandartBusinessRules;
        }

        //TODO : Hata kodları özelleştirilecek
        public async Task<Tuple<Error, DIMDevDto.UserClaimDto>> GetByIdFromIntAsync(int id)
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
                            return new Tuple<Error, DIMDevDto.UserClaimDto>(Errors.General.GeneralServerError(), null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, DIMDevDto.UserClaimDto>(Errors.General.GeneralServerError(), null);
                        }
                    default:
                        {
                            return new Tuple<Error, DIMDevDto.UserClaimDto>(Errors.General.GeneralServerError(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, DIMDevDto.UserClaimDto>(Errors.General.GeneralServerError(), null);
            }          
        }

        //TODO : Hata kodları özelleştirilecek
        public async Task<Tuple<Error, IEnumerable<DIMDevDto.UserClaimDto>>> GetAllAsync()
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            return await _genericStandartBusinessRules.GetAllV2();
                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            return new Tuple<Error, IEnumerable<DIMDevDto.UserClaimDto>>(Errors.General.GeneralServerError(), null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, IEnumerable<DIMDevDto.UserClaimDto>>(Errors.General.GeneralServerError(), null);
                        }
                    default:
                        {
                            return new Tuple<Error, IEnumerable<DIMDevDto.UserClaimDto>>(Errors.General.GeneralServerError(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, IEnumerable<DIMDevDto.UserClaimDto>>(Errors.General.GeneralServerError(), null);
            }             
        }

        //TODO : Hata kodları özelleştirilecek
        public async Task<Tuple<Error, DIMDevDto.UserClaimDto>> InsertAsync(DIMDevDto.UserClaimDto userClaimDto)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            return await _genericStandartBusinessRules.InsertV2(userClaimDto);
                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            return new Tuple<Error, DIMDevDto.UserClaimDto>(Errors.General.GeneralServerError(), null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, DIMDevDto.UserClaimDto>(Errors.General.GeneralServerError(), null);
                        }
                    default:
                        {
                            return new Tuple<Error, DIMDevDto.UserClaimDto>(Errors.General.GeneralServerError(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, DIMDevDto.UserClaimDto>(Errors.General.GeneralServerError(), null);
            }          
        }

        //TODO : Hata kodları özelleştirilecek
        public async Task<Tuple<Error, DIMDevDto.UserClaimDto>> UpdateAsync(DIMDevDto.UserClaimDto userClaimDto)
        {
           if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            List<string> uniqueColumns = new List<string>() { };
                            List<object> uniqueValues = new List<object>() { };
                            return await _genericStandartBusinessRules.UpdateV2(userClaimDto, uniqueColumns, uniqueValues, false);
                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            return new Tuple<Error, DIMDevDto.UserClaimDto>(Errors.General.GeneralServerError(), null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, DIMDevDto.UserClaimDto>(Errors.General.GeneralServerError(), null);
                        }
                    default:
                        {
                            return new Tuple<Error, DIMDevDto.UserClaimDto>(Errors.General.GeneralServerError(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, DIMDevDto.UserClaimDto>(Errors.General.GeneralServerError(), null);
            }           
        }

        //TODO : Hata kodları özelleştirilecek
        public async Task<Tuple<Error, DIMDevDto.UserClaimDto>> DeleteFromIntAsync(int id)
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
                            return new Tuple<Error, DIMDevDto.UserClaimDto>(Errors.General.GeneralServerError(), null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, DIMDevDto.UserClaimDto>(Errors.General.GeneralServerError(), null);
                        }
                    default:
                        {
                            return new Tuple<Error, DIMDevDto.UserClaimDto>(Errors.General.GeneralServerError(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, DIMDevDto.UserClaimDto>(Errors.General.GeneralServerError(), null);
            }         
        }

        //TODO : Doldurulacak
        public async Task<Tuple<Error, DIMDevDto.UserClaimDto>> GetAllUserClaimsByUserIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

    }
}
