using DiasShared.Errors;
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
    public interface IBasicTicketWrapperBusinessRules
    {
        public Task<Tuple<Error, IEnumerable<CustomDto.CustomBasicTicketDto>>> GetAllBasicTicketsWrapperAsync(DevExpressRequest devExpressRequestObj);
        public Task<Tuple<Error, CustomDto.CustomBasicTicketDto>> GetBasicTicketWrapperByIdAsync(int Id);
    }
}
