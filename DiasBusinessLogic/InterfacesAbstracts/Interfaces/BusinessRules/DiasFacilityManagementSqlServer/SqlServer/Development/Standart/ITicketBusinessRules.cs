using DiasShared.Errors;
using DiasShared.Services.Communication.BusinessLogicMessage.ThirdPartyLibrary.DevExpress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using StandartDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;


namespace DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Standart
{
    public interface ITicketBusinessRules
    {
        public Task<Tuple<Error, List<StandartDto.TicketDto>>> GetAllTicketAsync(DevExpressRequest devExpressRequestObj);
        public Task<Tuple<Error, List<StandartDto.TicketDto>>> GetAllTicketsMobileAsync(DevExpressRequest devExpressRequestObj);
        public Task<Tuple<Error, StandartDto.TicketDto>> GetTicketByIdAsync(int Id);
        public Task<Tuple<Error, List<StandartDto.TicketDto>>> GetAllTicketsByBasicTicketIdAsync(int Id);
        public Task<Tuple<Error, StandartDto.TicketDto>> AddAsync(StandartDto.TicketDto ticketDto);
        public Task<Tuple<Error, StandartDto.TicketDto>> UpdateAsync(StandartDto.TicketDto ticketDto, bool updateState = false);
        public Task<Tuple<Error, StandartDto.TicketDto>> DeleteAsync(StandartDto.TicketDto ticketDto);
    }
}
