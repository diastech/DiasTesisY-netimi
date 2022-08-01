using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Custom
{
    public class CustomUserDto
    {
        public CompanyRoleDto roleDto { get; set; }
        public UserDto userDto { get; set; }
        public CompanyRoleUserDto userRoleDto { get; set; }
    }
}
