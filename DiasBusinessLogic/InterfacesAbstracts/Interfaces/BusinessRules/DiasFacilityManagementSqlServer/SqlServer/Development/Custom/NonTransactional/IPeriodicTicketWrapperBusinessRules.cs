using DiasShared.Services.Communication.BusinessLogicMessage.ThirdPartyLibrary.DevExpress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using CustomDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;
using DiasShared.Errors;
using DiasShared.Classes.Dto;

namespace DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Custom
{
    public interface IPeriodicTicketWrapperBusinessRules
    {
        public Task<Tuple<Error, DevExpressLoadResultDto<IEnumerable<CustomDto.CustomPeriodicTicketDto>>>> GetAllPeriodicTicketsWrapperAsync(DevExpressRequest devExpressRequestObj);
        public Task<Tuple<Error, CustomDto.CustomPeriodicTicketDto>> GetPeriodicTicketWrapperByIdAsync(int Id);
    }
}
