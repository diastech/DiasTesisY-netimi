using System;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard
{
    public class ViewReasonCategoryDto
    {
        public int Id { get; set; }
        public string ReasonCategoryName { get; set; }
        public string ReasonCategoryDescription { get; set; }
        public string ReasonCategoryHierarchy { get; set; }
        public string ReasonCategory { get; set; }
        public string ReasonCategoryParentId { get; set; }
        public bool? IsDisabled { get; set; }
    }
}
