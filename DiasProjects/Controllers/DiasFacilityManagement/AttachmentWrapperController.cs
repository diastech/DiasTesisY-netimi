using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DFManagementStdDtoInterface = DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement;
using DFManagementCustomDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using DFManagementCustomTestDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Custom;
using DFManagementStandardTestDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard;
using DFManagementDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using CustomDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using DiasShared.Services.Communication.BusinessLogicMessage;
using static DiasShared.Enums.Standart.HttpCodesEnum;
using Newtonsoft.Json;
using System.Collections.Generic;
using static DiasShared.Enums.Standart.RemoteCommunicationEnums;
using Microsoft.Extensions.Configuration;
using DiasWebApi.Shared.Configuration;
using static DiasWebApi.Shared.Enums.WebApiApplicationEnums;
using DiasShared.Errors;
using DiasBusinessLogic.Shared.Error;
using DiasShared.Operations.CommunicationOperations.DiasFacilityManagement;

namespace DiasWebApi.DiasFacilityManagement.Controllers
{
    [ApiController]
    public class AttachmentWrapperController : Controller
    {
        private readonly DFManagementStdDtoInterface.IAttachmentWrapperDtoRepository _attachmentWrapperDtoRepository;

        public AttachmentWrapperController(DFManagementStdDtoInterface.IAttachmentWrapperDtoRepository attachmentWrapperDtoRepository)
        {
            _attachmentWrapperDtoRepository = attachmentWrapperDtoRepository;            
        }

        #region ControllerRoute
        

