using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.BaseModel;
using System;
using DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFM.SqlServer.Development.Models;


#nullable disable

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models
{
    public partial class Attachment : DevelopmentBaseEntity
    {
        #region WebMobile
        public int Id { get; set; }
        public int? TicketId { get; set; }
        public string AttachmentDescription { get; set; }
        public string FolderName { get; set; }
        public int? TicketNoteId { get; set; }
        public int? BasicTicketId { get; set; }
        #endregion WebMobile

        #region OnlyMobile
        public string FileType { get; set; }

        public byte[] FileData { get; set; }
        #endregion OnlyMobile


        public virtual User AddedByUser { get; set; }
        public virtual BasicTicket BasicTicket { get; set; }
        public virtual User LastModifiedByUser { get; set; }
        public virtual Ticket Ticket { get; set; }
        public virtual TicketNote TicketNote { get; set; }
    }
}
