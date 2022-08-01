using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.BaseModel;
using System;
using System.Collections.Generic;
using DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFM.SqlServer.Development.Models;


#nullable disable

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models
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
