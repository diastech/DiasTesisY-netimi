using DIAS.Application.Wrappers;
using DIAS.Domain.Models;
using DIAS.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DIAS.Application.Interfaces.Repositories
{
    public interface ITicketRepositoryAsync : IGenericRepositoryAsync<Ticket>
    {
        Task<Response<Ticket>> GetTicketByIdAsync(int id);
        Task<Response<TicketView>> GetTicketVwByIdAsync(int id);
        Task<Response<IReadOnlyList<Ticket>>> GetAllTicketsAsync();
        Task<Response<List<TicketView>>> GetAllTicketsVwAsync(string filterLocation, string filterReason, int? filterState, int? filterPriority, string filterUser, int? filterAsgGroup, string filterDescription);
        Task<Response<List<TicketView>>> GetAllTicketsVwByBasicTicketIdAsync(int id);
        Task<Response<Ticket>> AddTicketAsync(Ticket model);
        Task<Response<Ticket>> UpdateTicketAsync(Ticket model);
        Task<Response<Ticket>> UpdateTicketStateAsync(int ticketId, int ticketState, string userId);
        //Task<Response<string>> DeleteTicketAsync(Ticket model);
    }
}
