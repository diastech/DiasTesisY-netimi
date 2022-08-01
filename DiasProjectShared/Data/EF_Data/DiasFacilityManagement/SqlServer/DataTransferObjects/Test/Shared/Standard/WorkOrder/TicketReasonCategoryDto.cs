using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.BaseDto;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard
{
    public class TicketReasonCategoryDto : BaseDevelopmentStandartDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public string CategoryHierarchy { get; set; }
        public string HierarchicalParentId { get; set; }
    }
}
