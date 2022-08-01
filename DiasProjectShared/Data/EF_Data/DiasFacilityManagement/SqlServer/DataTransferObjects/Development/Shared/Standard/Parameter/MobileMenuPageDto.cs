using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.BaseDto;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard
{ 
    public class MobileMenuPageDto : BaseDevelopmentStandartDto
    {
        public int Id { get; set; }

        public int HierarchicalOrder { get; set; }

        public int HierarchicalLevel { get; set; }

        public int? ParentId { get; set; }

        public string MenuText { get; set; }

        public string MenuIcon { get; set; }

        public bool ExpandOnStart { get; set; }

        public string MenuImagePath { get; set; }

        public long AuthorizationCodeLevel { get; set; }

        public long AuthorizationCode { get; set; }
    }
}
