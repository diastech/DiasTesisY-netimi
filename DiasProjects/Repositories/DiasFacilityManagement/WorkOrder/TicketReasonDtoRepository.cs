using DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Development;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.BaseBusinessRules.SqlServer.Standart;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Generic.DiasFacilityManagementSqlServer.Development;
using DiasDataAccessLayer.Enums;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement;
using DiasWebApi.Shared.Operations.BusinessLogicOperations.SimpleInjectorOperations;
using System;
using System.Threading.Tasks;
using static DiasWebApi.Shared.Enums.WebApiApplicationEnums;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DevelopmentRepositoryInterface = DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement;
using System.Collections.Generic;
using TestDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard;
using TestAutomapper = DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Test;
using GenericInterfaceTest = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Generic.DiasFacilityManagementSqlServer.Test;

namespace DiasWebApi.Repositories.DiasFacilityManagement
{
    public class TicketReasonDtoRepository : DevelopmentRepositoryInterface.ITicketReasonDtoRepository
    {
        private IBaseTicketReasonBusinessRules _baseTicketReasonBusinessRules;
        private ApplicationBusinessLogicEnvironment _applicationBusinessLogicEnvironment;
        private IGenericStandartBusinessRules<TicketReasonDto, TicketReasonProfile> _genericStandartBusinessRules;
        private GenericInterfaceTest.IGenericStandartBusinessRules<TestDto.TicketReasonDto, TestAutomapper.TicketReasonProfile> _genericStandartBusinessRulesTest;
        private bool businessLogicContainerStatus { get; set; }
        public TicketReasonDtoRepository(
            IBaseTicketReasonBusinessRules baseTicketReasonBusinessRules, 
            ApplicationBusinessLogicEnvironment applicationBusinessLogicEnvironment,
            IGenericStandartBusinessRules<TicketReasonDto, TicketReasonProfile> genericStandartBusinessRules,
            GenericInterfaceTest.IGenericStandartBusinessRules<TestDto.TicketReasonDto, TestAutomapper.TicketReasonProfile> genericStandartBusinessRulesTest
            )
        {
            businessLogicContainerStatus = SimpleInjectorContainerOperations.VerifyContainer();

            if (businessLogicContainerStatus)
            {
                _baseTicketReasonBusinessRules = baseTicketReasonBusinessRules;
                _applicationBusinessLogicEnvironment = applicationBusinessLogicEnvironment;
            }
            // TODO: İnject edilmesi gerekiyor
            _genericStandartBusinessRules = genericStandartBusinessRules;
            _genericStandartBusinessRulesTest = genericStandartBusinessRulesTest;
        }

        public async Task<Tuple<BusinessLogicMessageCodes.ErrorCodes, IEnumerable<TicketReasonDto>>> GetAllAsync()
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
                            return new Tuple<ErrorCodes, IEnumerable<TicketReasonDto>>(ErrorCodes.UnknownError, null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<ErrorCodes, IEnumerable<TicketReasonDto>>(ErrorCodes.UnknownError, null);
                        }
                    default:
                        {
                            return new Tuple<ErrorCodes, IEnumerable<TicketReasonDto>>(ErrorCodes.UnknownError, null);
                        }
                }
            }
            else
            {
                return new Tuple<ErrorCodes, IEnumerable<TicketReasonDto>>(ErrorCodes.UnknownError, null);
            }
        }

        public async Task<Tuple<BusinessLogicMessageCodes.ErrorCodes, TicketReasonDto>> DeleteFromIntAsync(int id)
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
                            return new Tuple<ErrorCodes, TicketReasonDto>(ErrorCodes.UnknownError, null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<ErrorCodes, TicketReasonDto>(ErrorCodes.UnknownError, null);
                        }
                    default:
                        {
                            return new Tuple<ErrorCodes, TicketReasonDto>(ErrorCodes.UnknownError, null);
                        }
                }
            }
            else
            {
                return new Tuple<ErrorCodes, TicketReasonDto>(ErrorCodes.UnknownError, null);
            }
        }

        public async Task<Tuple<BusinessLogicMessageCodes.ErrorCodes, TicketReasonDto>> GetByIdFromIntAsync(int id)
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
                            return new Tuple<ErrorCodes, TicketReasonDto>(ErrorCodes.UnknownError, null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<ErrorCodes, TicketReasonDto>(ErrorCodes.UnknownError, null);
                        }
                    default:
                        {
                            return new Tuple<ErrorCodes, TicketReasonDto>(ErrorCodes.UnknownError, null);
                        }
                }
            }
            else
            {
                return new Tuple<ErrorCodes, TicketReasonDto>(ErrorCodes.UnknownError, null);
            }
        }

        public async Task<Tuple<BusinessLogicMessageCodes.ErrorCodes, TicketReasonDto>> InsertAsync(TicketReasonDto insertedDto)
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
                            return new Tuple<ErrorCodes, TicketReasonDto>(ErrorCodes.UnknownError, null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<ErrorCodes, TicketReasonDto>(ErrorCodes.UnknownError, null);
                        }
                    default:
                        {
                            return new Tuple<ErrorCodes, TicketReasonDto>(ErrorCodes.UnknownError, null);
                        }
                }
            }
            else
            {
                return new Tuple<ErrorCodes, TicketReasonDto>(ErrorCodes.UnknownError, null);
            }
        }

        public async Task<Tuple<BusinessLogicMessageCodes.ErrorCodes, TicketReasonDto>> UpdateAsync(TicketReasonDto updatedDto)
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
                            return new Tuple<ErrorCodes, TicketReasonDto>(ErrorCodes.UnknownError, null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<ErrorCodes, TicketReasonDto>(ErrorCodes.UnknownError, null);
                        }
                    default:
                        {
                            return new Tuple<ErrorCodes, TicketReasonDto>(ErrorCodes.UnknownError, null);
                        }
                }
            }
            else
            {
                return new Tuple<ErrorCodes, TicketReasonDto>(ErrorCodes.UnknownError, null);
            }
        }
    }
}
