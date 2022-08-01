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
using DiasShared.Services.Communication.BusinessLogicMessage.ThirdPartyLibrary.DevExpress;
using DiasShared.Services.Communication.BusinessLogicMessage;
using TestDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Custom;
using BusinessWrapperTestInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Test.Custom;
using DiasShared.Errors;
using DiasBusinessLogic.Shared.Error;
using DiasShared.Classes.Dto;

namespace DiasWebApi.Repositories.DiasFacilityManagement
{
    public class BasicTicketWrapperDtoRepository : IBasicTicketWrapperDtoRepository
    {
        private IBaseBasicTicketWrapperBusinessRules _baseBasicTicketWrapperBusinessRules;
        private ApplicationBusinessLogicEnvironment _applicationBusinessLogicEnvironment;
        private IBaseBasicTicketWrapperTransactionalBusinessRules _baseBasicTicketWrapperTransactionalBusinessRules;
        private bool businessLogicContainerStatus { get; set; }
        public BasicTicketWrapperDtoRepository(IBaseBasicTicketWrapperBusinessRules baseBasicTicketWrapperBusinessRules,
            IBaseBasicTicketWrapperTransactionalBusinessRules baseBasicTicketWrapperTransactionalBusinessRules,
            ApplicationBusinessLogicEnvironment applicationBusinessLogicEnvironment)
        {
            businessLogicContainerStatus = SimpleInjectorContainerOperations.VerifyContainer();
            if (businessLogicContainerStatus)
            {
                _baseBasicTicketWrapperBusinessRules = baseBasicTicketWrapperBusinessRules;
                _baseBasicTicketWrapperTransactionalBusinessRules = baseBasicTicketWrapperTransactionalBusinessRules;
            }       
        }
        
        #region WebClient

        #region Development
        public async Task<Tuple<Error, DevExpressLoadResultDto<IEnumerable<CustomBasicTicketDto>>>> GetAllBasicTicketsWrapperAsync(DevExpressRequest devExpressRequestObj)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            IBasicTicketWrapperBusinessRules businessRule =
                                (IBasicTicketWrapperBusinessRules)_baseBasicTicketWrapperBusinessRules;
                            return await businessRule.GetAllBasicTicketsWrapperAsync(devExpressRequestObj);
                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            return new Tuple<Error, DevExpressLoadResultDto<IEnumerable<CustomBasicTicketDto>>>(Errors.General.NotFoundDatabaseServer(), null);                            
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, DevExpressLoadResultDto<IEnumerable<CustomBasicTicketDto>>>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    default:
                        {
                            return new Tuple<Error, DevExpressLoadResultDto<IEnumerable<CustomBasicTicketDto>>>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, DevExpressLoadResultDto<IEnumerable<CustomBasicTicketDto>>>(Errors.General.GeneralServerError(), null);
            }
        }

