using DiasShared.Services.Communication.BusinessLogicMessage;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DFManagementStdDtoInterface = DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement;
using DFManagementStdDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using DFManagementStdTestDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard;
using DiasShared.Operations.CommunicationOperations.DiasFacilityManagement;
using static DiasShared.Enums.Standart.HttpCodesEnum;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using DiasWebApi.Shared.Configuration;
using Microsoft.Extensions.Configuration;
using static DiasWebApi.Shared.Enums.WebApiApplicationEnums;
using DiasShared.Operations.CommunicationOperations;

namespace DiasWebApi.DiasFacilityManagement.Controllers
{
    [ApiController]
    public class AttachmentController : Controller
    {

        private readonly DFManagementStdDtoInterface.IAttachmentDtoRepository _attachmentsRepository;

        public AttachmentController(DFManagementStdDtoInterface.IAttachmentDtoRepository attachmentsRepository)
        {
            _attachmentsRepository = attachmentsRepository;
        }

        #region ControllerRoute

        [Route("[controller]/GetAll")]
        [HttpGet]
        public async Task<BusinessLogicResponse> GetAllAsync()
        {
            IConfiguration settings = ConfigurationHelper.GetConfig();

            switch (ConfigurationHelper.GetWorkingEnvironment(settings))
            {
                case ApplicationWorkingEnvironment.Development:
                    {
                        if (!ModelState.IsValid)
                        {
                            BusinessLogicResponse returnResponseClient = ResponseOperationsStandartMulti<DFManagementStdDto.AttachmentDto>.
                                                                                    ConvertServiceResultToBusinessLogicResponse(new Tuple<ErrorCodes, object>(ErrorCodes.RequestBodyMalformedOnUndefinedRequest, null),
                                                                                            HttpResponseCodes.BadRequest, false);
                            return returnResponseClient;
                        }

                        Tuple<ErrorCodes, IEnumerable<DFManagementStdDto.AttachmentDto>> serviceResult = await _attachmentsRepository.GetAllAsync();

                        BusinessLogicResponse returnResponse = ResponseOperationsStandartMulti<DFManagementStdDto.AttachmentDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                            new Tuple<ErrorCodes, object>(serviceResult.Item1, (object)serviceResult.Item2));

                        returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

                        return returnResponse;
                   }

                case ApplicationWorkingEnvironment.Test:
                    {
                        if (!ModelState.IsValid)
                        {
                            BusinessLogicResponse returnResponseClientTest = ResponseOperationsStandartMultiTest<DFManagementStdTestDto.AttachmentDto>.
                                                                                    ConvertServiceResultToBusinessLogicResponse(new Tuple<ErrorCodes, object>(ErrorCodes.RequestBodyMalformedOnUndefinedRequest, null),
                                                                                            HttpResponseCodes.BadRequest, false);
                            return returnResponseClientTest;
                        }

                        Tuple<ErrorCodes, IEnumerable<DFManagementStdTestDto.AttachmentDto>> serviceResultTest = await _attachmentsRepository.GetAllTestAsync();

                        BusinessLogicResponse returnResponseTest = ResponseOperationsStandartMultiTest<DFManagementStdTestDto.AttachmentDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                            new Tuple<ErrorCodes, object>(serviceResultTest.Item1, (object)serviceResultTest.Item2));

                        returnResponseTest.OptionalJsonResult = JsonConvert.SerializeObject(serviceResultTest.Item2);

                        return returnResponseTest;
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

        [Route("[controller]/GetById/{id}")]
        [HttpPost]
        public async Task<BusinessLogicResponse> GetByIdFromIntAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartMulti<DFManagementStdDto.AttachmentDto>.
                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<ErrorCodes, object>(ErrorCodes.RequestBodyMalformedOnUndefinedRequest, null),
                                                                                HttpResponseCodes.BadRequest, false);
                return returnResponseClient;
            }


            Tuple<ErrorCodes, DFManagementStdDto.AttachmentDto> serviceResult = await _attachmentsRepository.GetByIdFromIntAsync(id);

