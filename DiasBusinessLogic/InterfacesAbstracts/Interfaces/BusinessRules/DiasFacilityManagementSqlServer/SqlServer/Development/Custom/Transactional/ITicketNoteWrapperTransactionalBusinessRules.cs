using DiasShared.Errors;
using DiasShared.Services.Communication.BusinessLogicMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;

namespace DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Custom
{
    public interface ITicketNoteWrapperTransactionalBusinessRules
    {
        public Task<Tuple<Error, CustomDto.CustomTicketNoteDto>> AddTicketNoteWrapperAsync(BusinessLogicRequest request);
        public Task<Tuple<Error, CustomDto.CustomTicketNoteDto>> DeleteTicketNoteWrapperAsync(BusinessLogicRequest request);
    }
}
