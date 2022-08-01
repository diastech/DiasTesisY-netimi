using DiasDataAccessLayer.Enums;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using static DiasWebApi.Shared.Enums.WebApiApplicationEnums;
using TestDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Custom;
using DevelopmentRepositoryInterface = DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement;
using BusinessWrapperInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Custom;
using DiasWebApi.Shared.Operations.BusinessLogicOperations.SimpleInjectorOperations;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Generic.DiasFacilityManagementSqlServer.Development;
using DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.CustomProfiles.Development;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Base.SqlServer.Custom;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Custom;
using DiasShared.Services.Communication.BusinessLogicMessage.ThirdPartyLibrary.DevExpress;
using DiasShared.Services.Communication.BusinessLogicMessage;
using BusinessWrapperTestInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Test.Custom;
using DiasBusinessLogic.Shared.Error;
using DiasShared.Errors;

namespace DiasWebApi.Repositories.DiasFacilityManagement
{
    public class PeriodicTicketWrapperDtoRepository : IPeriodicTicketWrapperDtoRepository
    {
        private IBasePeriodicTicketWrapperBusinessRules _basePeriodicTicketWrapperBusinessRules;
        private ApplicationBusinessLogicEnvironment _applicationBusinessLogicEnvironment;
        private IBasePeriodicTicketWrapperTransactionalBusinessRules _basePeriodicTicketWrapperTransactionalBusinessRules;
        private bool businessLogicContainerStatus { get; set; }
        public PeriodicTicketWrapperDtoRepository(IBasePeriodicTicketWrapperBusinessRules basePeriodicTicketWrapperBusinessRules,
            IBasePeriodicTicketWrapperTransactionalBusinessRules basePeriodicTicketWrapperTransactionalBusinessRules,
            ApplicationBusinessLogicEnvironment applicationBusinessLogicEnvironment
            )
        {
            businessLogicContainerStatus = SimpleInjectorContainerOperations.VerifyContainer();
            if (businessLogicContainerStatus)
            {
                _basePeriodicTicketWrapperBusinessRules = basePeriodicTicketWrapperBusinessRules;
                _basePeriodicTicketWrapperTransactionalBusinessRules = basePeriodicTicketWrapperTransactionalBusinessRules;
            }       
        }



        #region WebClient

        #region Development
        public async Task<Tuple<Error, IEnumerable<CustomPeriodicTicketDto>>> GetAllPeriodicTicketsWrapperAsync(DevExpressRequest devExpressRequestObj)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            IPeriodicTicketWrapperBusinessRules businessRule =
                                (IPeriodicTicketWrapperBusinessRules)_basePeriodicTicketWrapperBusinessRules;
                            return await businessRule.GetAllPeriodicTicketsWrapperAsync(devExpressRequestObj);
                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            return new Tuple<Error, IEnumerable<CustomPeriodicTicketDto>>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, IEnumerable<CustomPeriodicTicketDto>>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    default:
                        {
                            return new Tuple<Error, IEnumerable<CustomPeriodicTicketDto>>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, IEnumerable<CustomPeriodicTicketDto>>(Errors.General.GeneralServerError(), null);
            }
        }

