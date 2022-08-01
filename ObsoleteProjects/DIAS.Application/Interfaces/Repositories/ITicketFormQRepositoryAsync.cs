using DIAS.Application.Wrappers;
using DIAS.Domain.Models;
using DIAS.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DIAS.Application.Interfaces.Repositories
{
    public interface ITicketFormQRepositoryAsync : IGenericRepositoryAsync<TicketFormQView>
    {
        Task<Response<List<TicketFormQView>>> GetAllTicketFormQByFormIdAsync(int formId);
    }
}
