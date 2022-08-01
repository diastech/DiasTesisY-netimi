using DiasDataAccessLayer.Enums;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using TestDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Custom;
using DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using static DiasWebApi.Shared.Enums.WebApiApplicationEnums;
using DevelopmentRepositoryInterface = DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement;
using BusinessWrapperInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Custom;
using BusinessWrapperTestInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Test.Custom;
using DiasWebApi.Shared.Operations.BusinessLogicOperations.SimpleInjectorOperations;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Generic.DiasFacilityManagementSqlServer.Development;
using DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.CustomProfiles.Development;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Base.SqlServer.Custom;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Custom;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using DiasShared.Services.Communication.BusinessLogicMessage.ThirdPartyLibrary.DevExpress;
using DiasShared.Services.Communication.BusinessLogicMessage;
using DiasShared.Errors;
using DiasBusinessLogic.Shared.Error;

namespace DiasWebApi.Repositories.DiasFacilityManagement
{
    public class TicketWrapperDtoRepository : ITicketWrapperDtoRepository
    {
        private IBaseTicketWrapperBusinessRules _baseTicketWrapperBusinessRules;
        private IBaseTicketWrapperTransactionalBusinessRules _baseTicketWrapperTransactionalBusinessRules;
        private ApplicationBusinessLogicEnvironment _applicationBusinessLogicEnvironment;
        private bool businessLogicContainerStatus { get; set; }
        public TicketWrapperDtoRepository(IBaseTicketWrapperBusinessRules baseTicketWrapperBusinessRules,
            IBaseTicketWrapperTransactionalBusinessRules baseTicketWrapperTransactionalBusinessRules,
            ApplicationBusinessLogicEnvironment applicationBusinessLogicEnvironment
            )
        {
            businessLogicContainerStatus = SimpleInjectorContainerOperations.VerifyContainer();
            if (businessLogicContainerStatus)
            {
                _baseTicketWrapperBusinessRules = baseTicketWrapperBusinessRules;
                _baseTicketWrapperTransactionalBusinessRules = baseTicketWrapperTransactionalBusinessRules;
            }
                        
        }

        #region WebClient

        #region Development
        public async Task<Tuple<Error, IEnumerable<CustomTicketDto>>> GetAllTicketWrapperAsync(DevExpressRequest devExpressRequestObj)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            ITicketWrapperBusinessRules businessRule =
                                (ITicketWrapperBusinessRules)_baseTicketWrapperBusinessRules;

                            return await businessRule.GetAllTicketsWrapperAsync(devExpressRequestObj);
                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            return new Tuple<Error, IEnumerable<CustomTicketDto>>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, IEnumerable<CustomTicketDto>>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    default:
                        {
                            return new Tuple<Error, IEnumerable<CustomTicketDto>>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, IEnumerable<CustomTicketDto>>(Errors.General.GeneralServerError(), null);
            }
        }
         
        public async Task<Tuple<Error, IEnumerable<CustomTicketDto>>> GetAllTicketWrapperMobileAsync(DevExpressRequest devExpressRequestObj)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            ITicketWrapperBusinessRules businessRule =
                                (ITicketWrapperBusinessRules)_baseTicketWrapperBusinessRules;

