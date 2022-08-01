using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.BaseModel;
using System;
using System.Collections.Generic;
using DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFM.SqlServer.Development.Models;


#nullable disable

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models
{
    public partial class ResolutionFormV2 : DevelopmentBaseEntity
    {
        public ResolutionFormV2()
        {
            ResolutionFormAnswers = new HashSet<ResolutionFormAnswer>();
        }

        public int Id { get; set; }
        public int? TicketReasonId { get; set; }
        public int? TicketReasonCategoryId { get; set; }
        public int TicketStateId { get; set; }
        public string FormXml { get; set; }
        public bool? IsMandatory { get; set; }

        public virtual User AddedByUser { get; set; }
        public virtual User LastModifiedByUser { get; set; }
        public virtual TicketReason TicketReason { get; set; }
        public virtual TicketReasonCategoryV2 TicketReasonCategory { get; set; }
        public virtual TicketState TicketState { get; set; }
        public virtual ICollection<ResolutionFormAnswer> ResolutionFormAnswers { get; set; }
    }
}
