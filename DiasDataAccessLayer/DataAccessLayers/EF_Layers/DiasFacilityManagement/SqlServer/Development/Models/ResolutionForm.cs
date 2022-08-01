using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.BaseModel;
using System;
using System.Collections.Generic;
using DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFM.SqlServer.Development.Models;


#nullable disable

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models
{
    public partial class ResolutionForm : DevelopmentBaseEntity
    {
        public ResolutionForm()
        {
            ResolutionFormMultipleChoices = new HashSet<ResolutionFormMultipleChoice>();
            ResolutionFormQuestionAnswers = new HashSet<ResolutionFormQuestionAnswer>();
            ResolutionFormYesNos = new HashSet<ResolutionFormYesNo>();
        }

        public int Id { get; set; }
        public int? TicketReasonId { get; set; }
        public int? TicketReasonCategoryId { get; set; }
        public string FormDescription { get; set; }
        public int TicketStateId { get; set; }
        public bool? IsMandatory { get; set; }

        public virtual User AddedByUser { get; set; }
        public virtual User LastModifiedByUser { get; set; }
        public virtual TicketReason TicketReason { get; set; }
        public virtual TicketReasonCategoryV2 TicketReasonCategory { get; set; }
        public virtual TicketState TicketState { get; set; }
        public virtual ICollection<ResolutionFormMultipleChoice> ResolutionFormMultipleChoices { get; set; }
        public virtual ICollection<ResolutionFormQuestionAnswer> ResolutionFormQuestionAnswers { get; set; }
        public virtual ICollection<ResolutionFormYesNo> ResolutionFormYesNos { get; set; }
    }
}
