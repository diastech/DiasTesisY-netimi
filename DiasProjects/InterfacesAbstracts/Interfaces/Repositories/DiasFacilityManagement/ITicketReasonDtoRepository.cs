using DiasShared.Errors;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
namespace DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement
{
    public interface ITicketReasonDtoRepository
    {
        public Task<Tuple<ErrorCodes, IEnumerable<DevelopmentDto.TicketReasonDto>>> GetAllAsync();
        public Task<Tuple<ErrorCodes, DevelopmentDto.TicketReasonDto>> DeleteFromIntAsync(int id);
        public Task<Tuple<Error, DevelopmentDto.TicketReasonDto>> GetByIdFromIntAsync(int id);
        public Task<Tuple<Error, IEnumerable<DevelopmentDto.TicketReasonDto>>> GetByTicketReasonCategoryIdAsync(int categoryId);
        public Task<Tuple<ErrorCodes, DevelopmentDto.TicketReasonDto>> InsertAsync(DevelopmentDto.TicketReasonDto insertedDto);
        public Task<Tuple<ErrorCodes, DevelopmentDto.TicketReasonDto>> UpdateAsync(DevelopmentDto.TicketReasonDto updatedDto);

    }
}
