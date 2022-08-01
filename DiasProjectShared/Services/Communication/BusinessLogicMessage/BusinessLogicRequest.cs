using DiasShared.Services.Communication.BusinessLogicMessage.ThirdPartyLibrary;
using DiasShared.Services.Communication.BusinessLogicMessage.ThirdPartyLibrary.DevExpress;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using static DiasShared.Enums.Standart.RemoteCommunicationEnums;

namespace DiasShared.Services.Communication.BusinessLogicMessage
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class BusinessLogicRequest
    {
        #region standart 
        //TODO : İleride bunu konfigürasyona bağlayacağız
        [JsonProperty(Required = Required.Always)]
        public Uri ApiUrl { get; set; }       

        //null olabilir
        //Eklemek istediğimiz Dtoların tipleri
        public List<Type> RequestDtosTypes { get; set; }

        //null olabilir
        //Eklemek istediğimiz Dtoların serialize Jsonları
        public List<string> RequestDtosJsons { get; set; }

        //null olabilir
        //Shared da tanımlanan Dtolar için RequestDtosTypes-RequestDtosJsons ikilisi kullanılmalıdır
        public List<Type> AdditionalParamTypes { get; set; }

        //web apiye göndermek istediğimiz ek obje(ler)in serialize edilmiş Jsonları
        //Bu jsona ait yapı tek başına bir value type(integer, string vs.)
        //veya Shared ta tanımlanmayan Dto değilse
        //RequestDtosTypes-RequestDtosJsons ikilisi kullanılmalıdır
        public List<string> AdditionalParamJsons { get; set; }

        //İstekte bulunan client tipi
        [JsonProperty(Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public RemoteIncomingDomains RequestDomain { get; set; } = RemoteIncomingDomains.DiasTesisYonetimMobileClient;

        //Web api dönen dto'nun jsonu da göndersin mi?
        [JsonProperty(Required = Required.Always)]
        public bool RequestAdditionalJson = true;          

        #endregion standart

        #region 3rdPartyRequests
        //DevExpress e has request kalıbı
        public DevExpressRequest DevExpressRequestObj { get; set; }

        //Flutter a has request kalıbı
        public FlutterRequest FlutterRequestObj { get; set; }
        #endregion 3rdPartyRequests

    }
}
