using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
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
    public interface ITicketWrapperTransactionalBusinessRules
    {
        public Task<Tuple<Error, CustomDto.CustomTicketDto>> AddTicketWrapperAsync(BusinessLogicRequest request);
        public Task<Tuple<Error, CustomDto.CustomTicketDto>> AddTicketWithFastTicketWrapperAsync(CustomDto.CustomTicketDto customTicketDto);
        public Task<Tuple<Error, CustomDto.CustomTicketDto>> UpdateTicketWrapperAsync(BusinessLogicRequest request);
        public Task<Tuple<Error, CustomDto.CustomTicketDto>> UpdateTicketStateWrapperAsync(BusinessLogicRequest request);

        public Task<Tuple<Error, CustomDto.CustomTicketDto>> DeleteTicketWrapperAsync(BusinessLogicRequest request);
        public Task<Tuple<Error, CustomMobileTicketDto>> AddTicketWrapperMobileAsync(BusinessLogicRequest request);
    }
}
