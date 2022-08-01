using DiasShared.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;

namespace DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Custom
{
    public interface IAttachmentWrapperBusinessRules
    {
        public Task<Tuple<Error, IEnumerable<CustomDto.CustomAttachmentDto>>> GetTicketAttachmentsByTicketId(int Id);
    }
}
