using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.BaseDto;
using System;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard
{
    public class TicketStateTransitionDto : BaseDevelopmentStandartDto
    {
        public int Id { get; set; }
        public int SourceTicketStateId { get; set; }
        public int DestinationTicketStateId { get; set; }

        public TicketStateDto DestinationTicketState { get; set; }
        public TicketStateDto SourceTicketState { get; set; }
    }
}
