using DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.Configuration;
using DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFM.SqlServer.Development.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;
using DevelopmentUserInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Standart;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using DevelopmentModel = DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFM.SqlServer.Development.Models;
using DevelopmentLocationProfile = DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Development;
using DiasBusinessLogic.Shared.Configuration;
using System.Collections.Generic;
using System.Security.Claims;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Generic.DiasFacilityManagementSqlServer.Development;
using DevFmBlInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Generic.DiasFacilityManagementSqlServer.Development;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.CustomIdentity.Development;
using DiasBusinessLogic.AutoMapper.Configuration;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;

namespace DiasBusinessLogic.Shared.Classes.Identity.Development
{
    /// <summary>
    /// Metodları implemente edilmemiştir
    /// </summary>
    //TODO: Metodları implemente et
    public class CustomRoleStore : ICustomRoleStore<CompanyRole>
    {
        private static AutoMapperProfileMapper<DevelopmentLocationProfile.CompanyRoleProfile> DtoMapper_DiasFacilityManagementSqlServer_Development_CompanyRole
            => new(new(DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer")));


        private static AutoMapperProfileMapper<DevelopmentLocationProfile.CompanyRoleClaimProfile> DtoMapper_DiasFacilityManagementSqlServer_Development_CompanyRoleClaim
            => new(new(DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer")));


        private readonly DevelopmentUserInterface.ICompanyRoleBusinessRules _companyRoleBusinessRules;
        private readonly DevelopmentUserInterface.ICompanyRoleClaimBusinessRules _companyRoleClaimBusinessRules;
        private IGenericStandartBusinessRules<DevelopmentDto.CompanyRoleClaimDto, DevelopmentLocationProfile.CompanyRoleClaimProfile> _genericStandartBusinessRulesCompanyRoleClaim;

        public CustomRoleStore():  this(DI_ServiceLocator.Current.Get<DevelopmentUserInterface.ICompanyRoleBusinessRules>(),
                                         DI_ServiceLocator.Current.Get<DevelopmentUserInterface.ICompanyRoleClaimBusinessRules>(),
                                            DI_ServiceLocator.Current.Get<DevFmBlInterface.IGenericStandartBusinessRules<DevelopmentDto.CompanyRoleClaimDto, DevelopmentLocationProfile.CompanyRoleClaimProfile>>())
        {
        }

        private CustomRoleStore(DevelopmentUserInterface.ICompanyRoleBusinessRules companyRoleBusinessRules,
                                 DevelopmentUserInterface.ICompanyRoleClaimBusinessRules companyRoleClaimBusinessRules,
                                    DevFmBlInterface.IGenericStandartBusinessRules<DevelopmentDto.CompanyRoleClaimDto, DevelopmentLocationProfile.CompanyRoleClaimProfile> genericCompanyRoleClaimBL)
        {
            _companyRoleBusinessRules = companyRoleBusinessRules;
            _companyRoleClaimBusinessRules = companyRoleClaimBusinessRules;
            _genericStandartBusinessRulesCompanyRoleClaim = genericCompanyRoleClaimBL;
        }

        //TODO : implemente et
        public void Dispose()
        {            
        }

        #region IRoleStore
        public Task<IdentityResult> CreateAsync(CompanyRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> DeleteAsync(CompanyRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }       

        public Task<CompanyRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async  Task<CompanyRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            //TODO : Bu işlem daha sonra generic olmalıdır
            if (_companyRoleBusinessRules != null)
            {
                Tuple<DiasShared.Errors.Error, DevelopmentDto.CompanyRoleDto> resultBusinessRule = await _companyRoleBusinessRules.GetCompanyRoleByNormalizedNameAsync(normalizedRoleName);

                if((resultBusinessRule.Item1.BusinessOperationSucceed) && (resultBusinessRule.Item2 != null))
                {
                    return DtoMapper_DiasFacilityManagementSqlServer_Development_CompanyRole.Map<DevelopmentDto.CompanyRoleDto, DevelopmentModel.CompanyRole> (resultBusinessRule.Item2);
                }               
            }
           
            return null;            
        }

        public Task<string> GetNormalizedRoleNameAsync(CompanyRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetRoleIdAsync(CompanyRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetRoleNameAsync(CompanyRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedRoleNameAsync(CompanyRole role, string normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetRoleNameAsync(CompanyRole role, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(CompanyRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        #endregion IRoleStore

        #region IRoleClaimStore

        public async Task<DevelopmentDto.CompanyRoleClaimDto> AddClaimV2Async(CompanyRole role, Claim claim, int apiControllerDescriptionId, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            //Burada aynı kayıtı insert edip etmediğimizi kontrol edebiliriz
            //Aynı kayıtı insert etmeye kalkarsak hata verir
            //uniqueColumns dto propertysi değil entity propertysi olacak!(şimdi hepsi aynı gerçi)
            //TODO:ileride bunu dto propertysi olacak şekilde ayarlayacağım
            List<string> uniqueColumns = new List<string>()
                            {
                                "RoleId",
                                "ClaimType",
                                "ClaimValue",
                                "ApiControllerDescriptionId"
                            };

            //Tablodaki değerler uniqueColumns a göre
            List<object> uniqueValues = new List<object>()
                            {
                                role.Id,
                                claim.Type,
                                claim.Value,
                                apiControllerDescriptionId
                            };

            DevelopmentModel.CompanyRoleClaim insertedEntity = new DevelopmentModel.CompanyRoleClaim()
            {
                RoleId = role.Id,
                ClaimType = claim.Type,
                ClaimValue = claim.Value,
                ApiControllerDescriptionId = apiControllerDescriptionId
            };

            DevelopmentDto.CompanyRoleClaimDto insertedDto =  DtoMapper_DiasFacilityManagementSqlServer_Development_CompanyRoleClaim.Map<DevelopmentModel.CompanyRoleClaim, DevelopmentDto.CompanyRoleClaimDto>(insertedEntity);

            Tuple<DiasShared.Errors.Error, DevelopmentDto.CompanyRoleClaimDto> resultOperation =
                await _genericStandartBusinessRulesCompanyRoleClaim.InsertV2(insertedDto, uniqueColumns, uniqueValues);

            if ((resultOperation.Item1.BusinessOperationSucceed) && (resultOperation.Item2 != null))
            {
                return resultOperation.Item2;
            }
            else
            {
                return null;
            }
        }

        [ObsoleteAttribute]
        public async Task<IList<Claim>> GetClaimsAsync(CompanyRole role, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            //TODO : Bu işlem daha sonra generic olmalıdır
            if (_companyRoleBusinessRules != null)
            {
                Tuple<DiasShared.Errors.Error, IEnumerable<DevelopmentDto.CompanyRoleClaimDto>> resultBusinessRule = await _companyRoleClaimBusinessRules.GetCompanyRoleClaimsByCompanyRoleIdAsync(role.Id);

                if ((resultBusinessRule.Item1.BusinessOperationSucceed) && (resultBusinessRule.Item2 != null))
                {
                    List<Claim> returnedClaims = new();

                    foreach (DevelopmentDto.CompanyRoleClaimDto item in resultBusinessRule.Item2)
                    {
                        returnedClaims.Add(new Claim(item.ClaimType, item.ClaimValue));
                    }

                    return returnedClaims;
                }
            }

            return null;
        }

        public async Task<IList<Claim>> GetClaimV2Async(CompanyRole role, int remoteDomainId, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

           
            if (_companyRoleBusinessRules != null)
            {
                Tuple<DiasShared.Errors.Error, IEnumerable<DevelopmentDto.CompanyRoleClaimDto>> resultBusinessRule = await _companyRoleClaimBusinessRules.GetCompanyRoleClaimsByCompanyRoleIdAndRemoteDomainIdAsync(role.Id, remoteDomainId);

                if ((resultBusinessRule.Item1.BusinessOperationSucceed) && (resultBusinessRule.Item2 != null))
                {
                    List<Claim> returnedClaims = new();

                    foreach (DevelopmentDto.CompanyRoleClaimDto item in resultBusinessRule.Item2)
                    {
                        returnedClaims.Add(new Claim(item.ClaimType, item.ClaimValue));
                    }

                    return returnedClaims;
                }
            }

            return null;
        }



        public Task RemoveClaimAsync(CompanyRole role, Claim claim, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// AddClaimV2Async i kullanın, bunu değil 
        /// </summary>       
        public Task AddClaimAsync(CompanyRole role, Claim claim, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }       

        #endregion IRoleClaimStore 
    }
}
