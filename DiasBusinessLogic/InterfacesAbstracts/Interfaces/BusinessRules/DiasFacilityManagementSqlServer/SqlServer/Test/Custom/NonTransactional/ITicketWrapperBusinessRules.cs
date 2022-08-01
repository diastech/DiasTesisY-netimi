using DiasShared.Services.Communication.BusinessLogicMessage.ThirdPartyLibrary.DevExpress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using CustomDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Custom;

namespace DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Test.Custom
{
    public interface ITicketWrapperBusinessRules
    {
        public Task<Tuple<ErrorCodes, IEnumerable<CustomDto.CustomTicketDto>>> GetAllTicketsWrapperAsync(DevExpressRequest devExpressRequestObj);
        public Task<Tuple<ErrorCodes, CustomDto.CustomTicketDto>> GetTicketWrapperByTicketIdAsync(int Id);
        public Task<Tuple<ErrorCodes, IEnumerable<CustomDto.CustomTicketDto>>> GetAllTicketsWrapperByBasicTicketIdAsync(int Id);
    }
}
