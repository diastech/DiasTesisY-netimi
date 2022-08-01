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
    public class TicketPriorityController : Controller
    {

        private readonly DFManagementStdDtoInterface.ITicketPriorityDtoRepository _ticketPriorityDtoRepository;

        public TicketPriorityController(
            DFManagementStdDtoInterface.ITicketPriorityDtoRepository ticketPriorityDtoRepository)
        {
            _ticketPriorityDtoRepository = ticketPriorityDtoRepository;
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
                                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartMulti<DFManagementStdDto.TicketPriorityDto>.
                                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<Error, object>(Errors.General.GeneralServerError(), null),
                                                                                                HttpResponseCodes.BadRequest, false);
                                return returnResponseClient;
                            }

                            Tuple<Error, IEnumerable<DFManagementStdDto.TicketPriorityDto>> serviceResult = new(Errors.General.None(), new List<DFManagementStdDto.TicketPriorityDto>());

                            if (request != null)
                            {
                                switch (request.RequestDomain)
                                {
                                    case RemoteIncomingDomains.DiasTesisYonetimWebClient:
                                        {
                                            serviceResult = await _ticketPriorityDtoRepository.GetAllAsync();

                                            BusinessLogicResponse returnResponse = ResponseOperationsStandartMulti<DFManagementStdDto.TicketPriorityDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                                                new Tuple<Error, object>(serviceResult.Item1, (object)serviceResult.Item2));

                                            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

                                            return returnResponse;
                                        }
                                    //todo :  mobile için
                                    //case RemoteIncomingDomains.DiasTesisYonetimMobileClient:
                                    //    {
                                    //        serviceResultMobile = await _ticketWrapperDtoRepository.AddTicketWrapperMobileAsync(request);

                                    //        BusinessLogicResponse returnResponse = ResponseOperationsCustomSingle<DFManagementCustomDto.CustomTicketDto>.ConvertServiceResultToBusinessLogicResponse(
                                    //                            new Tuple<ErrorCodes, object>(serviceResult.Item1, (object)serviceResult.Item2));

                                    //        returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

                                    //        return returnResponse;
                                    //    }

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

                    //todo: test için yapılcak
                    case ApplicationWorkingEnvironment.Test:
                        {
                            if (!ModelState.IsValid)
                            {
                                BusinessLogicResponse returnResponseClientTest = ResponseOperationsStandartMultiTest<DFManagementStdTestDto.TicketPriorityDto>.
                                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<ErrorCodes, object>(ErrorCodes.RequestBodyMalformedOnUndefinedRequest, null),
                                                                                                HttpResponseCodes.BadRequest, false);
                                return returnResponseClientTest;
                            }
                            Tuple<ErrorCodes, IEnumerable<DFManagementStdTestDto.TicketPriorityDto>> serviceResult = new(ErrorCodes.None, new List<DFManagementStdTestDto.TicketPriorityDto>());
                            if (request != null)
                            {
                                switch (request.RequestDomain)
                                {
                                    case RemoteIncomingDomains.DiasTesisYonetimWebClient:
                                        {
                                            serviceResult = await _ticketPriorityDtoRepository.GetAllTestAsync();
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
                                BusinessLogicResponse returnResponse = ResponseOperationsStandartMultiTest<DFManagementStdTestDto.TicketPriorityDto>.ConvertServiceResultToBusinessLogicResponse(
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

        //TODO : Hata kodları özelleştirilecek
        [Route("[controller]/GetById/{id}")]
        [HttpGet]
        public async Task<BusinessLogicResponse> GetByIdFromIntAsync([FromQuery] int id)
        {
            if (!ModelState.IsValid)
            {
                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartMulti<DFManagementStdDto.TicketPriorityDto>.
                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<Error, object>(Errors.General.GeneralServerError(), null),
                                                                                HttpResponseCodes.BadRequest, false);
                return returnResponseClient;
            }


            Tuple<Error, DFManagementStdDto.TicketPriorityDto> serviceResult = await _ticketPriorityDtoRepository.GetByIdFromIntAsync(id);

            BusinessLogicResponse returnResponse = ResponseOperationsStandartMulti<DFManagementStdDto.TicketPriorityDto>.ConvertServiceResultToBusinessLogicResponse(
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
                                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartMulti<DFManagementStdDto.TicketPriorityDto>.
                                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<Error, object>(Errors.General.GeneralServerError(), null),
                                                                                                HttpResponseCodes.BadRequest, false);
                                return returnResponseClient;
                            }
                            Tuple<Error, DFManagementStdDto.TicketPriorityDto> serviceResult = new(Errors.General.None(), new DFManagementStdDto.TicketPriorityDto());

                            if (request != null)
                            {
                                switch (request.RequestDomain)
                                {
                                    case RemoteIncomingDomains.DiasTesisYonetimWebClient:
                                        {
                                            serviceResult = await _ticketPriorityDtoRepository.DeleteAsync(request);

                                            BusinessLogicResponse returnResponse = ResponseOperationsStandartSingle<DFManagementStdDto.TicketPriorityDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                                                new Tuple<Error, object>(serviceResult.Item1, (object)serviceResult.Item2));

                                            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

                                            return returnResponse;
                                        }
                                    //todo :  mobile için
                                    //case RemoteIncomingDomains.DiasTesisYonetimMobileClient:
                                    //    {
                                    //        serviceResultMobile = await _ticketWrapperDtoRepository.AddTicketWrapperMobileAsync(request);

                                    //        BusinessLogicResponse returnResponse = ResponseOperationsCustomSingle<DFManagementCustomDto.CustomTicketDto>.ConvertServiceResultToBusinessLogicResponse(
                                    //                            new Tuple<ErrorCodes, object>(serviceResult.Item1, (object)serviceResult.Item2));

                                    //        returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

                                    //        return returnResponse;
                                    //    }

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
                                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartSingleTest<DFManagementStdTestDto.TicketPriorityDto>.
                                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<ErrorCodes, object>(ErrorCodes.RequestBodyMalformedOnUndefinedRequest, null),
                                                                                                HttpResponseCodes.BadRequest, false);
                                return returnResponseClient;
                            }
                            Tuple<ErrorCodes, DFManagementStdTestDto.TicketPriorityDto> serviceResult = new(ErrorCodes.None, new DFManagementStdTestDto.TicketPriorityDto());

                            if (request != null)
                            {
                                switch (request.RequestDomain)
                                {
                                    case RemoteIncomingDomains.DiasTesisYonetimWebClient:
                                        {
                                            serviceResult = await _ticketPriorityDtoRepository.DeleteTestAsync(request);

                                            BusinessLogicResponse returnResponse = ResponseOperationsStandartSingleTest<DFManagementStdTestDto.TicketPriorityDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                                                new Tuple<ErrorCodes, object>(serviceResult.Item1, (object)serviceResult.Item2));

                                            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

                                            return returnResponse;
                                        }
                                    //todo :  mobile için
                                    //case RemoteIncomingDomains.DiasTesisYonetimMobileClient:
                                    //    {
                                    //        serviceResultMobile = await _ticketWrapperDtoRepository.AddTicketWrapperMobileAsync(request);

                                    //        BusinessLogicResponse returnResponse = ResponseOperationsCustomSingle<DFManagementCustomDto.CustomTicketDto>.ConvertServiceResultToBusinessLogicResponse(
                                    //                            new Tuple<ErrorCodes, object>(serviceResult.Item1, (object)serviceResult.Item2));

                                    //        returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

                                    //        return returnResponse;
                                    //    }

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
                                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartMulti<DFManagementStdDto.TicketPriorityDto>.
                                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<Error, object>(Errors.General.GeneralServerError(), null),
                                                                                                HttpResponseCodes.BadRequest, false);
                                return returnResponseClient;
                            }
                            Tuple<Error, DFManagementStdDto.TicketPriorityDto> serviceResult = new(Errors.General.None(), new DFManagementStdDto.TicketPriorityDto());

                            if (request != null)
                            {
                                switch (request.RequestDomain)
                                {
                                    case RemoteIncomingDomains.DiasTesisYonetimWebClient:
                                        {
                                            serviceResult = await _ticketPriorityDtoRepository.InsertAsync(request);

                                            BusinessLogicResponse returnResponse = ResponseOperationsStandartSingle<DFManagementStdDto.TicketPriorityDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<Error, object>(serviceResult.Item1, (object)serviceResult.Item2));

                                            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

                                            return returnResponse;
                                        }

                                    //todo :  mobile için
                                    //case RemoteIncomingDomains.DiasTesisYonetimMobileClient:
                                    //    {
                                    //        serviceResultMobile = await _ticketWrapperDtoRepository.AddTicketWrapperMobileAsync(request);

                                    //        BusinessLogicResponse returnResponse = ResponseOperationsCustomSingle<DFManagementCustomDto.CustomTicketDto>.ConvertServiceResultToBusinessLogicResponse(
                                    //                            new Tuple<ErrorCodes, object>(serviceResult.Item1, (object)serviceResult.Item2));

                                    //        returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

                                    //        return returnResponse;
                                    //    }

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
                                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartSingleTest<DFManagementStdTestDto.TicketPriorityDto>.
                                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<ErrorCodes, object>(ErrorCodes.RequestBodyMalformedOnUndefinedRequest, null),
                                                                                                HttpResponseCodes.BadRequest, false);
                                return returnResponseClient;
                            }
                            Tuple<ErrorCodes, DFManagementStdTestDto.TicketPriorityDto> serviceResult = new(ErrorCodes.None, new DFManagementStdTestDto.TicketPriorityDto());

                            if (request != null)
                            {
                                switch (request.RequestDomain)
                                {
                                    case RemoteIncomingDomains.DiasTesisYonetimWebClient:
                                        {
                                            serviceResult = await _ticketPriorityDtoRepository.InsertTestAsync(request);

                                            BusinessLogicResponse returnResponse = ResponseOperationsStandartSingleTest<DFManagementStdTestDto.TicketPriorityDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<ErrorCodes, object>(serviceResult.Item1, (object)serviceResult.Item2));

                                            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

                                            return returnResponse;
                                        }

                                    //todo :  mobile için
                                    //case RemoteIncomingDomains.DiasTesisYonetimMobileClient:
                                    //    {
                                    //        serviceResultMobile = await _ticketWrapperDtoRepository.AddTicketWrapperMobileAsync(request);

                                    //        BusinessLogicResponse returnResponse = ResponseOperationsCustomSingle<DFManagementCustomDto.CustomTicketDto>.ConvertServiceResultToBusinessLogicResponse(
                                    //                            new Tuple<ErrorCodes, object>(serviceResult.Item1, (object)serviceResult.Item2));

                                    //        returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

                                    //        return returnResponse;
                                    //    }

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
                                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartMulti<DFManagementStdDto.TicketPriorityDto>.
                                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<Error, object>(Errors.General.GeneralServerError(), null),
                                                                                                HttpResponseCodes.BadRequest, false);
                                return returnResponseClient;
                            }

                            Tuple<Error, DFManagementStdDto.TicketPriorityDto> serviceResult = new(Errors.General.None(), new DFManagementStdDto.TicketPriorityDto());


                            if (request != null)
                            {
                                switch (request.RequestDomain)
                                {
                                    case RemoteIncomingDomains.DiasTesisYonetimWebClient:
                                        {
                                            serviceResult = await _ticketPriorityDtoRepository.UpdateAsync(request);
                                            BusinessLogicResponse returnResponse = ResponseOperationsStandartSingle<DFManagementStdDto.TicketPriorityDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                                new Tuple<Error, object>(serviceResult.Item1, (object)serviceResult.Item2));

                                            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

                                            return returnResponse;
                                        }

                                    //todo :  mobile için
                                    //case RemoteIncomingDomains.DiasTesisYonetimMobileClient:
                                    //    {
                                    //        serviceResultMobile = await _ticketWrapperDtoRepository.AddTicketWrapperMobileAsync(request);

                                    //        BusinessLogicResponse returnResponse = ResponseOperationsCustomSingle<DFManagementCustomDto.CustomTicketDto>.ConvertServiceResultToBusinessLogicResponse(
                                    //                            new Tuple<ErrorCodes, object>(serviceResult.Item1, (object)serviceResult.Item2));

                                    //        returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

                                    //        return returnResponse;
                                    //    }

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
                                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartSingleTest<DFManagementStdTestDto.TicketPriorityDto>.
                                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<ErrorCodes, object>(ErrorCodes.RequestBodyMalformedOnUndefinedRequest, null),
                                                                                                HttpResponseCodes.BadRequest, false);
                                return returnResponseClient;
                            }

                            Tuple<ErrorCodes, DFManagementStdTestDto.TicketPriorityDto> serviceResult = new(ErrorCodes.None, new DFManagementStdTestDto.TicketPriorityDto());


                            if (request != null)
                            {
                                switch (request.RequestDomain)
                                {
                                    case RemoteIncomingDomains.DiasTesisYonetimWebClient:
                                        {
                                            serviceResult = await _ticketPriorityDtoRepository.UpdateTestAsync(request);
                                            BusinessLogicResponse returnResponse = ResponseOperationsStandartSingleTest<DFManagementStdTestDto.TicketPriorityDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                                new Tuple<ErrorCodes, object>(serviceResult.Item1, (object)serviceResult.Item2));

                                            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

                                            return returnResponse;
                                        }

                                    //todo :  mobile için
                                    //case RemoteIncomingDomains.DiasTesisYonetimMobileClient:
                                    //    {
                                    //        serviceResultMobile = await _ticketWrapperDtoRepository.AddTicketWrapperMobileAsync(request);

                                    //        BusinessLogicResponse returnResponse = ResponseOperationsCustomSingle<DFManagementCustomDto.CustomTicketDto>.ConvertServiceResultToBusinessLogicResponse(
                                    //                            new Tuple<ErrorCodes, object>(serviceResult.Item1, (object)serviceResult.Item2));

                                    //        returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

                                    //        return returnResponse;
                                    //    }

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
