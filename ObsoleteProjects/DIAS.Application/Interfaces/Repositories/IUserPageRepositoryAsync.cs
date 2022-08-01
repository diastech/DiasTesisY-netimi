using DIAS.Application.Wrappers;
using DIAS.Domain.Models;
using DIAS.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DIAS.Application.Interfaces.Repositories
{
    public interface IUserPageRepositoryAsync : IGenericRepositoryAsync<UserPage>
    {
        Task<Response<List<UserPageView>>> GetAllUserPagesAsync(string id);
        Task<Response<IReadOnlyList<UserPage>>> GetAllUserPagesAsync();
        Task<Response<UserPage>> AddUserPageAsync(UserPage model);
        Task<Response<UserPage>> UpdateUserPageAsync(UserPage model);
        Task<Response<string>> DeleteUserPageAsync(UserPage model);
    }
}
