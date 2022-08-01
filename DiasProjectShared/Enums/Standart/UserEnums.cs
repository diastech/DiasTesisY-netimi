using System.ComponentModel;

namespace DiasShared.Enums.Standart
{
    public class UserEnums
    {
        public enum UserStatusTypes
        {
            [Description("Aktif")]
            ACTIVE = 1,

            [Description("Pasif")]
            PASSIVE = 0
        }

        //int karşılıkları veritabanı Idleri ile aynı olmalıdır
        //Veritabanı Idleri değişirse, int karşılıkları da değişmelidir
        public enum UserRolesTypes 
        {
            /// <summary>
            /// kullanılmayacaktır, veritabanı bütünlüğü için vardır 
            /// </summary>
            [Description("Default")]
            DEFAULT = 0,

            [Description("Administrator")]
            ADMINISTRATOR = 8,
            [Description("Sponsor")]
            SPONSOR = 9,
            [Description("FacilityManager")]
            FACILITYMANAGER = 10,
            [Description("TeamLeader")]
            TEAMLEADER = 11,
            [Description("TeamMember")]
            TEAMMEMBER = 12,
            [Description("InstanceReportUser")]
            INSTANCEREPORTUSER = 13,
        }

        //int karşılıkları veritabanı Idleri ile aynı olmalıdır
        //Veritabanı Idleri değişirse, int karşılıkları da değişmelidir
        public enum UserIdentityTypes
        {
        }

        //int karşılıkları veritabanı Idleri ile aynı olmalıdır
        //Veritabanı Idleri değişirse, int karşılıkları da değişmelidir
        public enum UserMaritalTypes
        {
            [Description("Belirtilmemiş")]
            NOTSPECIFIED = 1,

            [Description("Bekar")]
            SINGLE = 2,

            [Description("Evli")]
            MARRIED = 3,

            [Description("Dul")]
            WIDOWED = 4,

            [Description("Boşanmak Üzere / Ayrı")]
            ONDIVORCEDORSEPERATED = 5
        }

    }
}
