using DiasShared.Errors;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;

namespace DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement
{
    public interface ITicketStateFlowRoleDtoRepository
    {
        public Task<Tuple<Error, IEnumerable<DevelopmentDto.TicketStateFlowRoleDto>>> GetAllAsync();
        public Task<Tuple<Error, DevelopmentDto.TicketStateFlowRoleDto>> DeleteFromIntAsync(int id);
        public Task<Tuple<Error, DevelopmentDto.TicketStateFlowRoleDto>> GetByIdFromIntAsync(int id);
        public Task<Tuple<Error, DevelopmentDto.TicketStateFlowRoleDto>> InsertAsync(DevelopmentDto.TicketStateFlowRoleDto insertedDto);
        public Task<Tuple<Error, DevelopmentDto.TicketStateFlowRoleDto>> UpdateAsync(DevelopmentDto.TicketStateFlowRoleDto updatedDto);

    }
}
