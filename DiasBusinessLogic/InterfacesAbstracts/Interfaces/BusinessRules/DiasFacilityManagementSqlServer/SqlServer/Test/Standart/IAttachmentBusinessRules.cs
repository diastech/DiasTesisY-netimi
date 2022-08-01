using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using StandartDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard;


namespace DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Test.Standart
{
    public interface IAttachmentBusinessRules
    {
        public Task<Tuple<ErrorCodes, StandartDto.AttachmentDto>> AddAsync(StandartDto.AttachmentDto attachmentDto);
        public Task<Tuple<ErrorCodes, StandartDto.AttachmentDto>> UpdateAsync(StandartDto.AttachmentDto attachmentDto);
        public Task<Tuple<ErrorCodes, StandartDto.AttachmentDto>> DeleteAsync(StandartDto.AttachmentDto attachmentDto);
        public  Task<Tuple<ErrorCodes, IEnumerable<StandartDto.AttachmentDto>>> GetAllTicketAttachmentsByTicketIdAsync(int basicTicketId);
    }
}
