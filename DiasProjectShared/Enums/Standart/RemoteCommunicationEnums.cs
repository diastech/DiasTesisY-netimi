using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiasShared.Enums.Standart
{
    /// <summary>
    /// Buradaki enumlar veritabanındaki RestClientType tablosu ile birebir aynıdır
    /// Enum nameleri tablodaki nameler ile eşlenik
    /// Enum tamsayıları tablo idleri ile eşleniktir
    /// </summary>
    public class RemoteCommunicationEnums
    {
        //Projenin remote olarak haberleşeceği(incoming request) domain kodları burada tutulur
        //Buraya eklenen domainler muhakkak web api konfigurasyon dosyasına 
        //bağlantı bilgileri ile birlikte  Incoming  kısmına
        //environmenta bağlı olarak eklenmelidir
        //Bu projeye gelen domainler
        public enum RemoteIncomingDomains
        {
            [Description("Dias Tesis Yönetimi Web İstemci")]
            DiasTesisYonetimWebClient = 1,

            [Description("Dias Tesis Yönetimi Mobil İstemci")]
            DiasTesisYonetimMobileClient = 2
        }

        //Web apinin gittiği(istekte bulunduğu) domainler
        //Buraya eklenen domainler muhakkak web api konfigurasyon dosyasına 
        //bağlantı bilgileri ile birlikte Outgoing kısmına
        //environmenta bağlı olarak eklenmelidir
        public enum RemoteOutgoingDomains
        {
            [Description("MFiles")]
            M_Files = 3
        }
    }
}
