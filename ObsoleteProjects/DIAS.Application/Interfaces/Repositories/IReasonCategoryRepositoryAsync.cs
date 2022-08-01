using DIAS.Application.Wrappers;
using DIAS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DIAS.Application.Interfaces.Repositories
{
    public interface IReasonCategoryRepositoryAsync : IGenericRepositoryAsync<ReasonCategory>
    {
        Task<Response<ReasonCategory>> GetReasonCategoryByIdAsync(int id);
        Task<Response<IReadOnlyList<ReasonCategory>>> GetAllReasonCategoriesAsync();
        Task<Response<List<ReasonCategoryView>>> GetAllReasonCategoriesVwAsync();
        Task<Response<ReasonCategory>> AddReasonCategoryAsync(ReasonCategory model);
        Task<Response<ReasonCategory>> UpdateReasonCategoryAsync(ReasonCategory model);
        Task<Response<string>> DeleteReasonCategoryAsync(ReasonCategory model);
    }
}
