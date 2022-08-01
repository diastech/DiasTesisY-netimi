using DIAS.Application.Wrappers;
using DIAS.Domain.Models;
using DIAS.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DIAS.Application.Interfaces.Repositories
{
    public interface ITicketRelatedLocationRepositoryAsync : IGenericRepositoryAsync<TicketRelatedLocation>
    {
        Task<Response<TicketRelatedLocation>> GetTicketRelatedLocationByIdAsync(int id);
        Task<Response<List<TicketRelatedLocationView>>> GetTicketRelatedLocationVwByIdAsync(int id);
        Task<Response<IReadOnlyList<TicketRelatedLocation>>> GetAllTicketRelatedLocationsAsync();
        Task<Response<List<TicketRelatedLocationView>>> GetAllTicketRelatedLocationsVwAsync();
        Task<Response<TicketRelatedLocation>> AddTicketRelatedLocationAsync(TicketRelatedLocation model);
        Task<Response<TicketRelatedLocation>> UpdateTicketRelatedLocationAsync(TicketRelatedLocation model);
        //Task<Response<string>> DeleteTicketRelatedLocationAsync(TicketRelatedLocation model);
    }
}
