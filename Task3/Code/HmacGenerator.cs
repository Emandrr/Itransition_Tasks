using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
namespace Task3_DiceGame
{
    public static class HmacGenerator
    {
        public static byte[] RandomHMACKey()
        {
            RandomNumberGenerator Generator = RandomNumberGenerator.Create();
            byte[] randomKey = new byte[32];
            Generator.GetBytes(randomKey);
            return randomKey;
        }
        public static byte[] CreationOfHMACSHA3_256(byte[] HMACkey, int add)
        {
            var hmac = new HMac(new Org.BouncyCastle.Crypto.Digests.Sha256Digest());
            hmac.Init(new KeyParameter(HMACkey));
            byte[] result = new byte[hmac.GetMacSize()];
            byte[] bytesData = BitConverter.GetBytes(add);
            hmac.BlockUpdate(bytesData, 0, bytesData.Length);
            hmac.DoFinal(result, 0);
            return result;
        }
    }
}
