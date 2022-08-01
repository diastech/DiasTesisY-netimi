using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DiasDataAccessLayer.Enums.BusinessLogicMessageCodes;
using DevelopmentDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using TestDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard;

namespace DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement
{
    public interface IAttachmentDtoRepository
    {
        #region Development
        public Task<Tuple<ErrorCodes, IEnumerable<DevelopmentDto.AttachmentDto>>> GetAllTicketAttachmentsByTicketIdAsync(int ticketId);
        public Task<Tuple<ErrorCodes, IEnumerable<DevelopmentDto.AttachmentDto>>> GetAllNoteAttachmentsByNoteIdAsync(int ticketNoteId);
        public Task<Tuple<ErrorCodes, IEnumerable<DevelopmentDto.AttachmentDto>>> GetAllTicketAttachmentsByBasicTicketIdAsync(int basicTicketId);

        public Task<Tuple<ErrorCodes, IEnumerable<DevelopmentDto.AttachmentDto>>> GetAllAsync();
        public Task<Tuple<ErrorCodes, DevelopmentDto.AttachmentDto>> DeleteFromIntAsync(int id);
        public Task<Tuple<ErrorCodes, DevelopmentDto.AttachmentDto>> GetByIdFromIntAsync(int id);
        public Task<Tuple<ErrorCodes, DevelopmentDto.AttachmentDto>> InsertAsync(DevelopmentDto.AttachmentDto insertedDto);
        public Task<Tuple<ErrorCodes, DevelopmentDto.AttachmentDto>> UpdateAsync(DevelopmentDto.AttachmentDto updatedDto);
        #endregion Development

        #region Test
        public Task<Tuple<ErrorCodes, IEnumerable<TestDto.AttachmentDto>>> GetAllTestAsync();
        public Task<Tuple<ErrorCodes, TestDto.AttachmentDto>> DeleteFromIntTestAsync(int id);
        public Task<Tuple<ErrorCodes, TestDto.AttachmentDto>> GetByIdFromIntTestAsync(int id);
        public Task<Tuple<ErrorCodes, TestDto.AttachmentDto>> InsertTestAsync(TestDto.AttachmentDto insertedDto);
        public Task<Tuple<ErrorCodes, TestDto.AttachmentDto>> UpdateTestAsync(TestDto.AttachmentDto updatedDto);

        public Task<Tuple<ErrorCodes, IEnumerable<TestDto.AttachmentDto>>> GetAllTicketAttachmentsByTicketIdTestAsync(int ticketId);
        public Task<Tuple<ErrorCodes, IEnumerable<TestDto.AttachmentDto>>> GetAllNoteAttachmentsByNoteIdTestAsync(int ticketNoteId);
        public Task<Tuple<ErrorCodes, IEnumerable<TestDto.AttachmentDto>>> GetAllTicketAttachmentsByBasicTicketIdTestAsync(int basicTicketId);
        #endregion Test

    }
}
