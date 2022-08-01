using DiasShared.Errors;
using DiasShared.Services.Communication.BusinessLogicMessage;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using CustomDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using CustomTestDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Custom;
namespace DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement
{
    public interface ITicketReasonCategoryWrapperDtoRepository
    {
        #region Development
        public Task<Tuple<Error, IEnumerable<CustomDto.CustomTicketReasonCategoryDto>>> GetAllTicketReasonCategoriesWrapperAsync();
        public Task<Tuple<Error, CustomDto.CustomTicketReasonCategoryDto>> GetTicketReasonCategoryWrapperByIdAsync(string hierarchyId);
        public Task<Tuple<Error, IEnumerable<CustomDto.CustomTicketReasonCategoryDto>>> GetTicketReasonCategoryWrapperLastNodeByIdAsync(string hierarchyId);
        public Task<Tuple<Error, CustomDto.CustomTicketReasonCategoryDto>> InsertV2Async(BusinessLogicRequest request);
        #endregion

        #region Test
        public Task<Tuple<ErrorCodes, IEnumerable<CustomTestDto.CustomTicketReasonCategoryDto>>> GetAllTicketReasonCategoriesWrapperTestAsync();
        public Task<Tuple<ErrorCodes, CustomTestDto.CustomTicketReasonCategoryDto>> GetTicketReasonCategoryWrapperByIdTestAsync(string hierarchyId);
        #endregion

    }
}
