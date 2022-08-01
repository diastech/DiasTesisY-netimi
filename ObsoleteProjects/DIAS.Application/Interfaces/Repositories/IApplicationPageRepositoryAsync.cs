using DIAS.Application.Wrappers;
using DIAS.Domain.Models;
using DIAS.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DIAS.Application.Interfaces.Repositories
{
    public interface IApplicationPageRepositoryAsync : IGenericRepositoryAsync<ApplicationPage>
    {
        Task<Response<ApplicationPage>> GetPageByIdAsync(string id);
        Task<Response<IReadOnlyList<ApplicationPage>>> GetAllPagesAsync();
        Task<Response<ApplicationPage>> AddPageAsync(ApplicationPage model);
        Task<Response<ApplicationPage>> UpdatePageAsync(ApplicationPage model);
        Task<Response<string>> DeletePageAsync(ApplicationPage model);
    }
}
