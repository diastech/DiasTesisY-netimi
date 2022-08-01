using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.BaseDto;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard
{
    public class UserMenuPageDto : BaseDevelopmentStandartDto
    {
        public int Id { get; set; }
        public string ApplicationPageId { get; set; }
        public string ApplicationUserId { get; set; }
    }
}
