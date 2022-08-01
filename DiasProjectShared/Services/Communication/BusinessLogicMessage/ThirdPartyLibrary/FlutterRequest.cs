using Newtonsoft.Json;

namespace DiasShared.Services.Communication.BusinessLogicMessage.ThirdPartyLibrary
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class FlutterRequest
    {
        //Flutter a has ek bir obje göndermek istiyorsak 
        //BusinessLogicRequest deki AdditionalParamJson ile karıştırılmamalıdır,
        //bu Flutter a has ek bir obje olmalıdır
        //serialize edilmiş Json
        public string AdditionalFlutterParamJson { get; set; }
    }
}
