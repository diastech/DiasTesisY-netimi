using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.BaseDto;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard
{
    public class LocationV2Dto : BaseDevelopmentStandartDto
    {
        public int Id { get; set; }
        public string LocationName { get; set; }
        public string LocationNumber { get; set; }
        public string LocationDescription { get; set; }

        #region Out Of Database

        /// <summary>
        /// Comes from HierarchyId field of entity
        /// </summary>
        public string HierarchyId { get; set; }

        /// <summary>
        /// Comes from OldHierarchyId field of entity
        /// </summary>
        public string OldHierarchyId { get; set; }


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
