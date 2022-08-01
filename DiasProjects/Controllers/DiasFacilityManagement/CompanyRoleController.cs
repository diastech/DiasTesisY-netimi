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


namespace DiasWebApi.DiasFacilityManagement.Controllers
{
    [ApiController]
    public class CompanyRoleController : Controller
    {
        private readonly ICompanyRoleDtoRepository _companyRolesRepository;

        public CompanyRoleController(ICompanyRoleDtoRepository userRolesRepository)
        {
            _companyRolesRepository = userRolesRepository;
        }

        #region ControllerRoute

        //TODO : Hata kodları özelleştirilecek
        [Route("[controller]/GetAll")]
        [HttpGet]
        public async Task<BusinessLogicResponse> GetAllAsync()
        {
            if (!ModelState.IsValid)
            {
                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartMulti<DFManagementStdDto.CompanyRoleDto>.
                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<Error, object>(Errors.General.GeneralServerError(), null),
                                                                                HttpResponseCodes.BadRequest, false);
                return returnResponseClient;
            }


            Tuple<Error, IEnumerable<DFManagementStdDto.CompanyRoleDto>> serviceResult = await _companyRolesRepository.GetAllAsync();

            BusinessLogicResponse returnResponse = ResponseOperationsStandartMulti<DFManagementStdDto.CompanyRoleDto>.ConvertServiceResultToBusinessLogicResponse(
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
                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartMulti<DFManagementStdDto.CompanyRoleDto>.
                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<Error, object>(Errors.General.GeneralServerError(), null),
                                                                                HttpResponseCodes.BadRequest, false);
                return returnResponseClient;
            }


            Tuple<Error, DFManagementStdDto.CompanyRoleDto> serviceResult = await _companyRolesRepository.GetByIdFromIntAsync(id);

            BusinessLogicResponse returnResponse = ResponseOperationsStandartMulti<DFManagementStdDto.CompanyRoleDto>.ConvertServiceResultToBusinessLogicResponse(
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
                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartMulti<DFManagementStdDto.CompanyRoleDto>.
                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<Error, object>(Errors.General.GeneralServerError(), null),
                                                                                HttpResponseCodes.BadRequest, false);
                return returnResponseClient;
            }


            Tuple<Error, DFManagementStdDto.CompanyRoleDto> serviceResult = await _companyRolesRepository.DeleteFromIntAsync(id);

            BusinessLogicResponse returnResponse = ResponseOperationsStandartMulti<DFManagementStdDto.CompanyRoleDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<Error, object>(serviceResult.Item1, (object)serviceResult.Item2));

            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

            return returnResponse;
        }

        //TODO : Hata kodları özelleştirilecek
        [Route("[controller]/Insert")]
        [HttpGet]
        public async Task<BusinessLogicResponse> InsertAsync([FromBody] DFManagementStdDto.CompanyRoleDto resource)
        {
            if (!ModelState.IsValid)
            {
                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartMulti<DFManagementStdDto.CompanyRoleDto>.
                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<Error, object>(Errors.General.GeneralServerError(), null),
                                                                                HttpResponseCodes.BadRequest, false);
                return returnResponseClient;
            }


            Tuple<Error, DFManagementStdDto.CompanyRoleDto> serviceResult = await _companyRolesRepository.InsertAsync(resource);

            BusinessLogicResponse returnResponse = ResponseOperationsStandartMulti<DFManagementStdDto.CompanyRoleDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<Error, object>(serviceResult.Item1, (object)serviceResult.Item2));

            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

            return returnResponse;
        }

        //TODO : Hata kodları özelleştirilecek
        [Route("[controller]/Update")]
        [HttpGet]
        public async Task<BusinessLogicResponse> UpdateAsync([FromBody] DFManagementStdDto.CompanyRoleDto resource)
        {
            if (!ModelState.IsValid)
            {
                BusinessLogicResponse returnResponseClient = ResponseOperationsStandartMulti<DFManagementStdDto.CompanyRoleDto>.
                                                                        ConvertServiceResultToBusinessLogicResponse(new Tuple<Error, object>(Errors.General.GeneralServerError(), null),
                                                                                HttpResponseCodes.BadRequest, false);
                return returnResponseClient;
            }

            Tuple<Error, DFManagementStdDto.CompanyRoleDto> serviceResult = await _companyRolesRepository.UpdateAsync(resource);

            BusinessLogicResponse returnResponse = ResponseOperationsStandartMulti<DFManagementStdDto.CompanyRoleDto>.ConvertServiceResultToBusinessLogicResponse(
                                                                new Tuple<Error, object>(serviceResult.Item1, (object)serviceResult.Item2));

            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

            return returnResponse;
        }

        #endregion

        #region Controller_ActionRoute

        #endregion

    }
}
