using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
namespace DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement
{
    public interface ITicketStateTransitionDtoRepository
    {
        public Task<Tuple<ErrorCodes, IEnumerable<DevelopmentDto.TicketStateTransitionDto>>> GetAllAsync();
        public Task<Tuple<ErrorCodes, DevelopmentDto.TicketStateTransitionDto>> DeleteFromIntAsync(int id);
        public Task<Tuple<ErrorCodes, DevelopmentDto.TicketStateTransitionDto>> GetByIdFromIntAsync(int id);
        public Task<Tuple<ErrorCodes, DevelopmentDto.TicketStateTransitionDto>> InsertAsync(DevelopmentDto.TicketStateTransitionDto insertedDto);
        public Task<Tuple<ErrorCodes, DevelopmentDto.TicketStateTransitionDto>> UpdateAsync(DevelopmentDto.TicketStateTransitionDto updatedDto);
    }
}
