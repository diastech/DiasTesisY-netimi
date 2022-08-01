using DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFM.SqlServer.Development.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.CustomIdentity.Development
{
    public interface ICustomUserStore<TUser> : IUserStore<TUser>, IUserRoleStore<TUser>, IUserPasswordStore<TUser> where TUser : class
    {
        public Task<IList<CompanyRole>> GetRolesV2Async(TUser user, CancellationToken cancellationToken = default);
    }
}
