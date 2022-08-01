using DiasDataAccessLayer.Enums;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using static DiasWebApi.Shared.Enums.WebApiApplicationEnums;
using DevelopmentRepositoryInterface = DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement;
using BusinessWrapperInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Custom;
using DiasWebApi.Shared.Operations.BusinessLogicOperations.SimpleInjectorOperations;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Generic.DiasFacilityManagementSqlServer.Development;
using DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.CustomProfiles.Development;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Base.SqlServer.Custom;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Custom;
using TestDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Custom;
using BusinessWrapperTestInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Test.Custom;
using DiasShared.Errors;
using DiasBusinessLogic.Shared.Error;

namespace DiasWebApi.Repositories.DiasFacilityManagement
{
    public class LocationWrapperDtoRepository : ILocationWrapperDtoRepository
    {
        private IBaseLocationWrapperBusinessRules _baseLocationWrapperBusinessRules;
        private ApplicationBusinessLogicEnvironment _applicationBusinessLogicEnvironment;
        private bool businessLogicContainerStatus { get; set; }
        public LocationWrapperDtoRepository(IBaseLocationWrapperBusinessRules baseLocationWrapperBusinessRules, ApplicationBusinessLogicEnvironment applicationBusinessLogicEnvironment)
        {
            businessLogicContainerStatus = SimpleInjectorContainerOperations.VerifyContainer();
            if (businessLogicContainerStatus)
            {
                _baseLocationWrapperBusinessRules = baseLocationWrapperBusinessRules;
            }       
        }




        #region WebClient
        #region Development
        public async Task<Tuple<Error, IEnumerable<CustomLocationDto>>> GetAllLocationsWrapperAsync()
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            ILocationWrapperBusinessRules businessRule =
                                (ILocationWrapperBusinessRules)_baseLocationWrapperBusinessRules;
                            return await businessRule.GetAllLocationsWrapperAsync();
                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            return new Tuple<Error, IEnumerable<CustomLocationDto>>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, IEnumerable<CustomLocationDto>>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    default:
                        {
                            return new Tuple<Error, IEnumerable<CustomLocationDto>>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, IEnumerable<CustomLocationDto>>(Errors.General.GeneralServerError(), null);
            }
        }

        public async Task<Tuple<Error, CustomLocationDto>> GetLocationWrapperByIdAsync(string hierarchyId)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            ILocationWrapperBusinessRules businessRule =
                                (ILocationWrapperBusinessRules)_baseLocationWrapperBusinessRules;
                            return await businessRule.GetLocationWrapperByIdAsync(hierarchyId);
                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            return new Tuple<Error, CustomLocationDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, CustomLocationDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    default:
                        {
                            return new Tuple<Error, CustomLocationDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, CustomLocationDto>(Errors.General.GeneralServerError(), null);
            }
        }

        public async Task<Tuple<Error, CustomLocationDto>> GetLocationWrapperByNfcCodeAsync(string nfcCode)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            ILocationWrapperBusinessRules businessRule =
                                (ILocationWrapperBusinessRules)_baseLocationWrapperBusinessRules;
                            return await businessRule.GetLocationWrapperByNfcCodeAsync(nfcCode);
                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            return new Tuple<Error, CustomLocationDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, CustomLocationDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    default:
                        {
                            return new Tuple<Error, CustomLocationDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, CustomLocationDto>(Errors.General.GeneralServerError(), null);
            }
        }
        #endregion

        #region Test
        public async Task<Tuple<ErrorCodes, IEnumerable<TestDto.CustomLocationDto>>> GetAllLocationsWrapperTestAsync()
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            return new Tuple<ErrorCodes, IEnumerable<TestDto.CustomLocationDto>>(ErrorCodes.UnknownError, null);

                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            BusinessWrapperTestInterface.ILocationWrapperBusinessRules businessRule =
                                (BusinessWrapperTestInterface.ILocationWrapperBusinessRules)_baseLocationWrapperBusinessRules;

                            return await businessRule.GetAllLocationsWrapperAsync();
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<ErrorCodes, IEnumerable<TestDto.CustomLocationDto>>(ErrorCodes.UnknownError, null);
                        }
                    default:
                        {
                            return new Tuple<ErrorCodes, IEnumerable<TestDto.CustomLocationDto>>(ErrorCodes.UnknownError, null);
                        }
                }
            }
            else
            {
                return new Tuple<ErrorCodes, IEnumerable<TestDto.CustomLocationDto>>(ErrorCodes.UnknownError, null);
            }
        }

        public async Task<Tuple<ErrorCodes, TestDto.CustomLocationDto>> GetLocationWrapperByIdTestAsync(string hierarchyId)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            return new Tuple<ErrorCodes, TestDto.CustomLocationDto>(ErrorCodes.UnknownError, null);

                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            BusinessWrapperTestInterface.ILocationWrapperBusinessRules businessRule =
                                (BusinessWrapperTestInterface.ILocationWrapperBusinessRules)_baseLocationWrapperBusinessRules;

                            return await businessRule.GetLocationWrapperByIdAsync(hierarchyId);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<ErrorCodes, TestDto.CustomLocationDto>(ErrorCodes.UnknownError, null);
                        }
                    default:
                        {
                            return new Tuple<ErrorCodes,TestDto.CustomLocationDto>(ErrorCodes.UnknownError, null);
                        }
                }
            }
            else
            {
                return new Tuple<ErrorCodes, TestDto.CustomLocationDto>(ErrorCodes.UnknownError, null);
            }
        }
        #endregion


        #endregion
    }
}
