using DIAS.Application.Wrappers;
using DIAS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DIAS.Application.Interfaces.Repositories
{
    public interface ITicketStateRepositoryAsync : IGenericRepositoryAsync<TicketState>
    {
        Task<Response<TicketState>> GetTicketStateByIdAsync(int id);
        Task<Response<IReadOnlyList<TicketState>>> GetAllTicketStatesAsync();
        Task<Response<TicketState>> AddTicketStateAsync(TicketState model);
        Task<Response<TicketState>> UpdateTicketStateAsync(TicketState model);
        Task<Response<string>> DeleteTicketStateAsync(TicketState model);
    }
}
