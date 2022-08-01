using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.BaseDto;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard
{
    public class MenuPageDto : BaseDevelopmentStandartDto
    {
        public string Id { get; set; }
        public int HierarchicalOrder { get; set; }
        public int HierarchicalLevel { get; set; }
        public string ParentId { get; set; }
        public string MenuText { get; set; }
        public string UrlPath { get; set; }
        public string MenuIcon { get; set; }
        public bool ExpandOnStart { get; set; }
        public string MenuImagePath { get; set; }
    }
}