        [Route("[controller]/GetById/{id}")]
        [HttpPost]
        public async Task<BusinessLogicResponse> GetTicketNoteByTicketIdAsync(int id,[FromBody] BusinessLogicRequest request = null)
        {
            try
            {
                IConfiguration settings = ConfigurationHelper.GetConfig();

                switch (ConfigurationHelper.GetWorkingEnvironment(settings))
                {
                    case ApplicationWorkingEnvironment.Development:
                        {
                            
                            Tuple<Error, IEnumerable<DFManagementCustomDto.CustomAttachmentDto>> serviceResult =new(Errors.General.None(), new List<DFManagementCustomDto.CustomAttachmentDto>());

                            if (request != null)
                            {
                                switch (request.RequestDomain)
                                {
                                    case RemoteIncomingDomains.DiasTesisYonetimWebClient:
                                        {
                                            serviceResult = await _attachmentWrapperDtoRepository.GetAttachmentsByTicketIdAsync(id);

                                            BusinessLogicResponse returnResponse = ResponseOperationsCustomMulti<DFManagementCustomDto.CustomAttachmentDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<Error, object>(serviceResult.Item1, (object)serviceResult.Item2));

                                            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

                                            return returnResponse;
                                        }

                                    //şuna mobile için yok
                                    case RemoteIncomingDomains.DiasTesisYonetimMobileClient:
                                        {
                                            throw new NotImplementedException();
                                            //serviceResultMobile = await _ticketWrapperDtoRepository.AddTicketWrapperMobileAsync(request);

                                            //BusinessLogicResponse returnResponse = ResponseOperationsCustomSingle<DFManagementCustomDto.CustomTicketDto>.ConvertServiceResultToBusinessLogicRequestResponse(
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
                            //if (!ModelState.IsValid)
                            //{
                            //    BusinessLogicResponse returnResponseClientTest = ResponseOperationsStandartMultiTest<DFManagementCustomTestDto.CustomTicketDto>.
                            //                                                            ConvertServiceResultToBusinessLogicRequestResponse(new Tuple<ErrorCodes, object>(ErrorCodes.RequestBodyMalformedOnUndefinedRequest, null),
                            //                                                                    HttpResponseCodes.BadRequest, false);
                            //    return returnResponseClientTest;
                            //}

                            //Tuple<ErrorCodes, DFManagementCustomTestDto.CustomTicketDto> serviceResult = new(ErrorCodes.None, new DFManagementCustomTestDto.CustomTicketDto());

                            //if (request != null)
                            //{
                            //    switch (request.RequestDomain)
                            //    {
                            //        case RemoteIncomingDomains.DiasTesisYonetimWebClient:
                            //            {
                            //                serviceResult = await _ticketWrapperDtoRepository.GetTicketWrapperByIdTestAsync(id);

                            //                BusinessLogicResponse returnResponse = ResponseOperationsCustomSingleTest<DFManagementCustomTestDto.CustomTicketDto>.ConvertServiceResultToBusinessLogicRequestResponse(
                            //                                    new Tuple<ErrorCodes, object>(serviceResult.Item1, (object)serviceResult.Item2));

                            //                returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

                            //                return returnResponse;
                            //            }

                            //        //şuna mobile için yok
                            //        //case RemoteIncomingDomains.DiasTesisYonetimMobileClient:
                            //        //    {
                            //        //        serviceResultMobile = await _ticketWrapperDtoRepository.AddTicketWrapperMobileAsync(request);

                            //        //        BusinessLogicResponse returnResponse = ResponseOperationsCustomSingle<DFManagementCustomDto.CustomTicketDto>.ConvertServiceResultToBusinessLogicRequestResponse(
                            //        //                            new Tuple<ErrorCodes, object>(serviceResult.Item1, (object)serviceResult.Item2));

                            //        //        returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

                            //        //        return returnResponse;
                            //        //    }

                            //        default:
                            //            {
                            //                throw new ArgumentException();
                            //            }
                            //    }
                            //}
                            //else
                            //{
                            //    throw new ArgumentException();
                            //}

                            throw new NotImplementedException();
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
                                BusinessLogicResponse returnResponseClient = ResponseOperationsCustomSingle<DFManagementCustomDto.CustomAttachmentDto>.
                                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<ErrorCodes, object>(ErrorCodes.RequestBodyMalformedOnUndefinedRequest, null),
                                                                                                HttpResponseCodes.BadRequest, false);
                                return returnResponseClient;
                            }

                            Tuple<Error, DFManagementCustomDto.CustomAttachmentDto> serviceResult =
                                    new(Errors.General.None(), new DFManagementCustomDto.CustomAttachmentDto());

                            //Tuple<Error, DFManagementCustomDto.CustomMobileTicketDto> serviceResultMobile =
                            //                    new(Errors.General.None(), new DFManagementCustomDto.CustomMobileTicketDto());

                            if (request != null)
                            {
                                switch (request.RequestDomain)
                                {
                                    case RemoteIncomingDomains.DiasTesisYonetimWebClient:
                                        {
                                            serviceResult = await _attachmentWrapperDtoRepository.AddAttachmentWrapperAsync(request);

                                            BusinessLogicResponse returnResponse = ResponseOperationsCustomSingle<DFManagementCustomDto.CustomAttachmentDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<Error, object>(serviceResult.Item1, (object)serviceResult.Item2));

                                            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

                                            return returnResponse;
                                        }

                                    case RemoteIncomingDomains.DiasTesisYonetimMobileClient:
                                        {

                                            serviceResult = await _attachmentWrapperDtoRepository.AddAttachmentWrapperAsync(request);

                                            BusinessLogicResponse returnResponse = ResponseOperationsCustomSingle<DFManagementCustomDto.CustomAttachmentDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<Error, object>(serviceResult.Item1, (object)serviceResult.Item2));

                                            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

                                            return returnResponse;
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
                            //if (!ModelState.IsValid)
                            //{
                            //    BusinessLogicResponse returnResponseClientTest = ResponseOperationsStandartMultiTest<DFManagementCustomTestDto.CustomTicketDto>.
                            //                                                            ConvertServiceResultToBusinessLogicRequestResponse(new Tuple<ErrorCodes, object>(ErrorCodes.RequestBodyMalformedOnUndefinedRequest, null),
                            //                                                                    HttpResponseCodes.BadRequest, false);
                            //    return returnResponseClientTest;
                            //}

                            //Tuple<ErrorCodes, DFManagementCustomTestDto.CustomTicketDto> serviceResult =
                            //        new(ErrorCodes.None, new DFManagementCustomTestDto.CustomTicketDto());

                            //Tuple<ErrorCodes, DFManagementCustomTestDto.CustomMobileTicketDto> serviceResultMobile =
                            //                    new(ErrorCodes.None, new DFManagementCustomTestDto.CustomMobileTicketDto());

                            //if (request != null)
                            //{
                            //    switch (request.RequestDomain)
                            //    {
                            //        case RemoteIncomingDomains.DiasTesisYonetimWebClient:
                            //            {
                            //                serviceResult = await _ticketWrapperDtoRepository.AddTicketWrapperTestAsync(request);

                            //                BusinessLogicResponse returnResponse = ResponseOperationsCustomSingleTest<DFManagementCustomTestDto.CustomTicketDto>.ConvertServiceResultToBusinessLogicRequestResponse(
                            //                                    new Tuple<ErrorCodes, object>(serviceResult.Item1, (object)serviceResult.Item2));

                            //                returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

                            //                return returnResponse;
                            //            }

                            //        case RemoteIncomingDomains.DiasTesisYonetimMobileClient:
                            //            {
                            //                serviceResultMobile = await _ticketWrapperDtoRepository.AddTicketWrapperMobileTestAsync(request);

                            //                BusinessLogicResponse returnResponse = ResponseOperationsCustomSingleTest<DFManagementCustomTestDto.CustomMobileTicketDto>.ConvertServiceResultToBusinessLogicRequestResponse(
                            //                                    new Tuple<ErrorCodes, object>(serviceResult.Item1, (object)serviceResult.Item2));

                            //                returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

                            //                return returnResponse;
                            //            }

                            //        default:
                            //            {
                            //                throw new ArgumentException();
                            //            }
                            //    }

                            //}
                            //else
                            //{
                            //    throw new ArgumentException();
                            //}
                            throw new NotImplementedException();
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


        [Route("[controller]/Delete")]
        [HttpPost]
        public async Task<BusinessLogicResponse> DeleteAsync([FromBody] BusinessLogicRequest request = null)
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
                                BusinessLogicResponse returnResponseClient = ResponseOperationsCustomSingle<DFManagementCustomDto.CustomAttachmentDto>.
                                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<ErrorCodes, object>(ErrorCodes.RequestBodyMalformedOnUndefinedRequest, null),
                                                                                                HttpResponseCodes.BadRequest, false);
                                return returnResponseClient;
                            }

                            Tuple<Error, DFManagementCustomDto.CustomAttachmentDto> serviceResult =
                                    new(Errors.General.None(), new DFManagementCustomDto.CustomAttachmentDto());

                            //Tuple<Error, DFManagementCustomDto.CustomMobileTicketDto> serviceResultMobile =
                            //                    new(Errors.General.None(), new DFManagementCustomDto.CustomMobileTicketDto());

                            if (request != null)
                            {
                                switch (request.RequestDomain)
                                {
                                    case RemoteIncomingDomains.DiasTesisYonetimWebClient:
                                        {
                                            serviceResult = await _attachmentWrapperDtoRepository.DeleteAttachmentWrapperAsync(request);

                                            BusinessLogicResponse returnResponse = ResponseOperationsCustomSingle<DFManagementCustomDto.CustomAttachmentDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<Error, object>(serviceResult.Item1, (object)serviceResult.Item2));

                                            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

                                            return returnResponse;
                                        }

                                    case RemoteIncomingDomains.DiasTesisYonetimMobileClient:
                                        {
                                            //serviceResultMobile = await _ticketWrapperDtoRepository.AddTicketWrapperMobileAsync(request);

                                            //BusinessLogicResponse returnResponse = ResponseOperationsCustomSingle<DFManagementCustomDto.CustomTicketDto>.ConvertServiceResultToBusinessLogicRequestResponse(
                                            //                    new Tuple<Error, object>(serviceResult.Item1, (object)serviceResult.Item2));

                                            //returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

                                            //return returnResponse;
                                            throw new ArgumentException();
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
                            //if (!ModelState.IsValid)
                            //{
                            //    BusinessLogicResponse returnResponseClientTest = ResponseOperationsStandartMultiTest<DFManagementCustomTestDto.CustomTicketDto>.
                            //                                                            ConvertServiceResultToBusinessLogicRequestResponse(new Tuple<ErrorCodes, object>(ErrorCodes.RequestBodyMalformedOnUndefinedRequest, null),
                            //                                                                    HttpResponseCodes.BadRequest, false);
                            //    return returnResponseClientTest;
                            //}

                            //Tuple<ErrorCodes, DFManagementCustomTestDto.CustomTicketDto> serviceResult =
                            //        new(ErrorCodes.None, new DFManagementCustomTestDto.CustomTicketDto());

                            //Tuple<ErrorCodes, DFManagementCustomTestDto.CustomMobileTicketDto> serviceResultMobile =
                            //                    new(ErrorCodes.None, new DFManagementCustomTestDto.CustomMobileTicketDto());

                            //if (request != null)
                            //{
                            //    switch (request.RequestDomain)
                            //    {
                            //        case RemoteIncomingDomains.DiasTesisYonetimWebClient:
                            //            {
                            //                serviceResult = await _ticketWrapperDtoRepository.AddTicketWrapperTestAsync(request);

                            //                BusinessLogicResponse returnResponse = ResponseOperationsCustomSingleTest<DFManagementCustomTestDto.CustomTicketDto>.ConvertServiceResultToBusinessLogicRequestResponse(
                            //                                    new Tuple<ErrorCodes, object>(serviceResult.Item1, (object)serviceResult.Item2));

                            //                returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

                            //                return returnResponse;
                            //            }

                            //        case RemoteIncomingDomains.DiasTesisYonetimMobileClient:
                            //            {
                            //                serviceResultMobile = await _ticketWrapperDtoRepository.AddTicketWrapperMobileTestAsync(request);

                            //                BusinessLogicResponse returnResponse = ResponseOperationsCustomSingleTest<DFManagementCustomTestDto.CustomMobileTicketDto>.ConvertServiceResultToBusinessLogicRequestResponse(
                            //                                    new Tuple<ErrorCodes, object>(serviceResult.Item1, (object)serviceResult.Item2));

                            //                returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

                            //                return returnResponse;
                            //            }

                            //        default:
                            //            {
                            //                throw new ArgumentException();
                            //            }
                            //    }

                            //}
                            //else
                            //{
                            //    throw new ArgumentException();
                            //}
                            throw new NotImplementedException();
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
