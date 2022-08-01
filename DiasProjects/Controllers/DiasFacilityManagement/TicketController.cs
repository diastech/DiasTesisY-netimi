using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DFManagementStdDtoInterface = DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement;
using DFManagementStdDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using CustomDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using DiasShared.Services.Communication.BusinessLogicMessage;
using DiasShared.Operations.CommunicationOperations.DiasFacilityManagement;
using static DiasShared.Enums.Standart.HttpCodesEnum;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DiasWebApi.DiasFacilityManagement.Controllers
{
    [ApiController]
    public class TicketController : Controller
    {
        private readonly DFManagementStdDtoInterface.ITicketDtoRepository _ticketsRepository;
        private readonly DFManagementStdDtoInterface.ITicketNoteDtoRepository _ticketNotesRepository;
        private readonly DFManagementStdDtoInterface.IAttachmentDtoRepository _attachmentsRepository;

        public TicketController(DFManagementStdDtoInterface.ITicketDtoRepository ticketsRepository,
            DFManagementStdDtoInterface.ITicketNoteDtoRepository ticketNotesRepository,
            DFManagementStdDtoInterface.IAttachmentDtoRepository attachmentsRepository)
        {
            _ticketsRepository = ticketsRepository;
            _ticketNotesRepository = ticketNotesRepository;
            _attachmentsRepository = attachmentsRepository;
        }

        #region ControllerRoute
        [Route("[controller]/GetAll")]
        [HttpGet]
        public async Task<BusinessLogicResponse> GetAllAsync()
        {
            if (!ModelState.IsValid)
            {
                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartMulti<DFManagementStdDto.TicketDto>.
                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<ErrorCodes, object>(ErrorCodes.RequestBodyMalformedOnUndefinedRequest, null),
                                                                                HttpResponseCodes.BadRequest, false);
                return returnResponseClient;
            }


            Tuple<ErrorCodes, IEnumerable<DFManagementStdDto.TicketDto>> serviceResult = await _ticketsRepository.GetAllAsync();

            BusinessLogicResponse returnResponse = ResponseOperationsStandartMulti<DFManagementStdDto.TicketDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<ErrorCodes, object>(serviceResult.Item1, (object)serviceResult.Item2));

            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

            return returnResponse;

        }

        [Route("[controller]/GetById/{id}")]
        [HttpGet]
        public async Task<BusinessLogicResponse> GetByIdFromIntAsync([FromQuery] int id)
        {
            if (!ModelState.IsValid)
            {
                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartMulti<DFManagementStdDto.TicketDto>.
                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<ErrorCodes, object>(ErrorCodes.RequestBodyMalformedOnUndefinedRequest, null),
                                                                                HttpResponseCodes.BadRequest, false);
                return returnResponseClient;
            }


            Tuple<ErrorCodes, DFManagementStdDto.TicketDto> serviceResult = await _ticketsRepository.GetByIdFromIntAsync(id);

            BusinessLogicResponse returnResponse = ResponseOperationsStandartMulti<DFManagementStdDto.TicketDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<ErrorCodes, object>(serviceResult.Item1, (object)serviceResult.Item2));

            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

            return returnResponse;
        }

        [Route("[controller]/Delete/{id}")]
        [HttpGet]
        public async Task<BusinessLogicResponse> DeleteFromIntAsync([FromQuery] int id)
        {
            if (!ModelState.IsValid)
            {
                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartMulti<DFManagementStdDto.TicketDto>.
                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<ErrorCodes, object>(ErrorCodes.RequestBodyMalformedOnUndefinedRequest, null),
                                                                                HttpResponseCodes.BadRequest, false);
                return returnResponseClient;
            }


            Tuple<ErrorCodes, DFManagementStdDto.TicketDto> serviceResult = await _ticketsRepository.DeleteFromIntAsync(id);

            BusinessLogicResponse returnResponse = ResponseOperationsStandartMulti<DFManagementStdDto.TicketDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<ErrorCodes, object>(serviceResult.Item1, (object)serviceResult.Item2));

            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

            return returnResponse;
        }

        [Route("[controller]/Insert")]
        [HttpGet]
        public async Task<BusinessLogicResponse> InsertAsync([FromBody] DFManagementStdDto.TicketDto resource)
        {
            if (!ModelState.IsValid)
            {
                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartMulti<DFManagementStdDto.TicketDto>.
                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<ErrorCodes, object>(ErrorCodes.RequestBodyMalformedOnUndefinedRequest, null),
                                                                                HttpResponseCodes.BadRequest, false);
                return returnResponseClient;
            }


            Tuple<ErrorCodes, DFManagementStdDto.TicketDto> serviceResult = await _ticketsRepository.InsertAsync(resource);

            BusinessLogicResponse returnResponse = ResponseOperationsStandartMulti<DFManagementStdDto.TicketDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<ErrorCodes, object>(serviceResult.Item1, (object)serviceResult.Item2));

            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

            return returnResponse;
        }

        [Route("[controller]/Update")]
        [HttpGet]
        public async Task<BusinessLogicResponse> UpdateAsync([FromBody] DFManagementStdDto.TicketDto resource)
        {
            if (!ModelState.IsValid)
            {
                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartMulti<DFManagementStdDto.TicketDto>.
                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<ErrorCodes, object>(ErrorCodes.RequestBodyMalformedOnUndefinedRequest, null),
                                                                                HttpResponseCodes.BadRequest, false);
                return returnResponseClient;
            }

            Tuple<ErrorCodes, DFManagementStdDto.TicketDto> serviceResult = await _ticketsRepository.UpdateAsync(resource);

            BusinessLogicResponse returnResponse = ResponseOperationsStandartMulti<DFManagementStdDto.TicketDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<ErrorCodes, object>(serviceResult.Item1, (object)serviceResult.Item2));

            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

            return returnResponse;
        }

        #endregion

        #region Controller_ActionRoute
        [Route("[controller]/GetAllTicketsByBasicTicketId/{ticketId}")]
        [HttpGet]
        public async Task<BusinessLogicResponse> GetAllTicketsByBasicTicketIdAsync([FromQuery] int ticketId)
        {
            if ((!ModelState.IsValid) || (ticketId == 0))
            {

                BusinessLogicResponse returnResponseClientError = null;

                return returnResponseClientError;
            }
            Tuple<ErrorCodes, IEnumerable<DFManagementStdDto.TicketDto>> serviceResult = await _ticketsRepository.GetAllTicketsByBasicTicketIdAsync(ticketId);
            BusinessLogicResponse returnResponse = null;

            return returnResponse;
        }

        [Route("[controller]/UpdateTicketState")]
        [HttpPost]
        public async Task<BusinessLogicResponse> UpdateTicketStateAsync([FromBody] CustomDto.CustomTicketDto resource)
        {
            if ((!ModelState.IsValid) || (resource == null))
            {

                BusinessLogicResponse returnResponseClientError = null;

                return returnResponseClientError;
            }
            resource.TicketStatusId = resource.TicketStatusId;
            Tuple<ErrorCodes, DFManagementStdDto.TicketDto> serviceResult = await _ticketsRepository.UpdateTicketStateAsync(resource);
            BusinessLogicResponse returnResponse = null;

            return returnResponse;
        }
        #endregion

    }

}
