using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.BaseDto;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard
{
    public class MenuPageV2Dto : BaseDevelopmentStandartDto
    {
        public int Id { get; set; }
        public string MenuText { get; set; }
        public string UrlPath { get; set; }
        public string MenuIcon { get; set; }
        public bool ExpandOnStart { get; set; }
        public string MenuImagePath { get; set; }

        #region Out Of Database

        /// <summary>
        /// Comes from HierarchyId field of entity
        /// </summary>
        public string HierarchyId { get; set; }

        /// <summary>
        /// Comes from OldHierarchyId field of entity
        /// </summary>
        public string OldHierarchyId { get; set; }

        #endregion 

    }
}
