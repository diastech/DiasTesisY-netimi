using DiasShared.Errors;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;

namespace DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement
{
    public interface ITicketStateFlowDtoRepository
    {
        public Task<Tuple<Error, IEnumerable<DevelopmentDto.TicketStateFlowDto>>> GetAllAsync();
        public Task<Tuple<Error, DevelopmentDto.TicketStateFlowDto>> DeleteFromIntAsync(int id);
        public Task<Tuple<Error, DevelopmentDto.TicketStateFlowDto>> GetByIdFromIntAsync(int id);
        public Task<Tuple<Error, DevelopmentDto.TicketStateFlowDto>> InsertAsync(DevelopmentDto.TicketStateFlowDto insertedDto);
        public Task<Tuple<Error, DevelopmentDto.TicketStateFlowDto>> UpdateAsync(DevelopmentDto.TicketStateFlowDto updatedDto);

    }
}
