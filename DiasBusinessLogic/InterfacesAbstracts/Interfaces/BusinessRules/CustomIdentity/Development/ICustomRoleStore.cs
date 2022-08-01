using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.CustomIdentity.Development
{
    public interface ICustomRoleStore<TRole> :  IRoleStore<TRole>, IRoleClaimStore<TRole> where TRole : class
    {
        public Task<CompanyRoleClaimDto> AddClaimV2Async(TRole role, Claim claim, int apiControllerDescriptionId, CancellationToken cancellationToken = default);

        public Task<IList<Claim>> GetClaimV2Async(TRole role, int remoteDomainId, CancellationToken cancellationToken = default);
    }
}
