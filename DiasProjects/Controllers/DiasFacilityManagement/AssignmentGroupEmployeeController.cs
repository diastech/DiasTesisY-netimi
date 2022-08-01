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
using DiasShared.Operations.CommunicationOperations.DiasFacilityManagement;
using Microsoft.Extensions.Configuration;
using DiasWebApi.Shared.Configuration;
using static DiasWebApi.Shared.Enums.WebApiApplicationEnums;
using DiasShared.Errors;
using DiasBusinessLogic.Shared.Error;
using static DiasShared.Enums.Standart.RemoteCommunicationEnums;

namespace DiasWebApi.DiasFacilityManagement.Controllers
{
    [ApiController]
    public class AssignmentGroupEmployeeController : Controller
    {
        
        private readonly DFManagementStdDtoInterface.IAssignmentGroupEmployeeDtoRepository _assignmentGroupEmployeesRepository;        

        public AssignmentGroupEmployeeController(DFManagementStdDtoInterface.IAssignmentGroupEmployeeDtoRepository assignmentGroupEmployeesRepository)
        {            
            _assignmentGroupEmployeesRepository = assignmentGroupEmployeesRepository;            
        }

        #region ControllerRoute

        [Route("[controller]/GetAll")]
        [HttpPost]
        public async Task<BusinessLogicResponse> GetAllAssignmentGroupEmployeeAsync([FromBody] BusinessLogicRequest request = null)
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
                                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartMulti<DFManagementStdDto.AssignmentGroupEmployeeDto>.
                                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<ErrorCodes, object>(ErrorCodes.RequestBodyMalformedOnUndefinedRequest, null),
                                                                                                HttpResponseCodes.BadRequest, false);
                                return returnResponseClient;
                            }

                            Tuple<Error, IEnumerable<DFManagementStdDto.AssignmentGroupEmployeeDto>> serviceResult =
                                    new(Errors.General.None(), new List<DFManagementStdDto.AssignmentGroupEmployeeDto>());

                            //Tuple<Error, DFManagementCustomDto.CustomMobileTicketDto> serviceResultMobile =
                            //                    new(Errors.General.None(), new DFManagementCustomDto.CustomMobileTicketDto());

                            if (request != null)
                            {
                                switch (request.RequestDomain)
                                {
                                    case RemoteIncomingDomains.DiasTesisYonetimWebClient:
                                        {
                                            serviceResult = await _assignmentGroupEmployeesRepository.GetAllAsync();

                                            BusinessLogicResponse returnResponse = ResponseOperationsStandartMulti<DFManagementStdDto.AssignmentGroupEmployeeDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<Error, object>(serviceResult.Item1, (object)serviceResult.Item2));

                                            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

                                            return returnResponse;
                                        }

                                    //case RemoteIncomingDomains.DiasTesisYonetimMobileClient:
                                    //    {

                                    //        serviceResult = await _attachmentWrapperDtoRepository.AddAttachmentWrapperAsync(request);

                                    //        BusinessLogicResponse returnResponse = ResponseOperationsCustomSingle<DFManagementCustomDto.CustomAttachmentDto>.ConvertServiceResultToBusinessLogicResponse(
                                    //                            new Tuple<Error, object>(serviceResult.Item1, (object)serviceResult.Item2));

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

        [Route("[controller]/GetById/{groupId}")]
        [HttpPost]
        public async Task<BusinessLogicResponse> GetUsersByGroupIdAsync(int groupId,[FromBody] BusinessLogicRequest request = null)
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
                                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartMulti<DFManagementStdDto.AssignmentGroupEmployeeDto>.
                                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<ErrorCodes, object>(ErrorCodes.RequestBodyMalformedOnUndefinedRequest, null),
                                                                                                HttpResponseCodes.BadRequest, false);
                                return returnResponseClient;
                            }

                            Tuple<Error, IEnumerable<DFManagementStdDto.AssignmentGroupEmployeeDto>> serviceResult =
                                    new(Errors.General.None(), new List<DFManagementStdDto.AssignmentGroupEmployeeDto>());                            

                            if (request != null)
                            {
                                switch (request.RequestDomain)
                                {
                                    case RemoteIncomingDomains.DiasTesisYonetimWebClient:
                                        {
                                            //if ((request.AdditionalParamTypes != null) && (request.AdditionalParamTypes.Count > 0) &&
                                            //     (request.AdditionalParamJsons != null) && (request.AdditionalParamJsons.Count > 0))
                                            //{
                                            //    int groupId = JsonConvert.DeserializeObject<int>(request.AdditionalParamJsons[0]);

                                            //}

                                            serviceResult = await _assignmentGroupEmployeesRepository.GetUsersByGroupIdAsync(groupId);

                                            BusinessLogicResponse returnResponse = ResponseOperationsStandartMulti<DFManagementStdDto.AssignmentGroupEmployeeDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<Error, object>(serviceResult.Item1, (object)serviceResult.Item2));

                                            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

                                            return returnResponse;
                                        }

                                    //case RemoteIncomingDomains.DiasTesisYonetimMobileClient:
                                    //    {

                                    //        serviceResult = await _attachmentWrapperDtoRepository.AddAttachmentWrapperAsync(request);

                                    //        BusinessLogicResponse returnResponse = ResponseOperationsCustomSingle<DFManagementCustomDto.CustomAttachmentDto>.ConvertServiceResultToBusinessLogicResponse(
                                    //                            new Tuple<Error, object>(serviceResult.Item1, (object)serviceResult.Item2));

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
