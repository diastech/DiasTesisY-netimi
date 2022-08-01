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
using DiasShared.Operations.CommunicationOperations.DiasFacilityManagement;
using static DiasShared.Enums.Standart.HttpCodesEnum;
using Newtonsoft.Json;
using System.Collections.Generic;
using DevExtreme.AspNet.Mvc;
using System.Text.RegularExpressions;
using static DiasShared.Enums.Standart.RemoteCommunicationEnums;
using Microsoft.Extensions.Configuration;
using DiasWebApi.Shared.Configuration;
using static DiasWebApi.Shared.Enums.WebApiApplicationEnums;
using DiasShared.Errors;
using DiasBusinessLogic.Shared.Error;
using DiasShared.Operations.CommunicationOperations;
using DevExtreme.AspNet.Data;
using DiasShared.Operations.JsonOperation.Resolvers;

namespace DiasWebApi.DiasFacilityManagement.Controllers
{
    [ApiController]
    public class TicketWrapperController : Controller
    {
        private readonly DFManagementStdDtoInterface.ITicketWrapperDtoRepository _ticketWrapperDtoRepository;
        private readonly DFManagementStdDtoInterface.IAttachmentDtoRepository _attachmentDtoRepository;
        private readonly DFManagementStdDtoInterface.ILocationWrapperDtoRepository _locationWrapperDtoRepository;

        public TicketWrapperController(DFManagementStdDtoInterface.ITicketWrapperDtoRepository ticketWrapperDtoRepository,
            DFManagementStdDtoInterface.ILocationWrapperDtoRepository locationWrapperDtoRepository,
            DFManagementStdDtoInterface.IAttachmentDtoRepository attachmentDtoRepository)
        {
            _ticketWrapperDtoRepository = ticketWrapperDtoRepository;
            _attachmentDtoRepository = attachmentDtoRepository;
            _locationWrapperDtoRepository = locationWrapperDtoRepository;
        }

