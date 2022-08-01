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
using TestDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard;
using TestAutomapper = DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Test;
using GenericInterfaceTest = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Generic.DiasFacilityManagementSqlServer.Test;

namespace DiasWebApi.Repositories.DiasFacilityManagement
{
    public class PeriodicTicketDtoRepository : DevelopmentRepositoryInterface.IPeriodicTicketDtoRepository
    {
        private IBasePeriodicTicketBusinessRules _basePeriodicTicketBusinessRules;
        private ApplicationBusinessLogicEnvironment _applicationBusinessLogicEnvironment;
        private IGenericStandartBusinessRules<PeriodicTicketDto, PeriodicTicketProfile> _genericStandartBusinessRules;
        private GenericInterfaceTest.IGenericStandartBusinessRules<TestDto.PeriodicTicketDto, TestAutomapper.PeriodicTicketProfile> _genericStandartBusinessRulesTest;
        private bool businessLogicContainerStatus { get; set; }
        public PeriodicTicketDtoRepository(
            IBasePeriodicTicketBusinessRules basePeriodicTicketBusinessRules, 
            ApplicationBusinessLogicEnvironment applicationBusinessLogicEnvironment,
            IGenericStandartBusinessRules<PeriodicTicketDto, PeriodicTicketProfile> genericStandartBusinessRules,
            GenericInterfaceTest.IGenericStandartBusinessRules<TestDto.PeriodicTicketDto, TestAutomapper.PeriodicTicketProfile> genericStandartBusinessRulesTest
            )
        {
            businessLogicContainerStatus = SimpleInjectorContainerOperations.VerifyContainer();

            if (businessLogicContainerStatus)
            {
                _basePeriodicTicketBusinessRules = basePeriodicTicketBusinessRules;
                _applicationBusinessLogicEnvironment = applicationBusinessLogicEnvironment;
            }

            _genericStandartBusinessRules = genericStandartBusinessRules;
            _genericStandartBusinessRulesTest = genericStandartBusinessRulesTest;
        }

        public async Task<Tuple<BusinessLogicMessageCodes.ErrorCodes, IEnumerable<PeriodicTicketDto>>> GetAllAsync()
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
                            return new Tuple<ErrorCodes, IEnumerable<PeriodicTicketDto>>(ErrorCodes.UnknownError, null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<ErrorCodes, IEnumerable<PeriodicTicketDto>>(ErrorCodes.UnknownError, null);
                        }
                    default:
                        {
                            return new Tuple<ErrorCodes, IEnumerable<PeriodicTicketDto>>(ErrorCodes.UnknownError, null);
                        }
                }
            }
            else
            {
                return new Tuple<ErrorCodes, IEnumerable<PeriodicTicketDto>>(ErrorCodes.UnknownError, null);
            }
        }

        public async Task<Tuple<BusinessLogicMessageCodes.ErrorCodes, PeriodicTicketDto>> DeleteFromIntAsync(int id)
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
                            return new Tuple<ErrorCodes, PeriodicTicketDto>(ErrorCodes.UnknownError, null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<ErrorCodes, PeriodicTicketDto>(ErrorCodes.UnknownError, null);
                        }
                    default:
                        {
                            return new Tuple<ErrorCodes, PeriodicTicketDto>(ErrorCodes.UnknownError, null);
                        }
                }
            }
            else
            {
                return new Tuple<ErrorCodes, PeriodicTicketDto>(ErrorCodes.UnknownError, null);
            }
        }

        public async Task<Tuple<BusinessLogicMessageCodes.ErrorCodes, PeriodicTicketDto>> GetByIdFromIntAsync(int id)
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
                            return new Tuple<ErrorCodes, PeriodicTicketDto>(ErrorCodes.UnknownError, null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<ErrorCodes, PeriodicTicketDto>(ErrorCodes.UnknownError, null);
                        }
                    default:
                        {
                            return new Tuple<ErrorCodes, PeriodicTicketDto>(ErrorCodes.UnknownError, null);
                        }
                }
            }
            else
            {
                return new Tuple<ErrorCodes, PeriodicTicketDto>(ErrorCodes.UnknownError, null);
            }
        }

        public async Task<Tuple<BusinessLogicMessageCodes.ErrorCodes, PeriodicTicketDto>> InsertAsync(PeriodicTicketDto insertedDto)
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
                            return new Tuple<ErrorCodes, PeriodicTicketDto>(ErrorCodes.UnknownError, null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<ErrorCodes, PeriodicTicketDto>(ErrorCodes.UnknownError, null);
                        }
                    default:
                        {
                            return new Tuple<ErrorCodes, PeriodicTicketDto>(ErrorCodes.UnknownError, null);
                        }
                }
            }
            else
            {
                return new Tuple<ErrorCodes, PeriodicTicketDto>(ErrorCodes.UnknownError, null);
            }
        }

        public async Task<Tuple<BusinessLogicMessageCodes.ErrorCodes, PeriodicTicketDto>> UpdateAsync(PeriodicTicketDto updatedDto)
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
                            return new Tuple<ErrorCodes, PeriodicTicketDto>(ErrorCodes.UnknownError, null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<ErrorCodes, PeriodicTicketDto>(ErrorCodes.UnknownError, null);
                        }
                    default:
                        {
                            return new Tuple<ErrorCodes, PeriodicTicketDto>(ErrorCodes.UnknownError, null);
                        }
                }
            }
            else
            {
                return new Tuple<ErrorCodes, PeriodicTicketDto>(ErrorCodes.UnknownError, null);
            }
        }
    }
}
