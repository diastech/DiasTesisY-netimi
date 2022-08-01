using DiasShared.Operations.CommunicationOperations.DiasFacilityManagement;
using DiasShared.Services.Communication.BusinessLogicMessage;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using static DiasShared.Enums.Standart.HttpCodesEnum;
using DFManagementStdDtoInterface = DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement;
using DFManagementStdDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using Newtonsoft.Json;
using System.Collections.Generic;
using static DiasWebApi.Shared.Enums.WebApiApplicationEnums;
using Microsoft.Extensions.Configuration;
using DiasWebApi.Shared.Configuration;
using DiasShared.Errors;
using static DiasShared.Enums.Standart.RemoteCommunicationEnums;
using DiasBusinessLogic.Shared.Error;

namespace DiasWebApi.DiasFacilityManagement.Controllers
{
    [ApiController]
    public class TicketReasonController : Controller
    {

        private readonly DFManagementStdDtoInterface.ITicketReasonDtoRepository _ticketReasonsRepository;
        

        public TicketReasonController(
            DFManagementStdDtoInterface.ITicketReasonDtoRepository ticketReasonsRepository)
        {
            _ticketReasonsRepository = ticketReasonsRepository;   
        }

        #region ControllerRoute
        [Route("[controller]/GetAll")]
        [HttpGet]
        public async Task<BusinessLogicResponse> GetAllAsync()
        {
            if (!ModelState.IsValid)
            {
                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartMulti<DFManagementStdDto.TicketReasonDto>.
                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<ErrorCodes, object>(ErrorCodes.RequestBodyMalformedOnUndefinedRequest, null),
                                                                                HttpResponseCodes.BadRequest, false);
                return returnResponseClient;
            }


            Tuple<ErrorCodes, IEnumerable<DFManagementStdDto.TicketReasonDto>> serviceResult = await _ticketReasonsRepository.GetAllAsync();

            BusinessLogicResponse returnResponse = ResponseOperationsStandartMulti<DFManagementStdDto.TicketReasonDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<ErrorCodes, object>(serviceResult.Item1, (object)serviceResult.Item2));

            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

