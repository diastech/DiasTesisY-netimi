using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.BaseDto;
using System;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard
{
    public class UserDto : BaseDevelopmentStandartDto
    {
        //TODO : Bunu int yapalım, referans ettiği yerleri değiştirerek
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }

        //TODO:Client içinde bu dolaşmamalı, araştır
        public string PasswordHash { get; set; }

        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string PhoneNumber { get; set; }
        public string MobilePhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public int WorkShiftId { get; set; }

        #region NonDatabase
        /// <summary>
        /// İş kuralında doldurulmalı
        /// COntrollerdan geri dönüş yapılırken nullanmalıdır
        /// </summary>
        public string JwtToken { get; set; }
        #endregion NonDatabase
    }
}
