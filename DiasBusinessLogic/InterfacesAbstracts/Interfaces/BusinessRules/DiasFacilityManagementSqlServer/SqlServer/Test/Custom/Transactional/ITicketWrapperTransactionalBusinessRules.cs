using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Custom;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard;
using DiasShared.Services.Communication.BusinessLogicMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using CustomDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Custom;
namespace DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Test.Custom
{
    public interface ITicketWrapperTransactionalBusinessRules
    {
        public Task<Tuple<ErrorCodes, CustomDto.CustomTicketDto>> AddTicketWrapperAsync(BusinessLogicRequest request);
        public Task<Tuple<ErrorCodes, CustomDto.CustomTicketDto>> AddTicketWithFastTicketWrapperAsync(CustomDto.CustomTicketDto customTicketDto);
        public Task<Tuple<ErrorCodes, CustomDto.CustomTicketDto>> UpdateTicketWrapperAsync(BusinessLogicRequest request);
        public Task<Tuple<ErrorCodes, CustomDto.CustomTicketDto>> UpdateTicketStateWrapperAsync(BusinessLogicRequest request);
        public Task<Tuple<ErrorCodes, CustomDto.CustomTicketDto>> DeleteTicketWrapperAsync(BusinessLogicRequest request);
        public Task<Tuple<ErrorCodes, CustomMobileTicketDto>> AddTicketWrapperMobileAsync(BusinessLogicRequest request);
    }
}