            return returnResponse;

        }

        [Route("[controller]/GetById")]
        [HttpPost]
        public async Task<BusinessLogicResponse> GetByIdFromIntAsync([FromBody] BusinessLogicRequest request = null)
        {
            try
            {
                IConfiguration settings = ConfigurationHelper.GetConfig();

                switch (ConfigurationHelper.GetWorkingEnvironment(settings))
                {
                    case ApplicationWorkingEnvironment.Development:
                        {
                            Tuple<Error, DFManagementStdDto.TicketReasonDto> serviceResult = new(Errors.General.None(), new DFManagementStdDto.TicketReasonDto());

                            if (request != null)
                            {
                                switch (request.RequestDomain)
                                {
                                    case RemoteIncomingDomains.DiasTesisYonetimWebClient:
                                        {
                                            if ((request.AdditionalParamTypes != null) && (request.AdditionalParamTypes.Count > 0) &&
                                                 (request.AdditionalParamJsons != null) && (request.AdditionalParamJsons.Count > 0))
                                            {
                                                int id = JsonConvert.DeserializeObject<int>(request.AdditionalParamJsons[0]);
                                                serviceResult = await _ticketReasonsRepository.GetByIdFromIntAsync(id);
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

                                BusinessLogicResponse returnResponse = ResponseOperationsStandartSingle<DFManagementStdDto.TicketReasonDto>.ConvertServiceResultToBusinessLogicResponse(
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
            catch (Exception e)
            {
                throw;
            }
        }

        [Route("[controller]/GetByTicketReasonCategoryId")]
        [HttpPost]
        public async Task<BusinessLogicResponse> GetByTicketReasonCategoryIdAsync([FromBody] BusinessLogicRequest request = null)
        {
            try
            {
                IConfiguration settings = ConfigurationHelper.GetConfig();

                switch (ConfigurationHelper.GetWorkingEnvironment(settings))
                {
                    case ApplicationWorkingEnvironment.Development:
                        {
                            Tuple<Error, IEnumerable<DFManagementStdDto.TicketReasonDto>> serviceResult = new(Errors.General.None(), new List<DFManagementStdDto.TicketReasonDto>());

                            if (request != null)
                            {
                                switch (request.RequestDomain)
                                {
                                    case RemoteIncomingDomains.DiasTesisYonetimWebClient:
                                        {
                                            if ((request.AdditionalParamTypes != null) && (request.AdditionalParamTypes.Count > 0) &&
                                                 (request.AdditionalParamJsons != null) && (request.AdditionalParamJsons.Count > 0))
                                            {
                                                int id = JsonConvert.DeserializeObject<int>(request.AdditionalParamJsons[0]);
                                                serviceResult = await _ticketReasonsRepository.GetByTicketReasonCategoryIdAsync(id);
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

                                BusinessLogicResponse returnResponse = ResponseOperationsStandartMulti<DFManagementStdDto.TicketReasonDto>.ConvertServiceResultToBusinessLogicResponse(
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
            catch (Exception e)
            {
                throw;
            }
        }


        [Route("[controller]/Delete/{id}")]
        [HttpGet]
        public async Task<BusinessLogicResponse> DeleteFromIntAsync([FromQuery] int id)
        {
            if (!ModelState.IsValid)
            {
                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartMulti<DFManagementStdDto.TicketReasonDto>.
                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<ErrorCodes, object>(ErrorCodes.RequestBodyMalformedOnUndefinedRequest, null),
                                                                                HttpResponseCodes.BadRequest, false);
                return returnResponseClient;
            }


            Tuple<ErrorCodes, DFManagementStdDto.TicketReasonDto> serviceResult = await _ticketReasonsRepository.DeleteFromIntAsync(id);

            BusinessLogicResponse returnResponse = ResponseOperationsStandartMulti<DFManagementStdDto.TicketReasonDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<ErrorCodes, object>(serviceResult.Item1, (object)serviceResult.Item2));

            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

            return returnResponse;
        }

        [Route("[controller]/Insert")]
        [HttpGet]
        public async Task<BusinessLogicResponse> InsertAsync([FromBody] DFManagementStdDto.TicketReasonDto resource)
        {
            if (!ModelState.IsValid)
            {
                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartMulti<DFManagementStdDto.TicketReasonDto>.
                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<ErrorCodes, object>(ErrorCodes.RequestBodyMalformedOnUndefinedRequest, null),
                                                                                HttpResponseCodes.BadRequest, false);
                return returnResponseClient;
            }


            Tuple<ErrorCodes, DFManagementStdDto.TicketReasonDto> serviceResult = await _ticketReasonsRepository.InsertAsync(resource);

            BusinessLogicResponse returnResponse = ResponseOperationsStandartMulti<DFManagementStdDto.TicketReasonDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<ErrorCodes, object>(serviceResult.Item1, (object)serviceResult.Item2));

            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

            return returnResponse;
        }

        [Route("[controller]/Update")]
        [HttpGet]
        public async Task<BusinessLogicResponse> UpdateAsync([FromBody] DFManagementStdDto.TicketReasonDto resource)
        {
            if (!ModelState.IsValid)
            {
                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartMulti<DFManagementStdDto.TicketReasonDto>.
                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<ErrorCodes, object>(ErrorCodes.RequestBodyMalformedOnUndefinedRequest, null),
                                                                                HttpResponseCodes.BadRequest, false);
                return returnResponseClient;
            }

            Tuple<ErrorCodes, DFManagementStdDto.TicketReasonDto> serviceResult = await _ticketReasonsRepository.UpdateAsync(resource);

            BusinessLogicResponse returnResponse = ResponseOperationsStandartMulti<DFManagementStdDto.TicketReasonDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<ErrorCodes, object>(serviceResult.Item1, (object)serviceResult.Item2));

            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

            return returnResponse;
        }

        #endregion

        #region Controller_ActionRoute

        #endregion

    }

    
}
