using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DFManagementStdDtoInterface = DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement;
using DFManagementCustomDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using CustomDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using DFManagementCustomTestDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Custom;
using DiasShared.Services.Communication.BusinessLogicMessage;
using DiasShared.Operations.CommunicationOperations.DiasFacilityManagement;
using static DiasShared.Enums.Standart.HttpCodesEnum;
using Newtonsoft.Json;
using System.Collections.Generic;
using static DiasShared.Enums.Standart.RemoteCommunicationEnums;
using Microsoft.Extensions.Configuration;
using DiasWebApi.Shared.Configuration;
using static DiasWebApi.Shared.Enums.WebApiApplicationEnums;
using DiasShared.Errors;
using DiasBusinessLogic.Shared.Error;
using DiasShared.Operations.CommunicationOperations;

namespace DiasWebApi.DiasFacilityManagement.Controllers
{
    [ApiController]
    public class BasicTicketWrapperController : Controller
    {
        private readonly DFManagementStdDtoInterface.IBasicTicketWrapperDtoRepository _basicTicketWrapperDtoRepository;

        public BasicTicketWrapperController(DFManagementStdDtoInterface.IBasicTicketWrapperDtoRepository basicTicketWrapperDtoRepository)
        {
            _basicTicketWrapperDtoRepository = basicTicketWrapperDtoRepository;
        }

