using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.BaseDto;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard
{
    public class LocationDto : BaseDevelopmentStandartDto
    {
        public int Id { get; set; }
        public string LocationName { get; set; }
        public string LocationNumber { get; set; }
        public string LocationDescription { get; set; }
        public string LocationHierarchy { get; set; }
        public string HierarchicalParentId { get; set; }

        #region Out Of Database

        /// <summary>
        /// Comes from LatitudeLongitude field of entity
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Comes from LatitudeLongitude field of entity
        /// </summary>
        public double Longitude { get; set; }


        #endregion 
    }
}
