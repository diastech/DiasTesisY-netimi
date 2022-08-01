using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.BaseDto;
using System.Collections.Generic;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard
{
    public class TicketStateDto : BaseDevelopmentStandartDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string NormalizedName { get; set; }

        #region OutOfDatabase
        public string FirstLastName { get; set; }

        public  UserDto AddedByUser { get; set; }

        public List<TicketStateTransitionDto> TicketStateTransitionDestinationTicketStates { get; set; }

        public List<TicketStateTransitionDto> TicketStateTransitionSourceTicketStates { get; set; }
        #endregion OutOfDatabase
    }
}
