using DiasShared.Services.Communication.BusinessLogicMessage;
using DiasShared.Services.Communication.BusinessLogicMessage.ThirdPartyLibrary.DevExpress;
using CustomTestDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using CustomDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using DiasBusinessLogic.Shared.Error;
using DiasShared.Errors;
using DiasShared.Classes.Dto;

namespace DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement
{
    public interface IPeriodicTicketWrapperDtoRepository
    {


        #region Development
        public Task<Tuple<Error, DevExpressLoadResultDto<IEnumerable<CustomDto.CustomPeriodicTicketDto>>>> GetAllPeriodicTicketsWrapperAsync(DevExpressRequest devExpressRequestObj);
        public Task<Tuple<Error, CustomDto.CustomPeriodicTicketDto>> GetPeriodicTicketWrapperByIdAsync(int Id);
        public Task<Tuple<Error, CustomDto.CustomPeriodicTicketDto>> AddPeriodicTicketWrapperAsync(BusinessLogicRequest request);
        public Task<Tuple<Error, CustomDto.CustomPeriodicTicketDto>> UpdatePeriodicTicketWrapperAsync(BusinessLogicRequest request);
        public Task<Tuple<Error, CustomDto.CustomPeriodicTicketDto>> DeletePeriodicTicketWrapperAsync(BusinessLogicRequest request);

        #endregion

        #region Test
        public Task<Tuple<Error, IEnumerable<CustomTestDto.CustomPeriodicTicketDto>>> GetAllPeriodicTicketsWrapperTestAsync(DevExpressRequest devExpressRequestObj);
        public Task<Tuple<Error, CustomTestDto.CustomPeriodicTicketDto>> GetPeriodicTicketWrapperByIdTestAsync(int Id);

        public Task<Tuple<Error, CustomTestDto.CustomPeriodicTicketDto>> AddPeriodicTicketWrapperTestAsync(BusinessLogicRequest request);

        public Task<Tuple<Error, CustomTestDto.CustomPeriodicTicketDto>> UpdatePeriodicTicketWrapperTestAsync(BusinessLogicRequest request);

        public Task<Tuple<Error, CustomTestDto.CustomPeriodicTicketDto>> DeletePeriodicTicketWrapperTestAsync(BusinessLogicRequest request);
        #endregion
    }
}
