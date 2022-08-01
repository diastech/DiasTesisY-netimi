using DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Development;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.BaseBusinessRules.SqlServer.Standart;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Generic.DiasFacilityManagementSqlServer.Development;
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
using DiasShared.Services.Communication.BusinessLogicMessage;
using Newtonsoft.Json;
using DiasShared.Errors;
using DiasBusinessLogic.Shared.Error;

namespace DiasWebApi.Repositories.DiasFacilityManagement
{
    public class TicketStateDtoRepository : DevelopmentRepositoryInterface.ITicketStateDtoRepository
    {
        private IBaseTicketStateBusinessRules _baseTicketStateBusinessRules;
        private ApplicationBusinessLogicEnvironment _applicationBusinessLogicEnvironment;
        private IGenericStandartBusinessRules<TicketStateDto, TicketStateProfile> _genericStandartBusinessRules;        
        private GenericInterfaceTest.IGenericStandartBusinessRules<TestDto.TicketStateDto, TestAutomapper.TicketStateProfile> _genericStandartBusinessRulesTest;
        private bool businessLogicContainerStatus { get; set; }
        public TicketStateDtoRepository(
            IBaseTicketStateBusinessRules baseTicketStateBusinessRules, 
            ApplicationBusinessLogicEnvironment applicationBusinessLogicEnvironment,
            IGenericStandartBusinessRules<TicketStateDto, TicketStateProfile> genericStandartBusinessRules,
            GenericInterfaceTest.IGenericStandartBusinessRules<TestDto.TicketStateDto, TestAutomapper.TicketStateProfile> genericStandartBusinessRulesTest
            )
        {
            businessLogicContainerStatus = SimpleInjectorContainerOperations.VerifyContainer();

            if (businessLogicContainerStatus)
            {
                _baseTicketStateBusinessRules = baseTicketStateBusinessRules;
                _applicationBusinessLogicEnvironment = applicationBusinessLogicEnvironment;
            }

            _genericStandartBusinessRules = genericStandartBusinessRules;
            _genericStandartBusinessRulesTest = genericStandartBusinessRulesTest;
        }


        #region WebClient

            #region Development

