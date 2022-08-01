using DIAS.Application.Wrappers;
using DIAS.Domain.Models;
using DIAS.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DIAS.Application.Interfaces.Repositories
{
    public interface ITicketNotesRepositoryAsync : IGenericRepositoryAsync<TicketNotes>
    {
        Task<Response<TicketNotes>> GetTicketNotesByIdAsync(int id);
        Task<Response<IReadOnlyList<TicketNotes>>> GetAllTicketNotesAsync();
        Task<Response<List<TicketNotes>>> GetAllTicketNotesByTicketIdAsync(int id);
        Task<Response<List<TicketNotesView>>> GetAllTicketNotesVwByTicketIdAsync(int id);
        Task<Response<TicketNotes>> AddTicketNotesAsync(TicketNotes model);
        //Task<Response<TicketNotes>> UpdateTicketNotesAsync(TicketNotes model);
    }
}
