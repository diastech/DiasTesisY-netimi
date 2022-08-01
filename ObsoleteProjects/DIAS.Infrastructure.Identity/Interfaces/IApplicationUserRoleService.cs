using DIAS.Application.DTOs.Account;
using DIAS.Application.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace DIAS.Infrastructure.Identity.Interfaces
{
    public interface IApplicationUserRoleService
    {
        Task<Response<List<IdentityRole>>> GetRolesAsync();
        Task<Response<List<IdentityRole>>> GetUserRolesAsync(string id);
        Task<Response<IdentityRole>> CreateAsync(IdentityRole model);
        Task<Response<IdentityRole>> UpdateAsync(IdentityRole model);
        Task<Response<string>> DeleteAsync(string id);
        Task<Response<string>> AddUserRolesAsync(string id, string role);
        Task<Response<string>> DeleteUserRolesAsync(string id, string role);
    }
}
