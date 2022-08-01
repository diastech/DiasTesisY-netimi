using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Generic.DiasFacilityManagementSqlServer.Development;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DiasWebApi.Shared.Enums.WebApiApplicationEnums;
using DevelopmentRepositoryInterface = DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement;
using GenericInterfaceTest = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Generic.DiasFacilityManagementSqlServer.Test;
using TestDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard;
using TestAutomapper = DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Test;
using DiasWebApi.Shared.Operations.BusinessLogicOperations.SimpleInjectorOperations;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DiasShared.Services.Communication.BusinessLogicMessage;
using Newtonsoft.Json;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.BaseBusinessRules.SqlServer.Standart;
using DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Development;
using DiasShared.Errors;
using DiasBusinessLogic.Shared.Error;

namespace DiasWebApi.Repositories.DiasFacilityManagement
{
    public class BasicTicketStateDtoRepository : DevelopmentRepositoryInterface.IBasicTicketStateDtoRepository
    {
        private IBaseBasicTicketStateBusinessRules _baseBasicTicketStateBusinessRules;
        private ApplicationBusinessLogicEnvironment _applicationBusinessLogicEnvironment;
        private IGenericStandartBusinessRules<BasicTicketStateDto, BasicTicketStateProfile> _genericStandartBusinessRules;
        private GenericInterfaceTest.IGenericStandartBusinessRules<TestDto.BasicTicketStateDto, TestAutomapper.BasicTicketStateProfile> _genericStandartBusinessRulesTest;

        private bool businessLogicContainerStatus { get; set; }

        public BasicTicketStateDtoRepository(
            IBaseBasicTicketStateBusinessRules baseBasicTicketStateBusinessRules,
            ApplicationBusinessLogicEnvironment applicationBusinessLogicEnvironment,
            IGenericStandartBusinessRules<BasicTicketStateDto, BasicTicketStateProfile> genericStandartBusinessRules,
            GenericInterfaceTest.IGenericStandartBusinessRules<TestDto.BasicTicketStateDto, TestAutomapper.BasicTicketStateProfile> genericStandartBusinessRulesTest
            )
        {
            businessLogicContainerStatus = SimpleInjectorContainerOperations.VerifyContainer();

            if (businessLogicContainerStatus)
            {
                _baseBasicTicketStateBusinessRules = baseBasicTicketStateBusinessRules;
                _applicationBusinessLogicEnvironment = applicationBusinessLogicEnvironment;
            }

            _genericStandartBusinessRules = genericStandartBusinessRules;
            _genericStandartBusinessRulesTest = genericStandartBusinessRulesTest;
        }
        #region WebClient

            #region Development

