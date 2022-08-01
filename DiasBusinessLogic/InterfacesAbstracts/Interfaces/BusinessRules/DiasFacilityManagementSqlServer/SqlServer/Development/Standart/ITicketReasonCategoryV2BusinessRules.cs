using DiasShared.Errors;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using StandartDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;

namespace DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Standart
{
    public interface ITicketReasonCategoryV2BusinessRules
    {
        public Task<Tuple<Error, List<StandartDto.TicketReasonCategoryV2Dto>>> GetAllTicketReasonCategoriesAsync();
        public Task<Tuple<Error, StandartDto.TicketReasonCategoryV2Dto>> GetTicketReasonCategoryByIdAsync(string hierarchyId);
    }
}