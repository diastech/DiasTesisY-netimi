using DiasShared.Services.Communication.BusinessLogicMessage;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
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
    public class UserTokenController : Controller
    {
        private readonly DFManagementStdDtoInterface.IUserTokenDtoRepository _userTokenDtoRepository;

        public UserTokenController(DFManagementStdDtoInterface.IUserTokenDtoRepository userTokenDtoRepository)
        {
            _userTokenDtoRepository = userTokenDtoRepository;
        }

        #region ControllerRoute

        //TODO : Hata kodları özelleştirilecek
        [Route("[controller]/GetAll")]
        [HttpGet]
        public async Task<BusinessLogicResponse> GetAllAsync()
        {
            if (!ModelState.IsValid)
            {
                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartMulti<DFManagementStdDto.UserTokenDto>.
                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<Error, object>(Errors.General.GeneralServerError(), null),
                                                                                HttpResponseCodes.BadRequest, false);
                return returnResponseClient;
            }


            Tuple<Error, IEnumerable<DFManagementStdDto.UserTokenDto>> serviceResult = await _userTokenDtoRepository.GetAllAsync();

            BusinessLogicResponse returnResponse = ResponseOperationsStandartMulti<DFManagementStdDto.UserTokenDto>.ConvertServiceResultToBusinessLogicResponse(
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
                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartMulti<DFManagementStdDto.UserTokenDto>.
                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<Error, object>(Errors.General.GeneralServerError(), null),
                                                                                HttpResponseCodes.BadRequest, false);
                return returnResponseClient;
            }


            Tuple<Error, DFManagementStdDto.UserTokenDto> serviceResult = await _userTokenDtoRepository.GetByIdFromIntAsync(id);

            BusinessLogicResponse returnResponse = ResponseOperationsStandartMulti<DFManagementStdDto.UserTokenDto>.ConvertServiceResultToBusinessLogicResponse(
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
                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartMulti<DFManagementStdDto.UserTokenDto>.
                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<Error, object>(Errors.General.GeneralServerError(), null),
                                                                                HttpResponseCodes.BadRequest, false);
                return returnResponseClient;
            }


            Tuple<Error, DFManagementStdDto.UserTokenDto> serviceResult = await _userTokenDtoRepository.DeleteFromIntAsync(id);

            BusinessLogicResponse returnResponse = ResponseOperationsStandartMulti<DFManagementStdDto.UserTokenDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<Error, object>(serviceResult.Item1, (object)serviceResult.Item2));

            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

            return returnResponse;
        }

        //TODO : Hata kodları özelleştirilecek
        [Route("[controller]/Insert")]
        [HttpGet]
        public async Task<BusinessLogicResponse> InsertAsync([FromBody] DFManagementStdDto.UserTokenDto resource)
        {
            if (!ModelState.IsValid)
            {
                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartMulti<DFManagementStdDto.UserTokenDto>.
                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<Error, object>(Errors.General.GeneralServerError(), null),
                                                                                HttpResponseCodes.BadRequest, false);
                return returnResponseClient;
            }


            Tuple<Error, DFManagementStdDto.UserTokenDto> serviceResult = await _userTokenDtoRepository.InsertAsync(resource);

            BusinessLogicResponse returnResponse = ResponseOperationsStandartMulti<DFManagementStdDto.UserTokenDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<Error, object>(serviceResult.Item1, (object)serviceResult.Item2));

            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

            return returnResponse;
        }

        //TODO : Hata kodları özelleştirilecek
        [Route("[controller]/Update")]
        [HttpGet]
        public async Task<BusinessLogicResponse> UpdateAsync([FromBody] DFManagementStdDto.UserTokenDto resource)
        {
            if (!ModelState.IsValid)
            {
                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartMulti<DFManagementStdDto.UserTokenDto>.
                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<Error, object>(Errors.General.GeneralServerError(), null),
                                                                                HttpResponseCodes.BadRequest, false);
                return returnResponseClient;
            }

            Tuple<Error, DFManagementStdDto.UserTokenDto> serviceResult = await _userTokenDtoRepository.UpdateAsync(resource);

            BusinessLogicResponse returnResponse = ResponseOperationsStandartMulti<DFManagementStdDto.UserTokenDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<Error, object>(serviceResult.Item1, (object)serviceResult.Item2));

            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

            return returnResponse;
        }

        #endregion

        #region Controller_ActionRoute

        //TODO : Hata kodları özelleştirilecek
        [Route("[controller]/GetAllUserTokensByUserId/{id}")]
        [HttpGet]
        public async Task<BusinessLogicResponse> GetAllUserTokensByUserIdAsync([FromQuery] int id)
        {
            if (!ModelState.IsValid)
            {
                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartMulti<DFManagementStdDto.UserTokenDto>.
                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<Error, object>(Errors.General.GeneralServerError(), null),
                                                                                HttpResponseCodes.BadRequest, false);
                return returnResponseClient;
            }


            Tuple<Error, DFManagementStdDto.UserTokenDto> serviceResult = await _userTokenDtoRepository.GetAllUserTokensByUserIdAsync(id);

            BusinessLogicResponse returnResponse = ResponseOperationsStandartMulti<DFManagementStdDto.UserTokenDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<Error, object>(serviceResult.Item1, (object)serviceResult.Item2));

            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

            return returnResponse;
        }


        #endregion

    }
}
