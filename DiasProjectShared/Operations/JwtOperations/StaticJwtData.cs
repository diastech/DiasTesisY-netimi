using System.Text;

namespace DiasShared.Operations.JwtOperations
{
    public static class StaticJwtData
    {
        private static string JwtSecretKey = "ambybtN+1t3ahS1HGVFLIZu38zfPIxUebaCu91q66Q6hdqOEMaqeTIFXg+9FSPDgu9tULJtkJfnxJqd2pTXJVg==";

        public static byte[] GetSecretKeyByBytes()
        {           
            return (Encoding.ASCII.GetBytes(JwtSecretKey));
        }
    }
}
