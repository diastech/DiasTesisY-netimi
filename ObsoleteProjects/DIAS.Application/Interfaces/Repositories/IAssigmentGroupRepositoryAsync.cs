using DIAS.Application.Wrappers;
using DIAS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DIAS.Application.Interfaces.Repositories
{
    public interface IAssigmentGroupRepositoryAsync : IGenericRepositoryAsync<AssigmentGroup>
    {
        Task<Response<AssigmentGroup>> GetAssigmentGroupByIdAsync(int id);
        Task<Response<IReadOnlyList<AssigmentGroup>>> GetAllAssigmentGroupsAsync();
        Task<Response<AssigmentGroup>> AddAssigmentGroupAsync(AssigmentGroup model);
        Task<Response<AssigmentGroup>> UpdateAssigmentGroupAsync(AssigmentGroup model);
        Task<Response<string>> DeleteAssigmentGroupAsync(AssigmentGroup model);
    }
}
