using DiasShared.Errors;
using DiasShared.Services.Communication.BusinessLogicMessage;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using StandartTestDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard;

namespace DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement
{
    public interface ITicketPriorityDtoRepository
    {
        #region Development
        public Task<Tuple<Error, IEnumerable<DevelopmentDto.TicketPriorityDto>>> GetAllAsync();
        public Task<Tuple<Error, DevelopmentDto.TicketPriorityDto>> DeleteAsync(BusinessLogicRequest request);
        public Task<Tuple<Error, DevelopmentDto.TicketPriorityDto>> GetByIdFromIntAsync(int id);
        public Task<Tuple<Error, DevelopmentDto.TicketPriorityDto>> InsertAsync(BusinessLogicRequest request);
        public Task<Tuple<Error, DevelopmentDto.TicketPriorityDto>> UpdateAsync(BusinessLogicRequest request);
        #endregion

        #region Test
        public Task<Tuple<ErrorCodes, IEnumerable<StandartTestDto.TicketPriorityDto>>> GetAllTestAsync();
        public Task<Tuple<ErrorCodes, StandartTestDto.TicketPriorityDto>> InsertTestAsync(BusinessLogicRequest request);
        public Task<Tuple<ErrorCodes, StandartTestDto.TicketPriorityDto>> UpdateTestAsync(BusinessLogicRequest request);
        public Task<Tuple<ErrorCodes, StandartTestDto.TicketPriorityDto>> DeleteTestAsync(BusinessLogicRequest request);
        #endregion
    }
}
