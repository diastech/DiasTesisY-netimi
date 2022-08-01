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
    public interface IPeriodicTicketWrapperTransactionalBusinessRules
    {
        public Task<Tuple<Error, CustomDto.CustomPeriodicTicketDto>> AddPeriodicTicketWrapperAsync(BusinessLogicRequest request);
        public Task<Tuple<Error, CustomDto.CustomPeriodicTicketDto>> UpdatePeriodicTicketWrapperAsync(BusinessLogicRequest request);
        public Task<Tuple<Error, CustomDto.CustomPeriodicTicketDto>> DeletePeriodicTicketWrapperAsync(BusinessLogicRequest request);
    }
}
