using DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Development;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Generic.DiasFacilityManagementSqlServer.Development;
using DiasBusinessLogic.Shared.Error;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using DiasShared.Errors;
using DiasWebApi.Shared.Operations.BusinessLogicOperations.SimpleInjectorOperations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DiasWebApi.Shared.Enums.WebApiApplicationEnums;
using DevelopmentRepositoryInterface = DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement;


namespace DiasWebApi.Repositories.DiasFacilityManagement 
{
    public class TicketStateFlowRoleRepository : DevelopmentRepositoryInterface.ITicketStateFlowRoleDtoRepository
    {
        private ApplicationBusinessLogicEnvironment _applicationBusinessLogicEnvironment;
        private IGenericStandartBusinessRules<TicketStateFlowRoleDto, TicketStateFlowRoleProfile> _genericStandartBusinessRules;
        private bool businessLogicContainerStatus { get; set; }

        public TicketStateFlowRoleRepository(
            ApplicationBusinessLogicEnvironment applicationBusinessLogicEnvironment,
            IGenericStandartBusinessRules<TicketStateFlowRoleDto, TicketStateFlowRoleProfile> genericStandartBusinessRules
            )
        {
            businessLogicContainerStatus = SimpleInjectorContainerOperations.VerifyContainer();

            if (businessLogicContainerStatus)
            {
                _applicationBusinessLogicEnvironment = applicationBusinessLogicEnvironment;
            }

            _genericStandartBusinessRules = genericStandartBusinessRules;
        }

        //TODO : Hata kodları özelleştirilecek
        public async Task<Tuple<Error, IEnumerable<TicketStateFlowRoleDto>>> GetAllAsync()
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
                            return new Tuple<Error, IEnumerable<TicketStateFlowRoleDto>>(Errors.General.GeneralServerError(), null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, IEnumerable<TicketStateFlowRoleDto>>(Errors.General.GeneralServerError(), null);
                        }
                    default:
                        {
                            return new Tuple<Error, IEnumerable<TicketStateFlowRoleDto>>(Errors.General.GeneralServerError(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, IEnumerable<TicketStateFlowRoleDto>>(Errors.General.GeneralServerError(), null);
            }
        }

        //TODO : Hata kodları özelleştirilecek
        public async Task<Tuple<Error, TicketStateFlowRoleDto>> DeleteFromIntAsync(int id)
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
                            return new Tuple<Error, TicketStateFlowRoleDto>(Errors.General.GeneralServerError(), null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, TicketStateFlowRoleDto>(Errors.General.GeneralServerError(), null);
                        }
                    default:
                        {
                            return new Tuple<Error, TicketStateFlowRoleDto>(Errors.General.GeneralServerError(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, TicketStateFlowRoleDto>(Errors.General.GeneralServerError(), null);
            }
        }

        //TODO : Hata kodları özelleştirilecek
        public async Task<Tuple<Error, TicketStateFlowRoleDto>> GetByIdFromIntAsync(int id)
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
                            return new Tuple<Error, TicketStateFlowRoleDto>(Errors.General.GeneralServerError(), null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, TicketStateFlowRoleDto>(Errors.General.GeneralServerError(), null);
                        }
                    default:
                        {
                            return new Tuple<Error, TicketStateFlowRoleDto>(Errors.General.GeneralServerError(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, TicketStateFlowRoleDto>(Errors.General.GeneralServerError(), null);
            }
        }

        //TODO : Hata kodları özelleştirilecek
        public async Task<Tuple<Error, TicketStateFlowRoleDto>> InsertAsync(TicketStateFlowRoleDto insertedDto)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            return await _genericStandartBusinessRules.InsertV2(insertedDto);
                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            return new Tuple<Error, TicketStateFlowRoleDto>(Errors.General.GeneralServerError(), null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, TicketStateFlowRoleDto>(Errors.General.GeneralServerError(), null);
                        }
                    default:
                        {
                            return new Tuple<Error, TicketStateFlowRoleDto>(Errors.General.GeneralServerError(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, TicketStateFlowRoleDto>(Errors.General.GeneralServerError(), null);
            }
        }

        //TODO : Hata kodları özelleştirilecek
        public async Task<Tuple<Error, TicketStateFlowRoleDto>> UpdateAsync(TicketStateFlowRoleDto updatedDto)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            List<string> uniqueColumns = new List<string>() { "Id" };
                            List<object> uniqueValues = new List<object>() { updatedDto.Id };

                            return await _genericStandartBusinessRules.UpdateV2(updatedDto, uniqueColumns, uniqueValues);
                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            return new Tuple<Error, TicketStateFlowRoleDto>(Errors.General.GeneralServerError(), null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, TicketStateFlowRoleDto>(Errors.General.GeneralServerError(), null);
                        }
                    default:
                        {
                            return new Tuple<Error, TicketStateFlowRoleDto>(Errors.General.GeneralServerError(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, TicketStateFlowRoleDto>(Errors.General.GeneralServerError(), null);
            }
        }

    }
}
