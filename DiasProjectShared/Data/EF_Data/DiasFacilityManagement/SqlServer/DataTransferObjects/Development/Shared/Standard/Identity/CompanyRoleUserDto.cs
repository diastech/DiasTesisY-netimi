using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.BaseDto;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard
{
    public class CompanyRoleUserDto : BaseDevelopmentStandartDto
    {
        public int UserId { get; set; }

        public int RoleId { get; set; }
    }
}
