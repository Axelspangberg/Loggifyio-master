using System.Security.Cryptography;

namespace Loggifyio.Queries.Processors
{
    public static class RsaKeyHelper
    {
        
        public static RSAParameters GenerateKey()
        {
            using (var key = new RSACryptoServiceProvider(2048))
            {
                return key.ExportParameters(true);
            }
        }
    }
}