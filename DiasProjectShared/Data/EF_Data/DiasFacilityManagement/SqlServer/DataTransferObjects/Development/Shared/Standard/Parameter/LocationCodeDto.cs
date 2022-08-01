using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.BaseDto;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard
{
    public class LocationCodeDto : BaseDevelopmentStandartDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string NormalizedName { get; set; }
    }
}