                //TODO : Hata kodları özelleştirilecek
                public async Task<Tuple<Error, IEnumerable<BasicTicketStateDto>>> GetAllAsync()
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
                                        return new Tuple<Error, IEnumerable<BasicTicketStateDto>>(Errors.General.GeneralServerError(), null);
                                    }
                                case ApplicationBusinessLogicEnvironment.Live:
                                    {
                                        return new Tuple<Error, IEnumerable<BasicTicketStateDto>>(Errors.General.GeneralServerError(), null);
                                    }
                                default:
                                    {
                                        return new Tuple<Error, IEnumerable<BasicTicketStateDto>>(Errors.General.GeneralServerError(), null);
                                    }
                            }
                        }
                        else
                        {
                            return new Tuple<Error, IEnumerable<BasicTicketStateDto>>(Errors.General.GeneralServerError(), null);
                        }
                    }

                //TODO : Hata kodları özelleştirilecek
                public async Task<Tuple<Error, BasicTicketStateDto>> DeleteAsync(BusinessLogicRequest request)
                    {
                        if (businessLogicContainerStatus)
                        {
                            switch (_applicationBusinessLogicEnvironment)
                            {
                                case ApplicationBusinessLogicEnvironment.Development:
                                    {
                                        if ((request == null) || (request.RequestDtosTypes == null) || (request.RequestDtosJsons == null) ||
                                                      (request.RequestDtosTypes.Count < 1) || (request.RequestDtosJsons.Count < 1) ||
                                                            (!Type.Equals(request.RequestDtosTypes[0], typeof(BasicTicketStateDto))))
                                        {
                                            return new Tuple<Error, BasicTicketStateDto>(Errors.General.GeneralServerError(), null);
                                        }
                                        BasicTicketStateDto castedDto = JsonConvert.DeserializeObject<BasicTicketStateDto>(request.RequestDtosJsons[0]);

                                        if (castedDto == null)
                                        {
                                            return new Tuple<Error, BasicTicketStateDto>(Errors.General.GeneralServerError(), null);
                                        }


                                        return await _genericStandartBusinessRules.DeleteFromIntV2(castedDto.Id);
                                    }
                                case ApplicationBusinessLogicEnvironment.Test:
                                    {
                                        return new Tuple<Error, BasicTicketStateDto>(Errors.General.GeneralServerError(), null);
                                    }
                                case ApplicationBusinessLogicEnvironment.Live:
                                    {
                                        return new Tuple<Error, BasicTicketStateDto>(Errors.General.GeneralServerError(), null);
                                    }
                                default:
                                    {
                                        return new Tuple<Error, BasicTicketStateDto>(Errors.General.GeneralServerError(), null);
                                    }
                            }
                        }
                        else
                        {
                            return new Tuple<Error, BasicTicketStateDto>(Errors.General.GeneralServerError(), null);
                        }
                    }

                //TODO : Hata kodları özelleştirilecek
                public async Task<Tuple<Error, BasicTicketStateDto>> GetByIdFromIntAsync(int id)
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
                                        return new Tuple<Error, BasicTicketStateDto>(Errors.General.GeneralServerError(), null);
                                    }
                                case ApplicationBusinessLogicEnvironment.Live:
                                    {
                                        return new Tuple<Error, BasicTicketStateDto>(Errors.General.GeneralServerError(), null);
                                    }
                                default:
                                    {
                                        return new Tuple<Error, BasicTicketStateDto>(Errors.General.GeneralServerError(), null);
                                    }
                            }
                        }
                        else
                        {
                            return new Tuple<Error, BasicTicketStateDto>(Errors.General.GeneralServerError(), null);
                        }
                    }

                //TODO : Hata kodları özelleştirilecek
                public async Task<Tuple<Error, BasicTicketStateDto>> InsertAsync(BusinessLogicRequest request)
                    {
                        if (businessLogicContainerStatus)
                        {
                            switch (_applicationBusinessLogicEnvironment)
                            {
                                case ApplicationBusinessLogicEnvironment.Development:
                                    {
                                        if ((request == null) || (request.RequestDtosTypes == null) || (request.RequestDtosJsons == null) ||
                                                      (request.RequestDtosTypes.Count < 1) || (request.RequestDtosJsons.Count < 1) ||
                                                            (!Type.Equals(request.RequestDtosTypes[0], typeof(BasicTicketStateDto))))
                                        {
                                            return new Tuple<Error, BasicTicketStateDto>(Errors.General.GeneralServerError(), null);
                                        }
                                        BasicTicketStateDto castedDto = JsonConvert.DeserializeObject<BasicTicketStateDto>(request.RequestDtosJsons[0]);

                                        if (castedDto == null)
                                        {
                                            return new Tuple<Error, BasicTicketStateDto>(Errors.General.GeneralServerError(), null);
                                        }

                                        List<string> uniqueColumns = new List<string>()
                                        {
                                            "BasicStateDescription"
                                        };

                                        List<object> uniqueValues = new List<object>()
                                        {
                                            castedDto.Name,
                                        };

                                        return await _genericStandartBusinessRules.InsertV2(castedDto, uniqueColumns, uniqueValues);
                                    }
                                case ApplicationBusinessLogicEnvironment.Test:
                                    {
                                        return new Tuple<Error, BasicTicketStateDto>(Errors.General.GeneralServerError(), null);
                                    }
                                case ApplicationBusinessLogicEnvironment.Live:
                                    {
                                        return new Tuple<Error, BasicTicketStateDto>(Errors.General.GeneralServerError(), null);
                                    }
                                default:
                                    {
                                        return new Tuple<Error, BasicTicketStateDto>(Errors.General.GeneralServerError(), null);
                                    }
                            }
                        }
                        else
                        {
                            return new Tuple<Error, BasicTicketStateDto>(Errors.General.GeneralServerError(), null);
                        }
                    }

                //TODO : Hata kodları özelleştirilecek
                public async Task<Tuple<Error, BasicTicketStateDto>> UpdateAsync(BusinessLogicRequest request)
                    {
                        if (businessLogicContainerStatus)
                        {
                            switch (_applicationBusinessLogicEnvironment)
                            {
                                case ApplicationBusinessLogicEnvironment.Development:
                                    {
                                        if ((request == null) || (request.RequestDtosTypes == null) || (request.RequestDtosJsons == null) ||
                                                      (request.RequestDtosTypes.Count < 1) || (request.RequestDtosJsons.Count < 1) ||
                                                            (!Type.Equals(request.RequestDtosTypes[0], typeof(BasicTicketStateDto))))
                                        {
                                            return new Tuple<Error, BasicTicketStateDto>(Errors.General.GeneralServerError(), null);
                                        }
                                        BasicTicketStateDto castedDto = JsonConvert.DeserializeObject<BasicTicketStateDto>(request.RequestDtosJsons[0]);

                                        if (castedDto == null)
                                        {
                                            return new Tuple<Error, BasicTicketStateDto>(Errors.General.GeneralServerError(), null);
                                        }

                                        List<string> uniqueColumns = new List<string>() { "Id" };
                                        List<object> uniqueValues = new List<object>() { castedDto.Id.ToString() };

                                        return await _genericStandartBusinessRules.UpdateV2(castedDto, uniqueColumns, uniqueValues);
                                    }
                                case ApplicationBusinessLogicEnvironment.Test:
                                    {
                                        return new Tuple<Error, BasicTicketStateDto>(Errors.General.GeneralServerError(), null);
                                    }
                                case ApplicationBusinessLogicEnvironment.Live:
                                    {
                                        return new Tuple<Error, BasicTicketStateDto>(Errors.General.GeneralServerError(), null);
                                    }
                                default:
                                    {
                                        return new Tuple<Error, BasicTicketStateDto>(Errors.General.GeneralServerError(), null);
                                    }
                            }
                        }
                        else
                        {
                            return new Tuple<Error, BasicTicketStateDto>(Errors.General.GeneralServerError(), null);
                        }
                    }

                #endregion

            #region Test
            public async Task<Tuple<ErrorCodes, IEnumerable<TestDto.BasicTicketStateDto>>> GetAllTestAsync()
            {
                if (businessLogicContainerStatus)
                {
                    switch (_applicationBusinessLogicEnvironment)
                    {
                        case ApplicationBusinessLogicEnvironment.Development:
                            {
                                return new Tuple<ErrorCodes, IEnumerable<TestDto.BasicTicketStateDto>>(ErrorCodes.UnknownError, null);

                            }
                        case ApplicationBusinessLogicEnvironment.Test:
                            {
                                return await _genericStandartBusinessRulesTest.GetAll();
                            }
                        case ApplicationBusinessLogicEnvironment.Live:
                            {
                                return new Tuple<ErrorCodes, IEnumerable<TestDto.BasicTicketStateDto>>(ErrorCodes.UnknownError, null);
                            }
                        default:
                            {
                                return new Tuple<ErrorCodes, IEnumerable<TestDto.BasicTicketStateDto>>(ErrorCodes.UnknownError, null);
                            }
                    }
                }
                else
                {
                    return new Tuple<ErrorCodes, IEnumerable<TestDto.BasicTicketStateDto>>(ErrorCodes.UnknownError, null);
                }
            }

            public async Task<Tuple<ErrorCodes, TestDto.BasicTicketStateDto>> InsertTestAsync(BusinessLogicRequest request)
            {
                if (businessLogicContainerStatus)
                {
                    switch (_applicationBusinessLogicEnvironment)
                    {
                        case ApplicationBusinessLogicEnvironment.Development:
                            {
                                return new Tuple<ErrorCodes, TestDto.BasicTicketStateDto>(ErrorCodes.UnknownError, null);
                            }
                        case ApplicationBusinessLogicEnvironment.Test:
                            {
                                if ((request == null) || (request.RequestDtosTypes == null) || (request.RequestDtosJsons == null) ||
                                              (request.RequestDtosTypes.Count < 1) || (request.RequestDtosJsons.Count < 1) ||
                                                    (!Type.Equals(request.RequestDtosTypes[0], typeof(TestDto.BasicTicketStateDto))))
                                {
                                    return new Tuple<ErrorCodes, TestDto.BasicTicketStateDto>(ErrorCodes.UnknownError, null);
                                }
                                TestDto.BasicTicketStateDto castedDto = JsonConvert.DeserializeObject<TestDto.BasicTicketStateDto>(request.RequestDtosJsons[0]);

                                if (castedDto == null)
                                {
                                    return new Tuple<ErrorCodes, TestDto.BasicTicketStateDto>(ErrorCodes.UnknownError, null);
                                }

                                List<string> uniqueColumns = new List<string>()
                                {
                                    "BasicStateDescription"
                                };

                                List<object> uniqueValues = new List<object>()
                                {
                                    castedDto.BasicStateDescription,
                                };

                                return await _genericStandartBusinessRulesTest.Insert(castedDto, uniqueColumns, uniqueValues);

                            }
                        case ApplicationBusinessLogicEnvironment.Live:
                            {
                                return new Tuple<ErrorCodes, TestDto.BasicTicketStateDto>(ErrorCodes.UnknownError, null);
                            }
                        default:
                            {
                                return new Tuple<ErrorCodes, TestDto.BasicTicketStateDto>(ErrorCodes.UnknownError, null);
                            }
                    }
                }
                else
                {
                    return new Tuple<ErrorCodes, TestDto.BasicTicketStateDto>(ErrorCodes.UnknownError, null);
                }
            }

            public async Task<Tuple<ErrorCodes, TestDto.BasicTicketStateDto>> UpdateTestAsync(BusinessLogicRequest request)
            {
                if (businessLogicContainerStatus)
                {
                    switch (_applicationBusinessLogicEnvironment)
                    {
                        case ApplicationBusinessLogicEnvironment.Development:
                            {
                                return new Tuple<ErrorCodes, TestDto.BasicTicketStateDto>(ErrorCodes.UnknownError, null);
                            }
                        case ApplicationBusinessLogicEnvironment.Test:
                            {
                                if ((request == null) || (request.RequestDtosTypes == null) || (request.RequestDtosJsons == null) ||
                                              (request.RequestDtosTypes.Count < 1) || (request.RequestDtosJsons.Count < 1) ||
                                                    (!Type.Equals(request.RequestDtosTypes[0], typeof(TestDto.BasicTicketStateDto))))
                                {
                                    return new Tuple<ErrorCodes, TestDto.BasicTicketStateDto>(ErrorCodes.UnknownError, null);
                                }
                                TestDto.BasicTicketStateDto castedDto = JsonConvert.DeserializeObject<TestDto.BasicTicketStateDto>(request.RequestDtosJsons[0]);

                                if (castedDto == null)
                                {
                                    return new Tuple<ErrorCodes, TestDto.BasicTicketStateDto>(ErrorCodes.UnknownError, null);
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
                                return new Tuple<ErrorCodes, TestDto.BasicTicketStateDto>(ErrorCodes.UnknownError, null);
                            }
                        default:
                            {
                                return new Tuple<ErrorCodes, TestDto.BasicTicketStateDto>(ErrorCodes.UnknownError, null);
                            }
                    }
                }
                else
                {
                    return new Tuple<ErrorCodes, TestDto.BasicTicketStateDto>(ErrorCodes.UnknownError, null);
                }
            }

            public async Task<Tuple<ErrorCodes, TestDto.BasicTicketStateDto>> DeleteTestAsync(BusinessLogicRequest request)
            {
                if (businessLogicContainerStatus)
                {
                    switch (_applicationBusinessLogicEnvironment)
                    {
                        case ApplicationBusinessLogicEnvironment.Development:
                            {
                                return new Tuple<ErrorCodes, TestDto.BasicTicketStateDto>(ErrorCodes.UnknownError, null);
                            }
                        case ApplicationBusinessLogicEnvironment.Test:
                            {
                                if ((request == null) || (request.RequestDtosTypes == null) || (request.RequestDtosJsons == null) ||
                                              (request.RequestDtosTypes.Count < 1) || (request.RequestDtosJsons.Count < 1) ||
                                                    (!Type.Equals(request.RequestDtosTypes[0], typeof(TestDto.BasicTicketStateDto))))
                                {
                                    return new Tuple<ErrorCodes, TestDto.BasicTicketStateDto>(ErrorCodes.UnknownError, null);
                                }
                                TestDto.BasicTicketStateDto castedDto = JsonConvert.DeserializeObject<TestDto.BasicTicketStateDto>(request.RequestDtosJsons[0]);

                                if (castedDto == null)
                                {
                                    return new Tuple<ErrorCodes, TestDto.BasicTicketStateDto>(ErrorCodes.UnknownError, null);
                                }



                                return await _genericStandartBusinessRulesTest.DeleteFromInt(castedDto.Id);

                            }
                        case ApplicationBusinessLogicEnvironment.Live:
                            {
                                return new Tuple<ErrorCodes, TestDto.BasicTicketStateDto>(ErrorCodes.UnknownError, null);
                            }
                        default:
                            {
                                return new Tuple<ErrorCodes, TestDto.BasicTicketStateDto>(ErrorCodes.UnknownError, null);
                            }
                    }
                }
                else
                {
                    return new Tuple<ErrorCodes, TestDto.BasicTicketStateDto>(ErrorCodes.UnknownError, null);
                }
            }
            #endregion

        #endregion
    }


}
