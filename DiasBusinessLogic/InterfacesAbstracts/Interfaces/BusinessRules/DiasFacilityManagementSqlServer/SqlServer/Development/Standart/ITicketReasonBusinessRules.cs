using DiasShared.Errors;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;


namespace DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Standart
{
    public interface ITicketReasonBusinessRules
    {
        public Task<Tuple<ErrorCodes, IEnumerable<DevelopmentDto.TicketReasonDto>>> GetTicketReasonsByCategoryIdAsync(int id);
        public Task<Tuple<Error, DevelopmentDto.TicketReasonDto>> GetTicketReasonByIdAsync(int id);
    }
}
