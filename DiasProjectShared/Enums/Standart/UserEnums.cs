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
            [Description("Admin")]
            ADMIN = 1,

            [Description("Kullanıcı")]
            USER = 2
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
