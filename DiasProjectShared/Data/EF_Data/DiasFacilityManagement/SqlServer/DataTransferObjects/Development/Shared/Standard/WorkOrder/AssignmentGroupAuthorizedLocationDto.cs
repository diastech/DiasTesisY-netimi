using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.BaseDto;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard
{
    public class AssignmentGroupAuthorizedLocationDto : BaseDevelopmentStandartDto
    {
        public int Id { get; set; }
        public int AssignmentGroupId { get; set; }
        public int LocationId { get; set; }
    }
}
