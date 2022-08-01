using DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Development;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Generic.DiasFacilityManagementSqlServer.Development;
using DiasBusinessLogic.Shared.Error;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.BaseBusinessRules.SqlServer.Standart;
using DIMDevDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using DiasWebApi.Shared.Operations.BusinessLogicOperations.SimpleInjectorOperations;
using System;
using System.Threading.Tasks;
using static DiasWebApi.Shared.Enums.WebApiApplicationEnums;
using System.Collections.Generic;
using DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement;
using DiasShared.Errors;
using DevelopmentBL_Interface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Standart;


namespace DiasWebApi.Repositories.DiasFacilityManagement
{
    public class AuthenticationTestRepository : IAuthenticationTestRepository
    {
        private IBaseCompanyRoleClaimBusinessRules _baseCompanyRoleClaimBusinessRules;
        private ApplicationBusinessLogicEnvironment _applicationBusinessLogicEnvironment;
        private bool businessLogicContainerStatus { get; set; }

        public AuthenticationTestRepository(
            IBaseCompanyRoleClaimBusinessRules baseCompanyRoleClaimBusinessRules,
            ApplicationBusinessLogicEnvironment applicationBusinessLogicEnvironment
            )
        {
            businessLogicContainerStatus = SimpleInjectorContainerOperations.VerifyContainer();

            if (businessLogicContainerStatus)
            {
                _baseCompanyRoleClaimBusinessRules = baseCompanyRoleClaimBusinessRules;
                _applicationBusinessLogicEnvironment = applicationBusinessLogicEnvironment;
            }
        }

        public async Task<Tuple<Error, IEnumerable<DIMDevDto.CompanyRoleClaimDto>>> TestAddCompanyRoleClaimAsync()
        {
            if (businessLogicContainerStatus)
            {
                switch (_applicationBusinessLogicEnvironment)
                {
                    case ApplicationBusinessLogicEnvironment.Development:
                        {
                            DevelopmentBL_Interface.ICompanyRoleClaimBusinessRules businessRule =
                                 (DevelopmentBL_Interface.ICompanyRoleClaimBusinessRules)_baseCompanyRoleClaimBusinessRules;

                            return await businessRule.TestAddCompanyRoleClaimAsync();
                        }

                    //TODO: paremetre test dtosu olacak
                    case ApplicationBusinessLogicEnvironment.Test:
                        {
                            return new Tuple<Error, IEnumerable<DIMDevDto.CompanyRoleClaimDto>>(Errors.General.GeneralServerError(), null);
                        }

                    //TODO: paremetre canlı dtosu olacak
                    case ApplicationBusinessLogicEnvironment.Live:
                        {
                            return new Tuple<Error, IEnumerable<DIMDevDto.CompanyRoleClaimDto>>(Errors.General.GeneralServerError(), null);
                        }

                    default:
                        {
                            return new Tuple<Error, IEnumerable<DIMDevDto.CompanyRoleClaimDto>>(Errors.General.GeneralServerError(), null);
                        }
                }
            }
            else
            {
                return new Tuple<Error, IEnumerable<DIMDevDto.CompanyRoleClaimDto>>(Errors.General.GeneralServerError(), null);
            }
        }



    }
}
