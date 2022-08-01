using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard;
using StandartDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard;

namespace DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Test.Standart
{
    public interface ITicketReasonCategoryV2BusinessRules
    {
        public Task<Tuple<ErrorCodes, List<StandartDto.TicketReasonCategoryV2Dto>>> GetAllTicketReasonCategoriesAsync();
        public Task<Tuple<ErrorCodes, StandartDto.TicketReasonCategoryV2Dto>> GetTicketReasonCategoryByIdAsync(string hierarchyId);
    }
}