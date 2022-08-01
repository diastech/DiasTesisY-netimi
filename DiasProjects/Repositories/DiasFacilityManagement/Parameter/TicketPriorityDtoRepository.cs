using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevelopmentRepositoryInterface = DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement;
using TestDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard;
using TestAutomapper = DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Test;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.BaseBusinessRules.SqlServer.Standart;
using static DiasWebApi.Shared.Enums.WebApiApplicationEnums;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Generic.DiasFacilityManagementSqlServer.Development;
using GenericInterfaceTest = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Generic.DiasFacilityManagementSqlServer.Test;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Development;
using DiasWebApi.Shared.Operations.BusinessLogicOperations.SimpleInjectorOperations;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DiasShared.Services.Communication.BusinessLogicMessage;
using Newtonsoft.Json;
using DiasShared.Errors;
using DiasBusinessLogic.Shared.Error;
using DiasShared.Operations.StringOperation;

namespace DiasWebApi.Repositories.DiasFacilityManagement 
{
    public class TicketPriorityDtoRepository : DevelopmentRepositoryInterface.ITicketPriorityDtoRepository
    {
        private IBaseTicketPriorityBusinessRules _baseTicketPriorityBusinessRules;
        private ApplicationBusinessLogicEnvironment _applicationBusinessLogicEnvironment;
        private IGenericStandartBusinessRules<TicketPriorityDto, TicketPriorityProfile> _genericStandartBusinessRules;
        private GenericInterfaceTest.IGenericStandartBusinessRules<TestDto.TicketPriorityDto, TestAutomapper.TicketPriorityProfile> _genericStandartBusinessRulesTest;

        private bool businessLogicContainerStatus { get; set; }

        public TicketPriorityDtoRepository(
            IBaseTicketPriorityBusinessRules baseTicketPriorityBusinessRules,
            ApplicationBusinessLogicEnvironment applicationBusinessLogicEnvironment,
            IGenericStandartBusinessRules<TicketPriorityDto, TicketPriorityProfile> genericStandartBusinessRules,
            GenericInterfaceTest.IGenericStandartBusinessRules<TestDto.TicketPriorityDto, TestAutomapper.TicketPriorityProfile> genericStandartBusinessRulesTest
            )
        {
            businessLogicContainerStatus = SimpleInjectorContainerOperations.VerifyContainer();

            if (businessLogicContainerStatus)
            {
                _baseTicketPriorityBusinessRules = baseTicketPriorityBusinessRules;
                _applicationBusinessLogicEnvironment = applicationBusinessLogicEnvironment;
            }

            _genericStandartBusinessRules = genericStandartBusinessRules;
            _genericStandartBusinessRulesTest = genericStandartBusinessRulesTest;
        }

        #region WebClient

            #region Development

