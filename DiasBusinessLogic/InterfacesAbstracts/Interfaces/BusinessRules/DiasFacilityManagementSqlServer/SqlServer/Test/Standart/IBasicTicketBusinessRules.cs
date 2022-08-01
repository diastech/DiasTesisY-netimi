using DiasShared.Services.Communication.BusinessLogicMessage.ThirdPartyLibrary.DevExpress;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using StandartDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard;


namespace DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Test.Standart
{
    public interface IBasicTicketBusinessRules
    {
        public Task<Tuple<ErrorCodes, List<StandartDto.BasicTicketDto>>> GetAllBasicTicketsAsync(DevExpressRequest devExpressRequestObj);
        public Task<Tuple<ErrorCodes, StandartDto.BasicTicketDto>> GetBasicTicketByIdAsync(int Id);
        public Task<Tuple<ErrorCodes, StandartDto.BasicTicketDto>> AddAsync(StandartDto.BasicTicketDto basicTicketDto);
        public Task<Tuple<ErrorCodes, StandartDto.BasicTicketDto>> UpdateAsync(StandartDto.BasicTicketDto basicTicketDto);
        public Task<Tuple<ErrorCodes, StandartDto.BasicTicketDto>> DeleteAsync(StandartDto.BasicTicketDto basicTicketDto);
    }
}
