using DiasShared.Errors;
using DiasShared.Services.Communication.BusinessLogicMessage;
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
        public Task<Tuple<Error, IEnumerable<CustomDto.CustomTicketReasonCategoryDto>>> GetTicketReasonCategoryWrapperLastNodeByIdAsync(string hierarchyId);
        public Task<Tuple<Error, CustomDto.CustomTicketReasonCategoryDto>> InsertTicketReasonOrCategoryAsync(BusinessLogicRequest request);
    }
}
