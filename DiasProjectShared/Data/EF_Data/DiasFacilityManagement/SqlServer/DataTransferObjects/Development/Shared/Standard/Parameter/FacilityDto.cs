using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.BaseDto;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard
{
    public class FacilityDto : BaseDevelopmentStandartDto
    {
        public int Id { get; set; }
        public string FacilityCode { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string Address { get; set; }
    }
}
