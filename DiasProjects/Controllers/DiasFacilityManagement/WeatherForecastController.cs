using DiasShared.Operations.CommunicationOperations.DiasFacilityManagement;
using DiasShared.Services.Communication.BusinessLogicMessage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DiasShared.Enums.Standart.HttpCodesEnum;
using DFManagementStdDtoInterface = DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement;
using DFManagementStdDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using Newtonsoft.Json;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using DiasShared.Errors;
using DiasBusinessLogic.Shared.Error;

namespace DiasWebApi.DiasFacilityManagement.Controllers
{

    //Bu controller test amaçlıdır silmeyin.
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly DFManagementStdDtoInterface.IUserDtoRepository _userRepository;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        // The Web API will only accept tokens 1) for users, and 2) having the "access_as_user" scope for this API
        static readonly string[] scopeRequiredByApi = new string[] { "access_as_user" };

        public WeatherForecastController(ILogger<WeatherForecastController> logger, DFManagementStdDtoInterface.IUserDtoRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        //[HttpGet]
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);

        //    var rng = new Random();
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateTime.Now.AddDays(index),
        //        TemperatureC = rng.Next(-20, 55),
        //        Summary = Summaries[rng.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}


        //TODO : Hata kodları özelleştirilecek
        [HttpGet]
        [Route("[controller]/Test")]
        public async Task<BusinessLogicResponse> GetAllAsync()
        {
            if (!ModelState.IsValid)
            {
                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartMulti<DFManagementStdDto.UserDto>.
                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<Error, object>(Errors.General.GeneralServerError(), null),
                                                                                HttpResponseCodes.BadRequest, false);
                return returnResponseClient;
            }


            Tuple<Error, IEnumerable<DFManagementStdDto.UserDto>> serviceResult = await _userRepository.GetAllAsync();

            BusinessLogicResponse returnResponse = ResponseOperationsStandartMulti<DFManagementStdDto.UserDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<Error, object>(serviceResult.Item1, (object)serviceResult.Item2));

            //Başka projelere de servis verebilmek için
            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

            return returnResponse;

        }

        //TODO : Hata kodları özelleştirilecek
        [HttpGet]
        [Route("[controller]/TestGetId/{userId}")]
        public async Task<BusinessLogicResponse> GetUserByIdAsync(int userId)
        {
            if (!ModelState.IsValid)
            {
                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartSingle<DFManagementStdDto.UserDto>.
                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<Error, object>(Errors.General.GeneralServerError(), null),
                                                                                HttpResponseCodes.BadRequest, false);
                return returnResponseClient;
            }


            Tuple<Error, DFManagementStdDto.UserDto> serviceResult = await _userRepository.GetByIdFromIntAsync(userId);

            BusinessLogicResponse returnResponse = ResponseOperationsStandartSingle<DFManagementStdDto.UserDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<Error, object>(serviceResult.Item1, (object)serviceResult.Item2));

            //Başka projelere de servis verebilmek için
            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

            return returnResponse;

        }

        //TODO : Hata kodları özelleştirilecek
        [HttpPost]
        [Route("[controller]/TestDelete/{userId}")]
        public async Task<BusinessLogicResponse> DeleteUserByIdAsync(int userId)
        {
            if (!ModelState.IsValid)
            {
                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartSingle<DFManagementStdDto.UserDto>.
                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<Error, object>(Errors.General.GeneralServerError(), null),
                                                                                HttpResponseCodes.BadRequest, false);
                return returnResponseClient;
            }


            Tuple<Error, DFManagementStdDto.UserDto> serviceResult = await _userRepository.DeleteFromIntAsync(userId);

            BusinessLogicResponse returnResponse = ResponseOperationsStandartSingle<DFManagementStdDto.UserDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<Error, object>(serviceResult.Item1, (object)serviceResult.Item2));

            //Başka projelere de servis verebilmek için
            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

            return returnResponse;

        }

        //TODO : Hata kodları özelleştirilecek
        [HttpPost]
        [Route("[controller]/TestInsert")]
        public async Task<BusinessLogicResponse> InsertUserAsync([FromBody]UserDto insertedDto)
        {
            if (!ModelState.IsValid)
            {
                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartSingle<DFManagementStdDto.UserDto>.
                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<Error, object>(Errors.General.GeneralServerError(), null),
                                                                                HttpResponseCodes.BadRequest, false);

                return null;
            }


            Tuple<Error, DFManagementStdDto.UserDto> serviceResult = await _userRepository.InsertAsync(insertedDto);

            BusinessLogicResponse returnResponse = ResponseOperationsStandartSingle<DFManagementStdDto.UserDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<Error, object>(serviceResult.Item1, (object)serviceResult.Item2));

            //Başka projelere de servis verebilmek için
            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

            return returnResponse;
        }

        //TODO : Hata kodları özelleştirilecek
        [HttpPost]
        [Route("[controller]/TestUpdate")]
        public async Task<BusinessLogicResponse> UpdateUserAsync([FromBody] UserDto updatedDto)
        {
            if (!ModelState.IsValid)
            {
                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartMulti<DFManagementStdDto.UserDto>.
                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<Error, object>(Errors.General.GeneralServerError(), null),
                                                                                HttpResponseCodes.BadRequest, false);

                return null;
            }


            Tuple<Error, DFManagementStdDto.UserDto> serviceResult = await _userRepository.UpdateAsync(updatedDto);

            BusinessLogicResponse returnResponse = ResponseOperationsStandartSingle<DFManagementStdDto.UserDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<Error, object>(serviceResult.Item1, (object)serviceResult.Item2));

            //Başka projelere de servis verebilmek için
            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

            return returnResponse;
        }

    }
}
