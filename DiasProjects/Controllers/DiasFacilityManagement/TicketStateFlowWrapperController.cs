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
using DiasBusinessLogic.Shared.Error;
using DFManagementCustomDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using DiasShared.Errors;
using static DiasShared.Enums.Standart.RemoteCommunicationEnums;

namespace DiasWebApi.DiasFacilityManagement.Controllers
{
    [ApiController]
    public class TicketStateFlowWrapperController : Controller
    {

        private readonly DFManagementStdDtoInterface.ITicketStateFlowWrapperDtoRepository _ticketStateFlowWrapperDtoRepository;

        public TicketStateFlowWrapperController(DFManagementStdDtoInterface.ITicketStateFlowWrapperDtoRepository ticketStateFlowWrapperDtoRepository)
        {
            _ticketStateFlowWrapperDtoRepository = ticketStateFlowWrapperDtoRepository;
        }

        #region ControllerRoute

        [Route("[controller]/GetAll")]
        [HttpPost]
        public async Task<BusinessLogicResponse> GetAll(int id, [FromBody] BusinessLogicRequest request = null)
        {
            try
            {
                IConfiguration settings = ConfigurationHelper.GetConfig();

                switch (ConfigurationHelper.GetWorkingEnvironment(settings))
                {
                    case ApplicationWorkingEnvironment.Development:
                        {

                            Tuple<Error, IEnumerable<DFManagementCustomDto.CustomTicketStateFlowDto>> serviceResult = new(Errors.General.None(), new List<DFManagementCustomDto.CustomTicketStateFlowDto>());

                            if (request != null)
                            {
                                switch (request.RequestDomain)
                                {
                                    case RemoteIncomingDomains.DiasTesisYonetimWebClient:
                                        {
                                            serviceResult = await _ticketStateFlowWrapperDtoRepository.GetAllTicketStateFlowdWrapperAsync();

                                            BusinessLogicResponse returnResponse = ResponseOperationsCustomMulti<DFManagementCustomDto.CustomTicketStateFlowDto>.ConvertServiceResultToBusinessLogicResponse(
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



        #endregion

        #region Controller_ActionRoute

        #endregion

    }


}
