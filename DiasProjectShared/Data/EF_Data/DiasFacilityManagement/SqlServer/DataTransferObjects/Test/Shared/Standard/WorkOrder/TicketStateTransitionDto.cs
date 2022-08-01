using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.BaseDto;
using System;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard
{
    public class TicketStateTransitionDto : BaseDevelopmentStandartDto
    {
        public int Id { get; set; }
        public int SourceTicketStateId { get; set; }
        public int DestinationTicketStateId { get; set; }
    }
}
