using DiasShared.Errors;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;

namespace DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement
{
    public interface ITicketStateRoleDtoRepository
    {
        public Task<Tuple<Error, IEnumerable<DevelopmentDto.TicketStateRoleDto>>> GetAllAsync();
        public Task<Tuple<Error, DevelopmentDto.TicketStateRoleDto>> DeleteFromIntAsync(int id);
        public Task<Tuple<Error, DevelopmentDto.TicketStateRoleDto>> GetByIdFromIntAsync(int id);
        public Task<Tuple<Error, DevelopmentDto.TicketStateRoleDto>> InsertAsync(DevelopmentDto.TicketStateRoleDto insertedDto);
        public Task<Tuple<Error, DevelopmentDto.TicketStateRoleDto>> UpdateAsync(DevelopmentDto.TicketStateRoleDto updatedDto);

    }
}
