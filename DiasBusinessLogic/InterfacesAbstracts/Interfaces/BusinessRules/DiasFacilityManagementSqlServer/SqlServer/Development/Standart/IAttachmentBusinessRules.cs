using DiasShared.Errors;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using StandartDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;


namespace DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.DiasFacilityManagementSqlServer.SqlServer.Development.Standart
{
    public interface IAttachmentBusinessRules
    {
        public Task<Tuple<Error, List<StandartDto.AttachmentDto>>> GetAttachmentsByTicketId(int ticketId);
        public Task<Tuple<Error, StandartDto.AttachmentDto>> AddAsync(StandartDto.AttachmentDto attachmentDto);
        public Task<Tuple<Error, StandartDto.AttachmentDto>> UpdateAsync(StandartDto.AttachmentDto attachmentDto);
        public Task<Tuple<Error, StandartDto.AttachmentDto>> DeleteAsync(StandartDto.AttachmentDto attachmentDto);
        public Task<Tuple<Error, int>> GetAttachmentsCountByTicketId(int ticketId);
    }
}