        #region ControllerRoute
        [Route("[controller]/GetAll")]
        [HttpPost]
        public async Task<BusinessLogicResponse> GetAllTicketWrapperAsync([FromBody] BusinessLogicRequest request = null)
        {
            try
            {
                //standart json clientdan gelen multi filtreyi bozduğu için newtonsoft'u kullanıyoruz
                //kullanırken multi, tekli filtre ayrımı yapmıyoruz
                if ((request != null) && (request.DevExpressRequestObj != null) && 
                      (request.DevExpressRequestObj.RequestOptions != null) &&
                       (request.DevExpressRequestObj.RequestOptions.DataSourceLoadOption != null) &&
                        (request.DevExpressRequestObj.RequestOptions.DataSourceLoadOption.Filter != null) && 
                         (request.DevExpressRequestObj.RequestOptions.DataSourceLoadOption.Filter.Count > 0) &&
                          (!(String.IsNullOrEmpty(request.DevExpressRequestObj.AdditionalDevExpressParamJson))))
                {
                    DataSourceLoadOptionsBase serializedFilter = JsonConvert.DeserializeObject<DataSourceLoadOptionsBase>(request.DevExpressRequestObj.AdditionalDevExpressParamJson);
                    request.DevExpressRequestObj.RequestOptions.DataSourceLoadOption.Filter = serializedFilter.Filter;
                }

                IConfiguration settings = ConfigurationHelper.GetConfig();

                JsonSerializerSettings jsonSettings = new JsonSerializerSettings
                {
                    ContractResolver = new NonVirtualResolver(),
                    PreserveReferencesHandling = PreserveReferencesHandling.None,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    Formatting = Formatting.Indented
                };

                switch (ConfigurationHelper.GetWorkingEnvironment(settings))
                {
                    case ApplicationWorkingEnvironment.Development:
                        {
                            if (!ModelState.IsValid)
                            {
                                BusinessLogicResponse returnResponseClient = ResponseOperationsCustomMulti<DFManagementCustomDto.CustomTicketDto>.
                                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<ErrorCodes, object>(ErrorCodes.RequestBodyMalformedOnUndefinedRequest, null),
                                                                                                HttpResponseCodes.BadRequest, false);
                                return returnResponseClient;
                            }

                            Tuple<Error, IEnumerable<DFManagementCustomDto.CustomTicketDto>> serviceResult =new(Errors.General.None(), new List<DFManagementCustomDto.CustomTicketDto>());

                            if (request != null)
                            {
                                switch (request.RequestDomain) 
                                {
                                    case RemoteIncomingDomains.DiasTesisYonetimWebClient:
                                        {                                         

                                            serviceResult = await _ticketWrapperDtoRepository.GetAllTicketWrapperAsync(request.DevExpressRequestObj);
                                            BusinessLogicResponse returnResponse = ResponseOperationsCustomMulti<DFManagementCustomDto.CustomTicketDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                    new Tuple<Error, object>(serviceResult.Item1, (object)serviceResult.Item2));

                                            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2, jsonSettings);   
                                            
                                            return returnResponse;                                            
                                        }
                                    case RemoteIncomingDomains.DiasTesisYonetimMobileClient:
                                        {
                                            serviceResult = await _ticketWrapperDtoRepository.GetAllTicketWrapperMobileAsync(request.DevExpressRequestObj);
                                            BusinessLogicResponse returnResponse = ResponseOperationsCustomMulti<DFManagementCustomDto.CustomTicketDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                    new Tuple<Error, object>(serviceResult.Item1, (object)serviceResult.Item2));

                                            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2, jsonSettings);

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
                            if (!ModelState.IsValid)
                            {
                                BusinessLogicResponse returnResponseClientTest = ResponseOperationsCustomMultiTest<DFManagementCustomTestDto.CustomTicketDto>.
                                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<ErrorCodes, object>(ErrorCodes.RequestBodyMalformedOnUndefinedRequest, null),
                                                                                                HttpResponseCodes.BadRequest, false);
                                return returnResponseClientTest;
                            }                          

                            Tuple<ErrorCodes, IEnumerable<DFManagementCustomTestDto.CustomTicketDto>> serviceResult =new(ErrorCodes.None, new List<DFManagementCustomTestDto.CustomTicketDto>());

                            if (request != null)
                            {
                                switch (request.RequestDomain)
                                {
                                    case RemoteIncomingDomains.DiasTesisYonetimWebClient:
                                        {
                                            serviceResult = await _ticketWrapperDtoRepository.GetAllTicketWrapperTestAsync(request.DevExpressRequestObj);
                                            BusinessLogicResponse returnResponse = ResponseOperationsCustomMultiTest<DFManagementCustomTestDto.CustomTicketDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                    new Tuple<ErrorCodes, object>(serviceResult.Item1, (object)serviceResult.Item2));
                                            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2, jsonSettings);
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
        [HttpPost]
        public async Task<BusinessLogicResponse> GetTicketWrapperByIdAsync(int id,[FromBody] BusinessLogicRequest request = null)
        {
            try
            {
                IConfiguration settings = ConfigurationHelper.GetConfig();

                switch (ConfigurationHelper.GetWorkingEnvironment(settings))
                {
                    case ApplicationWorkingEnvironment.Development:
                        {
                            
                            Tuple<Error, DFManagementCustomDto.CustomTicketDto> serviceResult =new(Errors.General.None(), new DFManagementCustomDto.CustomTicketDto());

                            if (request != null)
                            {
                                switch (request.RequestDomain)
                                {
                                    case RemoteIncomingDomains.DiasTesisYonetimWebClient:
                                        {
                                            serviceResult = await _ticketWrapperDtoRepository.GetTicketWrapperByIdAsync(id);

                                            BusinessLogicResponse returnResponse = ResponseOperationsCustomSingle<DFManagementCustomDto.CustomTicketDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<Error, object>(serviceResult.Item1, (object)serviceResult.Item2));

                                            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

                                            return returnResponse;
                                        }

                                    //şuna mobile için yok
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
                                BusinessLogicResponse returnResponseClientTest = ResponseOperationsStandartMultiTest<DFManagementCustomTestDto.CustomTicketDto>.
                                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<ErrorCodes, object>(ErrorCodes.RequestBodyMalformedOnUndefinedRequest, null),
                                                                                                HttpResponseCodes.BadRequest, false);
                                return returnResponseClientTest;
                            }

                            Tuple<ErrorCodes, DFManagementCustomTestDto.CustomTicketDto> serviceResult = new(ErrorCodes.None, new DFManagementCustomTestDto.CustomTicketDto());

                            if (request != null)
                            {
                                switch (request.RequestDomain)
                                {
                                    case RemoteIncomingDomains.DiasTesisYonetimWebClient:
                                        {
                                            serviceResult = await _ticketWrapperDtoRepository.GetTicketWrapperByIdTestAsync(id);

                                            BusinessLogicResponse returnResponse = ResponseOperationsCustomSingleTest<DFManagementCustomTestDto.CustomTicketDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<ErrorCodes, object>(serviceResult.Item1, (object)serviceResult.Item2));

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
                                BusinessLogicResponse returnResponseClient = ResponseOperationsCustomSingle<DFManagementCustomDto.CustomTicketDto>.
                                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<ErrorCodes, object>(ErrorCodes.RequestBodyMalformedOnUndefinedRequest, null),
                                                                                                HttpResponseCodes.BadRequest, false);
                                return returnResponseClient;
                            }

                            Tuple<Error, DFManagementCustomDto.CustomTicketDto> serviceResult =
                                    new(Errors.General.None(), new DFManagementCustomDto.CustomTicketDto());

                            Tuple<Error, DFManagementCustomDto.CustomMobileTicketDto> serviceResultMobile =
                                                new(Errors.General.None(), new DFManagementCustomDto.CustomMobileTicketDto());

                            if (request != null)
                            {
                                switch (request.RequestDomain)
                                {
                                    case RemoteIncomingDomains.DiasTesisYonetimWebClient:
                                        {
                                            serviceResult = await _ticketWrapperDtoRepository.AddTicketWrapperAsync(request);

                                            BusinessLogicResponse returnResponse = ResponseOperationsCustomSingle<DFManagementCustomDto.CustomTicketDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<Error, object>(serviceResult.Item1, (object)serviceResult.Item2));

                                            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

                                            return returnResponse;
                                        }

                                    case RemoteIncomingDomains.DiasTesisYonetimMobileClient:
                                        {
                                            serviceResultMobile = await _ticketWrapperDtoRepository.AddTicketWrapperMobileAsync(request);

                                            BusinessLogicResponse returnResponse = ResponseOperationsCustomSingle<DFManagementCustomDto.CustomTicketDto>.ConvertServiceResultToBusinessLogicResponse(
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
                            if (!ModelState.IsValid)
                            {
                                BusinessLogicResponse returnResponseClientTest = ResponseOperationsStandartMultiTest<DFManagementCustomTestDto.CustomTicketDto>.
                                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<ErrorCodes, object>(ErrorCodes.RequestBodyMalformedOnUndefinedRequest, null),
                                                                                                HttpResponseCodes.BadRequest, false);
                                return returnResponseClientTest;
                            }

                            Tuple<ErrorCodes, DFManagementCustomTestDto.CustomTicketDto> serviceResult =
                                    new(ErrorCodes.None, new DFManagementCustomTestDto.CustomTicketDto());

                            Tuple<ErrorCodes, DFManagementCustomTestDto.CustomMobileTicketDto> serviceResultMobile =
                                                new(ErrorCodes.None, new DFManagementCustomTestDto.CustomMobileTicketDto());

                            if (request != null)
                            {
                                switch (request.RequestDomain)
                                {
                                    case RemoteIncomingDomains.DiasTesisYonetimWebClient:
                                        {
                                            serviceResult = await _ticketWrapperDtoRepository.AddTicketWrapperTestAsync(request);

                                            BusinessLogicResponse returnResponse = ResponseOperationsCustomSingleTest<DFManagementCustomTestDto.CustomTicketDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<ErrorCodes, object>(serviceResult.Item1, (object)serviceResult.Item2));

                                            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

                                            return returnResponse;
                                        }

                                    case RemoteIncomingDomains.DiasTesisYonetimMobileClient:
                                        {
                                            serviceResultMobile = await _ticketWrapperDtoRepository.AddTicketWrapperMobileTestAsync(request);

                                            BusinessLogicResponse returnResponse = ResponseOperationsCustomSingleTest<DFManagementCustomTestDto.CustomMobileTicketDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<ErrorCodes, object>(serviceResult.Item1, (object)serviceResult.Item2));

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

            try
            {
                IConfiguration settings = ConfigurationHelper.GetConfig();

                switch (ConfigurationHelper.GetWorkingEnvironment(settings))
                {
                    case ApplicationWorkingEnvironment.Development:
                        {
                            if (!ModelState.IsValid)
                            {
                                BusinessLogicResponse returnResponseClient = ResponseOperationsCustomSingle<DFManagementCustomDto.CustomTicketDto>.
                                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<ErrorCodes, object>(ErrorCodes.RequestBodyMalformedOnUndefinedRequest, null),
                                                                                                HttpResponseCodes.BadRequest, false);
                                return returnResponseClient;
                            }

                            Tuple<Error, DFManagementCustomDto.CustomTicketDto> serviceResult =
                                    new(Errors.General.None(), new DFManagementCustomDto.CustomTicketDto());

                            Tuple<ErrorCodes, DFManagementCustomDto.CustomMobileTicketDto> serviceResultMobile =
                                                new(ErrorCodes.None, new DFManagementCustomDto.CustomMobileTicketDto());

                            if (request != null)
                            {
                                switch (request.RequestDomain)
                                {
                                    case RemoteIncomingDomains.DiasTesisYonetimWebClient:
                                        {
                                            serviceResult = await _ticketWrapperDtoRepository.UpdateTicketWrapperAsync(request);

                                            BusinessLogicResponse returnResponse = ResponseOperationsCustomSingle<DFManagementCustomDto.CustomTicketDto>.ConvertServiceResultToBusinessLogicResponse(
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
                                BusinessLogicResponse returnResponseClientTest = ResponseOperationsStandartMultiTest<DFManagementCustomTestDto.CustomTicketDto>.
                                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<ErrorCodes, object>(ErrorCodes.RequestBodyMalformedOnUndefinedRequest, null),
                                                                                                HttpResponseCodes.BadRequest, false);
                                return returnResponseClientTest;
                            }

                            Tuple<ErrorCodes, DFManagementCustomTestDto.CustomTicketDto> serviceResult =
                                    new(ErrorCodes.None, new DFManagementCustomTestDto.CustomTicketDto());

                            Tuple<ErrorCodes, DFManagementCustomTestDto.CustomMobileTicketDto> serviceResultMobile =
                                                new(ErrorCodes.None, new DFManagementCustomTestDto.CustomMobileTicketDto());

                            if (request != null)
                            {
                                switch (request.RequestDomain)
                                {
                                    case RemoteIncomingDomains.DiasTesisYonetimWebClient:
                                        {
                                            serviceResult = await _ticketWrapperDtoRepository.UpdateTicketWrapperTestAsync(request);

                                            BusinessLogicResponse returnResponse = ResponseOperationsCustomSingleTest<DFManagementCustomTestDto.CustomTicketDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<ErrorCodes, object>(serviceResult.Item1, (object)serviceResult.Item2));

                                            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

                                            return returnResponse;
                                        }

                                    //todo : test için kullanılacak mobile methodu yapılacak ?

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

        [Route("[controller]/Delete")]
        [HttpGet]
        public async Task<BusinessLogicResponse> DeleteAsync([FromBody] BusinessLogicRequest request = null)
        {
            if (!ModelState.IsValid)
            {
                BusinessLogicResponse returnResponseClient = ResponseOperationsCustomMulti<DFManagementCustomDto.CustomTicketDto>.
                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<ErrorCodes, object>(ErrorCodes.RequestBodyMalformedOnUndefinedRequest, null),
                                                                                HttpResponseCodes.BadRequest, false);
                return returnResponseClient;
            }

            Tuple<Error, DFManagementCustomDto.CustomTicketDto> serviceResult =
                                    new(Errors.General.None(), new DFManagementCustomDto.CustomTicketDto());            

            if (request != null)
            {
                serviceResult = await _ticketWrapperDtoRepository.DeleteTicketWrapperAsync(request);
            }
            else
            {
                throw new ArgumentException();
            }

            BusinessLogicResponse returnResponse = ResponseOperationsCustomMulti<DFManagementCustomDto.CustomTicketDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<Error, object>(serviceResult.Item1, (object)serviceResult.Item2));

            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

            return returnResponse;
        }

        #endregion

        #region Controller_ActionRoute

        [Route("[controller]/InsertWithFastTicket")]
        [HttpPost]
        public async Task<BusinessLogicResponse> InsertFastTicketAsync([FromBody] DFManagementCustomDto.CustomTicketDto resource)
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
                                BusinessLogicResponse returnResponseClient = ResponseOperationsCustomSingle<DFManagementCustomDto.CustomTicketDto>.
                                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<ErrorCodes, object>(ErrorCodes.RequestBodyMalformedOnUndefinedRequest, null),
                                                                                                HttpResponseCodes.BadRequest, false);
                                return returnResponseClient;
                            }

                            Tuple<Error, DFManagementCustomDto.CustomTicketDto> serviceResult =
                                    new(Errors.General.None(), new DFManagementCustomDto.CustomTicketDto());

                            Tuple<ErrorCodes, DFManagementCustomDto.CustomMobileTicketDto> serviceResultMobile =
                                                new(ErrorCodes.None, new DFManagementCustomDto.CustomMobileTicketDto());
                            if (!ModelState.IsValid)
                            {
                                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartSingle<DFManagementCustomDto.CustomTicketDto>.
                                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<ErrorCodes, object>(ErrorCodes.RequestBodyMalformedOnUndefinedRequest, null),
                                                                                                HttpResponseCodes.BadRequest, false);
                                return returnResponseClient;
                            }

                            var match = Regex.Match(resource.TicketReasonHierarchyId, @"\/[^\/]*\/", RegexOptions.RightToLeft);
                            resource.TicketReasonId = Convert.ToInt32(match.Value.Replace("/", ""));

                            List<DFManagementDto.TicketRelatedLocationDto> list = new();
                            List<DFManagementDto.AttachmentDto> listAttachment = new();

                            foreach (var item in resource.TicketRelatedLocationHierarchyId)
                            {
                                Tuple<Error, DFManagementCustomDto.CustomLocationDto> serviceLocation = await _locationWrapperDtoRepository.GetLocationWrapperByIdAsync(item);
                                var JsonLocation = JsonConvert.SerializeObject(serviceLocation.Item2);
                                var location = JsonConvert.DeserializeObject<DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard.LocationV2Dto>(JsonLocation);
                                DFManagementDto.TicketRelatedLocationDto ticketRelatedLocationDto = new();
                                ticketRelatedLocationDto.TicketLocationId = location.Id;
                                ticketRelatedLocationDto.AddedByUserId = resource.AddedByUserId;
                                list.Add(ticketRelatedLocationDto);
                            }
                            resource.TicketRelatedLocations = list;


                            Tuple<ErrorCodes, DFManagementDto.AttachmentDto> serviceAttachment = await _attachmentDtoRepository.GetByIdFromIntAsync(Convert.ToInt32(resource.AttachmentId));

                            listAttachment.Add(serviceAttachment.Item2);
                            resource.Attachments = listAttachment;

                            Tuple<Error, DFManagementCustomDto.CustomTicketDto> serviceResultDevelopment = await _ticketWrapperDtoRepository.AddTicketWithFastTicketWrapperAsync(resource);

                            BusinessLogicResponse returnResponse = ResponseOperationsCustomSingle<DFManagementCustomDto.CustomTicketDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                                new Tuple<Error, object>(serviceResultDevelopment.Item1, (object)serviceResultDevelopment.Item2));

                            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

                            return returnResponse;

                        }

                    //case ApplicationWorkingEnvironment.Test:
                    //    {
                    //        if (!ModelState.IsValid)
                    //        {
                    //            BusinessLogicResponse returnResponseClientTest = ResponseOperationsStandartSingleTest<DFManagementCustomTestDto.CustomTicketDto>.
                    //                                                                    ConvertServiceResultToBusinessLogicResponse(new Tuple<ErrorCodes, object>(ErrorCodes.RequestBodyMalformedOnUndefinedRequest, null),
                    //                                                                            HttpResponseCodes.BadRequest, false);
                    //            return returnResponseClientTest;
                    //        }

                    //        Tuple<ErrorCodes, DFManagementCustomTestDto.CustomTicketDto> serviceResult =
                    //                new(ErrorCodes.None, new DFManagementCustomTestDto.CustomTicketDto());
                    //        if (!ModelState.IsValid)
                    //        {
                    //            BusinessLogicResponse returnResponseClient = ResponseOperationsStandartSingleTest<DFManagementCustomTestDto.CustomTicketDto>.
                    //                                                                    ConvertServiceResultToBusinessLogicResponse(new Tuple<ErrorCodes, object>(ErrorCodes.RequestBodyMalformedOnUndefinedRequest, null),
                    //                                                                            HttpResponseCodes.BadRequest, false);
                    //            return returnResponseClient;
                    //        }

                    //        var match = Regex.Match(resource.TicketReasonHierarchyId, @"\/[^\/]*\/", RegexOptions.RightToLeft);
                    //        resource.TicketReasonId = Convert.ToInt32(match.Value.Replace("/", ""));

                    //        List<DFManagementStandardTestDto.TicketRelatedLocationDto> list = new();
                    //        List<DFManagementStandardTestDto.AttachmentDto> listAttachment = new();

                    //        foreach (var item in resource.TicketRelatedLocationHierarchyId)
                    //        {
                    //            Tuple<ErrorCodes, DFManagementCustomDto.CustomLocationDto> serviceLocation = await _locationWrapperDtoRepository.GetLocationWrapperByIdAsync(item);
                    //            var JsonLocation = JsonConvert.SerializeObject(serviceLocation.Item2);
                    //            var location = JsonConvert.DeserializeObject<DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard.LocationV2Dto>(JsonLocation);
                    //            DFManagementStandardTestDto.TicketRelatedLocationDto ticketRelatedLocationDto = new();
                    //            ticketRelatedLocationDto.TicketLocationId = location.Id;
                    //            ticketRelatedLocationDto.AddedByUserId = resource.AddedByUserId;
                    //            list.Add(ticketRelatedLocationDto);
                    //        }
                    //        var a = (DFManagementCustomTestDto.CustomTicketDto)(resource)
                    //        resource.TicketRelatedLocations = list;


                    //        Tuple<ErrorCodes, DFManagementDto.AttachmentDto> serviceAttachment = await _attachmentDtoRepository.GetByIdFromIntAsync(Convert.ToInt32(resource.AttachmentId));

                    //        listAttachment.Add(serviceAttachment.Item2);
                    //        resource.Attachments = listAttachment;

                    //        Tuple<ErrorCodes, DFManagementCustomDto.CustomTicketDto> serviceResultTest = await _ticketWrapperDtoRepository.AddTicketWithFastTicketWrapperTestAsync(DFManagementCustomTestDto.CustomTicketDto(resource));

                    //        BusinessLogicResponse returnResponse = ResponseOperationsCustomSingle<DFManagementCustomDto.CustomTicketDto>.ConvertServiceResultToBusinessLogicResponse(
                    //                                                            new Tuple<ErrorCodes, object>(serviceResultTest.Item1, (object)serviceResultTest.Item2));

                    //        returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

                    //        return returnResponse;


                    //    }

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


        [Route("[controller]/GetAllTicketsByBasicTicketId/{basicTicketId}")]
        [HttpPost]
        public async Task<BusinessLogicResponse> GetAllTicketsByBasicTicketIdAsync(int basicTicketId,DataSourceLoadOptions dataSourceLoadOptions)
        {

            try
            {
                IConfiguration settings = ConfigurationHelper.GetConfig();

                switch (ConfigurationHelper.GetWorkingEnvironment(settings))
                {
                    case ApplicationWorkingEnvironment.Development:
                        {

                            Tuple<Error, IEnumerable<DFManagementCustomDto.CustomTicketDto>> serviceResult = await _ticketWrapperDtoRepository.GetAllTicketsWrapperByBasicTicketIdAsync(basicTicketId);

                            BusinessLogicResponse returnResponse = ResponseOperationsCustomSingle<DFManagementCustomDto.CustomTicketDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                                new Tuple<Error, object>(serviceResult.Item1, (object)serviceResult.Item2));
                            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

                            return returnResponse;
                        }

                    case ApplicationWorkingEnvironment.Test:
                        {

                            Tuple<ErrorCodes, IEnumerable<DFManagementCustomTestDto.CustomTicketDto>> serviceResultTest = await _ticketWrapperDtoRepository.GetAllTicketsWrapperByBasicTicketIdTestAsync(basicTicketId);

                            BusinessLogicResponse returnResponseTest = ResponseOperationsStandartMultiTest<DFManagementCustomTestDto.CustomTicketDto>.ConvertServiceResultToBusinessLogicResponse(
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
            catch (Exception e)
            {

                throw;
            }


        }


        //TODO : BusinessLogicRequest de TicketDto olmayacak sadece “TicketId”,  ”TicketStatusId” içeren Dto olacak
        [Route("[controller]/UpdateState")]
        [HttpPost]
        public async Task<BusinessLogicResponse> UpdateState([FromBody] BusinessLogicRequest request = null)
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
                                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartSingle<DFManagementCustomDto.CustomTicketDto>.
                                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<ErrorCodes, object>(ErrorCodes.RequestBodyMalformedOnUndefinedRequest, null),
                                                                                                HttpResponseCodes.BadRequest, false);
                                return returnResponseClient;
                            }

                            Tuple<Error, DFManagementCustomDto.CustomTicketDto> serviceResult =new(Errors.General.None(), new DFManagementCustomDto.CustomTicketDto());

                            //Tuple<ErrorCodes, DFManagementCustomDto.CustomTicketDto> serviceResult = await _ticketWrapperDtoRepository.UpdateTicketStatuWrapperAsync(resource);
                            if (request != null)
                            {
                                switch (request.RequestDomain)
                                {
                                    case RemoteIncomingDomains.DiasTesisYonetimWebClient:
                                        {
                                            serviceResult = await _ticketWrapperDtoRepository.UpdateTicketStateWrapperAsync(request);

                                            BusinessLogicResponse returnResponse = ResponseOperationsCustomSingle<DFManagementCustomDto.CustomTicketDto>.ConvertServiceResultToBusinessLogicResponse(
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
                                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartSingleTest<DFManagementCustomTestDto.CustomTicketDto>.
                                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<ErrorCodes, object>(ErrorCodes.RequestBodyMalformedOnUndefinedRequest, null),
                                                                                                HttpResponseCodes.BadRequest, false);
                                return returnResponseClient;
                            }

                            Tuple<ErrorCodes, DFManagementCustomTestDto.CustomTicketDto> serviceResult = new(ErrorCodes.None, new DFManagementCustomTestDto.CustomTicketDto());
                            if (request != null)
                            {
                                switch (request.RequestDomain)
                                {
                                    case RemoteIncomingDomains.DiasTesisYonetimWebClient:
                                        {
                                            serviceResult = await _ticketWrapperDtoRepository.UpdateTicketStateWrapperTestAsync(request);

                                            BusinessLogicResponse returnResponse = ResponseOperationsCustomSingleTest<DFManagementCustomTestDto.CustomTicketDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<ErrorCodes, object>(serviceResult.Item1, (object)serviceResult.Item2));

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
            catch (Exception e)
            {
                throw;
            }
        }

        #endregion

    }


}
