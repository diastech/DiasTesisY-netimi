using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
namespace DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement
{
    public interface ITicketReasonCategoryDtoRepository
    {
        public Task<Tuple<ErrorCodes, IEnumerable<DevelopmentDto.TicketReasonCategoryDto>>> GetAllAsync();
        public Task<Tuple<ErrorCodes, DevelopmentDto.TicketReasonCategoryDto>> DeleteFromIntAsync(int id);
        public Task<Tuple<ErrorCodes, DevelopmentDto.TicketReasonCategoryDto>> GetByIdFromIntAsync(int id);
        public Task<Tuple<ErrorCodes, DevelopmentDto.TicketReasonCategoryDto>> InsertAsync(DevelopmentDto.TicketReasonCategoryDto insertedDto);
        public Task<Tuple<ErrorCodes, DevelopmentDto.TicketReasonCategoryDto>> UpdateAsync(DevelopmentDto.TicketReasonCategoryDto updatedDto);
    }
}
