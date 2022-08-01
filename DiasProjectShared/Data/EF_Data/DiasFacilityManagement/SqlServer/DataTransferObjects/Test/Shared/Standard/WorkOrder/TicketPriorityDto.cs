using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.BaseDto;
using System;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard
{
    public class TicketPriorityDto : BaseDevelopmentStandartDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public UserDto AddedByUser { get; set; }
    }
}