        public async Task<Tuple<Error, CustomBasicTicketDto>> GetBasicTicketWrapperByIdAsync(int Id)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            IBasicTicketWrapperBusinessRules businessRule =
                                (IBasicTicketWrapperBusinessRules)_baseBasicTicketWrapperBusinessRules;
                            return await businessRule.GetBasicTicketWrapperByIdAsync(Id);
                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            return new Tuple<Error, CustomBasicTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, CustomBasicTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    default:
                        {
                            return new Tuple<Error, CustomBasicTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, CustomBasicTicketDto>(Errors.General.GeneralServerError(), null);
            }
        }

        public async Task<Tuple<Error, CustomBasicTicketDto>> AddBasicTicketWrapperAsync(BusinessLogicRequest request)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            IBasicTicketWrapperTransactionalBusinessRules businessRule =
                                (IBasicTicketWrapperTransactionalBusinessRules)_baseBasicTicketWrapperTransactionalBusinessRules;
                            return await businessRule.AddBasicTicketWrapperAsync(request);
                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            return new Tuple<Error, CustomBasicTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, CustomBasicTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                    default:
                        {
                            return new Tuple<Error, CustomBasicTicketDto>(Errors.General.NotFoundDatabaseServer(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, CustomBasicTicketDto>(Errors.General.GeneralServerError(), null);
            }
        }

        public Task<Tuple<Error, CustomBasicTicketDto>> UpdateBasicTicketWrapperAsync(CustomBasicTicketDto customBasicTicketDto)
        {
            throw new NotImplementedException();
        }

        public Task<Tuple<Error, CustomBasicTicketDto>> DeleteBasicTicketWrapperAsync(CustomBasicTicketDto customBasicTicketDto)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Test
        public async Task<Tuple<ErrorCodes, TestDto.CustomBasicTicketDto>> AddBasicTicketWrappertestAsync(BusinessLogicRequest request)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            return new Tuple<ErrorCodes, TestDto.CustomBasicTicketDto>(ErrorCodes.UnknownError, null);

                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            BusinessWrapperTestInterface.IBasicTicketWrapperTransactionalBusinessRules businessRule =
                                (BusinessWrapperTestInterface.IBasicTicketWrapperTransactionalBusinessRules)_baseBasicTicketWrapperTransactionalBusinessRules;
                            return await businessRule.AddBasicTicketWrapperAsync(request);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<ErrorCodes, TestDto.CustomBasicTicketDto>(ErrorCodes.UnknownError, null);
                        }
                    default:
                        {
                            return new Tuple<ErrorCodes, TestDto.CustomBasicTicketDto>(ErrorCodes.UnknownError, null);
                        }
                }
            }
            else
            {
                return new Tuple<ErrorCodes, TestDto.CustomBasicTicketDto>(ErrorCodes.UnknownError, null);
            }
        }

        public async Task<Tuple<ErrorCodes, TestDto.CustomBasicTicketDto>> GetBasicTicketWrapperByIdTestAsync(int Id)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            return new Tuple<ErrorCodes, TestDto.CustomBasicTicketDto>(ErrorCodes.UnknownError, null);

                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            BusinessWrapperTestInterface.IBasicTicketWrapperBusinessRules businessRule =
                                (BusinessWrapperTestInterface.IBasicTicketWrapperBusinessRules)_baseBasicTicketWrapperBusinessRules;

                            return await businessRule.GetBasicTicketWrapperByIdAsync(Id);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<ErrorCodes, TestDto.CustomBasicTicketDto>(ErrorCodes.UnknownError, null);
                        }
                    default:
                        {
                            return new Tuple<ErrorCodes, TestDto.CustomBasicTicketDto>(ErrorCodes.UnknownError, null);
                        }
                }
            }
            else
            {
                return new Tuple<ErrorCodes, TestDto.CustomBasicTicketDto>(ErrorCodes.UnknownError, null);
            }
        }

        public async Task<Tuple<ErrorCodes, IEnumerable<TestDto.CustomBasicTicketDto>>> GetAllBasicTicketsWrapperTestAsync(DevExpressRequest devExpressRequestObj)
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            return new Tuple<ErrorCodes, IEnumerable<TestDto.CustomBasicTicketDto>>(ErrorCodes.UnknownError, null);

                        }
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            BusinessWrapperTestInterface.IBasicTicketWrapperBusinessRules businessRule =
                                (BusinessWrapperTestInterface.IBasicTicketWrapperBusinessRules)_baseBasicTicketWrapperBusinessRules;

                            return await businessRule.GetAllBasicTicketsWrapperAsync(devExpressRequestObj);
                        }
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<ErrorCodes, IEnumerable<TestDto.CustomBasicTicketDto>>(ErrorCodes.UnknownError, null);
                        }
                    default:
                        {
                            return new Tuple<ErrorCodes, IEnumerable<TestDto.CustomBasicTicketDto>>(ErrorCodes.UnknownError, null);
                        }
                }
            }
            else
            {
                return new Tuple<ErrorCodes, IEnumerable<TestDto.CustomBasicTicketDto>>(ErrorCodes.UnknownError, null);
            }
        }
        #endregion

        #endregion

        #region Mobile

        #endregion
    }
}
