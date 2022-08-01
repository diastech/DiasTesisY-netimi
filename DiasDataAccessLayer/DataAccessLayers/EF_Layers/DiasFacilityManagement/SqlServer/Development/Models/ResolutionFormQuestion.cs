using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.BaseModel;
using System;
using System.Collections.Generic;
using DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFM.SqlServer.Development.Models;


#nullable disable

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models
{
    public partial class ResolutionFormQuestion : DevelopmentBaseEntity
    {
        public ResolutionFormQuestion()
        {
            ResolutionFormAnswers = new HashSet<ResolutionFormAnswer>();
        }

        public int Id { get; set; }
        public string QuestionText { get; set; }
        public int ResolutionFormQuestionTypeId { get; set; }

        public virtual User AddedByUser { get; set; }
        public virtual User LastModifiedByUser { get; set; }
        public virtual ResolutionFormQuestionType ResolutionFormQuestionType { get; set; }
        public virtual ICollection<ResolutionFormAnswer> ResolutionFormAnswers { get; set; }
    }
}
