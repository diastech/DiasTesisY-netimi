using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagementTemplate.SqlServer.Development.BaseModel;
using DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFMTemplate.SqlServer.Development.Models;
using System;
using System.Collections.Generic;

#nullable disable

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagementTemplate.SqlServer.Development.Models
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
