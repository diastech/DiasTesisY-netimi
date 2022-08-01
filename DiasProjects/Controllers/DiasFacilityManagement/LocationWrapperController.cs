using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DFManagementStdDtoInterface = DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement;
using DFManagementCustomDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using DFManagementCustomTestDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Custom;
using DiasShared.Services.Communication.BusinessLogicMessage;
using DiasShared.Operations.CommunicationOperations.DiasFacilityManagement;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DiasWebApi.Shared.Configuration;
using static DiasWebApi.Shared.Enums.WebApiApplicationEnums;
using static DiasShared.Enums.Standart.RemoteCommunicationEnums;
using DiasBusinessLogic.Shared.Error;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom.WorkOrder;
using DiasShared.Errors;
using DiasShared.Operations.CommunicationOperations;
using static DiasShared.Enums.Standart.HttpCodesEnum;

namespace DiasWebApi.DiasFacilityManagement.Controllers
{
    [ApiController]
    public class LocationWrapperController : Controller
    {
        private readonly DFManagementStdDtoInterface.ILocationWrapperDtoRepository _locationWrapperDtoRepository;

        public LocationWrapperController(DFManagementStdDtoInterface.ILocationWrapperDtoRepository locationWrapperDtoRepository)
        {
            _locationWrapperDtoRepository = locationWrapperDtoRepository;
        }