        #region ControllerRoute
        [Route("[controller]/GetAll")]
        [HttpPost]
        public async Task<BusinessLogicResponse> GetAllBasicTicketsWrapperAsync([FromBody] BusinessLogicRequest request = null)
        {
            IConfiguration settings = ConfigurationHelper.GetConfig();

            switch (ConfigurationHelper.GetWorkingEnvironment(settings))
            {
                case ApplicationWorkingEnvironment.Development:
                    {
                        if (!ModelState.IsValid)
                        {
                            BusinessLogicResponse returnResponseClient = ResponseOperationsStandartMulti<DFManagementCustomDto.CustomBasicTicketDto>.
                                                                                    ConvertServiceResultToBusinessLogicResponse(new Tuple<ErrorCodes, object>(ErrorCodes.RequestBodyMalformedOnUndefinedRequest, null),
                                                                                            HttpResponseCodes.BadRequest, false);
                            return returnResponseClient;
                        }

                        Tuple<Error, IEnumerable<DFManagementCustomDto.CustomBasicTicketDto>> serviceResult = new(Errors.General.None(), new List<DFManagementCustomDto.CustomBasicTicketDto>());

                        if(request != null)
                        {
                            switch (request.RequestDomain)
                            {
                                case RemoteIncomingDomains.DiasTesisYonetimWebClient:
                                    {
                                        serviceResult = await _basicTicketWrapperDtoRepository.GetAllBasicTicketsWrapperAsync(request.DevExpressRequestObj);
                                        BusinessLogicResponse returnResponse = ResponseOperationsCustomMulti<DFManagementCustomDto.CustomBasicTicketDto>.ConvertServiceResultToBusinessLogicResponse(
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
                            BusinessLogicResponse returnResponseClientTest = ResponseOperationsStandartMultiTest<DFManagementCustomTestDto.CustomBasicTicketDto>.
                                                                                    ConvertServiceResultToBusinessLogicResponse(new Tuple<ErrorCodes, object>(ErrorCodes.RequestBodyMalformedOnUndefinedRequest, null),
                                                                                            HttpResponseCodes.BadRequest, false);
                            return returnResponseClientTest;
                        }
                        Tuple<ErrorCodes, IEnumerable<DFManagementCustomTestDto.CustomBasicTicketDto>> serviceResult = new(ErrorCodes.None, new List<DFManagementCustomTestDto.CustomBasicTicketDto>());
                        if (request != null)
                        {
                            switch (request.RequestDomain)
                            {
                                case RemoteIncomingDomains.DiasTesisYonetimWebClient:
                                    {
                                        serviceResult = await _basicTicketWrapperDtoRepository.GetAllBasicTicketsWrapperTestAsync(request.DevExpressRequestObj);
                                        BusinessLogicResponse returnResponse = ResponseOperationsCustomMultiTest<DFManagementCustomTestDto.CustomBasicTicketDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<ErrorCodes, object>(serviceResult.Item1, (object)serviceResult.Item2));
                                        returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);
                                        return returnResponse;
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

        [Route("[controller]/GetById/{id}")]
        [HttpGet]
        public async Task<BusinessLogicResponse> GetBasicTicketWrapperByIdAsync(int id, [FromBody] BusinessLogicRequest request = null)
        {
            try
            {
                IConfiguration settings = ConfigurationHelper.GetConfig();
                switch (ConfigurationHelper.GetWorkingEnvironment(settings))
                {
                    case ApplicationWorkingEnvironment.Development:
                        {
                            Tuple<Error, DFManagementCustomDto.CustomBasicTicketDto> serviceResult = new(Errors.General.None(), new DFManagementCustomDto.CustomBasicTicketDto());
                            

                            if (request != null)
                            {
                                switch (request.RequestDomain)
                                {
                                    case RemoteIncomingDomains.DiasTesisYonetimWebClient:
                                        {
                                            serviceResult = await _basicTicketWrapperDtoRepository.GetBasicTicketWrapperByIdAsync(id);

                                            BusinessLogicResponse returnResponse = ResponseOperationsCustomSingle<DFManagementCustomDto.CustomBasicTicketDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<Error, object>(serviceResult.Item1, (object)serviceResult.Item2));

                                            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

                                            return returnResponse;
                                        }

                                    //şuna mobile için yok
                                    case RemoteIncomingDomains.DiasTesisYonetimMobileClient:
                                        {
                                            throw new NotImplementedException();
                                            //serviceResultMobile = await _ticketWrapperDtoRepository.AddTicketWrapperMobileAsync(request);

                                            //BusinessLogicResponse returnResponse = ResponseOperationsCustomSingle<DFManagementCustomDto.CustomTicketDto>.ConvertServiceResultToBusinessLogicResponse(
                                            //                    new Tuple<ErrorCodes, object>(serviceResult.Item1, (object)serviceResult.Item2));

                                            //returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

                                            //return returnResponse;
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
                                BusinessLogicResponse returnResponseClientTest = ResponseOperationsStandartMultiTest<DFManagementCustomTestDto.CustomBasicTicketDto>.
                                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<ErrorCodes, object>(ErrorCodes.RequestBodyMalformedOnUndefinedRequest, null),
                                                                                                HttpResponseCodes.BadRequest, false);
                                return returnResponseClientTest;
                            }

                            Tuple<ErrorCodes, DFManagementCustomTestDto.CustomBasicTicketDto> serviceResult = new(ErrorCodes.None, new DFManagementCustomTestDto.CustomBasicTicketDto());

                            if (request != null)
                            {
                                switch (request.RequestDomain)
                                {
                                    case RemoteIncomingDomains.DiasTesisYonetimWebClient:
                                        {
                                            serviceResult = await _basicTicketWrapperDtoRepository.GetBasicTicketWrapperByIdTestAsync(id);

                                            BusinessLogicResponse returnResponse = ResponseOperationsCustomSingleTest<DFManagementCustomTestDto.CustomTicketDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<ErrorCodes, object>(serviceResult.Item1, (object)serviceResult.Item2));

                                            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

                                            return returnResponse;
                                        }

                                    //şuna mobile için yok
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
                                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartSingle<DFManagementCustomDto.CustomBasicTicketDto>.
                                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<ErrorCodes, object>(ErrorCodes.RequestBodyMalformedOnUndefinedRequest, null),
                                                                                                HttpResponseCodes.BadRequest, false);
                                return returnResponseClient;
                            }

                            Tuple<Error, DFManagementCustomDto.CustomBasicTicketDto> serviceResult = new(Errors.General.None(), new DFManagementCustomDto.CustomBasicTicketDto());

                            if (request != null)
                            {
                                switch (request.RequestDomain)
                                {
                                    case RemoteIncomingDomains.DiasTesisYonetimWebClient:
                                        {
                                            serviceResult = await _basicTicketWrapperDtoRepository.AddBasicTicketWrapperAsync(request);

                                            BusinessLogicResponse returnResponse = ResponseOperationsCustomSingle<DFManagementCustomDto.CustomBasicTicketDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<Error, object>(serviceResult.Item1, (object)serviceResult.Item2));

                                            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

                                            return returnResponse;
                                        }

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
                                BusinessLogicResponse returnResponseClientTest = ResponseOperationsStandartMultiTest<DFManagementCustomTestDto.CustomTicketDto>.
                                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<ErrorCodes, object>(ErrorCodes.RequestBodyMalformedOnUndefinedRequest, null),
                                                                                                HttpResponseCodes.BadRequest, false);
                                return returnResponseClientTest;
                            }

                            Tuple<ErrorCodes, DFManagementCustomTestDto.CustomBasicTicketDto> serviceResult =new(ErrorCodes.None, new DFManagementCustomTestDto.CustomBasicTicketDto());

                            if (request != null)
                            {
                                switch (request.RequestDomain)
                                {
                                    case RemoteIncomingDomains.DiasTesisYonetimWebClient:
                                        {
                                            serviceResult = await _basicTicketWrapperDtoRepository.AddBasicTicketWrappertestAsync(request);

                                            BusinessLogicResponse returnResponse = ResponseOperationsCustomSingleTest<DFManagementCustomTestDto.CustomTicketDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<ErrorCodes, object>(serviceResult.Item1, (object)serviceResult.Item2));

                                            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

                                            return returnResponse;
                                        }

                                    //case RemoteIncomingDomains.DiasTesisYonetimMobileClient:
                                    //    {
                                    //        serviceResultMobile = await _ticketWrapperDtoRepository.AddTicketWrapperMobileTestAsync(request);

                                    //        BusinessLogicResponse returnResponse = ResponseOperationsCustomSingleTest<DFManagementCustomTestDto.CustomMobileTicketDto>.ConvertServiceResultToBusinessLogicResponse(
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
            catch (Exception e)
            {

                throw;
            }

        }

        #endregion

        #region Controller_ActionRoute

        #endregion

    }


}
