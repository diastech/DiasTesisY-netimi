using DiasShared.Services.Communication.BusinessLogicMessage;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DFManagementStdDtoInterface = DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement;
using DFManagementStdDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using Newtonsoft.Json;
using DiasShared.Operations.CommunicationOperations.DiasFacilityManagement;
using static DiasShared.Enums.Standart.HttpCodesEnum;
using System.Collections.Generic;

namespace DiasWebApi.DiasFacilityManagement.Controllers
{
    [ApiController]
    public class BasicTicketController : Controller
    {

        private readonly DFManagementStdDtoInterface.IBasicTicketDtoRepository _basicTicketsRepository;        
        private readonly DFManagementStdDtoInterface.ITicketStateTransitionDtoRepository _ticketStateTransitionsRepository;
        private readonly DFManagementStdDtoInterface.ITicketNoteDtoRepository _ticketNotesRepository;

        public BasicTicketController(DFManagementStdDtoInterface.IBasicTicketDtoRepository basicTicketsRepository,
            DFManagementStdDtoInterface.ITicketStateDtoRepository ticketStatesRepository,
            DFManagementStdDtoInterface.ITicketStateTransitionDtoRepository ticketStateTransitionsRepository,
            DFManagementStdDtoInterface.ITicketNoteDtoRepository ticketNotesRepository)
        {
            _basicTicketsRepository = basicTicketsRepository;            
            _ticketStateTransitionsRepository = ticketStateTransitionsRepository;
            _ticketNotesRepository = ticketNotesRepository;
        }

        #region ControllerRoute
        [Route("[controller]/GetAll")]
        [HttpGet]
        public async Task<BusinessLogicResponse> GetAllAsync()
        {
            if (!ModelState.IsValid)
            {
                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartMulti<DFManagementStdDto.BasicTicketDto>.
                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<ErrorCodes, object>(ErrorCodes.RequestBodyMalformedOnUndefinedRequest, null),
                                                                                HttpResponseCodes.BadRequest, false);
                return returnResponseClient;
            }


            Tuple<ErrorCodes, IEnumerable<DFManagementStdDto.BasicTicketDto>> serviceResult = await _basicTicketsRepository.GetAllAsync();

            BusinessLogicResponse returnResponse = ResponseOperationsStandartMulti<DFManagementStdDto.BasicTicketDto>.ConvertServiceResultToBusinessLogicResponse(
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
                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartMulti<DFManagementStdDto.BasicTicketDto>.
                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<ErrorCodes, object>(ErrorCodes.RequestBodyMalformedOnUndefinedRequest, null),
                                                                                HttpResponseCodes.BadRequest, false);
                return returnResponseClient;
            }


            Tuple<ErrorCodes, DFManagementStdDto.BasicTicketDto> serviceResult = await _basicTicketsRepository.GetByIdFromIntAsync(id);

            BusinessLogicResponse returnResponse = ResponseOperationsStandartMulti<DFManagementStdDto.BasicTicketDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<ErrorCodes, object>(serviceResult.Item1, (object)serviceResult.Item2));

            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

            return returnResponse;
        }

        [Route("[controller]/Delete/{id}")]
        [HttpPost]
        public async Task<BusinessLogicResponse> DeleteFromIntAsync([FromQuery] int id)
        {
            if (!ModelState.IsValid)
            {
                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartMulti<DFManagementStdDto.BasicTicketDto>.
                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<ErrorCodes, object>(ErrorCodes.RequestBodyMalformedOnUndefinedRequest, null),
                                                                                HttpResponseCodes.BadRequest, false);
                return returnResponseClient;
            }


            Tuple<ErrorCodes, DFManagementStdDto.BasicTicketDto> serviceResult = await _basicTicketsRepository.DeleteFromIntAsync(id);

            BusinessLogicResponse returnResponse = ResponseOperationsStandartMulti<DFManagementStdDto.BasicTicketDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<ErrorCodes, object>(serviceResult.Item1, (object)serviceResult.Item2));

            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

            return returnResponse;
        }

        [Route("[controller]/Insert")]
        [HttpPost]
        public async Task<BusinessLogicResponse> InsertAsync([FromBody] DFManagementStdDto.BasicTicketDto resource)
        {
            if (!ModelState.IsValid)
            {
                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartMulti<DFManagementStdDto.BasicTicketDto>.
                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<ErrorCodes, object>(ErrorCodes.RequestBodyMalformedOnUndefinedRequest, null),
                                                                                HttpResponseCodes.BadRequest, false);
                return returnResponseClient;
            }


            Tuple<ErrorCodes, DFManagementStdDto.BasicTicketDto> serviceResult = await _basicTicketsRepository.InsertAsync(resource);

            BusinessLogicResponse returnResponse = ResponseOperationsStandartMulti<DFManagementStdDto.BasicTicketDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<ErrorCodes, object>(serviceResult.Item1, (object)serviceResult.Item2));

            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

            return returnResponse;
        }

        [Route("[controller]/Update")]
        [HttpPost]
        public async Task<BusinessLogicResponse> UpdateAsync([FromBody] DFManagementStdDto.BasicTicketDto resource)
        {
            if (!ModelState.IsValid)
            {
                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartMulti<DFManagementStdDto.BasicTicketDto>.
                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<ErrorCodes, object>(ErrorCodes.RequestBodyMalformedOnUndefinedRequest, null),
                                                                                HttpResponseCodes.BadRequest, false);
                return returnResponseClient;
            }

            Tuple<ErrorCodes, DFManagementStdDto.BasicTicketDto> serviceResult = await _basicTicketsRepository.UpdateAsync(resource);

            BusinessLogicResponse returnResponse = ResponseOperationsStandartMulti<DFManagementStdDto.BasicTicketDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<ErrorCodes, object>(serviceResult.Item1, (object)serviceResult.Item2));

            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

            return returnResponse;
        }


        #endregion

        #region Controller_ActionRoute
        [Route("[controller]/GetAllBasicTicketsByUserId/{userId}")]
        [HttpGet]
        public async Task<BusinessLogicResponse> GetAllBasicTicketsByUserIdAsync([FromQuery] int userId)
        {
            if ((!ModelState.IsValid) || (userId == 0))
            {

                BusinessLogicResponse returnResponseClientError = null;

                return returnResponseClientError;
            }
            Tuple<ErrorCodes, IEnumerable<DFManagementStdDto.BasicTicketDto>> serviceResult = await _basicTicketsRepository.GetAllBasicTicketsByUserIdAsync(userId);
            BusinessLogicResponse returnResponse = null;

            return returnResponse;
        }
        #endregion

    }

    
}
