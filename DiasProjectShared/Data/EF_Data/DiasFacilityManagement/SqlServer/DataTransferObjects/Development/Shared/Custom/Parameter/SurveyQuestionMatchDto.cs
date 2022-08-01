using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.BaseDto;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom
{
    public class SurveyQuestionMatchDto : BaseDevelopmentStandartDto
    {
        public int SurveyQuestionId { get; set; }
        public int TicketStatusId { get; set; }
        public int TicketReasonId { get; set; }
        public List<int> TicketReasonIds { get; set; }
        public SurveyQuestionDto SurveyQuestion { get; set; }
        public TicketStateDto TicketState { get; set; }        
        public TicketReasonDto TicketReason { get; set; }
        public UserDto AddedByUser { get; set; }
    }
}
