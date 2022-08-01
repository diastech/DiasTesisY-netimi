using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.BaseDto;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard
{
    public class UserMenuPageDto : BaseDevelopmentStandartDto
    {
        public int Id { get; set; }
        public string ApplicationPageId { get; set; }
        public string ApplicationUserId { get; set; }
    }
}
