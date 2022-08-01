using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Test.BaseModel;
using System;

#nullable disable

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Test.Models
{
    public partial class ResolutionFormSingleQuestion : DevelopmentBaseEntity
    {
        public int Id { get; set; }
        public int TicketFormId { get; set; }
        public string QuestionText { get; set; }
        public bool? IsMandatory { get; set; }

        public virtual User AddedByUser { get; set; }
        public virtual User LastModifiedByUser { get; set; }
    }
}
