using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.BaseDto;
using System;


namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard
{
    public class TicketReasonDto : BaseDevelopmentStandartDto
    {
        public int Id { get; set; }
        public string ReasonName { get; set; }
        public string ReasonDescription { get; set; }
        public int ResponseTime { get; set; }
        public int ResolutionTime { get; set; }
        public int TicketReasonCategoryId { get; set; }
        public int Code { get; set; }

        public TicketReasonCategoryV2Dto TicketReasonCategory { get; set; }
        
    }
}
