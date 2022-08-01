using Microsoft.AspNetCore.Identity;
using DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFM.SqlServer.Development.Models;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using DevelopmentUserInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Standart;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using DevelopmentModel = DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFM.SqlServer.Development.Models;
using DiasBusinessLogic.AutoMapper.Configuration;
using DevelopmentLocationProfile = DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Development;
using DiasBusinessLogic.Shared.Configuration;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Generic.DiasFacilityManagementSqlServer.Development;
using DiasBusinessLogic.BusinessRules.DiasFacilityManagementSql.Configuration;
using DevFmBlInterface = DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Generic.DiasFacilityManagementSqlServer.Development;
using System.Linq;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.CustomIdentity.Development;

namespace DiasBusinessLogic.Shared.Classes.Identity.Development
{
    /// <summary>
    /// Metodları implemente edilmemiştir
    /// </summary>
    //TODO: Metodları implemente et
    public class CustomUserStore : ICustomUserStore<User>
    {
        private static AutoMapperProfileMapper<DevelopmentLocationProfile.UserProfile> DtoMapper_DiasFacilityManagementSqlServer_Development_User
                => new(new(DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer")));

        private static AutoMapperProfileMapper<DevelopmentLocationProfile.CompanyRoleProfile> DtoMapper_DiasFacilityManagementSqlServer_Development_CompanyRole
                => new(new(DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer")));

        private static AutoMapperProfileMapper<DevelopmentLocationProfile.CompanyRoleUserProfile> DtoMapper_DiasFacilityManagementSqlServer_Development_CompanyRoleUser
                => new(new(DataContextHelper.GetDataContextTypeViaConfiguration("DiasFacilityManagementSqlServer")));

        private readonly DevelopmentUserInterface.ICompanyRoleUserBusinessRules _companyRoleUserBusinessRules;
        private readonly DevelopmentUserInterface.ICompanyRoleBusinessRules _companyRoleBusinessRules;
        private IGenericStandartBusinessRules<DevelopmentDto.CompanyRoleUserDto, DevelopmentLocationProfile.CompanyRoleUserProfile> _genericStandartBusinessRulesCompanyRoleUser;


        public CustomUserStore() : this(DI_ServiceLocator.Current.Get<DevelopmentUserInterface.ICompanyRoleUserBusinessRules>(),
                                    DI_ServiceLocator.Current.Get<DevelopmentUserInterface.ICompanyRoleBusinessRules>(),
                                    DI_ServiceLocator.Current.Get<DevFmBlInterface.IGenericStandartBusinessRules<DevelopmentDto.CompanyRoleUserDto, DevelopmentLocationProfile.CompanyRoleUserProfile>>())
        {
        }

        private CustomUserStore(DevelopmentUserInterface.ICompanyRoleUserBusinessRules companyRoleUserBusinessRules,
            DevelopmentUserInterface.ICompanyRoleBusinessRules companyRoleBusinessRules,
            DevFmBlInterface.IGenericStandartBusinessRules<DevelopmentDto.CompanyRoleUserDto, DevelopmentLocationProfile.CompanyRoleUserProfile> genericCompanyRoleUserBL)
        {
            _companyRoleUserBusinessRules = companyRoleUserBusinessRules;
            _companyRoleBusinessRules = companyRoleBusinessRules;
            _genericStandartBusinessRulesCompanyRoleUser = genericCompanyRoleUserBL;
        }


        public void Dispose()
        {
        }


        #region IUserStore-IUserRoleStore

        public async Task<IdentityResult> CreateAsync(User user,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            //cancellationToken.ThrowIfCancellationRequested();
            //if (user == null) throw new ArgumentNullException(nameof(user));

            //return await _usersTable.CreateAsync(user);

            throw new NotImplementedException();
        }
       


        public async Task<IdentityResult> DeleteAsync(User user,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            //cancellationToken.ThrowIfCancellationRequested();
            //if (user == null) throw new ArgumentNullException(nameof(user));

            //return await _usersTable.DeleteAsync(user);

            throw new NotImplementedException();

        }

       

        public async Task<User> FindByIdAsync(string userId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            //cancellationToken.ThrowIfCancellationRequested();
            //if (userId == null) throw new ArgumentNullException(nameof(userId));
            //Guid idGuid;
            //if (!Guid.TryParse(userId, out idGuid))
            //{
            //    throw new ArgumentException("Not a valid Guid id", nameof(userId));
            //}

            //return await _usersTable.FindByIdAsync(idGuid);

            throw new NotImplementedException();

        }

        public async Task<User> FindByNameAsync(string userName,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            //cancellationToken.ThrowIfCancellationRequested();
            //if (userName == null) throw new ArgumentNullException(nameof(userName));

            //return await _usersTable.FindByNameAsync(userName);

            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            //cancellationToken.ThrowIfCancellationRequested();
            //if (user == null) throw new ArgumentNullException(nameof(user));

            //return Task.FromResult(user.PasswordHash);

            throw new NotImplementedException();
        }

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            //cancellationToken.ThrowIfCancellationRequested();
            //if (user == null) throw new ArgumentNullException(nameof(user));

            //return Task.FromResult(user.Id.ToString());

            throw new NotImplementedException();
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            //cancellationToken.ThrowIfCancellationRequested();
            //if (user == null) throw new ArgumentNullException(nameof(user));

            //return Task.FromResult(user.UserName);

            throw new NotImplementedException();
        }

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            //cancellationToken.ThrowIfCancellationRequested();
            //if (user == null) throw new ArgumentNullException(nameof(user));
            //if (normalizedName == null) throw new ArgumentNullException(nameof(normalizedName));

            //user.NormalizedUserName = normalizedName;
            //return Task.FromResult<object>(null);

            throw new NotImplementedException();
        }

        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            //cancellationToken.ThrowIfCancellationRequested();
            //if (user == null) throw new ArgumentNullException(nameof(user));
            //if (passwordHash == null) throw new ArgumentNullException(nameof(passwordHash));

            //user.PasswordHash = passwordHash;
            //return Task.FromResult<object>(null);

            throw new NotImplementedException();
        }

        #endregion IUserStore-IUserRoleStore

        #region IUserRoleStore

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task AddToRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<CompanyRole>> GetRolesV2Async(User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            //TODO : Bu işlem daha sonra generic olmalıdır
            if (_companyRoleUserBusinessRules != null)
            {
                Tuple<DiasShared.Errors.Error, IEnumerable<DevelopmentDto.CompanyRoleUserDto>> resultBusinessRule = await _companyRoleUserBusinessRules.GetCompanyRoleUsersByUserIdAsync(user.Id);

                if ((resultBusinessRule.Item1.BusinessOperationSucceed) && (resultBusinessRule.Item2 != null))
                {
                    //Rolleri çıkar
                    //Önce aynı roller varsa temizleyelim
                    List<int> roleIds = resultBusinessRule.Item2.GroupBy(x => x.RoleId).Select(g => g.First()).Select(x => x.RoleId).ToList();

                    if (_companyRoleBusinessRules != null)
                    {
                        Tuple<DiasShared.Errors.Error, IEnumerable<DevelopmentDto.CompanyRoleDto>> resultBusinessRuleInner =
                            await _companyRoleBusinessRules.GetCompanyRolesByIdsAsync(roleIds);

                        if ((resultBusinessRuleInner.Item1.BusinessOperationSucceed) && (resultBusinessRuleInner.Item2 != null))
                        {
                            List<CompanyRole> roles = new();

                            foreach (DevelopmentDto.CompanyRoleDto item in resultBusinessRuleInner.Item2)
                            {
                                roles.Add(DtoMapper_DiasFacilityManagementSqlServer_Development_CompanyRole.
                                            Map<DevelopmentDto.CompanyRoleDto, DevelopmentModel.CompanyRole>(item));
                            }

                            return roles;
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// GetRolesV2Async i kullanın, bunu değil 
        /// </summary>       
        public async Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsInRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        

        #endregion IUserRoleStore
    }
}
