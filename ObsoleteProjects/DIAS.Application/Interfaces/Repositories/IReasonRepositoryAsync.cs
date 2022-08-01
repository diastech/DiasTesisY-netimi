using DIAS.Application.Wrappers;
using DIAS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DIAS.Application.Interfaces.Repositories
{
    public interface IReasonRepositoryAsync : IGenericRepositoryAsync<Reason>
    {
        Task<Response<Reason>> GetReasonByIdAsync(int id);
        Task<Response<IReadOnlyList<Reason>>> GetAllReasonsAsync();
        Task<Response<Reason>> AddReasonAsync(Reason model);
        Task<Response<Reason>> UpdateReasonAsync(Reason model);
        Task<Response<string>> DeleteReasonAsync(Reason model);
    }
}
