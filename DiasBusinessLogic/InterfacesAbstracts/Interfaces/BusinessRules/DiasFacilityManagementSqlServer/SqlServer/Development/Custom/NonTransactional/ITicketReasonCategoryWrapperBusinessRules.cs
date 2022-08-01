using DiasShared.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using CustomDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;

namespace DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Custom
{
    public interface ITicketReasonCategoryWrapperBusinessRules
    {
        public Task<Tuple<Error, IEnumerable<CustomDto.CustomTicketReasonCategoryDto>>> GetAllTicketReasonCategoriesWrapperAsync();
        public Task<Tuple<Error, CustomDto.CustomTicketReasonCategoryDto>> GetTicketReasonCategoryWrapperByIdAsync(string hierarchyId);
    }
}
