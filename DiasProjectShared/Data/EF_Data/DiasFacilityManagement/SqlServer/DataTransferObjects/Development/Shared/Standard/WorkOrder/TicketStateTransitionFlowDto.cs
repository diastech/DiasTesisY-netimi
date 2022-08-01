using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.BaseDto;
using System.Collections.Generic;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard
{
    public class TicketStateTransitionFlowDto : BaseDevelopmentStandartDto
    {
        public int Id { get; set; }

        public int TicketStateTransitionId { get; set; }

        public int TicketStateFlowId { get; set; }
        public TicketStateTransitionDto TicketStateTransitionByTicketStateTransitionFlow { get; set; }
    }
}
