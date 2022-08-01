using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DiasShared.Services.Communication.BusinessLogicMessage;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using StandartTestDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard;
using DiasShared.Errors;

namespace DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement
{
    public interface ITicketStateDtoRepository
    {
        #region Development
        public Task<Tuple<Error, IEnumerable<DevelopmentDto.TicketStateDto>>> GetAllAsync();
        public Task<Tuple<Error, DevelopmentDto.TicketStateDto>> DeleteAsync(BusinessLogicRequest request);
        public Task<Tuple<Error, DevelopmentDto.TicketStateDto>> GetByIdFromIntAsync(int id);
        public Task<Tuple<Error, DevelopmentDto.TicketStateDto>> InsertAsync(BusinessLogicRequest request);
        public Task<Tuple<Error, DevelopmentDto.TicketStateDto>> UpdateAsync(BusinessLogicRequest request);
        #endregion

        #region Test
        public Task<Tuple<ErrorCodes, IEnumerable<StandartTestDto.TicketStateDto>>> GetAllTestAsync();
        public Task<Tuple<ErrorCodes, StandartTestDto.TicketStateDto>> InsertTestAsync(BusinessLogicRequest request);
        public Task<Tuple<ErrorCodes, StandartTestDto.TicketStateDto>> UpdateTestAsync(BusinessLogicRequest request);
        public Task<Tuple<ErrorCodes, StandartTestDto.TicketStateDto>> DeleteTestAsync(BusinessLogicRequest request);
        #endregion

    }
}
