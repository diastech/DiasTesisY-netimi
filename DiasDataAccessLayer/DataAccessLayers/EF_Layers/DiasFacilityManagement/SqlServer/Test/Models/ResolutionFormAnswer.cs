using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Test.BaseModel;
using System;

#nullable disable

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Test.Models
{
    public partial class ResolutionFormAnswer : DevelopmentBaseEntity
    {
        public int Id { get; set; }
        public string Answer { get; set; }
        public bool? YesOrNo { get; set; }
        public int ResolutionFormQuestionId { get; set; }
        public int ResolutionFormId { get; set; }

        public virtual User AddedByUser { get; set; }
        public virtual User LastModifiedByUser { get; set; }
        public virtual ResolutionFormV2 ResolutionForm { get; set; }
        public virtual ResolutionFormQuestion ResolutionFormQuestion { get; set; }
    }
}
