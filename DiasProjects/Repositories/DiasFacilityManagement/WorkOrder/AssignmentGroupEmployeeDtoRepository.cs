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
    public class AssignmentGroupEmployeeDtoRepository : DevelopmentRepositoryInterface.IAssignmentGroupEmployeeDtoRepository
    {
        private IBaseAssignmentGroupEmployeeBusinessRules _baseAssignmentGroupEmployeeBusinessRules;
        private ApplicationBusinessLogicEnvironment _applicationBusinessLogicEnvironment;
        private IGenericStandartBusinessRules<AssignmentGroupEmployeeDto, AssignmentGroupEmployeeProfile> _genericStandartBusinessRules;
        private GenericInterfaceTest.IGenericStandartBusinessRules<TestDto.AssignmentGroupEmployeeDto, TestAutomapper.AssignmentGroupEmployeeProfile> _genericStandartBusinessRulesTest;
        private bool businessLogicContainerStatus { get; set; }
        public AssignmentGroupEmployeeDtoRepository(
            IBaseAssignmentGroupEmployeeBusinessRules baseAssignmentGroupEmployeeBusinessRules, 
            ApplicationBusinessLogicEnvironment applicationBusinessLogicEnvironment,
            IGenericStandartBusinessRules<AssignmentGroupEmployeeDto, AssignmentGroupEmployeeProfile> genericStandartBusinessRules,
            GenericInterfaceTest.IGenericStandartBusinessRules<TestDto.AssignmentGroupEmployeeDto, TestAutomapper.AssignmentGroupEmployeeProfile> genericStandartBusinessRulesTest
            )
        {
            businessLogicContainerStatus = SimpleInjectorContainerOperations.VerifyContainer();

            if (businessLogicContainerStatus)
            {
                _baseAssignmentGroupEmployeeBusinessRules = baseAssignmentGroupEmployeeBusinessRules;
                _applicationBusinessLogicEnvironment = applicationBusinessLogicEnvironment;
            }

            _genericStandartBusinessRules = genericStandartBusinessRules;
            _genericStandartBusinessRulesTest = genericStandartBusinessRulesTest;
        }

        public async Task<Tuple<BusinessLogicMessageCodes.ErrorCodes, IEnumerable<AssignmentGroupEmployeeDto>>> GetAllAsync()
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
                            return new Tuple<ErrorCodes, IEnumerable<AssignmentGroupEmployeeDto>>(ErrorCodes.UnknownError, null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<ErrorCodes, IEnumerable<AssignmentGroupEmployeeDto>>(ErrorCodes.UnknownError, null);
                        }
                    default:
                        {
                            return new Tuple<ErrorCodes, IEnumerable<AssignmentGroupEmployeeDto>>(ErrorCodes.UnknownError, null);
                        }
                }
            }
            else
            {
                return new Tuple<ErrorCodes, IEnumerable<AssignmentGroupEmployeeDto>>(ErrorCodes.UnknownError, null);
            }
        }

        public async Task<Tuple<BusinessLogicMessageCodes.ErrorCodes, AssignmentGroupEmployeeDto>> DeleteFromIntAsync(int id)
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
                            return new Tuple<ErrorCodes, AssignmentGroupEmployeeDto>(ErrorCodes.UnknownError, null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<ErrorCodes, AssignmentGroupEmployeeDto>(ErrorCodes.UnknownError, null);
                        }
                    default:
                        {
                            return new Tuple<ErrorCodes, AssignmentGroupEmployeeDto>(ErrorCodes.UnknownError, null);
                        }
                }
            }
            else
            {
                return new Tuple<ErrorCodes, AssignmentGroupEmployeeDto>(ErrorCodes.UnknownError, null);
            }
        }

        public async Task<Tuple<BusinessLogicMessageCodes.ErrorCodes, AssignmentGroupEmployeeDto>> GetByIdFromIntAsync(int id)
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
                            return new Tuple<ErrorCodes, AssignmentGroupEmployeeDto>(ErrorCodes.UnknownError, null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<ErrorCodes, AssignmentGroupEmployeeDto>(ErrorCodes.UnknownError, null);
                        }
                    default:
                        {
                            return new Tuple<ErrorCodes, AssignmentGroupEmployeeDto>(ErrorCodes.UnknownError, null);
                        }
                }
            }
            else
            {
                return new Tuple<ErrorCodes, AssignmentGroupEmployeeDto>(ErrorCodes.UnknownError, null);
            }
        }

        public async Task<Tuple<BusinessLogicMessageCodes.ErrorCodes, AssignmentGroupEmployeeDto>> InsertAsync(AssignmentGroupEmployeeDto insertedDto)
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
                            return new Tuple<ErrorCodes, AssignmentGroupEmployeeDto>(ErrorCodes.UnknownError, null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<ErrorCodes, AssignmentGroupEmployeeDto>(ErrorCodes.UnknownError, null);
                        }
                    default:
                        {
                            return new Tuple<ErrorCodes, AssignmentGroupEmployeeDto>(ErrorCodes.UnknownError, null);
                        }
                }
            }
            else
            {
                return new Tuple<ErrorCodes, AssignmentGroupEmployeeDto>(ErrorCodes.UnknownError, null);
            }
        }

        public async Task<Tuple<BusinessLogicMessageCodes.ErrorCodes, AssignmentGroupEmployeeDto>> UpdateAsync(AssignmentGroupEmployeeDto updatedDto)
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
                            return new Tuple<ErrorCodes, AssignmentGroupEmployeeDto>(ErrorCodes.UnknownError, null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<ErrorCodes, AssignmentGroupEmployeeDto>(ErrorCodes.UnknownError, null);
                        }
                    default:
                        {
                            return new Tuple<ErrorCodes, AssignmentGroupEmployeeDto>(ErrorCodes.UnknownError, null);
                        }
                }
            }
            else
            {
                return new Tuple<ErrorCodes, AssignmentGroupEmployeeDto>(ErrorCodes.UnknownError, null);
            }
        }
    }
}
