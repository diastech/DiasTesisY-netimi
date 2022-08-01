using DIAS.Application.Wrappers;
using DIAS.Domain.Models;
using DIAS.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DIAS.Application.Interfaces.Repositories
{
    public interface IAttachmentsRepositoryAsync : IGenericRepositoryAsync<Attachments>
    {
        Task<Response<Attachments>> GetAttachmentsByIdAsync(int id);
        Task<Response<IReadOnlyList<Attachments>>> GetAllAttachmentsAsync();
        Task<Response<List<Attachments>>> GetAllTicketAttachmentsByTicketIdAsync(int id);
        Task<Response<List<Attachments>>> GetAllTicketAttachmentsByBasicTicketIdAsync(int id);
        Task<Response<List<Attachments>>> GetAllNoteAttachmentsByNoteIdAsync(int id);
        Task<Response<Attachments>> AddAttachmentsAsync(Attachments model);
        Task<Response<Attachments>> UpdateAttachmentsAsync(Attachments model);
        //Task<Response<string>> DeleteAttachmentsAsync(Attachments model);
    }
}
