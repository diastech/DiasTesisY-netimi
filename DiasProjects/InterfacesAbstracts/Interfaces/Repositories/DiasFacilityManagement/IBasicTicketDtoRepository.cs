using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
namespace DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement
{
    public interface IBasicTicketDtoRepository
    {
        public Task<Tuple<ErrorCodes, IEnumerable<DevelopmentDto.BasicTicketDto>>> GetAllBasicTicketsByUserIdAsync(int userId);
        public Task<Tuple<ErrorCodes, IEnumerable<DevelopmentDto.BasicTicketDto>>> GetAllAsync();
        public Task<Tuple<ErrorCodes, DevelopmentDto.BasicTicketDto>> DeleteFromIntAsync(int id);
        public Task<Tuple<ErrorCodes, DevelopmentDto.BasicTicketDto>> GetByIdFromIntAsync(int id);
        public Task<Tuple<ErrorCodes, DevelopmentDto.BasicTicketDto>> InsertAsync(DevelopmentDto.BasicTicketDto insertedDto);
        public Task<Tuple<ErrorCodes, DevelopmentDto.BasicTicketDto>> UpdateAsync(DevelopmentDto.BasicTicketDto updatedDto);
    }
}
