using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using CustomTestDto =  DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Custom;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using DiasShared.Services.Communication.BusinessLogicMessage;
using DiasShared.Services.Communication.BusinessLogicMessage.ThirdPartyLibrary.DevExpress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using CustomDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using DiasShared.Errors;

namespace DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement
{
    public interface ITicketWrapperDtoRepository
    {
        #region Development
        public Task<Tuple<Error, IEnumerable<CustomDto.CustomTicketDto>>> GetAllTicketWrapperAsync(DevExpressRequest devExpressRequestObj);
        public Task<Tuple<Error, IEnumerable<CustomTicketDto>>> GetAllTicketWrapperMobileAsync(DevExpressRequest devExpressRequestObj);
        public Task<Tuple<Error, CustomDto.CustomTicketDto>> GetTicketWrapperByIdAsync(int Id);
        public Task<Tuple<Error, IEnumerable<CustomDto.CustomTicketDto>>> GetAllTicketsWrapperByBasicTicketIdAsync(int Id);
        public Task<Tuple<Error, CustomDto.CustomTicketDto>> AddTicketWrapperAsync(BusinessLogicRequest request);
        public Task<Tuple<Error, CustomDto.CustomTicketDto>> AddTicketWithFastTicketWrapperAsync(CustomDto.CustomTicketDto customTicketDto);
        public Task<Tuple<Error, CustomDto.CustomTicketDto>> UpdateTicketWrapperAsync(BusinessLogicRequest request);
        public Task<Tuple<Error, CustomDto.CustomTicketDto>> DeleteTicketWrapperAsync(BusinessLogicRequest request);
        public Task<Tuple<Error, CustomMobileTicketDto>> AddTicketWrapperMobileAsync(BusinessLogicRequest request);
        public Task<Tuple<Error, CustomTicketDto>> UpdateTicketStateWrapperAsync(BusinessLogicRequest request);

        #endregion Development

        #region Test
        public Task<Tuple<ErrorCodes, IEnumerable<CustomTestDto.CustomTicketDto>>> GetAllTicketWrapperTestAsync(DevExpressRequest devExpressRequestObj);
        public Task<Tuple<ErrorCodes, CustomTestDto.CustomTicketDto>> GetTicketWrapperByIdTestAsync(int Id);
        public Task<Tuple<ErrorCodes, CustomTestDto.CustomTicketDto>> AddTicketWrapperTestAsync(BusinessLogicRequest request);
        public Task<Tuple<ErrorCodes, CustomTestDto.CustomMobileTicketDto>> AddTicketWrapperMobileTestAsync(BusinessLogicRequest request);
        public Task<Tuple<ErrorCodes, CustomTestDto.CustomTicketDto>> UpdateTicketWrapperTestAsync(BusinessLogicRequest request);
        public Task<Tuple<ErrorCodes, IEnumerable<CustomTestDto.CustomTicketDto>>> GetAllTicketsWrapperByBasicTicketIdTestAsync(int Id);
        public Task<Tuple<ErrorCodes, CustomTestDto.CustomTicketDto>> UpdateTicketStateWrapperTestAsync(BusinessLogicRequest request);

        #endregion Test
    }
}
