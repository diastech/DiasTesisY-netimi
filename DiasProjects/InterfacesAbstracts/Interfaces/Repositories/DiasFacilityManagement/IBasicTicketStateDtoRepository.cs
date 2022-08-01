using DiasShared.Errors;
using DiasShared.Services.Communication.BusinessLogicMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using StandartTestDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard;

namespace DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement
{
    public interface IBasicTicketStateDtoRepository
    {
        #region Development
        public Task<Tuple<Error, IEnumerable<DevelopmentDto.BasicTicketStateDto>>> GetAllAsync();
        public Task<Tuple<Error, DevelopmentDto.BasicTicketStateDto>> DeleteAsync(BusinessLogicRequest request);
        public Task<Tuple<Error, DevelopmentDto.BasicTicketStateDto>> GetByIdFromIntAsync(int id);
        public Task<Tuple<Error, DevelopmentDto.BasicTicketStateDto>> InsertAsync(BusinessLogicRequest request);
        public Task<Tuple<Error, DevelopmentDto.BasicTicketStateDto>> UpdateAsync(BusinessLogicRequest request);
        #endregion

        #region Test
        public Task<Tuple<ErrorCodes, IEnumerable<StandartTestDto.BasicTicketStateDto>>> GetAllTestAsync();
        public Task<Tuple<ErrorCodes, StandartTestDto.BasicTicketStateDto>> InsertTestAsync(BusinessLogicRequest request);
        public Task<Tuple<ErrorCodes, StandartTestDto.BasicTicketStateDto>> UpdateTestAsync(BusinessLogicRequest request);
        public Task<Tuple<ErrorCodes, StandartTestDto.BasicTicketStateDto>> DeleteTestAsync(BusinessLogicRequest request);
        #endregion
    }
}
