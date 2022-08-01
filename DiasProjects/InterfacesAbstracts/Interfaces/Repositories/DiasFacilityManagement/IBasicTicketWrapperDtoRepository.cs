using DiasShared.Errors;
using DiasShared.Services.Communication.BusinessLogicMessage;
using DiasShared.Services.Communication.BusinessLogicMessage.ThirdPartyLibrary.DevExpress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using CustomDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using CustomTestDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Custom;
namespace DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement
{
    public interface IBasicTicketWrapperDtoRepository
    {
        #region Development
        public Task<Tuple<Error, IEnumerable<CustomDto.CustomBasicTicketDto>>> GetAllBasicTicketsWrapperAsync(DevExpressRequest devExpressRequestObj);
        public Task<Tuple<Error, CustomDto.CustomBasicTicketDto>> GetBasicTicketWrapperByIdAsync(int Id);
        public Task<Tuple<Error, CustomDto.CustomBasicTicketDto>> AddBasicTicketWrapperAsync(BusinessLogicRequest request);
        public Task<Tuple<Error, CustomDto.CustomBasicTicketDto>> UpdateBasicTicketWrapperAsync(CustomDto.CustomBasicTicketDto customBasicTicketDto);
        public Task<Tuple<Error, CustomDto.CustomBasicTicketDto>> DeleteBasicTicketWrapperAsync(CustomDto.CustomBasicTicketDto customBasicTicketDto);
        #endregion

        #region Test
        public Task<Tuple<ErrorCodes, CustomTestDto.CustomBasicTicketDto>> AddBasicTicketWrappertestAsync(BusinessLogicRequest request);
        public Task<Tuple<ErrorCodes, CustomTestDto.CustomBasicTicketDto>> GetBasicTicketWrapperByIdTestAsync(int Id);

        public Task<Tuple<ErrorCodes, IEnumerable<CustomTestDto.CustomBasicTicketDto>>> GetAllBasicTicketsWrapperTestAsync(DevExpressRequest devExpressRequestObj);
        #endregion


    }
}