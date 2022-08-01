using DiasShared.Errors;
using DiasShared.Services.Communication.BusinessLogicMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using CustomDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom;

namespace DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement
{
    public interface IAttachmentWrapperDtoRepository
    {
        public Task<Tuple<Error, IEnumerable<CustomDto.CustomAttachmentDto>>> GetAttachmentsByTicketIdAsync(int ticketId);
        public Task<Tuple<Error, CustomDto.CustomAttachmentDto>> AddAttachmentWrapperAsync(BusinessLogicRequest request);
    }
}
