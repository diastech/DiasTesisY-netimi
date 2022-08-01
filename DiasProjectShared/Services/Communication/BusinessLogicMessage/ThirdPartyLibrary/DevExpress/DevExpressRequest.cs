
using DevExtreme.AspNet.Data;
using DiasShared.Data.JsonData.InputOutput.Filters.Web.DevExpress.Development;
using Newtonsoft.Json;

namespace DiasShared.Services.Communication.BusinessLogicMessage.ThirdPartyLibrary.DevExpress
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class DevExpressRequest
    {
        //multi filtrelemede RequestOptions ile birlikte kullanılmalıdır!
        //Devexpress has ek bir obje göndermek istiyorsak 
        //BusinessLogicRequest deki AdditionalParamJson ile karıştırılmamalıdır,
        //bu DevExpress e has ek bir obje olmalıdır
        //serialize edilmiş Json
        public string AdditionalDevExpressParamJson { get; set; }

        //multi filtrelemede AdditionalDevExpressParamJson ile birlikte kullanılmalıdır!
        //İçinde paging, filter vb. şeyleri barındıran Devexpress load options
        //Initialize edilirken kendimiz custom üretmeyeceksek
        //muhakkak Devexpress den gelen DataSourceLoadOptions nesnesinden deep copy yapılmalıdır, shallow copy değil        

        public CustomDataSourceLoadOptionDto RequestOptions { get; set; }
    }
}
