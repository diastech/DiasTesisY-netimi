using DiasShared.Classes.Dto;
using DiasShared.Errors;
using DiasShared.Services.Communication.BusinessLogicMessage;
using DiasShared.Services.Communication.BusinessLogicMessage.ThirdPartyLibrary.DevExpress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using CustomDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;

namespace DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Custom
{
    public interface ITicketWrapperBusinessRules
    {
        public Task<Tuple<Error, DevExpressLoadResultDto<IEnumerable<CustomDto.CustomTicketDto>>>> GetAllTicketsWrapperAsync(DevExpressRequest devExpressRequestObj,string token);
        public Task<Tuple<Error, IEnumerable<CustomDto.CustomTicketDto>>> GetAllTicketsWrapperMobileAsync(DevExpressRequest devExpressRequestObj);
        public Task<Tuple<Error, IEnumerable<CustomDto.CustomTicketDto>>> GetAllTicketWrapperWithLocationFilterNonWebAsync(BusinessLogicRequest request);
        public Task<Tuple<Error, IEnumerable<CustomDto.CustomTicketDto>>> GetAllTicketWrapperWithTowerFilterNonWebAsync(BusinessLogicRequest request);
        public Task<Tuple<Error, CustomDto.CustomTicketDto>> GetTicketWrapperByTicketIdAsync(int Id);
        public Task<Tuple<Error, CustomDto.CustomTicketDto>> GetTicketWrapperWithStatusByTicketIdAsync(int Id);
        public Task<Tuple<Error, IEnumerable<CustomDto.CustomTicketDto>>> GetAllTicketsWrapperByBasicTicketIdAsync(int Id);
    }
}
