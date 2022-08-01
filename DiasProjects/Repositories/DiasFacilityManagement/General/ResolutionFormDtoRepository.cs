using DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Development;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.BaseBusinessRules.SqlServer.Standart;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Generic.DiasFacilityManagementSqlServer.Development;
using DiasDataAccessLayer.Enums;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement;
using DiasWebApi.Shared.Operations.BusinessLogicOperations.SimpleInjectorOperations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using static DiasWebApi.Shared.Enums.WebApiApplicationEnums;
using DevelopmentRepositoryInterface = DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement;

namespace DiasWebApi.Repositories.DiasFacilityManagement
{
    public class ResolutionFormDtoRepository : DevelopmentRepositoryInterface.IResolutionFormDtoRepository
    {
        private IBaseResolutionFormBusinessRules _baseResolutionFormBusinessRules;
        private ApplicationBusinessLogicEnvironment _applicationBusinessLogicEnvironment;
        private IGenericStandartBusinessRules<ResolutionFormDto, ResolutionFormProfile> _genericStandartBusinessRules;
        private bool businessLogicContainerStatus { get; set; }
        public ResolutionFormDtoRepository(
            IBaseResolutionFormBusinessRules baseResolutionFormBusinessRules, 
            ApplicationBusinessLogicEnvironment applicationBusinessLogicEnvironment,
            IGenericStandartBusinessRules<ResolutionFormDto, ResolutionFormProfile> genericStandartBusinessRules
            )
        {
            businessLogicContainerStatus = SimpleInjectorContainerOperations.VerifyContainer();

            if (businessLogicContainerStatus)
            {
                _baseResolutionFormBusinessRules = baseResolutionFormBusinessRules;
                _applicationBusinessLogicEnvironment = applicationBusinessLogicEnvironment;
            }

            _genericStandartBusinessRules = genericStandartBusinessRules;
        }

        public async Task<Tuple<BusinessLogicMessageCodes.ErrorCodes, IEnumerable<ResolutionFormDto>>> GetAllAsync()
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
                            return new Tuple<ErrorCodes, IEnumerable<ResolutionFormDto>>(ErrorCodes.UnknownError, null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<ErrorCodes, IEnumerable<ResolutionFormDto>>(ErrorCodes.UnknownError, null);
                        }
                    default:
                        {
                            return new Tuple<ErrorCodes, IEnumerable<ResolutionFormDto>>(ErrorCodes.UnknownError, null);
                        }
                }
            }
            else
            {
                return new Tuple<ErrorCodes, IEnumerable<ResolutionFormDto>>(ErrorCodes.UnknownError, null);
            }
        }

        public async Task<Tuple<BusinessLogicMessageCodes.ErrorCodes, ResolutionFormDto>> DeleteFromIntAsync(int id)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            return await _genericStandartBusinessRules.DeleteFromInt(id);
                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            return new Tuple<ErrorCodes, ResolutionFormDto>(ErrorCodes.UnknownError, null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<ErrorCodes, ResolutionFormDto>(ErrorCodes.UnknownError, null);
                        }
                    default:
                        {
                            return new Tuple<ErrorCodes, ResolutionFormDto>(ErrorCodes.UnknownError, null);
                        }
                }
            }
            else
            {
                return new Tuple<ErrorCodes, ResolutionFormDto>(ErrorCodes.UnknownError, null);
            }
        }

        public async Task<Tuple<BusinessLogicMessageCodes.ErrorCodes, ResolutionFormDto>> GetByIdFromIntAsync(int id)
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
                            return new Tuple<ErrorCodes, ResolutionFormDto>(ErrorCodes.UnknownError, null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<ErrorCodes, ResolutionFormDto>(ErrorCodes.UnknownError, null);
                        }
                    default:
                        {
                            return new Tuple<ErrorCodes, ResolutionFormDto>(ErrorCodes.UnknownError, null);
                        }
                }
            }
            else
            {
                return new Tuple<ErrorCodes, ResolutionFormDto>(ErrorCodes.UnknownError, null);
            }
        }

        public async Task<Tuple<BusinessLogicMessageCodes.ErrorCodes, ResolutionFormDto>> InsertAsync(ResolutionFormDto insertedDto)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            return await _genericStandartBusinessRules.Insert(insertedDto);
                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            return new Tuple<ErrorCodes, ResolutionFormDto>(ErrorCodes.UnknownError, null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<ErrorCodes, ResolutionFormDto>(ErrorCodes.UnknownError, null);
                        }
                    default:
                        {
                            return new Tuple<ErrorCodes, ResolutionFormDto>(ErrorCodes.UnknownError, null);
                        }
                }
            }
            else
            {
                return new Tuple<ErrorCodes, ResolutionFormDto>(ErrorCodes.UnknownError, null);
            }
        }

        public async Task<Tuple<BusinessLogicMessageCodes.ErrorCodes, ResolutionFormDto>> UpdateAsync(ResolutionFormDto updatedDto)
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
                            return new Tuple<ErrorCodes, ResolutionFormDto>(ErrorCodes.UnknownError, null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<ErrorCodes, ResolutionFormDto>(ErrorCodes.UnknownError, null);
                        }
                    default:
                        {
                            return new Tuple<ErrorCodes, ResolutionFormDto>(ErrorCodes.UnknownError, null);
                        }
                }
            }
            else
            {
                return new Tuple<ErrorCodes, ResolutionFormDto>(ErrorCodes.UnknownError, null);
            }
        }
    }
}
