using DIAS.Application.Wrappers;
using DIAS.Domain.Models;
using DIAS.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DIAS.Application.Interfaces.Repositories
{
    public interface ITicketHistoryRepositoryAsync : IGenericRepositoryAsync<TicketHistory>
    {
        Task<Response<List<TicketHistory>>> GetAllTicketHistoryAsync(int ticketId);
        Task<Response<List<TicketHistoryView>>> GetAllTicketHistoryVwByTicketIdAsync(int ticketId);
        Task<Response<TicketHistory>> GetTicketHistoryLastActivityAsync(int ticketId);
        Task<Response<TicketHistory>> AddTicketHistoryAsync(int ticketId);
        Task<Response<TicketHistory>> UpdateTicketHistoryAsync(int id);
    }
}
