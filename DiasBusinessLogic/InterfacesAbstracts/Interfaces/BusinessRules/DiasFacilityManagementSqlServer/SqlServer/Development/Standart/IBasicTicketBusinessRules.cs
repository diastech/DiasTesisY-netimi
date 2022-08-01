using DiasShared.Errors;
using DiasShared.Services.Communication.BusinessLogicMessage.ThirdPartyLibrary.DevExpress;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using StandartDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;


namespace DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Standart
{
    public interface IBasicTicketBusinessRules
    {
        public Task<Tuple<Error, List<StandartDto.BasicTicketDto>>> GetAllBasicTicketsAsync(DevExpressRequest devExpressRequestObj);
        public Task<Tuple<Error, StandartDto.BasicTicketDto>> GetBasicTicketByIdAsync(int Id);
        public Task<Tuple<Error, StandartDto.BasicTicketDto>> AddAsync(StandartDto.BasicTicketDto basicTicketDto);
        public Task<Tuple<Error, StandartDto.BasicTicketDto>> UpdateAsync(StandartDto.BasicTicketDto basicTicketDto);
        public Task<Tuple<Error, StandartDto.BasicTicketDto>> DeleteAsync(StandartDto.BasicTicketDto basicTicketDto);
    }
}