        #region ControllerRoute
        [Route("[controller]/GetAll")]
        [HttpPost]
        public async Task<BusinessLogicResponse> GetAllLocationsWrapperAsync([FromBody] BusinessLogicRequest request = null)
        {
            try
            {
                IConfiguration settings = ConfigurationHelper.GetConfig();

                switch (ConfigurationHelper.GetWorkingEnvironment(settings))
                {
                    case ApplicationWorkingEnvironment.Development:
                        {
                            Tuple<Error, IEnumerable<CustomLocationDtoForMobileApi>> serviceResult = new(Errors.General.None(), new List<CustomLocationDtoForMobileApi>());
                            if (request != null)
                            {
                                switch (request.RequestDomain)
                                {
                                    case RemoteIncomingDomains.DiasTesisYonetimWebClient:
                                        {
                                            serviceResult = await _locationWrapperDtoRepository.GetAllLocationsWrapperAsync();
                                            break;
                                        }
                                    case RemoteIncomingDomains.DiasTesisYonetimMobileClient:
                                        {
                                            throw new NotImplementedException();
                                        }
                                    default:
                                        {
                                            throw new ArgumentException();
                                        }
                                }
                                BusinessLogicResponse returnResponse = ResponseOperationsCustomMulti<CustomLocationDtoForMobileApi>.ConvertServiceResultToBusinessLogicResponse(
                                                                    new Tuple<Error, object>(serviceResult.Item1, (object)serviceResult.Item2));

                                /**
                                 * Performans iyileştirmesi için yoruma alındı (VOLKAN)
                                 */
                                //returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);                     
                                return returnResponse;
                            }
                            else
                            {
                                throw new ArgumentException();
                            }

                        }

                    case ApplicationWorkingEnvironment.Test:
                        {
                            Tuple<ErrorCodes, IEnumerable<DFManagementCustomTestDto.CustomLocationDto>> serviceResult = new(ErrorCodes.None, new List<DFManagementCustomTestDto.CustomLocationDto>());
                            if (request != null)
                            {
                                switch (request.RequestDomain)
                                {
                                    case RemoteIncomingDomains.DiasTesisYonetimWebClient:
                                        {
                                            serviceResult = await _locationWrapperDtoRepository.GetAllLocationsWrapperTestAsync();
                                            break;
                                        }
                                    case RemoteIncomingDomains.DiasTesisYonetimMobileClient:
                                        {
                                            throw new NotImplementedException();
                                        }
                                    default:
                                        {
                                            throw new ArgumentException();
                                        }
                                }
                                BusinessLogicResponse returnResponse = ResponseOperationsCustomMultiTest<DFManagementCustomTestDto.CustomTicketReasonCategoryDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                    new Tuple<ErrorCodes, object>(serviceResult.Item1, (object)serviceResult.Item2));
                                returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);
                                return returnResponse;
                            }
                            else
                            {
                                throw new ArgumentException();
                            }


                        }

                    case ApplicationWorkingEnvironment.Live:
                        {
                            throw new NotImplementedException();
                        }

                    default:
                        {
                            throw new NotImplementedException();
                        }
                }





            }
            catch (Exception e)
            {

                throw;
            }
        }

        [Route("[controller]/GetById")]
        [HttpPost]
        public async Task<BusinessLogicResponse> GetLocationWrapperByIdAsync([FromBody] BusinessLogicRequest request = null)
        {
            try
            {
                IConfiguration settings = ConfigurationHelper.GetConfig();

                switch (ConfigurationHelper.GetWorkingEnvironment(settings))
                {
                    case ApplicationWorkingEnvironment.Development:
                        {
                            Tuple<Error, DFManagementCustomDto.CustomLocationDto> serviceResult = new(Errors.General.None(), new DFManagementCustomDto.CustomLocationDto());

                            if (request != null)
                            {
                                switch (request.RequestDomain)
                                {
                                    case RemoteIncomingDomains.DiasTesisYonetimWebClient:
                                        {
                                            if ((request.AdditionalParamTypes != null) && (request.AdditionalParamTypes.Count > 0) &&
                                                 (request.AdditionalParamJsons != null) && (request.AdditionalParamJsons.Count > 0))
                                            {
                                                string hierarchyIdStr = JsonConvert.DeserializeObject<string>(request.AdditionalParamJsons[0]);
                                                serviceResult = await _locationWrapperDtoRepository.GetLocationWrapperByIdAsync(hierarchyIdStr);
                                            }
                                            else
                                            {
                                                throw new ArgumentException();
                                            }

                                            break;
                                        }
                                    case RemoteIncomingDomains.DiasTesisYonetimMobileClient:
                                        {
                                            throw new NotImplementedException();
                                        }
                                    default:
                                        {
                                            throw new ArgumentException();
                                        }
                                }

                                BusinessLogicResponse returnResponse = ResponseOperationsCustomSingle<DFManagementCustomDto.CustomLocationDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                    new Tuple<Error, object>(serviceResult.Item1, (object)serviceResult.Item2));

                                returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);
                                return returnResponse;
                            }
                            else
                            {
                                throw new ArgumentException();
                            }
                        }

                    case ApplicationWorkingEnvironment.Test:
                        {
                            throw new ArgumentException();
                        }

                    case ApplicationWorkingEnvironment.Live:
                        {
                            throw new NotImplementedException();
                        }

                    default:
                        {
                            throw new NotImplementedException();
                        }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Controller_ActionRoute

        [Route("[controller]/GetByIdLastNodes")]
        [HttpPost]
        public async Task<BusinessLogicResponse> GetTicketLocationWrapperLastNodesByIdAsync([FromBody] BusinessLogicRequest request = null)
        {
            try
            {
                IConfiguration settings = ConfigurationHelper.GetConfig();

                switch (ConfigurationHelper.GetWorkingEnvironment(settings))
                {
                    case ApplicationWorkingEnvironment.Development:
                        {
                            Tuple<Error, IEnumerable<DFManagementCustomDto.CustomLocationDto>> serviceResult =
                                                new(Errors.General.None(), new List<DFManagementCustomDto.CustomLocationDto>());

                            if (request != null)
                            {
                                switch (request.RequestDomain)
                                {
                                    case RemoteIncomingDomains.DiasTesisYonetimWebClient:
                                        {
                                            if ((request.AdditionalParamTypes != null) && (request.AdditionalParamTypes.Count > 0) &&
                                                 (request.AdditionalParamJsons != null) && (request.AdditionalParamJsons.Count > 0))
                                            {
                                                string hierarchyIdStr = JsonConvert.DeserializeObject<string>(request.AdditionalParamJsons[0]);
                                                serviceResult = await _locationWrapperDtoRepository.GetTicketLocationWrapperLastNodeByIdAsync(hierarchyIdStr);
                                            }
                                            else
                                            {
                                                throw new ArgumentException();
                                            }

                                            break;
                                        }
                                    case RemoteIncomingDomains.DiasTesisYonetimMobileClient:
                                        {
                                            throw new NotImplementedException();
                                        }
                                    default:
                                        {
                                            throw new ArgumentException();
                                        }
                                }

                                BusinessLogicResponse returnResponse = ResponseOperationsCustomMulti<DFManagementCustomDto.CustomLocationDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                            new Tuple<Error, object>(serviceResult.Item1, (object)serviceResult.Item2));

                                returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);
                                return returnResponse;
                            }
                            else
                            {
                                throw new ArgumentException();
                            }

                        }

                    case ApplicationWorkingEnvironment.Test:
                        {
                            throw new ArgumentException();
                        }

                    case ApplicationWorkingEnvironment.Live:
                        {
                            throw new NotImplementedException();
                        }

                    default:
                        {
                            throw new NotImplementedException();
                        }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Route("[controller]/GetByNfcCode/{nfcCode}")]
        [HttpPost]
        public async Task<BusinessLogicResponse> GetLocationWrapperByNfcCodeAsync(string nfcCode)
        {
            if (!ModelState.IsValid)
            {
                BusinessLogicResponse returnResponseClient = ResponseOperationsCustomSingle<DFManagementCustomDto.CustomLocationDto>.
                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<Error, object>(Errors.General.GeneralServerError(), null),
                                                                                HttpResponseCodes.BadRequest, false);
                return returnResponseClient;
            }


            Tuple<Error, DFManagementCustomDto.CustomLocationDto> serviceResult = await _locationWrapperDtoRepository.GetLocationWrapperByNfcCodeAsync(nfcCode);

            BusinessLogicResponse returnResponse = ResponseOperationsCustomSingle<DFManagementCustomDto.CustomLocationDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<Error, object>(serviceResult.Item1, (object)serviceResult.Item2));

            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

            return returnResponse;
        }

        #endregion

    }


}