            //TODO : Hata kodları özelleştirilecek
                public async Task<Tuple<Error, IEnumerable<TicketPriorityDto>>> GetAllAsync()
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
                                        return new Tuple<Error, IEnumerable<TicketPriorityDto>>(Errors.General.GeneralServerError(), null);
                                    }
                                case ApplicationBusinessLogicEnvironment.Live:
                                    {
                                        return new Tuple<Error, IEnumerable<TicketPriorityDto>>(Errors.General.GeneralServerError(), null);
                                    }
                                default:
                                    {
                                        return new Tuple<Error, IEnumerable<TicketPriorityDto>>(Errors.General.GeneralServerError(), null);
                                    }
                            }
                        }
                        else
                        {
                            return new Tuple<Error, IEnumerable<TicketPriorityDto>>(Errors.General.GeneralServerError(), null);
                        }
                    }

                //TODO : Hata kodları özelleştirilecek
                public async Task<Tuple<Error, TicketPriorityDto>> DeleteAsync(BusinessLogicRequest request)
                    {
                        if (businessLogicContainerStatus)
                        {
                            switch (_applicationBusinessLogicEnvironment)
                            {
                                case ApplicationBusinessLogicEnvironment.Development:
                                    {
                                        if ((request == null) || (request.RequestDtosTypes == null) || (request.RequestDtosJsons == null) ||
                                                      (request.RequestDtosTypes.Count < 1) || (request.RequestDtosJsons.Count < 1) ||
                                                            (!Type.Equals(request.RequestDtosTypes[0], typeof(TicketPriorityDto))))
                                        {
                                            return new Tuple<Error, TicketPriorityDto>(Errors.General.GeneralServerError(), null);
                                        }
                                        TicketPriorityDto castedDto = JsonConvert.DeserializeObject<TicketPriorityDto>(request.RequestDtosJsons[0]);

                                        if (castedDto == null)
                                        {
                                            return new Tuple<Error, TicketPriorityDto>(Errors.General.GeneralServerError(), null);
                                        }


                                        return await _genericStandartBusinessRules.DeleteFromIntV2(castedDto.Id);
                                    }
                                case ApplicationBusinessLogicEnvironment.Test:
                                    {
                                        return new Tuple<Error, TicketPriorityDto>(Errors.General.GeneralServerError(), null);
                                    }
                                case ApplicationBusinessLogicEnvironment.Live:
                                    {
                                        return new Tuple<Error, TicketPriorityDto>(Errors.General.GeneralServerError(), null);
                                    }
                                default:
                                    {
                                        return new Tuple<Error, TicketPriorityDto>(Errors.General.GeneralServerError(), null);
                                    }
                            }
                        }
                        else
                        {
                            return new Tuple<Error, TicketPriorityDto>(Errors.General.GeneralServerError(), null);
                        }
                    }

                //TODO : Hata kodları özelleştirilecek
                public async Task<Tuple<Error, TicketPriorityDto>> GetByIdFromIntAsync(int id)
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
                                        return new Tuple<Error, TicketPriorityDto>(Errors.General.GeneralServerError(), null);
                                    }
                                case ApplicationBusinessLogicEnvironment.Live:
                                    {
                                        return new Tuple<Error, TicketPriorityDto>(Errors.General.GeneralServerError(), null);
                                    }
                                default:
                                    {
                                        return new Tuple<Error, TicketPriorityDto>(Errors.General.GeneralServerError(), null);
                                    }
                            }
                        }
                        else
                        {
                            return new Tuple<Error, TicketPriorityDto>(Errors.General.GeneralServerError(), null);
                        }
                    }

                //TODO : Hata kodları özelleştirilecek
                public async Task<Tuple<Error, TicketPriorityDto>> InsertAsync(BusinessLogicRequest request)
                    {
                        if (businessLogicContainerStatus)
                        {
                            switch (_applicationBusinessLogicEnvironment)
                            {
                                case ApplicationBusinessLogicEnvironment.Development:
                                    {
                                        if ((request == null) || (request.RequestDtosTypes == null) || (request.RequestDtosJsons == null) ||
                                                      (request.RequestDtosTypes.Count < 1) || (request.RequestDtosJsons.Count < 1) ||
                                                            (!Type.Equals(request.RequestDtosTypes[0], typeof(TicketPriorityDto))))
                                        {
                                            return new Tuple<Error, TicketPriorityDto>(Errors.General.GeneralServerError(), null);
                                        }
                                        TicketPriorityDto castedDto = JsonConvert.DeserializeObject<TicketPriorityDto>(request.RequestDtosJsons[0]);

                                        if (castedDto == null)
                                        {
                                            return new Tuple<Error, TicketPriorityDto>(Errors.General.GeneralServerError(), null);
                                        }

                                        List<string> uniqueColumns = new List<string>()
                                        {
                                            "Name"
                                        };

                                        List<object> uniqueValues = new List<object>()
                                        {
                                            castedDto.Name,
                                        };

                                        //Normalized Name'i ayarla
                                        castedDto.NormalizedName = StringOperations.NormalizeString(castedDto.Name);

                                        return await _genericStandartBusinessRules.InsertV2(castedDto, uniqueColumns, uniqueValues, true);
                                    }
                                case ApplicationBusinessLogicEnvironment.Test:
                                    {
                                        return new Tuple<Error, TicketPriorityDto>(Errors.General.GeneralServerError(), null);
                                    }
                                case ApplicationBusinessLogicEnvironment.Live:
                                    {
                                        return new Tuple<Error, TicketPriorityDto>(Errors.General.GeneralServerError(), null);
                                    }
                                default:
                                    {
                                        return new Tuple<Error, TicketPriorityDto>(Errors.General.GeneralServerError(), null);
                                    }
                            }
                        }
                        else
                        {
                            return new Tuple<Error, TicketPriorityDto>(Errors.General.GeneralServerError(), null);
                        }
                    }

                //TODO : Hata kodları özelleştirilecek
                public async Task<Tuple<Error, TicketPriorityDto>> UpdateAsync(BusinessLogicRequest request)
                    {
                        if (businessLogicContainerStatus)
                        {
                            switch (_applicationBusinessLogicEnvironment)
                            {
                                case ApplicationBusinessLogicEnvironment.Development:
                                    {
                                        if ((request == null) || (request.RequestDtosTypes == null) || (request.RequestDtosJsons == null) ||
                                                      (request.RequestDtosTypes.Count < 1) || (request.RequestDtosJsons.Count < 1) ||
                                                            (!Type.Equals(request.RequestDtosTypes[0], typeof(TicketPriorityDto))))
                                        {
                                            return new Tuple<Error, TicketPriorityDto>(Errors.General.GeneralServerError(), null);
                                        }
                                        TicketPriorityDto castedDto = JsonConvert.DeserializeObject<TicketPriorityDto>(request.RequestDtosJsons[0]);

                                        if (castedDto == null)
                                        {
                                            return new Tuple<Error, TicketPriorityDto>(Errors.General.GeneralServerError(), null);
                                        }

                                        List<string> uniqueColumns = new List<string>() { "Id" };
                                        List<object> uniqueValues = new List<object>() { castedDto.Id.ToString() };

                                        //Normalized Name'i ayarla
                                        castedDto.NormalizedName = StringOperations.NormalizeString(castedDto.Name);

                                        return await _genericStandartBusinessRules.UpdateV2(castedDto, uniqueColumns, uniqueValues);
                                    }
                                case ApplicationBusinessLogicEnvironment.Test:
                                    {
                                        return new Tuple<Error, TicketPriorityDto>(Errors.General.GeneralServerError(), null);
                                    }
                                case ApplicationBusinessLogicEnvironment.Live:
                                    {
                                        return new Tuple<Error, TicketPriorityDto>(Errors.General.GeneralServerError(), null);
                                    }
                                default:
                                    {
                                        return new Tuple<Error, TicketPriorityDto>(Errors.General.GeneralServerError(), null);
                                    }
                            }
                        }
                        else
                        {
                            return new Tuple<Error, TicketPriorityDto>(Errors.General.GeneralServerError(), null);
                        }
                    }

                #endregion

            #region Test
            public async Task<Tuple<ErrorCodes, IEnumerable<TestDto.TicketPriorityDto>>> GetAllTestAsync()
            {
                if (businessLogicContainerStatus)
                {
                    switch (_applicationBusinessLogicEnvironment)
                    {
                        case ApplicationBusinessLogicEnvironment.Development:
                            {
                                return new Tuple<ErrorCodes, IEnumerable<TestDto.TicketPriorityDto>>(ErrorCodes.UnknownError, null);

                            }
                        case ApplicationBusinessLogicEnvironment.Test:
                            {
                                return await _genericStandartBusinessRulesTest.GetAll();
                            }
                        case ApplicationBusinessLogicEnvironment.Live:
                            {
                                return new Tuple<ErrorCodes, IEnumerable<TestDto.TicketPriorityDto>>(ErrorCodes.UnknownError, null);
                            }
                        default:
                            {
                                return new Tuple<ErrorCodes, IEnumerable<TestDto.TicketPriorityDto>>(ErrorCodes.UnknownError, null);
                            }
                    }
                }
                else
                {
                    return new Tuple<ErrorCodes, IEnumerable<TestDto.TicketPriorityDto>>(ErrorCodes.UnknownError, null);
                }
            }

            public async Task<Tuple<ErrorCodes, TestDto.TicketPriorityDto>> InsertTestAsync(BusinessLogicRequest request)
            {
                if (businessLogicContainerStatus)
                {
                    switch (_applicationBusinessLogicEnvironment)
                    {
                        case ApplicationBusinessLogicEnvironment.Development:
                            {
                                return new Tuple<ErrorCodes, TestDto.TicketPriorityDto>(ErrorCodes.UnknownError, null);
                            }
                        case ApplicationBusinessLogicEnvironment.Test:
                            {
                                if ((request == null) || (request.RequestDtosTypes == null) || (request.RequestDtosJsons == null) ||
                                              (request.RequestDtosTypes.Count < 1) || (request.RequestDtosJsons.Count < 1) ||
                                                    (!Type.Equals(request.RequestDtosTypes[0], typeof(TestDto.TicketPriorityDto))))
                                {
                                    return new Tuple<ErrorCodes, TestDto.TicketPriorityDto>(ErrorCodes.UnknownError, null);
                                }
                                TestDto.TicketPriorityDto castedDto = JsonConvert.DeserializeObject<TestDto.TicketPriorityDto>(request.RequestDtosJsons[0]);

                                if (castedDto == null)
                                {
                                    return new Tuple<ErrorCodes, TestDto.TicketPriorityDto>(ErrorCodes.UnknownError, null);
                                }

                                List<string> uniqueColumns = new List<string>()
                                {
                                    "Name"
                                };

                                List<object> uniqueValues = new List<object>()
                                {
                                    castedDto.Name,
                                };

                                return await _genericStandartBusinessRulesTest.Insert(castedDto, uniqueColumns, uniqueValues);

                            }
                        case ApplicationBusinessLogicEnvironment.Live:
                            {
                                return new Tuple<ErrorCodes, TestDto.TicketPriorityDto>(ErrorCodes.UnknownError, null);
                            }
                        default:
                            {
                                return new Tuple<ErrorCodes, TestDto.TicketPriorityDto>(ErrorCodes.UnknownError, null);
                            }
                    }
                }
                else
                {
                    return new Tuple<ErrorCodes, TestDto.TicketPriorityDto>(ErrorCodes.UnknownError, null);
                }
            }

            public async Task<Tuple<ErrorCodes, TestDto.TicketPriorityDto>> UpdateTestAsync(BusinessLogicRequest request)
            {
                if (businessLogicContainerStatus)
                {
                    switch (_applicationBusinessLogicEnvironment)
                    {
                        case ApplicationBusinessLogicEnvironment.Development:
                            {
                                return new Tuple<ErrorCodes, TestDto.TicketPriorityDto>(ErrorCodes.UnknownError, null);
                            }
                        case ApplicationBusinessLogicEnvironment.Test:
                            {
                                if ((request == null) || (request.RequestDtosTypes == null) || (request.RequestDtosJsons == null) ||
                                              (request.RequestDtosTypes.Count < 1) || (request.RequestDtosJsons.Count < 1) ||
                                                    (!Type.Equals(request.RequestDtosTypes[0], typeof(TestDto.TicketPriorityDto))))
                                {
                                    return new Tuple<ErrorCodes, TestDto.TicketPriorityDto>(ErrorCodes.UnknownError, null);
                                }
                                TestDto.TicketPriorityDto castedDto = JsonConvert.DeserializeObject<TestDto.TicketPriorityDto>(request.RequestDtosJsons[0]);

                                if (castedDto == null)
                                {
                                    return new Tuple<ErrorCodes, TestDto.TicketPriorityDto>(ErrorCodes.UnknownError, null);
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
                                return new Tuple<ErrorCodes, TestDto.TicketPriorityDto>(ErrorCodes.UnknownError, null);
                            }
                        default:
                            {
                                return new Tuple<ErrorCodes, TestDto.TicketPriorityDto>(ErrorCodes.UnknownError, null);
                            }
                    }
                }
                else
                {
                    return new Tuple<ErrorCodes, TestDto.TicketPriorityDto>(ErrorCodes.UnknownError, null);
                }
            }

            public async Task<Tuple<ErrorCodes, TestDto.TicketPriorityDto>> DeleteTestAsync(BusinessLogicRequest request)
            {
                if (businessLogicContainerStatus)
                {
                    switch (_applicationBusinessLogicEnvironment)
                    {
                        case ApplicationBusinessLogicEnvironment.Development:
                            {
                                return new Tuple<ErrorCodes, TestDto.TicketPriorityDto>(ErrorCodes.UnknownError, null);
                            }
                        case ApplicationBusinessLogicEnvironment.Test:
                            {
                                if ((request == null) || (request.RequestDtosTypes == null) || (request.RequestDtosJsons == null) ||
                                              (request.RequestDtosTypes.Count < 1) || (request.RequestDtosJsons.Count < 1) ||
                                                    (!Type.Equals(request.RequestDtosTypes[0], typeof(TestDto.TicketPriorityDto))))
                                {
                                    return new Tuple<ErrorCodes, TestDto.TicketPriorityDto>(ErrorCodes.UnknownError, null);
                                }
                                TestDto.TicketPriorityDto castedDto = JsonConvert.DeserializeObject<TestDto.TicketPriorityDto>(request.RequestDtosJsons[0]);

                                if (castedDto == null)
                                {
                                    return new Tuple<ErrorCodes, TestDto.TicketPriorityDto>(ErrorCodes.UnknownError, null);
                                }



                                return await _genericStandartBusinessRulesTest.DeleteFromInt(castedDto.Id);

                            }
                        case ApplicationBusinessLogicEnvironment.Live:
                            {
                                return new Tuple<ErrorCodes, TestDto.TicketPriorityDto>(ErrorCodes.UnknownError, null);
                            }
                        default:
                            {
                                return new Tuple<ErrorCodes, TestDto.TicketPriorityDto>(ErrorCodes.UnknownError, null);
                            }
                    }
                }
                else
                {
                    return new Tuple<ErrorCodes, TestDto.TicketPriorityDto>(ErrorCodes.UnknownError, null);
                }
            }
            #endregion

        #endregion
    }
}
