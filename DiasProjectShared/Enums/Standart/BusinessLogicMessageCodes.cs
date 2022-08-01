using System.ComponentModel;

namespace DiasDataAccessLayer.Enums
{
    public class BusinessLogicMessageCodes
    {
        //TODO: Bu enum projeye uygun olarak tekrar revize edilecektir. Faz 2
        //TODO: Tüm hatalarda şimdilik UnknownError gönderilecek
        //İş kurallarının geri dönüşünde kullanıldığından tüm değerleri negatif olmalıdır
        public enum ErrorCodes
        {
            //Crud operasyonu başarılı ise
            [Description("Success")]
            None = 0,

            [Description("Non Api Error")]
            NonCrudError = -1,

            //TODO: Tüm hatalarda şimdilik bu gönderilecek
            [Description("Unknown Error")]
            UnknownError = -2,

            //TODO: Api validation hatalarında şimdilik hep bu gösterilecek
            [Description("Api Validation Error")]
            RequestBodyMalformedOnUndefinedRequest = -3,

            //Get(Select) Hataları -- -1000...-1999(Mevcut Son Değer -1020)
            // User Tablosu
            [Description("Error On Getting Record From User Table")]
            UnknownErrorOnGettingEntityFromUserTable = -1000,
           

        }
    }
}