        public async Task<Tuple<Error, CustomPeriodicTicketDto>> GetPeriodicTicketWrapperByIdAsync(int Id)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            IPeriodicTicketWrapperBusinessRules businessRule =
                                (IPeriodicTicketWrapperBusinessRules)_basePeriodicTicketWrapperBusinessRules;
                            return await businessRule.GetPeriodicTicketWrapperByIdAsync(Id);
                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            return new Tuple<Error, CustomPeriodicTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, CustomPeriodicTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    default:
                        {
                            return new Tuple<Error, CustomPeriodicTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, CustomPeriodicTicketDto>(Errors.General.GeneralServerError(), null);
            }
        }

        public async Task<Tuple<Error, CustomPeriodicTicketDto>> AddPeriodicTicketWrapperAsync(BusinessLogicRequest request)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            IPeriodicTicketWrapperTransactionalBusinessRules businessRule =
                                (IPeriodicTicketWrapperTransactionalBusinessRules)_basePeriodicTicketWrapperTransactionalBusinessRules;

                            return await businessRule.AddPeriodicTicketWrapperAsync(request);
                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            return new Tuple<Error, CustomPeriodicTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, CustomPeriodicTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    default:
                        {
                            return new Tuple<Error, CustomPeriodicTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, CustomPeriodicTicketDto>(Errors.General.GeneralServerError(), null);
            }
        }

        public async Task<Tuple<Error, CustomPeriodicTicketDto>> UpdatePeriodicTicketWrapperAsync(BusinessLogicRequest request)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            IPeriodicTicketWrapperTransactionalBusinessRules businessRule =
                                (IPeriodicTicketWrapperTransactionalBusinessRules)_basePeriodicTicketWrapperTransactionalBusinessRules;

                            return await businessRule.UpdatePeriodicTicketWrapperAsync(request);
                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            return new Tuple<Error, CustomPeriodicTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, CustomPeriodicTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    default:
                        {
                            return new Tuple<Error, CustomPeriodicTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, CustomPeriodicTicketDto>(Errors.General.GeneralServerError(), null);
            }
        }

        public async Task<Tuple<Error, CustomPeriodicTicketDto>> DeletePeriodicTicketWrapperAsync(BusinessLogicRequest request)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            IPeriodicTicketWrapperTransactionalBusinessRules businessRule =
                                (IPeriodicTicketWrapperTransactionalBusinessRules)_basePeriodicTicketWrapperTransactionalBusinessRules;

                            return await businessRule.DeletePeriodicTicketWrapperAsync(request);
                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            return new Tuple<Error, CustomPeriodicTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, CustomPeriodicTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    default:
                        {
                            return new Tuple<Error, CustomPeriodicTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, CustomPeriodicTicketDto>(Errors.General.GeneralServerError(), null);
            }
        }

        #endregion

        #region Test
        public async Task<Tuple<Error, IEnumerable<TestDto.CustomPeriodicTicketDto>>> GetAllPeriodicTicketsWrapperTestAsync(DevExpressRequest devExpressRequestObj)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            return new Tuple<Error, IEnumerable<TestDto.CustomPeriodicTicketDto>>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            
                            BusinessWrapperTestInterface.IPeriodicTicketWrapperBusinessRules businessRule =
                                (BusinessWrapperTestInterface.IPeriodicTicketWrapperBusinessRules)_basePeriodicTicketWrapperBusinessRules;

                            return await businessRule.GetAllPeriodicTicketsWrapperAsync(devExpressRequestObj);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, IEnumerable<TestDto.CustomPeriodicTicketDto>>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    default:
                        {
                            return new Tuple<Error, IEnumerable<TestDto.CustomPeriodicTicketDto>>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, IEnumerable<TestDto.CustomPeriodicTicketDto>>(Errors.General.GeneralServerError(), null);
            }
        }

        public async Task<Tuple<Error, TestDto.CustomPeriodicTicketDto>> GetPeriodicTicketWrapperByIdTestAsync(int Id)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            return new Tuple<Error, TestDto.CustomPeriodicTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            BusinessWrapperTestInterface.IPeriodicTicketWrapperBusinessRules businessRule =
                                (BusinessWrapperTestInterface.IPeriodicTicketWrapperBusinessRules)_basePeriodicTicketWrapperBusinessRules;

                            return await businessRule.GetPeriodicTicketWrapperByIdAsync(Id);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, TestDto.CustomPeriodicTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    default:
                        {
                            return new Tuple<Error, TestDto.CustomPeriodicTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, TestDto.CustomPeriodicTicketDto>(Errors.General.GeneralServerError(), null);
            }
        }

        public async Task<Tuple<Error, TestDto.CustomPeriodicTicketDto>> AddPeriodicTicketWrapperTestAsync(BusinessLogicRequest request)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            return new Tuple<Error, TestDto.CustomPeriodicTicketDto>(Errors.General.NotFoundDatabaseServer(), null);

                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            BusinessWrapperTestInterface.IPeriodicTicketWrapperTransactionalBusinessRules businessRule =
                                (BusinessWrapperTestInterface.IPeriodicTicketWrapperTransactionalBusinessRules)_basePeriodicTicketWrapperTransactionalBusinessRules;

                            return await businessRule.AddPeriodicTicketWrapperAsync(request);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, TestDto.CustomPeriodicTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    default:
                        {
                            return new Tuple<Error, TestDto.CustomPeriodicTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, TestDto.CustomPeriodicTicketDto>(Errors.General.GeneralServerError(), null);
            }
        }

        public async Task<Tuple<Error, TestDto.CustomPeriodicTicketDto>> UpdatePeriodicTicketWrapperTestAsync(BusinessLogicRequest request)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            return new Tuple<Error, TestDto.CustomPeriodicTicketDto>(Errors.General.NotFoundDatabaseServer(), null);

                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            BusinessWrapperTestInterface.IPeriodicTicketWrapperTransactionalBusinessRules businessRule =
                                (BusinessWrapperTestInterface.IPeriodicTicketWrapperTransactionalBusinessRules)_basePeriodicTicketWrapperTransactionalBusinessRules;

                            return await businessRule.UpdatePeriodicTicketWrapperAsync(request);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, TestDto.CustomPeriodicTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    default:
                        {
                            return new Tuple<Error, TestDto.CustomPeriodicTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, TestDto.CustomPeriodicTicketDto>(Errors.General.GeneralServerError(), null);
            }
        }

        public async Task<Tuple<Error, TestDto.CustomPeriodicTicketDto>> DeletePeriodicTicketWrapperTestAsync(BusinessLogicRequest request)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            return new Tuple<Error, TestDto.CustomPeriodicTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            BusinessWrapperTestInterface.IPeriodicTicketWrapperTransactionalBusinessRules businessRule =
                                (BusinessWrapperTestInterface.IPeriodicTicketWrapperTransactionalBusinessRules)_basePeriodicTicketWrapperTransactionalBusinessRules;

                            return await businessRule.DeletePeriodicTicketWrapperAsync(request);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, TestDto.CustomPeriodicTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    default:
                        {
                            return new Tuple<Error, TestDto.CustomPeriodicTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, TestDto.CustomPeriodicTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
            }
        }
        #endregion

        #endregion

        #region MobileClient

        #endregion

    }
}
