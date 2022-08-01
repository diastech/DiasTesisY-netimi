using DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement;
using Microsoft.AspNetCore.Mvc;
using DiasShared.Services.Communication.BusinessLogicMessage;
using System;
using System.Threading.Tasks;
using static DiasShared.Enums.Standart.HttpCodesEnum;
using DFManagementStdDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using Newtonsoft.Json;
using System.Collections.Generic;
using DiasShared.Operations.CommunicationOperations.DiasFacilityManagement;
using DiasShared.Errors;
using DiasBusinessLogic.Shared.Error;

namespace DiasWebApi.Controllers.DiasFacilityManagement
{
    [ApiController]
    public class CompanyRoleUserController : Controller
    {
        private readonly ICompanyRoleUserDtoRepository _companyRoleUserRepository;

        public CompanyRoleUserController(ICompanyRoleUserDtoRepository companyRoleUsersRepository)
        {
            _companyRoleUserRepository = companyRoleUsersRepository;
        }

        #region ControllerRoute

        //TODO : Hata kodları özelleştirilecek
        [Route("[controller]/GetAll")]
        [HttpGet]
        public async Task<BusinessLogicResponse> GetAllAsync()
        {
            if (!ModelState.IsValid)
            {
                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartMulti<DFManagementStdDto.CompanyRoleUserDto>.
                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<Error, object>(Errors.General.GeneralServerError(), null),
                                                                                HttpResponseCodes.BadRequest, false);
                return returnResponseClient;
            }


            Tuple<Error, IEnumerable<DFManagementStdDto.CompanyRoleUserDto>> serviceResult = await _companyRoleUserRepository.GetAllAsync();

            BusinessLogicResponse returnResponse = ResponseOperationsStandartMulti<DFManagementStdDto.CompanyRoleUserDto>.ConvertServiceResultToBusinessLogicResponse(
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
                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartMulti<DFManagementStdDto.CompanyRoleUserDto>.
                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<Error, object>(Errors.General.GeneralServerError(), null),
                                                                                HttpResponseCodes.BadRequest, false);
                return returnResponseClient;
            }


            Tuple<Error, DFManagementStdDto.CompanyRoleUserDto> serviceResult = await _companyRoleUserRepository.GetByIdFromIntAsync(id);

            BusinessLogicResponse returnResponse = ResponseOperationsStandartMulti<DFManagementStdDto.CompanyRoleUserDto>.ConvertServiceResultToBusinessLogicResponse(
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
                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartMulti<DFManagementStdDto.CompanyRoleUserDto>.
                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<Error, object>(Errors.General.GeneralServerError(), null),
                                                                                HttpResponseCodes.BadRequest, false);
                return returnResponseClient;
            }


            Tuple<Error, DFManagementStdDto.CompanyRoleUserDto> serviceResult = await _companyRoleUserRepository.DeleteFromIntAsync(id);

            BusinessLogicResponse returnResponse = ResponseOperationsStandartMulti<DFManagementStdDto.CompanyRoleUserDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<Error, object>(serviceResult.Item1, (object)serviceResult.Item2));

            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

            return returnResponse;
        }

        //TODO : Hata kodları özelleştirilecek
        [Route("[controller]/Insert")]
        [HttpGet]
        public async Task<BusinessLogicResponse> InsertAsync([FromBody] DFManagementStdDto.CompanyRoleUserDto resource)
        {
            if (!ModelState.IsValid)
            {
                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartMulti<DFManagementStdDto.CompanyRoleUserDto>.
                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<Error, object>(Errors.General.GeneralServerError(), null),
                                                                                HttpResponseCodes.BadRequest, false);
                return returnResponseClient;
            }


            Tuple<Error, DFManagementStdDto.CompanyRoleUserDto> serviceResult = await _companyRoleUserRepository.InsertAsync(resource);

            BusinessLogicResponse returnResponse = ResponseOperationsStandartMulti<DFManagementStdDto.CompanyRoleUserDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<Error, object>(serviceResult.Item1, (object)serviceResult.Item2));

            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

            return returnResponse;
        }

        //TODO : Hata kodları özelleştirilecek
        [Route("[controller]/Update")]
        [HttpGet]
        public async Task<BusinessLogicResponse> UpdateAsync([FromBody] DFManagementStdDto.CompanyRoleUserDto resource)
        {
            if (!ModelState.IsValid)
            {
                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartMulti<DFManagementStdDto.CompanyRoleUserDto>.
                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<Error, object>(Errors.General.GeneralServerError(), null),
                                                                                HttpResponseCodes.BadRequest, false);
                return returnResponseClient;
            }

            Tuple<Error, DFManagementStdDto.CompanyRoleUserDto> serviceResult = await _companyRoleUserRepository.UpdateAsync(resource);

            BusinessLogicResponse returnResponse = ResponseOperationsStandartMulti<DFManagementStdDto.CompanyRoleUserDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<Error, object>(serviceResult.Item1, (object)serviceResult.Item2));

            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

            return returnResponse;
        }

        #endregion

        #region Controller_ActionRoute

        //TODO : Hata kodları özelleştirilecek
        [Route("[controller]/GetByUserId/{id}")]
        [HttpGet]
        public async Task<BusinessLogicResponse> GetByUserIdAsync([FromQuery] int id)
        {
            if (!ModelState.IsValid)
            {
                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartMulti<DFManagementStdDto.CompanyRoleUserDto>.
                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<Error, object>(Errors.General.GeneralServerError(), null),
                                                                                HttpResponseCodes.BadRequest, false);
                return returnResponseClient;
            }


            Tuple<Error, DFManagementStdDto.CompanyRoleUserDto> serviceResult = await _companyRoleUserRepository.GetAllCompanyRolesByUserIdAsync(id);

            BusinessLogicResponse returnResponse = ResponseOperationsStandartMulti<DFManagementStdDto.CompanyRoleUserDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<Error, object>(serviceResult.Item1, (object)serviceResult.Item2));

            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

            return returnResponse;
        }

        #endregion


    }
}
