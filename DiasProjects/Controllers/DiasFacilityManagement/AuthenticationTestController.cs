using DiasShared.Services.Communication.BusinessLogicMessage;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using DFManagementStdDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using Newtonsoft.Json;
using System.Collections.Generic;
using DiasShared.Operations.CommunicationOperations.DiasFacilityManagement;
using DiasShared.Errors;
using DiasBusinessLogic.Shared.Error;
using Microsoft.AspNetCore.Authorization;
using DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement;

namespace DiasWebApi.Controllers.DiasFacilityManagement
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthenticationTestController : ControllerBase
    {
        private readonly IAuthenticationTestRepository _authenticationTestsRepository;
        public AuthenticationTestController(IAuthenticationTestRepository authenticationTestsRepository)
        {
            _authenticationTestsRepository = authenticationTestsRepository;
        }

        [Route("[controller]/Test")]
        [HttpGet]
        public async Task<BusinessLogicResponse> Test()
        {
            Tuple<Error, IEnumerable<DFManagementStdDto.ApiActionDescriptionDto>> serviceResult =
                new Tuple<Error, IEnumerable<DFManagementStdDto.ApiActionDescriptionDto>>
                (Errors.General.Success("ApiActionDescription"), new List<DFManagementStdDto.ApiActionDescriptionDto>());

            BusinessLogicResponse returnResponse = ResponseOperationsStandartMulti<DFManagementStdDto.ApiActionDescriptionDto>.ConvertServiceResultToBusinessLogicResponse(
                                                      new Tuple<Error, object>(serviceResult.Item1, (object)serviceResult.Item2));

            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

            return returnResponse;
        }

        [Route("[controller]/TestAnonymous")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<BusinessLogicResponse> TestAnonymous()
        {
            Tuple<Error, IEnumerable<DFManagementStdDto.ApiActionDescriptionDto>> serviceResult =
                new Tuple<Error, IEnumerable<DFManagementStdDto.ApiActionDescriptionDto>>
                (Errors.General.Success("ApiActionDescription"), new List<DFManagementStdDto.ApiActionDescriptionDto>());

            BusinessLogicResponse returnResponse = ResponseOperationsStandartMulti<DFManagementStdDto.ApiActionDescriptionDto>.ConvertServiceResultToBusinessLogicResponse(
                                                      new Tuple<Error, object>(serviceResult.Item1, (object)serviceResult.Item2));

            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

            return returnResponse;
        }


        [Route("[controller]/TestAddCompanyRoleClaim")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<BusinessLogicResponse> TestAddCompanyRoleClaim()
        {
            //Tuple<Error, IEnumerable<DFManagementStdDto.CompanyRoleClaimDto>> serviceResult =
            //    new Tuple<Error, IEnumerable<DFManagementStdDto.CompanyRoleClaimDto>>
            //    (Errors.General.Success(), new List<DFManagementStdDto.CompanyRoleClaimDto>());

            Tuple<Error, IEnumerable<DFManagementStdDto.CompanyRoleClaimDto>> serviceResult = await _authenticationTestsRepository.TestAddCompanyRoleClaimAsync();

            BusinessLogicResponse returnResponse = ResponseOperationsStandartMulti<DFManagementStdDto.CompanyRoleClaimDto>.ConvertServiceResultToBusinessLogicResponse(
                                                      new Tuple<Error, object>(serviceResult.Item1, (object)serviceResult.Item2));

            returnResponse.OptionalJsonResult = JsonConvert.SerializeObject(serviceResult.Item2);

            return returnResponse;
        }

    }
}
