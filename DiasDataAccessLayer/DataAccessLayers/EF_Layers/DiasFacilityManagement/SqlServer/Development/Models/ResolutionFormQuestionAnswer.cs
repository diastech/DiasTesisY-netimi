using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.BaseModel;
using System;
using DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFM.SqlServer.Development.Models;


#nullable disable

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models
{
    public partial class ResolutionFormQuestionAnswer : DevelopmentBaseEntity
    {
        public int Id { get; set; }
        public int ResolutionFormId { get; set; }
        public int TicketId { get; set; }
        public string QuestionText { get; set; }
        public string AnswerText { get; set; }

        public virtual User AddedByUser { get; set; }
        public virtual User LastModifiedByUser { get; set; }
        public virtual ResolutionForm ResolutionForm { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}
