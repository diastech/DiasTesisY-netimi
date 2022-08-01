using System.ComponentModel;

namespace DiasWebApi.Shared.Enums
{
    /// <summary>
    /// Web Api ye özel environmental enumlardır
    /// Enum açıklamaları appSettingsPlatformDependent.json config dosyasında yer almaktadır
    /// Shared projedeki ApplicationEnums ile karıştırılmamalıdır
    /// </summary>
    public class WebApiApplicationEnums
    {
        public enum ApplicationWorkingEnvironment
        {
            //Test sunucusu, lokal
            [Description("Development")]
            Development,

            //Test sunucusu
            [Description("Test")]
            Test,

            //Canlı sunucu
            [Description("Live")]
            Live

        }

        public enum ApplicationBusinessLogicEnvironment
        {
            [Description("Development")]
            Development,

            [Description("Test")]
            Test,

            [Description("Live")]
            Live
        }      

         public enum ApplicationRedisEnvironment
        {
            [Description("Development")]
            Development,

            [Description("Test")]
            Test,

            [Description("Live")]
            Live
        }       
    }
}
