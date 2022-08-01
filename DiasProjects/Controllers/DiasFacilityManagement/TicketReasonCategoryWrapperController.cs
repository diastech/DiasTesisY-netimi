using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DFManagementStdDtoInterface = DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement;
using DFManagementCustomDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using DFManagementCustomTestDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Custom;
using DiasShared.Services.Communication.BusinessLogicMessage;
using DiasShared.Operations.CommunicationOperations.DiasFacilityManagement;
using static DiasShared.Enums.Standart.HttpCodesEnum;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DiasWebApi.Shared.Configuration;
using static DiasWebApi.Shared.Enums.WebApiApplicationEnums;
using static DiasShared.Enums.Standart.RemoteCommunicationEnums;
using DiasShared.Errors;
using DiasBusinessLogic.Shared.Error;
using DiasShared.Operations.CommunicationOperations;

namespace DiasWebApi.DiasFacilityManagement.Controllers
{
    [ApiController]
    public class TicketReasonCategoryWrapperController : Controller
    {
        private readonly DFManagementStdDtoInterface.ITicketReasonCategoryWrapperDtoRepository _ticketReasonCategoryWrapperDtoRepository;

        public TicketReasonCategoryWrapperController(DFManagementStdDtoInterface.ITicketReasonCategoryWrapperDtoRepository ticketReasonCategoryWrapperDtoRepository)
        {
            _ticketReasonCategoryWrapperDtoRepository = ticketReasonCategoryWrapperDtoRepository;
        }

        #region ControllerRoute
        [Route("[controller]/GetAll")]
        [HttpPost]
        public async Task<BusinessLogicResponse> GetAllTicketReasonCategoriesWrapperAsync([FromBody] BusinessLogicRequest request = null)
        {
            try
            {
                IConfiguration settings = ConfigurationHelper.GetConfig();

                switch (ConfigurationHelper.GetWorkingEnvironment(settings))
                {
                    case ApplicationWorkingEnvironment.Development:
                        {
                            Tuple<Error, IEnumerable<DFManagementCustomDto.CustomTicketReasonCategoryDto>> serviceResult = new(Errors.General.None(), new List<DFManagementCustomDto.CustomTicketReasonCategoryDto>());
                            if (request != null)
                            {
                                switch (request.RequestDomain)
                                {
                                    case RemoteIncomingDomains.DiasTesisYonetimWebClient:
                                        {
                                            serviceResult = await _ticketReasonCategoryWrapperDtoRepository.GetAllTicketReasonCategoriesWrapperAsync();
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
                                BusinessLogicResponse returnResponse = ResponseOperationsCustomMulti<DFManagementCustomDto.CustomTicketReasonCategoryDto>.ConvertServiceResultToBusinessLogicResponse(
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
                            Tuple<ErrorCodes, IEnumerable<DFManagementCustomTestDto.CustomTicketReasonCategoryDto>> serviceResult = new(ErrorCodes.None, new List<DFManagementCustomTestDto.CustomTicketReasonCategoryDto>());
                            if (request != null)
                            {
                                switch (request.RequestDomain)
                                {
                                    case RemoteIncomingDomains.DiasTesisYonetimWebClient:
                                        {
                                            serviceResult = await _ticketReasonCategoryWrapperDtoRepository.GetAllTicketReasonCategoriesWrapperTestAsync();
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
                                BusinessLogicResponse returnResponse = ResponseOperationsCustomMultiTest<DFManagementCustomTestDto.CustomTicketReasonCategoryDto>.ConvertServiceResultToBusinessLogicResponse(
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

        [Route("[controller]/GetById")]
        [HttpGet]
        public async Task<BusinessLogicResponse> GetTicketReasonCategoryWrapperByIdAsync([FromQuery] string hierarchyId, [FromBody] BusinessLogicRequest request = null)
        {

            try
            {
                IConfiguration settings = ConfigurationHelper.GetConfig();

                switch (ConfigurationHelper.GetWorkingEnvironment(settings))
                {
                    case ApplicationWorkingEnvironment.Development:
                        {
                            Tuple<Error, DFManagementCustomDto.CustomTicketReasonCategoryDto> serviceResult = new(Errors.General.None(), new DFManagementCustomDto.CustomTicketReasonCategoryDto());
                            if (request != null)
                            {
                                switch (request.RequestDomain)
                                {
                                    case RemoteIncomingDomains.DiasTesisYonetimWebClient:
                                        {
                                            serviceResult = await _ticketReasonCategoryWrapperDtoRepository.GetTicketReasonCategoryWrapperByIdAsync(hierarchyId);
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
                                BusinessLogicResponse returnResponse = ResponseOperationsCustomMulti<DFManagementCustomDto.CustomTicketReasonCategoryDto>.ConvertServiceResultToBusinessLogicResponse(
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
                            Tuple<ErrorCodes, DFManagementCustomTestDto.CustomTicketReasonCategoryDto> serviceResult = new(ErrorCodes.None, new DFManagementCustomTestDto.CustomTicketReasonCategoryDto());
                            if (request != null)
                            {
                                switch (request.RequestDomain)
                                {
                                    case RemoteIncomingDomains.DiasTesisYonetimWebClient:
                                        {
                                            serviceResult = await _ticketReasonCategoryWrapperDtoRepository.GetTicketReasonCategoryWrapperByIdTestAsync(hierarchyId);
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
                                BusinessLogicResponse returnResponse = ResponseOperationsCustomMultiTest<DFManagementCustomTestDto.CustomTicketReasonCategoryDto>.ConvertServiceResultToBusinessLogicResponse(
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

        #endregion

        #region Controller_ActionRoute

        #endregion

    }


}
