using DIAS.Application.Wrappers;
using DIAS.Domain.Models;
using DIAS.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DIAS.Application.Interfaces.Repositories
{
    public interface IAssigmentGroupEmployeeRepositoryAsync : IGenericRepositoryAsync<AssigmentGroupEmployee>
    {
        Task<Response<AssigmentGroupEmployee>> GetAssigmentGroupEmployeeByIdAsync(int id);
        Task<Response<List<AssigmentGroupEmployeeView>>> GetAssigmentGroupEmployeeVwByIdAsync(int id);
        Task<Response<IReadOnlyList<AssigmentGroupEmployee>>> GetAllAssigmentGroupEmployeesAsync();
        Task<Response<List<AssigmentGroupEmployeeView>>> GetAllAssigmentGroupEmployeesVwAsync();
        Task<Response<AssigmentGroupEmployee>> AddAssigmentGroupEmployeeAsync(AssigmentGroupEmployee model);
        Task<Response<AssigmentGroupEmployee>> UpdateAssigmentGroupEmployeeAsync(AssigmentGroupEmployee model);
        //Task<Response<string>> DeleteAssigmentGroupEmployeeAsync(AssigmentGroupEmployee model);
    }
}
