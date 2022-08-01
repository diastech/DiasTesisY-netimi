using DIAS.Application.DTOs.Account;
using DIAS.Application.Wrappers;
using DIAS.Infrastructure.Identity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DIAS.Infrastructure.Identity.Interfaces
{
    public interface IApplicationUserService
    {
        Task<Response<List<ApplicationUser>>> GetAllUsersAsync();
        Task<Response<ApplicationUser>> GetUserAsync(string id);
        Task<Response<ApplicationUser>> CreateUserAsync(ApplicationUser user);
        Task<Response<ApplicationUser>> UpdateUserAsync(ApplicationUser user);

    }
}