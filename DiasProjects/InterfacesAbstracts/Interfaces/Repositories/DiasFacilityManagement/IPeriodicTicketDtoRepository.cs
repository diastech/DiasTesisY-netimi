using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
namespace DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement
{
    public interface IPeriodicTicketDtoRepository
    {
        public Task<Tuple<ErrorCodes, IEnumerable<DevelopmentDto.PeriodicTicketDto>>> GetAllAsync();
        public Task<Tuple<ErrorCodes, DevelopmentDto.PeriodicTicketDto>> DeleteFromIntAsync(int id);
        public Task<Tuple<ErrorCodes, DevelopmentDto.PeriodicTicketDto>> GetByIdFromIntAsync(int id);
        public Task<Tuple<ErrorCodes, DevelopmentDto.PeriodicTicketDto>> InsertAsync(DevelopmentDto.PeriodicTicketDto insertedDto);
        public Task<Tuple<ErrorCodes, DevelopmentDto.PeriodicTicketDto>> UpdateAsync(DevelopmentDto.PeriodicTicketDto updatedDto);
    }
}
