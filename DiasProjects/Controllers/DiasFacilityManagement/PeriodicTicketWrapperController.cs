using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DFManagementStdDtoInterface = DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement;
using DFManagementCustomDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using DiasShared.Services.Communication.BusinessLogicMessage;
using DiasShared.Operations.CommunicationOperations.DiasFacilityManagement;
using static DiasShared.Enums.Standart.HttpCodesEnum;
using Newtonsoft.Json;
using System.Collections.Generic;
using static DiasShared.Enums.Standart.RemoteCommunicationEnums;
using Microsoft.Extensions.Configuration;
using DiasWebApi.Shared.Configuration;
using static DiasWebApi.Shared.Enums.WebApiApplicationEnums;
using DFManagementCustomTestDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Custom;
using DiasBusinessLogic.Shared.Error;
using DiasShared.Errors;
using DiasShared.Operations.CommunicationOperations;

namespace DiasWebApi.DiasFacilityManagement.Controllers
{
    [ApiController]
    public class PeriodicTicketWrapperController : Controller
    {
        private readonly DFManagementStdDtoInterface.IPeriodicTicketWrapperDtoRepository _periodicTicketWrapperDtoRepository;

        public PeriodicTicketWrapperController(DFManagementStdDtoInterface.IPeriodicTicketWrapperDtoRepository periodicTicketWrapperDtoRepository)
        {
            _periodicTicketWrapperDtoRepository = periodicTicketWrapperDtoRepository;
        }