            BusinessLogicResponse returnResponse = ResponseOperationsStandartMulti<DFManagementStdDto.AttachmentDto>.ConvertServiceResultToBusinessLogicResponse(
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
                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartMulti<DFManagementStdDto.AttachmentDto>.
                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<ErrorCodes, object>(ErrorCodes.RequestBodyMalformedOnUndefinedRequest, null),
                                                                                HttpResponseCodes.BadRequest, false);
                return returnResponseClient;
            }

            Tuple<ErrorCodes, DFManagementStdDto.AttachmentDto> serviceResult = await _attachmentsRepository.DeleteFromIntAsync(id);

            BusinessLogicResponse returnResponse = ResponseOperationsStandartMulti<DFManagementStdDto.AttachmentDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<ErrorCodes, object>(serviceResult.Item1, (object)serviceResult.Item2));

            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

            return returnResponse;
        }

        [Route("[controller]/Insert")]
        [HttpPost]
        public async Task<BusinessLogicResponse> InsertAsync([FromBody] BusinessLogicRequest request = null)
        {
            if (!ModelState.IsValid)
            {
                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartMulti<DFManagementStdDto.AttachmentDto>.
                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<ErrorCodes, object>(ErrorCodes.RequestBodyMalformedOnUndefinedRequest, null),
                                                                                HttpResponseCodes.BadRequest, false);
                return returnResponseClient;
            }

            if ((request == null) || (request.RequestDtosTypes == null) || (request.RequestDtosJsons == null) ||
                                          (request.RequestDtosTypes.Count < 1) || (request.RequestDtosJsons.Count < 1) ||
                                                (!Type.Equals(request.RequestDtosTypes[0], typeof(DFManagementStdDto.AttachmentDto))))
            {
                return null;
                //return new Tuple<ErrorCodes, DFManagementStdDto.AttachmentDto>(ErrorCodes.NonCrudError, null);
            }
            DFManagementStdDto.AttachmentDto castedDto = JsonConvert.DeserializeObject<DFManagementStdDto.AttachmentDto>(request.RequestDtosJsons[0]);

            if (castedDto == null)
            {
                return null;
                //return new Tuple<Error, CustomTicketDto>(Errors.General.MapingError(), null);
            }


            Tuple<ErrorCodes, DFManagementStdDto.AttachmentDto> serviceResult = await _attachmentsRepository.InsertAsync(castedDto);

            BusinessLogicResponse returnResponse = ResponseOperationsStandartMulti<DFManagementStdDto.AttachmentDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<ErrorCodes, object>(serviceResult.Item1, (object)serviceResult.Item2));
            
            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

            return returnResponse;
        }

        [Route("[controller]/Update")]
        [HttpGet]
        public async Task<BusinessLogicResponse> UpdateAsync([FromBody] DFManagementStdDto.AttachmentDto resource)
        {
            if (!ModelState.IsValid)
            {
                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartMulti<DFManagementStdDto.AttachmentDto>.
                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<ErrorCodes, object>(ErrorCodes.RequestBodyMalformedOnUndefinedRequest, null),
                                                                                HttpResponseCodes.BadRequest, false);
                return returnResponseClient;
            }

            Tuple<ErrorCodes, DFManagementStdDto.AttachmentDto> serviceResult = await _attachmentsRepository.UpdateAsync(resource);

            BusinessLogicResponse returnResponse = ResponseOperationsStandartMulti<DFManagementStdDto.AttachmentDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<ErrorCodes, object>(serviceResult.Item1, (object)serviceResult.Item2));

            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

            return returnResponse;
        }
        #endregion

        #region Controller_ActionRoute
        [Route("[controller]/GetAllTicketAttachmentsByTicketId/{ticketId}")]
        [HttpGet]
        public async Task<BusinessLogicResponse> GetAllTicketAttachmentsByTicketIdAsync([FromQuery] int ticketId)
        {
            if ((!ModelState.IsValid) || (ticketId == 0))
            {

                BusinessLogicResponse returnResponseClientError = null;

                return returnResponseClientError;
            }
            Tuple<ErrorCodes, IEnumerable<DFManagementStdDto.AttachmentDto>> serviceResult = await _attachmentsRepository.GetAllTicketAttachmentsByTicketIdAsync(ticketId);

            BusinessLogicResponse returnResponse = null;

            return returnResponse;
        }

        [Route("[controller]/GetAllNoteAttachmentsByNoteId/{noteId}")]
        [HttpGet]
        public async Task<BusinessLogicResponse> GetAllNoteAttachmentsByNoteIdAsync([FromQuery] int ticketNoteId)
        {
            if ((!ModelState.IsValid) || (ticketNoteId == 0))
            {

                BusinessLogicResponse returnResponseClientError = null;

                return returnResponseClientError;
            }
            Tuple<ErrorCodes, IEnumerable<DFManagementStdDto.AttachmentDto>> serviceResult = await _attachmentsRepository.GetAllNoteAttachmentsByNoteIdAsync(ticketNoteId);
            BusinessLogicResponse returnResponse = null;

            return returnResponse;
        }

        [Route("[controller]/GetAllTicketAttachmentsByBasicTicketId/{basicTicketId}")]
        [HttpGet]
        public async Task<BusinessLogicResponse> GetAllTicketAttachmentsByBasicTicketIdAsync([FromQuery] int basicTicketId)
        {
            if ((!ModelState.IsValid) || (basicTicketId == 0))
            {

                BusinessLogicResponse returnResponseClientError = null;

                return returnResponseClientError;
            }
            Tuple<ErrorCodes, IEnumerable<DFManagementStdDto.AttachmentDto>> serviceResult = await _attachmentsRepository.GetAllTicketAttachmentsByBasicTicketIdAsync(basicTicketId);
            BusinessLogicResponse returnResponse = null;

            return returnResponse;
        }
        #endregion

    }

    
}