                //TODO : Hata kodları özelleştirilecek
                public async Task<Tuple<Error, IEnumerable<TicketStateDto>>> GetAllAsync()
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
                                        return new Tuple<Error, IEnumerable<TicketStateDto>>(Errors.General.GeneralServerError(), null);
                                    }
                                case ApplicationBusinessLogicEnvironment.Live:
                                    {
                                        return new Tuple<Error, IEnumerable<TicketStateDto>>(Errors.General.GeneralServerError(), null);
                                    }
                                default:
                                    {
                                        return new Tuple<Error, IEnumerable<TicketStateDto>>(Errors.General.GeneralServerError(), null);
                                    }
                            }
                        }
                        else
                        {
                            return new Tuple<Error, IEnumerable<TicketStateDto>>(Errors.General.GeneralServerError(), null);
                        }
                    }

                //TODO : Hata kodları özelleştirilecek
                public async Task<Tuple<Error, TicketStateDto>> DeleteAsync(BusinessLogicRequest request)
                    {
                        if (businessLogicContainerStatus)
                        {
                            switch (_applicationBusinessLogicEnvironment)
                            {
                                case ApplicationBusinessLogicEnvironment.Development:
                                    {
                                        if ((request == null) || (request.RequestDtosTypes == null) || (request.RequestDtosJsons == null) ||
                                                      (request.RequestDtosTypes.Count < 1) || (request.RequestDtosJsons.Count < 1) ||
                                                            (!Type.Equals(request.RequestDtosTypes[0], typeof(TicketStateDto))))
                                        {
                                            return new Tuple<Error, TicketStateDto>(Errors.General.GeneralServerError(), null);
                                        }
                                        TicketStateDto castedDto = JsonConvert.DeserializeObject<TicketStateDto>(request.RequestDtosJsons[0]);

                                        if (castedDto == null)
                                        {
                                            return new Tuple<Error, TicketStateDto>(Errors.General.GeneralServerError(), null);
                                        }


                                        return await _genericStandartBusinessRules.DeleteFromIntV2(castedDto.Id);
                                    }
                                case ApplicationBusinessLogicEnvironment.Test:
                                    {
                                        return new Tuple<Error, TicketStateDto>(Errors.General.GeneralServerError(), null);
                                    }
                                case ApplicationBusinessLogicEnvironment.Live:
                                    {
                                        return new Tuple<Error, TicketStateDto>(Errors.General.GeneralServerError(), null);
                                    }
                                default:
                                    {
                                        return new Tuple<Error, TicketStateDto>(Errors.General.GeneralServerError(), null);
                                    }
                            }
                        }
                        else
                        {
                            return new Tuple<Error, TicketStateDto>(Errors.General.GeneralServerError(), null);
                        }
                    }

                //TODO : Hata kodları özelleştirilecek
                public async Task<Tuple<Error, TicketStateDto>> GetByIdFromIntAsync(int id)
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
                                        return new Tuple<Error, TicketStateDto>(Errors.General.GeneralServerError(), null);
                                    }
                                case ApplicationBusinessLogicEnvironment.Live:
                                    {
                                        return new Tuple<Error, TicketStateDto>(Errors.General.GeneralServerError(), null);
                                    }
                                default:
                                    {
                                        return new Tuple<Error, TicketStateDto>(Errors.General.GeneralServerError(), null);
                                    }
                            }
                        }
                        else
                        {
                            return new Tuple<Error, TicketStateDto>(Errors.General.GeneralServerError(), null);
                        }
                    }

                //TODO : Hata kodları özelleştirilecek
                public async Task<Tuple<Error, TicketStateDto>> InsertAsync(BusinessLogicRequest request)
                    {
                        if (businessLogicContainerStatus)
                        {
                            switch (_applicationBusinessLogicEnvironment)
                            {
                                case ApplicationBusinessLogicEnvironment.Development:
                                    {
                                        if ((request == null) || (request.RequestDtosTypes == null) || (request.RequestDtosJsons == null) ||
                                                      (request.RequestDtosTypes.Count < 1) || (request.RequestDtosJsons.Count < 1) ||
                                                            (!Type.Equals(request.RequestDtosTypes[0], typeof(TicketStateDto))))
                                        {
                                            return new Tuple<Error, TicketStateDto>(Errors.General.GeneralServerError(), null);
                                        }
                                        TicketStateDto castedDto = JsonConvert.DeserializeObject<TicketStateDto>(request.RequestDtosJsons[0]);

                                        if (castedDto == null)
                                        {
                                            return new Tuple<Error, TicketStateDto>(Errors.General.GeneralServerError(), null);
                                        }

                                        List<string> uniqueColumns = new List<string>()
                                        {
                                            "StateDescription"                                
                                        };
                            
                                        List<object> uniqueValues = new List<object>()
                                        {
                                            castedDto.Name,
                                        };

                                        return await _genericStandartBusinessRules.InsertV2(castedDto, uniqueColumns, uniqueValues);
                                    }
                                case ApplicationBusinessLogicEnvironment.Test:
                                    {
                                        return new Tuple<Error, TicketStateDto>(Errors.General.GeneralServerError(), null);
                                    }
                                case ApplicationBusinessLogicEnvironment.Live:
                                    {
                                        return new Tuple<Error, TicketStateDto>(Errors.General.GeneralServerError(), null);
                                    }
                                default:
                                    {
                                        return new Tuple<Error, TicketStateDto>(Errors.General.GeneralServerError(), null);
                                    }
                            }
                        }
                        else
                        {
                            return new Tuple<Error, TicketStateDto>(Errors.General.GeneralServerError(), null);
                        }
                    }

                //TODO : Hata kodları özelleştirilecek
                public async Task<Tuple<Error, TicketStateDto>> UpdateAsync(BusinessLogicRequest request)
                    {
                        if (businessLogicContainerStatus)
                        {
                            switch (_applicationBusinessLogicEnvironment)
                            {
                                case ApplicationBusinessLogicEnvironment.Development:
                                    {
                                        if ((request == null) || (request.RequestDtosTypes == null) || (request.RequestDtosJsons == null) ||
                                                      (request.RequestDtosTypes.Count < 1) || (request.RequestDtosJsons.Count < 1) ||
                                                            (!Type.Equals(request.RequestDtosTypes[0], typeof(TicketStateDto))))
                                        {
                                            return new Tuple<Error, TicketStateDto>(Errors.General.GeneralServerError(), null);
                                        }
                                        TicketStateDto castedDto = JsonConvert.DeserializeObject<TicketStateDto>(request.RequestDtosJsons[0]);

                                        if (castedDto == null)
                                        {
                                            return new Tuple<Error, TicketStateDto>(Errors.General.GeneralServerError(), null);
                                        }

                                        List<string> uniqueColumns = new List<string>() { "Id" };
                                        List<object> uniqueValues = new List<object>() { castedDto.Id.ToString() };

                                        return await _genericStandartBusinessRules.UpdateV2(castedDto, uniqueColumns, uniqueValues);
                                    }
                                case ApplicationBusinessLogicEnvironment.Test:
                                    {
                                        return new Tuple<Error, TicketStateDto>(Errors.General.GeneralServerError(), null);
                                    }
                                case ApplicationBusinessLogicEnvironment.Live:
                                    {
                                        return new Tuple<Error, TicketStateDto>(Errors.General.GeneralServerError(), null);
                                    }
                                default:
                                    {
                                        return new Tuple<Error, TicketStateDto>(Errors.General.GeneralServerError(), null);
                                    }
                            }
                        }
                        else
                        {
                            return new Tuple<Error, TicketStateDto>(Errors.General.GeneralServerError(), null);
                        }
                    }

                #endregion

            #region Test
            public async Task<Tuple<ErrorCodes, IEnumerable<TestDto.TicketStateDto>>> GetAllTestAsync()
            {
                if (businessLogicContainerStatus)
                {
                    switch (_applicationBusinessLogicEnvironment)
                    {
                        case ApplicationBusinessLogicEnvironment.Development:
                            {
                                return new Tuple<ErrorCodes, IEnumerable<TestDto.TicketStateDto>>(ErrorCodes.UnknownError, null);

                            }
                        case ApplicationBusinessLogicEnvironment.Test:
                            {
                                return await _genericStandartBusinessRulesTest.GetAll();
                            }
                        case ApplicationBusinessLogicEnvironment.Live:
                            {
                                return new Tuple<ErrorCodes, IEnumerable<TestDto.TicketStateDto>>(ErrorCodes.UnknownError, null);
                            }
                        default:
                            {
                                return new Tuple<ErrorCodes, IEnumerable<TestDto.TicketStateDto>>(ErrorCodes.UnknownError, null);
                            }
                    }
                }
                else
                {
                    return new Tuple<ErrorCodes, IEnumerable<TestDto.TicketStateDto>>(ErrorCodes.UnknownError, null);
                }
            }

            public async Task<Tuple<ErrorCodes, TestDto.TicketStateDto>> InsertTestAsync(BusinessLogicRequest request)
            {
                if (businessLogicContainerStatus)
                {
                    switch (_applicationBusinessLogicEnvironment)
                    {
                        case ApplicationBusinessLogicEnvironment.Development:
                            {
                                return new Tuple<ErrorCodes, TestDto.TicketStateDto>(ErrorCodes.UnknownError, null);
                            }
                        case ApplicationBusinessLogicEnvironment.Test:
                            {
                                if ((request == null) || (request.RequestDtosTypes == null) || (request.RequestDtosJsons == null) ||
                                              (request.RequestDtosTypes.Count < 1) || (request.RequestDtosJsons.Count < 1) ||
                                                    (!Type.Equals(request.RequestDtosTypes[0], typeof(TestDto.TicketStateDto))))
                                {
                                    return new Tuple<ErrorCodes, TestDto.TicketStateDto>(ErrorCodes.UnknownError, null);
                                }
                                TestDto.TicketStateDto castedDto = JsonConvert.DeserializeObject<TestDto.TicketStateDto>(request.RequestDtosJsons[0]);

                                if (castedDto == null)
                                {
                                    return new Tuple<ErrorCodes, TestDto.TicketStateDto>(ErrorCodes.UnknownError, null);
                                }

                                List<string> uniqueColumns = new List<string>()
                                {
                                    "StateDescription"
                                };

                                List<object> uniqueValues = new List<object>()
                                {
                                    castedDto.StateDescription,
                                };

                                return await _genericStandartBusinessRulesTest.Insert(castedDto, uniqueColumns, uniqueValues);
                            
                            }
                        case ApplicationBusinessLogicEnvironment.Live:
                            {
                                return new Tuple<ErrorCodes, TestDto.TicketStateDto>(ErrorCodes.UnknownError, null);
                            }
                        default:
                            {
                                return new Tuple<ErrorCodes, TestDto.TicketStateDto>(ErrorCodes.UnknownError, null);
                            }
                    }
                }
                else
                {
                    return new Tuple<ErrorCodes, TestDto.TicketStateDto>(ErrorCodes.UnknownError, null);
                }
            }

            public async Task<Tuple<ErrorCodes, TestDto.TicketStateDto>> UpdateTestAsync(BusinessLogicRequest request)
            {
                if (businessLogicContainerStatus)
                {
                    switch (_applicationBusinessLogicEnvironment)
                    {
                        case ApplicationBusinessLogicEnvironment.Development:
                            {
                                return new Tuple<ErrorCodes, TestDto.TicketStateDto>(ErrorCodes.UnknownError, null);
                            }
                        case ApplicationBusinessLogicEnvironment.Test:
                            {
                                if ((request == null) || (request.RequestDtosTypes == null) || (request.RequestDtosJsons == null) ||
                                              (request.RequestDtosTypes.Count < 1) || (request.RequestDtosJsons.Count < 1) ||
                                                    (!Type.Equals(request.RequestDtosTypes[0], typeof(TestDto.TicketStateDto))))
                                {
                                    return new Tuple<ErrorCodes, TestDto.TicketStateDto>(ErrorCodes.UnknownError, null);
                                }
                                TestDto.TicketStateDto castedDto = JsonConvert.DeserializeObject<TestDto.TicketStateDto>(request.RequestDtosJsons[0]);

                                if (castedDto == null)
                                {
                                    return new Tuple<ErrorCodes, TestDto.TicketStateDto>(ErrorCodes.UnknownError, null);
                                }

                                List<string> uniqueColumns = new List<string>()
                                {
                                    "Id"
                                };

                                List<object> uniqueValues = new List<object>()
                                {
                                    castedDto.Id.ToString(),
                                };

                                return await _genericStandartBusinessRulesTest.Update(castedDto, uniqueColumns, uniqueValues);

                            }
                        case ApplicationBusinessLogicEnvironment.Live:
                            {
                                return new Tuple<ErrorCodes, TestDto.TicketStateDto>(ErrorCodes.UnknownError, null);
                            }
                        default:
                            {
                                return new Tuple<ErrorCodes, TestDto.TicketStateDto>(ErrorCodes.UnknownError, null);
                            }
                    }
                }
                else
                {
                    return new Tuple<ErrorCodes, TestDto.TicketStateDto>(ErrorCodes.UnknownError, null);
                }
            }

            public async Task<Tuple<ErrorCodes, TestDto.TicketStateDto>> DeleteTestAsync(BusinessLogicRequest request)
            {
                if (businessLogicContainerStatus)
                {
                    switch (_applicationBusinessLogicEnvironment)
                    {
                        case ApplicationBusinessLogicEnvironment.Development:
                            {
                                return new Tuple<ErrorCodes, TestDto.TicketStateDto>(ErrorCodes.UnknownError, null);
                            }
                        case ApplicationBusinessLogicEnvironment.Test:
                            {
                                if ((request == null) || (request.RequestDtosTypes == null) || (request.RequestDtosJsons == null) ||
                                              (request.RequestDtosTypes.Count < 1) || (request.RequestDtosJsons.Count < 1) ||
                                                    (!Type.Equals(request.RequestDtosTypes[0], typeof(TestDto.TicketStateDto))))
                                {
                                    return new Tuple<ErrorCodes, TestDto.TicketStateDto>(ErrorCodes.UnknownError, null);
                                }
                                TestDto.TicketStateDto castedDto = JsonConvert.DeserializeObject<TestDto.TicketStateDto>(request.RequestDtosJsons[0]);

                                if (castedDto == null)
                                {
                                    return new Tuple<ErrorCodes, TestDto.TicketStateDto>(ErrorCodes.UnknownError, null);
                                }

                           

                                return await _genericStandartBusinessRulesTest.DeleteFromInt(castedDto.Id);

                            }
                        case ApplicationBusinessLogicEnvironment.Live:
                            {
                                return new Tuple<ErrorCodes, TestDto.TicketStateDto>(ErrorCodes.UnknownError, null);
                            }
                        default:
                            {
                                return new Tuple<ErrorCodes, TestDto.TicketStateDto>(ErrorCodes.UnknownError, null);
                            }
                    }
                }
                else
                {
                    return new Tuple<ErrorCodes, TestDto.TicketStateDto>(ErrorCodes.UnknownError, null);
                }
            }
            #endregion

        #endregion


    }
}
