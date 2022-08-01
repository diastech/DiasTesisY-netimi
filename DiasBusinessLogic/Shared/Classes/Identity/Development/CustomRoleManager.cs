using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.CustomIdentity.Development;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Threading;
using static DiasShared.Enums.Standart.RemoteCommunicationEnums;

namespace DiasBusinessLogic.Shared.Classes.Identity.Development
{
    public class CustomRoleManager<TRole> : RoleManager<TRole> where TRole : class
    {
        /// <summary>
        /// Kullanılmayacak
        /// </summary>
        public CustomRoleManager(IRoleStore<TRole> a, IEnumerable<IRoleValidator<TRole>> b, ILookupNormalizer c,
            IdentityErrorDescriber d, ILogger<RoleManager<TRole>> e) : base(a, b, c, d, e)
        {
            this.StoreV2 = null;
        }


        /// <summary>
        /// Kullanılacak kurucu metod
        /// </summary>
        public CustomRoleManager(ICustomRoleStore<TRole> a, IEnumerable<IRoleValidator<TRole>> b, ILookupNormalizer c,
            IdentityErrorDescriber d, ILogger<RoleManager<TRole>> e) : base(a, b, c, d, e)
        {
            this.StoreV2 = a;
        }

        public ICustomRoleStore<TRole> StoreV2 { get; set; }


        public async Task<CompanyRoleClaimDto> AddClaimV2Async(TRole role, Claim claim, int apiControllerDescriptionId, CancellationToken cancellationToken = default)
        {           
            if (this.StoreV2 != null)
            {
                return (await this.StoreV2.AddClaimV2Async(role, claim, apiControllerDescriptionId, cancellationToken));
            }

            return null;
        }

        public async Task<IList<Claim>> GetClaimsV2Async(TRole role, int remoteDomainId = (int)(RemoteIncomingDomains.DiasTesisYonetimMobileClient),
                                                                                                                        CancellationToken cancellationToken = default)
        {
            if (this.StoreV2 != null)
            {
                return (await this.StoreV2.GetClaimV2Async(role, remoteDomainId, cancellationToken));
            }

            return null;
        }

    }
}
