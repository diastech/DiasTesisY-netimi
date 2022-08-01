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
    public interface IBasicTicketWrapperTransactionalBusinessRules
    {
        public Task<Tuple<Error, CustomDto.CustomBasicTicketDto>> AddBasicTicketWrapperAsync(BusinessLogicRequest request);
        public Task<Tuple<Error, CustomDto.CustomBasicTicketDto>> UpdateBasicTicketWrapperAsync(CustomDto.CustomBasicTicketDto customBasicTicketDto);
        public Task<Tuple<Error, CustomDto.CustomBasicTicketDto>> DeleteBasicTicketWrapperAsync(CustomDto.CustomBasicTicketDto customBasicTicketDto);
    }
}
