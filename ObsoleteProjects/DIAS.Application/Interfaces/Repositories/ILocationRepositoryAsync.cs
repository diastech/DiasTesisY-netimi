using DIAS.Application.Wrappers;
using DIAS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DIAS.Application.Interfaces.Repositories
{
    public interface ILocationRepositoryAsync : IGenericRepositoryAsync<Location>
    {
        Task<Response<Location>> GetLocationByIdAsync(int id);
        Task<Response<Location>> GetLocationByHierarchyAsync(string hierarchy);
        Task<Response<IReadOnlyList<Location>>> GetAllLocationsAsync();
        Task<Response<Location>> AddLocationAsync(Location model);
        Task<Response<Location>> UpdateLocationAsync(Location model);
        Task<Response<string>> DeleteLocationAsync(Location model);
    }
}
