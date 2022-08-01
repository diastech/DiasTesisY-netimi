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
using DevCustomDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using TestDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard;
using TestAutomapper = DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Test;
using GenericInterfaceTest = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Generic.DiasFacilityManagementSqlServer.Test;
using DiasShared.Errors;
using DiasBusinessLogic.Shared.Error;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Standart;
using DiasShared.Services.Communication.BusinessLogicMessage;
using static DiasShared.Enums.Standart.RemoteCommunicationEnums;

namespace DiasWebApi.Repositories.DiasFacilityManagement
{
    public class LocationV2DtoRepository : DevelopmentRepositoryInterface.ILocationV2DtoRepository
    {
        private IBaseLocationV2BusinessRules _baseLocationBusinessRules;
        private ApplicationBusinessLogicEnvironment _applicationBusinessLogicEnvironment;
        private IGenericStandartBusinessRules<LocationV2Dto, LocationV2Profile> _genericStandartBusinessRules;
        private GenericInterfaceTest.IGenericStandartBusinessRules<TestDto.LocationV2Dto, TestAutomapper.LocationV2Profile> _genericStandartBusinessRulesTest;
        private bool businessLogicContainerStatus { get; set; }
        public LocationV2DtoRepository(
            IBaseLocationV2BusinessRules baseLocationBusinessRules, 
            ApplicationBusinessLogicEnvironment applicationBusinessLogicEnvironment,
            IGenericStandartBusinessRules<LocationV2Dto, LocationV2Profile> genericStandartBusinessRules,
            GenericInterfaceTest.IGenericStandartBusinessRules<TestDto.LocationV2Dto, TestAutomapper.LocationV2Profile> genericStandartBusinessRulesTest
            )
        {
            businessLogicContainerStatus = SimpleInjectorContainerOperations.VerifyContainer();

            if (businessLogicContainerStatus)
            {
                _baseLocationBusinessRules = baseLocationBusinessRules;
                _applicationBusinessLogicEnvironment = applicationBusinessLogicEnvironment;
            }

            _genericStandartBusinessRules = genericStandartBusinessRules;
            _genericStandartBusinessRulesTest = genericStandartBusinessRulesTest;
        }

        public async Task<Tuple<BusinessLogicMessageCodes.ErrorCodes, IEnumerable<LocationV2Dto>>> GetAllAsync()
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
                            return new Tuple<ErrorCodes, IEnumerable<LocationV2Dto>>(ErrorCodes.UnknownError, null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<ErrorCodes, IEnumerable<LocationV2Dto>>(ErrorCodes.UnknownError, null);
                        }
                    default:
                        {
                            return new Tuple<ErrorCodes, IEnumerable<LocationV2Dto>>(ErrorCodes.UnknownError, null);
                        }
                }
            }
            else
            {
                return new Tuple<ErrorCodes, IEnumerable<LocationV2Dto>>(ErrorCodes.UnknownError, null);
            }
        }

        public async Task<Tuple<BusinessLogicMessageCodes.ErrorCodes, LocationV2Dto>> DeleteFromIntAsync(int id)
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
                            return new Tuple<ErrorCodes, LocationV2Dto>(ErrorCodes.UnknownError, null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<ErrorCodes, LocationV2Dto>(ErrorCodes.UnknownError, null);
                        }
                    default:
                        {
                            return new Tuple<ErrorCodes, LocationV2Dto>(ErrorCodes.UnknownError, null);
                        }
                }
            }
            else
            {
                return new Tuple<ErrorCodes, LocationV2Dto>(ErrorCodes.UnknownError, null);
            }
        }

        public async Task<Tuple<BusinessLogicMessageCodes.ErrorCodes, LocationV2Dto>> GetByIdFromIntAsync(int id)
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
                            return new Tuple<ErrorCodes, LocationV2Dto>(ErrorCodes.UnknownError, null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<ErrorCodes, LocationV2Dto>(ErrorCodes.UnknownError, null);
                        }
                    default:
                        {
                            return new Tuple<ErrorCodes, LocationV2Dto>(ErrorCodes.UnknownError, null);
                        }
                }
            }
            else
            {
                return new Tuple<ErrorCodes, LocationV2Dto>(ErrorCodes.UnknownError, null);
            }
        }

        public async Task<Tuple<BusinessLogicMessageCodes.ErrorCodes, LocationV2Dto>> InsertAsync(LocationV2Dto insertedDto)
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
                            return new Tuple<ErrorCodes, LocationV2Dto>(ErrorCodes.UnknownError, null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<ErrorCodes, LocationV2Dto>(ErrorCodes.UnknownError, null);
                        }
                    default:
                        {
                            return new Tuple<ErrorCodes, LocationV2Dto>(ErrorCodes.UnknownError, null);
                        }
                }
            }
            else
            {
                return new Tuple<ErrorCodes, LocationV2Dto>(ErrorCodes.UnknownError, null);
            }
        }

        public async Task<Tuple<BusinessLogicMessageCodes.ErrorCodes, LocationV2Dto>> UpdateAsync(LocationV2Dto updatedDto)
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
                            return new Tuple<ErrorCodes, LocationV2Dto>(ErrorCodes.UnknownError, null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<ErrorCodes, LocationV2Dto>(ErrorCodes.UnknownError, null);
                        }
                    default:
                        {
                            return new Tuple<ErrorCodes, LocationV2Dto>(ErrorCodes.UnknownError, null);
                        }
                }
            }
            else
            {
                return new Tuple<ErrorCodes, LocationV2Dto>(ErrorCodes.UnknownError, null);
            }
        }

        public async Task<Tuple<Error, IEnumerable<DevCustomDto.CustomLocationDto>>> GetNodesTicketLocationByIdWithinLevelAsync(BusinessLogicRequest request)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            ILocationV2BusinessRules businessRule =
                                (ILocationV2BusinessRules)_baseLocationBusinessRules;

                            if(request.RequestDomain == RemoteIncomingDomains.DiasTesisYonetimWebClient)
                            {
                                return await businessRule.GetNodesTicketLocationByIdWithinLevelAsync(request);
                            }
                            else
                            {
                                return await businessRule.GetNodesTicketLocationByIdWithinLevelMobileAsync(request);
                            }                                            
                           
                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            return new Tuple<Error, IEnumerable<DevCustomDto.CustomLocationDto>>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, IEnumerable<DevCustomDto.CustomLocationDto>>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    default:
                        {
                            return new Tuple<Error, IEnumerable<DevCustomDto.CustomLocationDto>>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, IEnumerable<DevCustomDto.CustomLocationDto>>(Errors.General.GeneralServerError(), null);
            }
        }

    }
}