                            return await businessRule.GetAllTicketsWrapperMobileAsync(devExpressRequestObj);
                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            return new Tuple<Error, IEnumerable<CustomTicketDto>>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, IEnumerable<CustomTicketDto>>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    default:
                        {
                            return new Tuple<Error, IEnumerable<CustomTicketDto>>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, IEnumerable<CustomTicketDto>>(Errors.General.GeneralServerError(), null);
            }
        }


        public async Task<Tuple<Error, CustomTicketDto>> GetTicketWrapperByIdAsync(int Id)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            ITicketWrapperBusinessRules businessRule =
                                (ITicketWrapperBusinessRules)_baseTicketWrapperBusinessRules;

                            return await businessRule.GetTicketWrapperByTicketIdAsync(Id);
                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            return new Tuple<Error, CustomTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, CustomTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    default:
                        {
                            return new Tuple<Error, CustomTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, CustomTicketDto>(Errors.General.GeneralServerError(), null);
            }
        }

        public async Task<Tuple<Error, IEnumerable<CustomTicketDto>>> GetAllTicketsWrapperByBasicTicketIdAsync(int Id)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            ITicketWrapperBusinessRules businessRule =
                                (ITicketWrapperBusinessRules)_baseTicketWrapperBusinessRules;

                            return await businessRule.GetAllTicketsWrapperByBasicTicketIdAsync(Id);
                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            return new Tuple<Error, IEnumerable<CustomTicketDto>>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, IEnumerable<CustomTicketDto>>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    default:
                        {
                            return new Tuple<Error, IEnumerable<CustomTicketDto>>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, IEnumerable<CustomTicketDto>>(Errors.General.GeneralServerError(), null);
            }
        }

        public async Task<Tuple<Error, CustomTicketDto>> AddTicketWrapperAsync(BusinessLogicRequest request)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            ITicketWrapperTransactionalBusinessRules businessRule =
                                (ITicketWrapperTransactionalBusinessRules)_baseTicketWrapperTransactionalBusinessRules;                            
                            return await businessRule.AddTicketWrapperAsync(request);
                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            return new Tuple<Error, CustomTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, CustomTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    default:
                        {
                            return new Tuple<Error, CustomTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, CustomTicketDto>(Errors.General.GeneralServerError(), null);
            }
        }

        public async Task<Tuple<Error, CustomTicketDto>> UpdateTicketWrapperAsync(BusinessLogicRequest request)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            ITicketWrapperTransactionalBusinessRules businessRule =
                                (ITicketWrapperTransactionalBusinessRules)_baseTicketWrapperTransactionalBusinessRules;                            
                            return await businessRule.UpdateTicketWrapperAsync(request);
                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            return new Tuple<Error, CustomTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, CustomTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    default:
                        {
                            return new Tuple<Error, CustomTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, CustomTicketDto>(Errors.General.GeneralServerError(), null);
            }
        }

        public async Task<Tuple<Error, CustomTicketDto>> DeleteTicketWrapperAsync(BusinessLogicRequest request)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            ITicketWrapperTransactionalBusinessRules businessRule =
                                (ITicketWrapperTransactionalBusinessRules)_baseTicketWrapperTransactionalBusinessRules;                            
                            return await businessRule.DeleteTicketWrapperAsync(request);
                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            return new Tuple<Error, CustomTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, CustomTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    default:
                        {
                            return new Tuple<Error, CustomTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, CustomTicketDto>(Errors.General.GeneralServerError(), null);
            }
        }

        public async Task<Tuple<Error, CustomTicketDto>> AddTicketWithFastTicketWrapperAsync(CustomTicketDto customTicketDto)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            ITicketWrapperTransactionalBusinessRules businessRule =
                                (ITicketWrapperTransactionalBusinessRules)_baseTicketWrapperTransactionalBusinessRules;                            
                            return await businessRule.AddTicketWithFastTicketWrapperAsync(customTicketDto);
                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            return new Tuple<Error, CustomTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, CustomTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    default:
                        {
                            return new Tuple<Error, CustomTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, CustomTicketDto>(Errors.General.GeneralServerError(), null);
            }
        }

        public async Task<Tuple<Error, CustomTicketDto>> UpdateTicketStateWrapperAsync(BusinessLogicRequest request)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            ITicketWrapperTransactionalBusinessRules businessRule =
                                (ITicketWrapperTransactionalBusinessRules)_baseTicketWrapperTransactionalBusinessRules;
                            return await businessRule.UpdateTicketStateWrapperAsync(request);
                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            return new Tuple<Error, CustomTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, CustomTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    default:
                        {
                            return new Tuple<Error, CustomTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, CustomTicketDto>(Errors.General.GeneralServerError(), null);
            }
        }

        #endregion Development

        #region Test
        public async Task<Tuple<ErrorCodes, IEnumerable<TestDto.CustomTicketDto>>> GetAllTicketWrapperTestAsync(DevExpressRequest devExpressRequestObj)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            return new Tuple<ErrorCodes, IEnumerable<TestDto.CustomTicketDto>>(ErrorCodes.UnknownError, null);

                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            BusinessWrapperTestInterface.ITicketWrapperBusinessRules businessRule =
                                (BusinessWrapperTestInterface.ITicketWrapperBusinessRules)_baseTicketWrapperBusinessRules;

                            return await businessRule.GetAllTicketsWrapperAsync(devExpressRequestObj);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<ErrorCodes, IEnumerable<TestDto.CustomTicketDto>>(ErrorCodes.UnknownError, null);
                        }
                    default:
                        {
                            return new Tuple<ErrorCodes, IEnumerable<TestDto.CustomTicketDto>>(ErrorCodes.UnknownError, null);
                        }
                }
            }
            else
            {
                return new Tuple<ErrorCodes, IEnumerable<TestDto.CustomTicketDto>>(ErrorCodes.UnknownError, null);
            }
        }
        public async Task<Tuple<ErrorCodes, TestDto.CustomTicketDto>> GetTicketWrapperByIdTestAsync(int Id)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            return new Tuple<ErrorCodes, TestDto.CustomTicketDto>(ErrorCodes.UnknownError, null);

                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            BusinessWrapperTestInterface.ITicketWrapperBusinessRules businessRule =
                                (BusinessWrapperTestInterface.ITicketWrapperBusinessRules)_baseTicketWrapperBusinessRules;

                            return await businessRule.GetTicketWrapperByTicketIdAsync(Id);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<ErrorCodes, TestDto.CustomTicketDto>(ErrorCodes.UnknownError, null);
                        }
                    default:
                        {
                            return new Tuple<ErrorCodes, TestDto.CustomTicketDto>(ErrorCodes.UnknownError, null);
                        }
                }
            }
            else
            {
                return new Tuple<ErrorCodes, TestDto.CustomTicketDto>(ErrorCodes.UnknownError, null);
            }
        }
        public async Task<Tuple<ErrorCodes, TestDto.CustomTicketDto>> AddTicketWrapperTestAsync(BusinessLogicRequest request)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            return new Tuple<ErrorCodes, TestDto.CustomTicketDto>(ErrorCodes.UnknownError, null);

                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            BusinessWrapperTestInterface.ITicketWrapperTransactionalBusinessRules businessRule =
                                (BusinessWrapperTestInterface.ITicketWrapperTransactionalBusinessRules)_baseTicketWrapperTransactionalBusinessRules;

                            return await businessRule.AddTicketWrapperAsync(request);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<ErrorCodes, TestDto.CustomTicketDto>(ErrorCodes.UnknownError, null);
                        }
                    default:
                        {
                            return new Tuple<ErrorCodes, TestDto.CustomTicketDto>(ErrorCodes.UnknownError, null);
                        }
                }
            }
            else
            {
                return new Tuple<ErrorCodes, TestDto.CustomTicketDto>(ErrorCodes.UnknownError, null);
            }
        }

        public async Task<Tuple<ErrorCodes, TestDto.CustomTicketDto>> UpdateTicketWrapperTestAsync(BusinessLogicRequest request)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            return new Tuple<ErrorCodes, TestDto.CustomTicketDto>(ErrorCodes.UnknownError, null);

                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            BusinessWrapperTestInterface.ITicketWrapperTransactionalBusinessRules businessRule =
                                (BusinessWrapperTestInterface.ITicketWrapperTransactionalBusinessRules)_baseTicketWrapperTransactionalBusinessRules;

                            return await businessRule.UpdateTicketWrapperAsync(request);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<ErrorCodes, TestDto.CustomTicketDto>(ErrorCodes.UnknownError, null);
                        }
                    default:
                        {
                            return new Tuple<ErrorCodes, TestDto.CustomTicketDto>(ErrorCodes.UnknownError, null);
                        }
                }
            }
            else
            {
                return new Tuple<ErrorCodes, TestDto.CustomTicketDto>(ErrorCodes.UnknownError, null);
            }
        }

        public async Task<Tuple<ErrorCodes, IEnumerable<TestDto.CustomTicketDto>>> GetAllTicketsWrapperByBasicTicketIdTestAsync(int Id)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            return new Tuple<ErrorCodes, IEnumerable<TestDto.CustomTicketDto>>(ErrorCodes.UnknownError, null);

                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            BusinessWrapperTestInterface.ITicketWrapperBusinessRules businessRule =
                                (BusinessWrapperTestInterface.ITicketWrapperBusinessRules)_baseTicketWrapperBusinessRules;

                            return await businessRule.GetAllTicketsWrapperByBasicTicketIdAsync(Id);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<ErrorCodes, IEnumerable<TestDto.CustomTicketDto>>(ErrorCodes.UnknownError, null);
                        }
                    default:
                        {
                            return new Tuple<ErrorCodes, IEnumerable<TestDto.CustomTicketDto>>(ErrorCodes.UnknownError, null);
                        }
                }
            }
            else
            {
                return new Tuple<ErrorCodes, IEnumerable<TestDto.CustomTicketDto>>(ErrorCodes.UnknownError, null);
            }
        }

        public async Task<Tuple<ErrorCodes, TestDto.CustomTicketDto>> UpdateTicketStateWrapperTestAsync(BusinessLogicRequest request)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            return new Tuple<ErrorCodes, TestDto.CustomTicketDto>(ErrorCodes.UnknownError, null);

                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            BusinessWrapperTestInterface.ITicketWrapperTransactionalBusinessRules businessRule =
                                (BusinessWrapperTestInterface.ITicketWrapperTransactionalBusinessRules)_baseTicketWrapperTransactionalBusinessRules;

                            return await businessRule.UpdateTicketStateWrapperAsync(request);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<ErrorCodes, TestDto.CustomTicketDto>(ErrorCodes.UnknownError, null);
                        }
                    default:
                        {
                            return new Tuple<ErrorCodes, TestDto.CustomTicketDto>(ErrorCodes.UnknownError, null);
                        }
                }
            }
            else
            {
                return new Tuple<ErrorCodes, TestDto.CustomTicketDto>(ErrorCodes.UnknownError, null);
            }
        }

        #endregion Test

        #endregion WebClient

        #region Mobile

        #region Development
        public async Task<Tuple<Error, CustomMobileTicketDto>> AddTicketWrapperMobileAsync(BusinessLogicRequest request)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            ITicketWrapperTransactionalBusinessRules businessRule =
                                (ITicketWrapperTransactionalBusinessRules)_baseTicketWrapperTransactionalBusinessRules;
                            return await businessRule.AddTicketWrapperMobileAsync(request);
                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            return new Tuple<Error, CustomMobileTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, CustomMobileTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    default:
                        {
                            return new Tuple<Error, CustomMobileTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, CustomMobileTicketDto>(Errors.General.GeneralServerError(), null);
            }
        }
        #endregion

        #region Test
        public async Task<Tuple<ErrorCodes, TestDto.CustomMobileTicketDto>> AddTicketWrapperMobileTestAsync(BusinessLogicRequest request)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            return new Tuple<ErrorCodes, TestDto.CustomMobileTicketDto>(ErrorCodes.UnknownError, null);

                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            BusinessWrapperTestInterface.ITicketWrapperTransactionalBusinessRules businessRule =
                                (BusinessWrapperTestInterface.ITicketWrapperTransactionalBusinessRules)_baseTicketWrapperTransactionalBusinessRules;

                            return await businessRule.AddTicketWrapperMobileAsync(request);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<ErrorCodes, TestDto.CustomMobileTicketDto>(ErrorCodes.UnknownError, null);
                        }
                    default:
                        {
                            return new Tuple<ErrorCodes, TestDto.CustomMobileTicketDto>(ErrorCodes.UnknownError, null);
                        }
                }
            }
            else
            {
                return new Tuple<ErrorCodes, TestDto.CustomMobileTicketDto>(ErrorCodes.UnknownError, null);
            }
        }

        
        #endregion

        #endregion WebClient

    }

}

