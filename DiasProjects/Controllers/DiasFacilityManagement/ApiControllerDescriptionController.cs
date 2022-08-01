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
using DiasShared.Errors;
using DiasBusinessLogic.Shared.Error;

namespace DiasWebApi.Controllers.DiasFacilityManagement
{
    [ApiController]
    public class ApiControllerDescriptionController : Controller
    {
        private readonly DFManagementStdDtoInterface.IApiControllerDescriptionDtoRepository _apiControllerDescriptionRepository;

        public ApiControllerDescriptionController(DFManagementStdDtoInterface.IApiControllerDescriptionDtoRepository apiControllerDescriptionRepository)
        {
            _apiControllerDescriptionRepository = apiControllerDescriptionRepository;
        }

        #region ControllerRoute

        //TODO : Hata kodları özelleştirilecek
        [Route("[controller]/GetAll")]
        [HttpGet]
        public async Task<BusinessLogicResponse> GetAllAsync()
        {
            if (!ModelState.IsValid)
            {
                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartMulti<DFManagementStdDto.ApiControllerDescriptionDto>.
                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<Error, object>(Errors.General.GeneralServerError(), null),
                                                                                HttpResponseCodes.BadRequest, false);
                return returnResponseClient;
            }


            Tuple<Error, IEnumerable<DFManagementStdDto.ApiControllerDescriptionDto>> serviceResult = await _apiControllerDescriptionRepository.GetAllAsync();

            BusinessLogicResponse returnResponse = ResponseOperationsStandartMulti<DFManagementStdDto.ApiControllerDescriptionDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<Error, object>(serviceResult.Item1, (object)serviceResult.Item2));

            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

            return returnResponse;
        }

        //TODO : Hata kodları özelleştirilecek
        [Route("[controller]/GetById/{id}")]
        [HttpGet]
        public async Task<BusinessLogicResponse> GetByIdFromIntAsync([FromQuery] int id)
        {
            if (!ModelState.IsValid)
            {
                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartMulti<DFManagementStdDto.ApiControllerDescriptionDto>.
                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<Error, object>(Errors.General.GeneralServerError(), null),
                                                                                HttpResponseCodes.BadRequest, false);
                return returnResponseClient;
            }


            Tuple<Error, DFManagementStdDto.ApiControllerDescriptionDto> serviceResult = await _apiControllerDescriptionRepository.GetByIdFromIntAsync(id);

            BusinessLogicResponse returnResponse = ResponseOperationsStandartMulti<DFManagementStdDto.ApiControllerDescriptionDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<Error, object>(serviceResult.Item1, (object)serviceResult.Item2));

            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

            return returnResponse;
        }

        //TODO : Hata kodları özelleştirilecek
        [Route("[controller]/Delete/{id}")]
        [HttpGet]
        public async Task<BusinessLogicResponse> DeleteFromIntAsync([FromQuery] int id)
        {
            if (!ModelState.IsValid)
            {
                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartMulti<DFManagementStdDto.ApiControllerDescriptionDto>.
                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<Error, object>(Errors.General.GeneralServerError(), null),
                                                                                HttpResponseCodes.BadRequest, false);
                return returnResponseClient;
            }


            Tuple<Error, DFManagementStdDto.ApiControllerDescriptionDto> serviceResult = await _apiControllerDescriptionRepository.DeleteFromIntAsync(id);

            BusinessLogicResponse returnResponse = ResponseOperationsStandartMulti<DFManagementStdDto.ApiControllerDescriptionDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<Error, object>(serviceResult.Item1, (object)serviceResult.Item2));

            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

            return returnResponse;
        }

        //TODO : Hata kodları özelleştirilecek
        [Route("[controller]/Insert")]
        [HttpGet]
        public async Task<BusinessLogicResponse> InsertAsync([FromBody] DFManagementStdDto.ApiControllerDescriptionDto resource)
        {
            if (!ModelState.IsValid)
            {
                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartMulti<DFManagementStdDto.ApiControllerDescriptionDto>.
                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<Error, object>(Errors.General.GeneralServerError(), null),
                                                                                HttpResponseCodes.BadRequest, false);
                return returnResponseClient;
            }


            Tuple<Error, DFManagementStdDto.ApiControllerDescriptionDto> serviceResult = await _apiControllerDescriptionRepository.InsertAsync(resource);

            BusinessLogicResponse returnResponse = ResponseOperationsStandartMulti<DFManagementStdDto.ApiControllerDescriptionDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<Error, object>(serviceResult.Item1, (object)serviceResult.Item2));

            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

            return returnResponse;
        }

        //TODO : Hata kodları özelleştirilecek
        [Route("[controller]/Update")]
        [HttpGet]
        public async Task<BusinessLogicResponse> UpdateAsync([FromBody] DFManagementStdDto.ApiControllerDescriptionDto resource)
        {
            if (!ModelState.IsValid)
            {
                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartMulti<DFManagementStdDto.ApiControllerDescriptionDto>.
                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<Error, object>(Errors.General.GeneralServerError(), null),
                                                                                HttpResponseCodes.BadRequest, false);
                return returnResponseClient;
            }

            Tuple<Error, DFManagementStdDto.ApiControllerDescriptionDto> serviceResult = await _apiControllerDescriptionRepository.UpdateAsync(resource);

            BusinessLogicResponse returnResponse = ResponseOperationsStandartMulti<DFManagementStdDto.ApiControllerDescriptionDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<Error, object>(serviceResult.Item1, (object)serviceResult.Item2));

            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

            return returnResponse;
        }

        #endregion

        #region Controller_ActionRoute

        #endregion

    }
}
