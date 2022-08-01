using DIAS.Application.Wrappers;
using DIAS.Domain.Models;
using DIAS.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DIAS.Application.Interfaces.Repositories
{
    public interface IBasicTicketRepositoryAsync : IGenericRepositoryAsync<BasicTicket>
    {
        Task<Response<BasicTicket>> GetBasicTicketByIdAsync(int id);
        Task<Response<IReadOnlyList<BasicTicket>>> GetAllBasicTicketsAsync();
        Task<Response<List<BasicTicketView>>> GetAllBasicTicketsVwAsync();
        Task<Response<List<BasicTicketView>>> GetAllBasicTicketsVwByUserIdAsync(string id);
        Task<Response<BasicTicket>> AddBasicTicketAsync(BasicTicket model);
        Task<Response<BasicTicket>> UpdateBasicTicketAsync(BasicTicket model);
    }
}
