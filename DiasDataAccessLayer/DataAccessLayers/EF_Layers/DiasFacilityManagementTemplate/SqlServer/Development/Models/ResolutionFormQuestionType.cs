using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagementTemplate.SqlServer.Development.BaseModel;
using DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFMTemplate.SqlServer.Development.Models;
using System;
using System.Collections.Generic;

#nullable disable

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagementTemplate.SqlServer.Development.Models
{
    public partial class ResolutionFormQuestionType : DevelopmentBaseEntity
    {
        public ResolutionFormQuestionType()
        {
            ResolutionFormQuestions = new HashSet<ResolutionFormQuestion>();
        }

        public int Id { get; set; }
        public string QuestionType { get; set; }

        public virtual User AddedByUser { get; set; }
        public virtual User LastModifiedByUser { get; set; }
        public virtual ICollection<ResolutionFormQuestion> ResolutionFormQuestions { get; set; }
    }
}
