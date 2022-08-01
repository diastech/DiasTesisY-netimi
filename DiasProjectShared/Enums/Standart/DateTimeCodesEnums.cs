using System.ComponentModel;

namespace DiasShared.Enums.Standart
{
    /// <summary>
    /// Sadece invariant culturede olan veya sabit formatları içerir 
    /// </summary>
    public class DateTimeCodesEnums
    {
        /// <summary>
        /// Sadece Date türleri veya Entityleri için kullanılmalıdır
        /// Ay : MM -> 01-12; MMMM -> Eylül, Ekim vs (Turkish culture de)
        /// Gün : dd -> 01-31; dddd -> Pazartesi , Salı vs (Turkish culture de)
        /// Yıl : yyyy -> 2000,2001 vs
        /// Aşağıdakilerin dışında custom bir format kullanılanabilir ama tavsiye edilmez
        /// </summary>
        public enum DateCodes
        {
            [Description("MM/dd/yyyy")]
            d,

            [Description("dddd, dd MMMM yyyy")]
            D,

            [Description("MMMM dd")]
            m,

            [Description("yyyy MMMM")]
            y,

            //Yukarıdakilerin dışında BL'de bir format kullanacaksak
            //Description a formatı belirtmemiz gerek
            [Description("dd'/'MM'/'yyyy")]
            BusinessRuleDateStandardFormat,

            //Yukarıdakilerin dışında Web Client'da bir format kullanacaksak
            //Description a formatı belirtmemiz gerek
            [Description("dd'/'MM'/'yyyy")]
            WebClientDateStandardFormat,
        }


        /// <summary>
        /// Sadece DateTime türleri veya Entityleri için kullanılmalıdır
        /// Ay : MM -> 01-12; MMM -> Eyl, Eki, Ağu vs (Turkish culture de); MMMM -> Eylül, Ekim vs (Turkish culture de)
        /// Gün : dd -> 01-31; ddd -> Paz, Sal, Per vb (Turkish culture de); dddd -> Pazartesi , Salı vs (Turkish culture de)
        /// Yıl : yyyy -> 2000,2001 vs
        /// Saat : HH -> 00-23
        /// Dakika : mm -> 00-59
        /// Saniye : ss -> 00-59
        /// Nanosaniye : fffffff -> 0000000- 9999999
        /// UTC : K -> -11:00 - 11:00
        /// Aşağıdakilerin dışında custom bir format kullanılanabilir ama tavsiye edilmez
        /// </summary>
        public enum DateTimeCodes
        {
            //Tam date/time pattern (uzun zaman)
            [Description("dddd, dd MMMM yyyy HH:mm:ss")]
            F,

            //Round-trip
            [Description("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffffffK")]
            o,

            //RFC1123 
            //Culture bağımsızdır(sabitdir)
            [Description("ddd, dd MMM yyyy HH':'mm':'ss 'GMT'")]
            r,

            //Sıralanabilir date/time patern
            //Culture bağımsızdır(sabitdir)
            [Description("yyyy'-'MM'-'dd'T'HH':'mm':'ss")]
            s,

            //Evrensel sıralanabilir date/time patern 
            //Culture bağımsızdır(sabitdir)
            [Description("yyyy'-'MM'-'dd HH':'mm':'ss'Z'")]
            u,

            //Yukarıdakilerin dışında BL'de bir format kullanacaksak
            //Description a formatı belirtmemiz gerek
            [Description("dd'/'MM'/'yyyy HH':'mm':'ss'")]
            BusinessRuleDateTimeStandardFormat,

            //Yukarıdakilerin dışında Web Client'da bir format kullanacaksak
            //Description a formatı belirtmemiz gerek
            [Description("dd'/'MM'/'yyyy HH':'mm':'ss'")]
            WebClientDateTimeStandardFormat
        }

        /// <summary>
        /// Sadece Time türleri (DateTime'dan ayıklanmışsa olabilir) veya Entityleri için kullanılmalıdır
        /// Saat : HH -> 00-23
        /// Dakika : mm -> 00-59
        /// Saniye : ss -> 00-59
        /// Aşağıdakilerin dışında custom bir format kullanılanabilir ama tavsiye edilmez
        /// </summary>
        public enum TimeCodes
        {
            //Kısa time pattern
            [Description("HH:mm")]
            t,

            //Uzun time pattern
            [Description("HH:mm:ss")]
            T,

            //Yukarıdakilerin dışında BL'de bir format kullanacaksak
            //Description a formatı belirtmemiz gerek
            [Description("HH:mm:ss")]
            BusinessRuleTimeStandardFormat,

            //Yukarıdakilerin dışında BL'de bir format kullanacaksak
            //Description a formatı belirtmemiz gerek
            [Description("HH:mm:ss")]
            WebClientTimeStandardFormat,
        }
    }
}
