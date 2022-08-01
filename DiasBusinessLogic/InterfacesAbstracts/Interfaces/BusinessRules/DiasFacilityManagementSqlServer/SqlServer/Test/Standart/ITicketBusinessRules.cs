using DiasShared.Services.Communication.BusinessLogicMessage.ThirdPartyLibrary.DevExpress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using StandartDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard;


namespace DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Test.Standart
{
    public interface ITicketBusinessRules
    {
        public Task<Tuple<ErrorCodes, List<StandartDto.TicketDto>>> GetAllTicketAsync(DevExpressRequest devExpressRequestObj);
        public Task<Tuple<ErrorCodes, StandartDto.TicketDto>> GetTicketByIdAsync(int Id);
        public Task<Tuple<ErrorCodes, List<StandartDto.TicketDto>>> GetAllTicketsByBasicTicketIdAsync(int Id);
        public Task<Tuple<ErrorCodes, StandartDto.TicketDto>> AddAsync(StandartDto.TicketDto ticketDto);
        public Task<Tuple<ErrorCodes, StandartDto.TicketDto>> UpdateAsync(StandartDto.TicketDto ticketDto);
        public Task<Tuple<ErrorCodes, StandartDto.TicketDto>> DeleteAsync(StandartDto.TicketDto ticketDto);
    }
}
