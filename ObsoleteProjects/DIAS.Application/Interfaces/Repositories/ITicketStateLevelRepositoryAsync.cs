using DIAS.Application.Wrappers;
using DIAS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DIAS.Application.Interfaces.Repositories
{
    public interface ITicketStateLevelRepositoryAsync : IGenericRepositoryAsync<TicketStateLevel>
    {
        Task<Response<TicketStateLevel>> GetTicketStateLevelByIdAsync(int id);
        Task<Response<IReadOnlyList<TicketStateLevel>>> GetAllTicketStateLevelsAsync();
        Task<Response<List<TicketStateLevelView>>> GetTicketStateDestinationByIdAsync(int id);
        Task<Response<TicketStateLevel>> AddTicketStateLevelAsync(TicketStateLevel model);
        Task<Response<TicketStateLevel>> UpdateTicketStateLevelAsync(TicketStateLevel model);
        Task<Response<string>> DeleteTicketStateLevelAsync(TicketStateLevel model);
    }
}
