using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.BaseDto;
using System;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard
{
    public class UserDto : BaseDevelopmentStandartDto
    {
        #region Database Properties

        public int Id { get; set; }
        public string FirstMiddleName { get; set; }
        public string LastName { get; set; }
        public string MobilePhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public int WorkShiftId { get; set; }
        public string UserPassword { get; set; }
        public string UserName { get; set; }
        public DateTime AccountLockTime { get; set; }
        public byte AccountLockout { get; set; }
        public int AccessFailedCount { get; set; }

        #endregion Database Properties


        #region Out Of Database

        #endregion Out Of Database

    }
}
