using DiasShared.Errors;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using StandartDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;

namespace DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Standart
{
    public interface ITicketStateFlowBusinessRules
    {
        public Task<Tuple<Error, List<StandartDto.TicketStateFlowDto>>> GetAllTicketAsync();
    }
}
