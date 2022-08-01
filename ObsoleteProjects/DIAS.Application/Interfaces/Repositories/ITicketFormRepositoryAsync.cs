using DIAS.Application.Wrappers;
using DIAS.Domain.Models;
using DIAS.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DIAS.Application.Interfaces.Repositories
{
    public interface ITicketFormRepositoryAsync : IGenericRepositoryAsync<TicketForm>
    {
        Task<Response<TicketForm>> GetTicketFormByIdAsync(int id);
        Task<Response<IReadOnlyList<TicketForm>>> GetAllTicketFormsAsync();
        Task<Response<List<TicketForm>>> GetAllTicketFormsAsync(int reasonId, int? reasonCategoryId, int ticketState);
    }
}
