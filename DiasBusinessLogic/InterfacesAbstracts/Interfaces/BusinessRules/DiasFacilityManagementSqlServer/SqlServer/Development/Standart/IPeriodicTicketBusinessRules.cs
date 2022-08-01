using DiasShared.Services.Communication.BusinessLogicMessage.ThirdPartyLibrary.DevExpress;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using StandartDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using DiasShared.Errors;
using DiasShared.Classes.Dto;

namespace DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Standart
{
    public interface IPeriodicTicketBusinessRules
    {
        public Task<Tuple<Error, DevExpressLoadResultDto<List<StandartDto.PeriodicTicketDto>>>> GetAllPeriodicTicketsAsync(DevExpressRequest devExpressRequestObj);
        public Task<Tuple<Error, StandartDto.PeriodicTicketDto>> GetPeriodicTicketByIdAsync(int Id);

        public Task<Tuple<Error, StandartDto.PeriodicTicketDto>> AddAsync(StandartDto.PeriodicTicketDto periodicTicketDto);
        public Task<Tuple<Error, StandartDto.PeriodicTicketDto>> UpdateAsync(StandartDto.PeriodicTicketDto periodicTicketDto);
        public Task<Tuple<Error, StandartDto.PeriodicTicketDto>> DeleteAsync(StandartDto.PeriodicTicketDto periodicTicketDto);
    }
}
