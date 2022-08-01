using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.BaseDto;
using System;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard
{
    public class TicketReasonCategoryV2Dto : BaseDevelopmentStandartDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }

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
