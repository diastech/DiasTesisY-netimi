using DiasShared.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
namespace DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement
{
    public interface ITicketStateFlowWrapperDtoRepository
    {
        public Task<Tuple<Error, IEnumerable<CustomDto.CustomTicketStateFlowDto>>> GetAllTicketStateFlowdWrapperAsync();
    }
}
