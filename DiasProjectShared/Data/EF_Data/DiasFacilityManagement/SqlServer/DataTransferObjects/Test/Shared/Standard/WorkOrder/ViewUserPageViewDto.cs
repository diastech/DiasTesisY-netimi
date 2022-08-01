
namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard
{
    public class ViewUserPageViewDto 
    {
        public string UserId { get; set; }
        public string PageId { get; set; }
        public string ParentId { get; set; }
        public int Level { get; set; }
        public int Order { get; set; }
        public string Text { get; set; }
        public string Path { get; set; }
        public string Icon { get; set; }
        public bool Expanded { get; set; }
        public string Image { get; set; }
    }
}
