using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.BaseDto;
using System;


namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard
{
    public class ResolutionFormV2Dto : BaseDevelopmentStandartDto
    {
        public int Id { get; set; }
        public int? TicketReasonId { get; set; }
        public int? TicketReasonCategoryId { get; set; }
        public int TicketStateId { get; set; }
        public string FormXml { get; set; }
        public bool? IsMandatory { get; set; }
    }
}
