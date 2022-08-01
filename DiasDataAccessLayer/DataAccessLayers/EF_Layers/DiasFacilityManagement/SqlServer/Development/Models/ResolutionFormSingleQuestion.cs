using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.BaseModel;
using System;
using DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFM.SqlServer.Development.Models;


#nullable disable

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models
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
