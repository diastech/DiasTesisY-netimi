using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DFManagementStdDtoInterface = DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement;
using DFManagementStdDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using DFManagementStdTestDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard;
using DiasShared.Services.Communication.BusinessLogicMessage;
using DiasShared.Operations.CommunicationOperations.DiasFacilityManagement;
using static DiasShared.Enums.Standart.HttpCodesEnum;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DiasWebApi.Shared.Configuration;
using static DiasWebApi.Shared.Enums.WebApiApplicationEnums;
using static DiasShared.Enums.Standart.RemoteCommunicationEnums;
using DiasShared.Operations.CommunicationOperations;
using DiasShared.Errors;
using DiasBusinessLogic.Shared.Error;

namespace DiasWebApi.DiasFacilityManagement.Controllers
{
    [ApiController]
    public class BasicTicketStateController : Controller
    {

        private readonly DFManagementStdDtoInterface.IBasicTicketStateDtoRepository _basicTicketStateDtoRepository;

        public BasicTicketStateController(
            DFManagementStdDtoInterface.IBasicTicketStateDtoRepository basicTicketStateDtoRepository)
        {
            _basicTicketStateDtoRepository = basicTicketStateDtoRepository;
        }

        #region ControllerRoute

        //TODO : Hata kodları özelleştirilecek
        [Route("[controller]/GetAll")]
        [HttpPost]
        public async Task<BusinessLogicResponse> GetAllAsync([FromBody] BusinessLogicRequest request = null)
        {
            try
            {
                IConfiguration settings = ConfigurationHelper.GetConfig();

                switch (ConfigurationHelper.GetWorkingEnvironment(settings))
                {
                    case ApplicationWorkingEnvironment.Development:
                        {
                            if (!ModelState.IsValid)
                            {
                                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartMulti<DFManagementStdDto.BasicTicketStateDto>.
                                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<Error, object>(Errors.General.GeneralServerError(), null),
                                                                                                HttpResponseCodes.BadRequest, false);
                                return returnResponseClient;
                            }


                            Tuple<Error, IEnumerable<DFManagementStdDto.BasicTicketStateDto>> serviceResult = await _basicTicketStateDtoRepository.GetAllAsync();

                            BusinessLogicResponse returnResponse = ResponseOperationsStandartMulti<DFManagementStdDto.BasicTicketStateDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                                new Tuple<Error, object>(serviceResult.Item1, (object)serviceResult.Item2));

                            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

                            return returnResponse;
                        }

                    //todo: test için yapılcak
                    case ApplicationWorkingEnvironment.Test:
                        {
                            if (!ModelState.IsValid)
                            {
                                BusinessLogicResponse returnResponseClientTest = ResponseOperationsStandartMultiTest<DFManagementStdTestDto.BasicTicketStateDto>.
                                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<ErrorCodes, object>(ErrorCodes.RequestBodyMalformedOnUndefinedRequest, null),
                                                                                                HttpResponseCodes.BadRequest, false);
                                return returnResponseClientTest;
                            }
                            Tuple<ErrorCodes, IEnumerable<DFManagementStdTestDto.BasicTicketStateDto>> serviceResult = new(ErrorCodes.None, new List<DFManagementStdTestDto.BasicTicketStateDto>());
                            if (request != null)
                            {
                                switch (request.RequestDomain)
                                {
                                    case RemoteIncomingDomains.DiasTesisYonetimWebClient:
                                        {
                                            serviceResult = await _basicTicketStateDtoRepository.GetAllTestAsync();
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
                                BusinessLogicResponse returnResponse = ResponseOperationsStandartMultiTest<DFManagementStdTestDto.BasicTicketStateDto>.ConvertServiceResultToBusinessLogicResponse(
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
            catch (Exception)
            {
                throw;
            }
        }

        //TODO : Hata kodları özelleştirilecek
        [Route("[controller]/GetById/{id}")]
        [HttpGet]
        public async Task<BusinessLogicResponse> GetByIdFromIntAsync([FromQuery] int id)
        {
            if (!ModelState.IsValid)
            {
                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartSingle<DFManagementStdDto.BasicTicketStateDto>.
                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<Error, object>(Errors.General.GeneralServerError(), null),
                                                                                HttpResponseCodes.BadRequest, false);
                return returnResponseClient;
            }


            Tuple<Error, DFManagementStdDto.BasicTicketStateDto> serviceResult = await _basicTicketStateDtoRepository.GetByIdFromIntAsync(id);

            BusinessLogicResponse returnResponse = ResponseOperationsStandartSingle<DFManagementStdDto.BasicTicketStateDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<Error, object>(serviceResult.Item1, (object)serviceResult.Item2));

            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

            return returnResponse;
        }

        //TODO : Hata kodları özelleştirilecek
        [Route("[controller]/Delete")]
        [HttpPost]
        public async Task<BusinessLogicResponse> DeleteFromIntAsync([FromBody] BusinessLogicRequest request = null)
        {
            try
            {
                IConfiguration settings = ConfigurationHelper.GetConfig();

                switch (ConfigurationHelper.GetWorkingEnvironment(settings))
                {
                    case ApplicationWorkingEnvironment.Development:
                        {
                            if (!ModelState.IsValid)
                            {
                                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartSingle<DFManagementStdDto.BasicTicketStateDto>.
                                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<Error, object>(Errors.General.GeneralServerError(), null),
                                                                                                HttpResponseCodes.BadRequest, false);
                                return returnResponseClient;
                            }
                            Tuple<Error, DFManagementStdDto.BasicTicketStateDto> serviceResult = new(Errors.General.None(), new DFManagementStdDto.BasicTicketStateDto());

                            if (request != null)
                            {
                                switch (request.RequestDomain)
                                {
                                    case RemoteIncomingDomains.DiasTesisYonetimWebClient:
                                        {
                                            serviceResult = await _basicTicketStateDtoRepository.DeleteAsync(request);

                                            BusinessLogicResponse returnResponse = ResponseOperationsStandartSingle<DFManagementStdDto.BasicTicketStateDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                                                new Tuple<Error, object>(serviceResult.Item1, (object)serviceResult.Item2));

                                            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

                                            return returnResponse;
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
                            }
                            else
                            {
                                throw new ArgumentException();
                            }
                        }

                    case ApplicationWorkingEnvironment.Test:
                        {
                            if (!ModelState.IsValid)
                            {
                                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartSingleTest<DFManagementStdTestDto.BasicTicketStateDto>.
                                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<ErrorCodes, object>(ErrorCodes.RequestBodyMalformedOnUndefinedRequest, null),
                                                                                                HttpResponseCodes.BadRequest, false);
                                return returnResponseClient;
                            }
                            Tuple<ErrorCodes, DFManagementStdTestDto.BasicTicketStateDto> serviceResult = new(ErrorCodes.None, new DFManagementStdTestDto.BasicTicketStateDto());

                            if (request != null)
                            {
                                switch (request.RequestDomain)
                                {
                                    case RemoteIncomingDomains.DiasTesisYonetimWebClient:
                                        {
                                            serviceResult = await _basicTicketStateDtoRepository.DeleteTestAsync(request);

                                            BusinessLogicResponse returnResponse = ResponseOperationsStandartSingleTest<DFManagementStdTestDto.BasicTicketStateDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                                                new Tuple<ErrorCodes, object>(serviceResult.Item1, (object)serviceResult.Item2));

                                            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

                                            return returnResponse;
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
            catch (Exception)
            {

                throw;
            }            
        }

        //TODO : Hata kodları özelleştirilecek
        [Route("[controller]/Insert")]
        [HttpPost]
        public async Task<BusinessLogicResponse> InsertAsync([FromBody] BusinessLogicRequest request = null)
        {
            try
            {
                IConfiguration settings = ConfigurationHelper.GetConfig();
                switch (ConfigurationHelper.GetWorkingEnvironment(settings))
                {
                    case ApplicationWorkingEnvironment.Development:
                        {
                            if (!ModelState.IsValid)
                            {
                                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartSingle<DFManagementStdDto.BasicTicketStateDto>.
                                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<Error, object>(Errors.General.GeneralServerError(), null),
                                                                                                HttpResponseCodes.BadRequest, false);
                                return returnResponseClient;
                            }
                            Tuple<Error, DFManagementStdDto.BasicTicketStateDto> serviceResult = new(Errors.General.None(), new DFManagementStdDto.BasicTicketStateDto());

                            if (request != null)
                            {
                                switch (request.RequestDomain)
                                {
                                    case RemoteIncomingDomains.DiasTesisYonetimWebClient:
                                        {
                                            serviceResult = await _basicTicketStateDtoRepository.InsertAsync(request);

                                            BusinessLogicResponse returnResponse = ResponseOperationsStandartSingle<DFManagementStdDto.BasicTicketStateDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<Error, object>(serviceResult.Item1, (object)serviceResult.Item2));

                                            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

                                            return returnResponse;
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
                            }
                            else
                            {
                                throw new ArgumentException();
                            }
                        }
                    case ApplicationWorkingEnvironment.Test:
                        {
                            if (!ModelState.IsValid)
                            {
                                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartSingleTest<DFManagementStdTestDto.BasicTicketStateDto>.
                                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<ErrorCodes, object>(ErrorCodes.RequestBodyMalformedOnUndefinedRequest, null),
                                                                                                HttpResponseCodes.BadRequest, false);
                                return returnResponseClient;
                            }
                            Tuple<ErrorCodes, DFManagementStdTestDto.BasicTicketStateDto> serviceResult = new(ErrorCodes.None, new DFManagementStdTestDto.BasicTicketStateDto());

                            if (request != null)
                            {
                                switch (request.RequestDomain)
                                {
                                    case RemoteIncomingDomains.DiasTesisYonetimWebClient:
                                        {
                                            serviceResult = await _basicTicketStateDtoRepository.InsertTestAsync(request);

                                            BusinessLogicResponse returnResponse = ResponseOperationsStandartSingleTest<DFManagementStdTestDto.BasicTicketStateDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<ErrorCodes, object>(serviceResult.Item1, (object)serviceResult.Item2));

                                            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

                                            return returnResponse;
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
            catch (Exception)
            {
                throw;
            }

        }

        //TODO : Hata kodları özelleştirilecek
        [Route("[controller]/Update")]
        [HttpPost]
        public async Task<BusinessLogicResponse> UpdateAsync([FromBody] BusinessLogicRequest request = null)
        {
            try
            {
                IConfiguration settings = ConfigurationHelper.GetConfig();
                switch (ConfigurationHelper.GetWorkingEnvironment(settings))
                {
                    case ApplicationWorkingEnvironment.Development:
                        {
                            if (!ModelState.IsValid)
                            {
                                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartSingle<DFManagementStdDto.BasicTicketStateDto>.
                                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<Error, object>(Errors.General.GeneralServerError(), null),
                                                                                                HttpResponseCodes.BadRequest, false);
                                return returnResponseClient;
                            }

                            Tuple<Error, DFManagementStdDto.BasicTicketStateDto> serviceResult = new(Errors.General.None(), new DFManagementStdDto.BasicTicketStateDto());

                            if (request != null)
                            {
                                switch (request.RequestDomain)
                                {
                                    case RemoteIncomingDomains.DiasTesisYonetimWebClient:
                                        {
                                            serviceResult = await _basicTicketStateDtoRepository.UpdateAsync(request);
                                            BusinessLogicResponse returnResponse = ResponseOperationsStandartSingle<DFManagementStdDto.BasicTicketStateDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                                new Tuple<Error, object>(serviceResult.Item1, (object)serviceResult.Item2));

                                            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

                                            return returnResponse;
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
                            }
                            else
                            {
                                throw new ArgumentException();
                            }
                            
                        }

                    case ApplicationWorkingEnvironment.Test:
                        {
                            if (!ModelState.IsValid)
                            {
                                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartSingleTest<DFManagementStdTestDto.BasicTicketStateDto>.
                                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<ErrorCodes, object>(ErrorCodes.RequestBodyMalformedOnUndefinedRequest, null),
                                                                                                HttpResponseCodes.BadRequest, false);
                                return returnResponseClient;
                            }

                            Tuple<ErrorCodes, DFManagementStdTestDto.BasicTicketStateDto> serviceResult = new(ErrorCodes.None, new DFManagementStdTestDto.BasicTicketStateDto());

                            if (request != null)
                            {
                                switch (request.RequestDomain)
                                {
                                    case RemoteIncomingDomains.DiasTesisYonetimWebClient:
                                        {
                                            serviceResult = await _basicTicketStateDtoRepository.UpdateTestAsync(request);
                                            BusinessLogicResponse returnResponse = ResponseOperationsStandartSingleTest<DFManagementStdTestDto.BasicTicketStateDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                                new Tuple<ErrorCodes, object>(serviceResult.Item1, (object)serviceResult.Item2));

                                            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

                                            return returnResponse;
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
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region Controller_ActionRoute

        #endregion

    }


}
