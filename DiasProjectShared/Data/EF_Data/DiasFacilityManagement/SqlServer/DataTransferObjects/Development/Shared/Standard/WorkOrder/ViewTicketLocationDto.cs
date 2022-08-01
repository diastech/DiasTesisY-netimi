using System;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard
{
    public class ViewTicketLocationDto
    {
        public int TicketId { get; set; }
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

        #endregion Out Of Database

    }
}