        #region ControllerRoute
        [Route("[controller]/GetAll")]
        [HttpPost]
        public async Task<BusinessLogicResponse> GetAllPeriodicTicketsWrapperAsync([FromBody] BusinessLogicRequest request = null)
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
                                BusinessLogicResponse returnResponseClient = ResponseOperationsCustomMulti<DFManagementCustomDto.CustomPeriodicTicketDto>.
                                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<Error, object>(Errors.General.ModelisInValid("PeriodicTicket"), null),
                                                                                                HttpResponseCodes.BadRequest, false);
                                return returnResponseClient;
                            }

                            if (request != null)
                            {
                                //Hangi istemciden geldi
                                switch (request.RequestDomain)
                                {
                                    case RemoteIncomingDomains.DiasTesisYonetimWebClient:
                                        {
                                            Tuple<Error, IEnumerable<DFManagementCustomDto.CustomPeriodicTicketDto>> serviceResult = await _periodicTicketWrapperDtoRepository.GetAllPeriodicTicketsWrapperAsync(request.DevExpressRequestObj);
                                            BusinessLogicResponse returnResponse = ResponseOperationsCustomMulti<DFManagementCustomDto.CustomPeriodicTicketDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                    new Tuple<Error, object>(serviceResult.Item1, (object)serviceResult.Item2));

                                            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

                                            return returnResponse;
                                        }

                                    case RemoteIncomingDomains.DiasTesisYonetimMobileClient:
                                        {
                                            //TODO : Hata yönetimi yapılacak
                                            throw new NotImplementedException();
                                        }

                                    default:
                                        {
                                            //TODO : Hata yönetimi yapılacak
                                            throw new ArgumentException();
                                        }
                                }


                            }
                            else
                            {
                                //TODO : Hata yönetimi yapılacak
                                throw new ArgumentException();
                            }
                        }
                    case ApplicationWorkingEnvironment.Test:
                        {
                            if (!ModelState.IsValid)
                            {
                                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartMultiTest<DFManagementCustomTestDto.CustomPeriodicTicketDto>.
                                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<ErrorCodes, object>(ErrorCodes.RequestBodyMalformedOnUndefinedRequest, null),
                                                                                                HttpResponseCodes.BadRequest, false);
                                return returnResponseClient;
                            }
                            Tuple<Error, IEnumerable<DFManagementCustomTestDto.CustomPeriodicTicketDto>> serviceResult = new(Errors.General.None(), new List<DFManagementCustomTestDto.CustomPeriodicTicketDto>());


                            if (request != null)
                            {
                                switch (request.RequestDomain)
                                {
                                    case RemoteIncomingDomains.DiasTesisYonetimWebClient:
                                        {
                                            serviceResult = await _periodicTicketWrapperDtoRepository.GetAllPeriodicTicketsWrapperTestAsync(request.DevExpressRequestObj);
                                            BusinessLogicResponse returnResponse = ResponseOperationsCustomMultiTest<DFManagementCustomTestDto.CustomPeriodicTicketDto>.ConvertServiceResultToBusinessLogicResponse(
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

        [Route("[controller]/GetById/{id}")]
        [HttpGet]
        public async Task<BusinessLogicResponse> GetTicketWrapperByIdAsync(int id, [FromBody] BusinessLogicRequest request = null)
        {

            try
            {

                IConfiguration settings = ConfigurationHelper.GetConfig();
                switch (ConfigurationHelper.GetWorkingEnvironment(settings))
                {
                    case ApplicationWorkingEnvironment.Development:
                        {
                            Tuple<Error, DFManagementCustomDto.CustomPeriodicTicketDto> serviceResult = new(Errors.General.None(), new DFManagementCustomDto.CustomPeriodicTicketDto());

                            if (request != null)
                            {
                                switch (request.RequestDomain)
                                {
                                    case RemoteIncomingDomains.DiasTesisYonetimWebClient:
                                        {
                                            serviceResult = await _periodicTicketWrapperDtoRepository.GetPeriodicTicketWrapperByIdAsync(id);

                                            BusinessLogicResponse returnResponse = ResponseOperationsCustomSingle<DFManagementCustomDto.CustomPeriodicTicketDto>.ConvertServiceResultToBusinessLogicResponse(
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
                            Tuple<Error, DFManagementCustomTestDto.CustomPeriodicTicketDto> serviceResult = new(Errors.General.None(), new DFManagementCustomTestDto.CustomPeriodicTicketDto());

                            if (request != null)
                            {
                                switch (request.RequestDomain)
                                {
                                    case RemoteIncomingDomains.DiasTesisYonetimWebClient:
                                        {
                                            serviceResult = await _periodicTicketWrapperDtoRepository.GetPeriodicTicketWrapperByIdTestAsync(id);

                                            BusinessLogicResponse returnResponse = ResponseOperationsCustomSingleTest<DFManagementCustomTestDto.CustomPeriodicTicketDto>.ConvertServiceResultToBusinessLogicResponse(
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
                                BusinessLogicResponse returnResponseClient = ResponseOperationsCustomSingle<DFManagementCustomDto.CustomPeriodicTicketDto>.
                                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<Error, object>(Errors.General.ModelisInValid("PeriodicTicket"), null),
                                                                                                HttpResponseCodes.BadRequest, false);
                                return returnResponseClient;
                            }

                            Tuple<Error, DFManagementCustomDto.CustomPeriodicTicketDto> serviceResult = new(Errors.General.None(), new DFManagementCustomDto.CustomPeriodicTicketDto());
                            if (request != null)
                            {
                                switch (request.RequestDomain)
                                {
                                    case RemoteIncomingDomains.DiasTesisYonetimWebClient:
                                        {
                                            serviceResult = await _periodicTicketWrapperDtoRepository.AddPeriodicTicketWrapperAsync(request);
                                            BusinessLogicResponse returnResponse = ResponseOperationsCustomSingle<DFManagementCustomDto.CustomPeriodicTicketDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                    new Tuple<Error, object>(serviceResult.Item1, (object)serviceResult.Item2));

                                            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

                                            return returnResponse;
                                        }
                                    case RemoteIncomingDomains.DiasTesisYonetimMobileClient:
                                        {
                                            //TODO : Hata yönetimi yapılacak
                                            throw new NotImplementedException();
                                        }

                                    default:
                                        {
                                            //TODO : Hata yönetimi yapılacak
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
                                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartSingleTest<DFManagementCustomTestDto.CustomPeriodicTicketDto>.
                                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<ErrorCodes, object>(ErrorCodes.RequestBodyMalformedOnUndefinedRequest, null),
                                                                                                HttpResponseCodes.BadRequest, false);
                                return returnResponseClient;
                            }

                            Tuple<Error, DFManagementCustomTestDto.CustomPeriodicTicketDto> serviceResult = new(Errors.General.None(), new DFManagementCustomTestDto.CustomPeriodicTicketDto());
                            if (request != null)
                            {
                                switch (request.RequestDomain)
                                {
                                    case RemoteIncomingDomains.DiasTesisYonetimWebClient:
                                        {
                                            serviceResult = await _periodicTicketWrapperDtoRepository.AddPeriodicTicketWrapperTestAsync(request);
                                            BusinessLogicResponse returnResponse = ResponseOperationsCustomSingleTest<DFManagementCustomTestDto.CustomPeriodicTicketDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                    new Tuple<Error, object>(serviceResult.Item1, (object)serviceResult.Item2));

                                            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

                                            return returnResponse;
                                        }
                                    case RemoteIncomingDomains.DiasTesisYonetimMobileClient:
                                        {
                                            //TODO : Hata yönetimi yapılacak
                                            throw new NotImplementedException();
                                        }

                                    default:
                                        {
                                            //TODO : Hata yönetimi yapılacak
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

        [Route("[controller]/Update")]
        [HttpPost]
        public async Task<BusinessLogicResponse> UpdateAsync([FromBody] BusinessLogicRequest request = null)
        {

            IConfiguration settings = ConfigurationHelper.GetConfig();
            switch (ConfigurationHelper.GetWorkingEnvironment(settings))
            {
                case ApplicationWorkingEnvironment.Development:
                    {
                        if (!ModelState.IsValid)
                        {
                            BusinessLogicResponse returnResponseClient = ResponseOperationsCustomSingle<DFManagementCustomDto.CustomPeriodicTicketDto>.
                                                                                    ConvertServiceResultToBusinessLogicResponse(new Tuple<Error, object>(Errors.General.ModelisInValid("PeriodicTicket"), null),
                                                                                            HttpResponseCodes.BadRequest, false);
                            return returnResponseClient;
                        }
                        Tuple<Error, DFManagementCustomDto.CustomPeriodicTicketDto> serviceResult = new(Errors.General.None(), new DFManagementCustomDto.CustomPeriodicTicketDto());

                        if (request != null)
                        {
                            switch (request.RequestDomain)
                            {
                                case RemoteIncomingDomains.DiasTesisYonetimWebClient:
                                    {
                                        serviceResult = await _periodicTicketWrapperDtoRepository.UpdatePeriodicTicketWrapperAsync(request);
                                        BusinessLogicResponse returnResponse = ResponseOperationsCustomSingle<DFManagementCustomDto.CustomPeriodicTicketDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<Error, object>(serviceResult.Item1, (object)serviceResult.Item2));

                                        returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

                                        return returnResponse;
                                    }
                                //todo : development için kullanılacak mobile methodu yapılacak ? 
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
                            BusinessLogicResponse returnResponseClient = ResponseOperationsCustomSingleTest<DFManagementCustomTestDto.CustomPeriodicTicketDto>.
                                                                                    ConvertServiceResultToBusinessLogicResponse(new Tuple<Error, object>(Errors.General.ModelisInValid("PeriodicTicket"), null),
                                                                                            HttpResponseCodes.BadRequest, false);
                            return returnResponseClient;
                        }
                        Tuple<Error, DFManagementCustomTestDto.CustomPeriodicTicketDto> serviceResult = new(Errors.General.None(), new DFManagementCustomTestDto.CustomPeriodicTicketDto());

                        if (request != null)
                        {
                            switch (request.RequestDomain)
                            {
                                case RemoteIncomingDomains.DiasTesisYonetimWebClient:
                                    {
                                        serviceResult = await _periodicTicketWrapperDtoRepository.UpdatePeriodicTicketWrapperTestAsync(request);
                                        BusinessLogicResponse returnResponse = ResponseOperationsCustomSingleTest<DFManagementCustomTestDto.CustomPeriodicTicketDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<Error, object>(serviceResult.Item1, (object)serviceResult.Item2));

                                        returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

                                        return returnResponse;
                                    }
                                //todo : development için kullanılacak mobile methodu yapılacak ? 
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

        [Route("[controller]/Delete")]
        [HttpPost]
        public async Task<BusinessLogicResponse> DeleteAsync([FromBody] BusinessLogicRequest request = null)
        {
            IConfiguration settings = ConfigurationHelper.GetConfig();
            switch (ConfigurationHelper.GetWorkingEnvironment(settings))
            {
                case ApplicationWorkingEnvironment.Development:
                    {
                        if (!ModelState.IsValid)
                        {
                            BusinessLogicResponse returnResponseClient = ResponseOperationsCustomSingle<DFManagementCustomDto.CustomPeriodicTicketDto>.
                                                                                    ConvertServiceResultToBusinessLogicResponse(new Tuple<Error, object>(Errors.General.ModelisInValid("PeriodicTicket"), null),
                                                                                            HttpResponseCodes.BadRequest, false);
                            return returnResponseClient;
                        }
                        Tuple<Error, DFManagementCustomDto.CustomPeriodicTicketDto> serviceResult = new(Errors.General.None(), new DFManagementCustomDto.CustomPeriodicTicketDto());
                        if (request != null)
                        {
                            switch (request.RequestDomain)
                            {
                                case RemoteIncomingDomains.DiasTesisYonetimWebClient:
                                    {
                                        serviceResult = await _periodicTicketWrapperDtoRepository.DeletePeriodicTicketWrapperAsync(request);

                                        BusinessLogicResponse returnResponse = ResponseOperationsCustomSingle<DFManagementCustomDto.CustomPeriodicTicketDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<Error, object>(serviceResult.Item1, (object)serviceResult.Item2));

                                        returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

                                        return returnResponse;
                                    }
                                //todo : development için kullanılacak mobile methodu yapılacak ? 
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
                            BusinessLogicResponse returnResponseClient = ResponseOperationsCustomSingleTest<DFManagementCustomTestDto.CustomPeriodicTicketDto>.
                                                                                    ConvertServiceResultToBusinessLogicResponse(new Tuple<Error, object>(Errors.General.ModelisInValid("PeriodicTicket"), null),
                                                                                            HttpResponseCodes.BadRequest, false);
                            return returnResponseClient;
                        }
                        Tuple<Error, DFManagementCustomTestDto.CustomPeriodicTicketDto> serviceResult = new(Errors.General.None(), new DFManagementCustomTestDto.CustomPeriodicTicketDto());

                        if (request != null)
                        {
                            switch (request.RequestDomain)
                            {
                                case RemoteIncomingDomains.DiasTesisYonetimWebClient:
                                    {
                                        serviceResult = await _periodicTicketWrapperDtoRepository.DeletePeriodicTicketWrapperTestAsync(request);
                                        BusinessLogicResponse returnResponse = ResponseOperationsCustomSingleTest<DFManagementCustomTestDto.CustomPeriodicTicketDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<Error, object>(serviceResult.Item1, (object)serviceResult.Item2));

                                        returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

                                        return returnResponse;
                                    }
                                //todo : development için kullanılacak mobile methodu yapılacak ? 
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

        #endregion ControllerRoute

        #region Controller_ActionRoute


        #endregion Controller_ActionRoute       

    }
}
