
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.CustomIdentity.Development;
using DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFM.SqlServer.Development.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DiasBusinessLogic.Shared.Classes.Identity.Development
{
    public class CustomUserManager<TUser> : UserManager<TUser> where TUser : class
    {
        /// <summary>
        /// Kullanılmayacak
        /// </summary>
        public CustomUserManager(IUserStore<TUser> a, IOptions<IdentityOptions>  b, IPasswordHasher<TUser> c, 
            IEnumerable<IUserValidator<TUser>> d, IEnumerable<IPasswordValidator<TUser>> e, ILookupNormalizer f, 
            IdentityErrorDescriber g, IServiceProvider h, ILogger<UserManager<TUser>> i) : base(a, b, c, d, e, f, g, h, i)
        {
            this.StoreV2 = null;
        }


        /// <summary>
        /// Kullanılacak kurucu metod
        /// </summary>
        public CustomUserManager(ICustomUserStore<TUser> a, IOptions<IdentityOptions> b, IPasswordHasher<TUser> c,
            IEnumerable<IUserValidator<TUser>> d, IEnumerable<IPasswordValidator<TUser>> e, ILookupNormalizer f,
            IdentityErrorDescriber g, IServiceProvider h, ILogger<UserManager<TUser>> i) : base(a, b, c, d, e, f, g, h, i)
        {
            this.StoreV2 = a;
        }

        public ICustomUserStore<TUser> StoreV2 { get; set; }

        public async Task<IList<CompanyRole>> GetRolesV2Async(TUser user, CancellationToken cancellationToken = default)
        {
            if (this.StoreV2 != null)
            {
                return (await this.StoreV2.GetRolesV2Async(user, cancellationToken));
            }

            return null;
        }
    }
}
