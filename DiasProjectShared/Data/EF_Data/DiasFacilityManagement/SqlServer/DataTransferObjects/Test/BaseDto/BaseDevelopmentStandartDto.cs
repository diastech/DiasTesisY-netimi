using System;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.BaseDto
{
    public class BaseDevelopmentStandartDto
    {
        public int AddedByUserId { get; set; }
        public DateTime AddedTime { get; set; }
        public int? LastModifiedByUserId { get; set; }
        public DateTime? LastModifiedTime { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }

    }
}
