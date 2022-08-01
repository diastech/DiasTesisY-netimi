using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.BaseDto;
using System;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard
{
    public class TicketStateDto : BaseDevelopmentStandartDto
    {
        public int Id { get; set; }
        public string StateDescription { get; set; }
        public string FirstLastName { get; set; }
        public UserDto AddedByUser { get; set; }
    }
}
