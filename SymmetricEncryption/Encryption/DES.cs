using System.IO;
using System.Security.Cryptography;

namespace SymmetricEncryption
{
    public class DES : SymmetricCryptography
    {
        public override byte[] Decrypt(byte[] dataToDecrypt, byte[] key, byte[] iv)
        {
            using (var des = new DESCryptoServiceProvider())
            {
                return Decrypt(des, dataToDecrypt, key, iv);
            }
        }

        public override byte[] Encrypt(byte[] dataToEncrypt, byte[] key, byte[] iv)
        {
            using (var des = new DESCryptoServiceProvider())
            {
                return Encrypt(des, dataToEncrypt, key, iv);
            }
        }
    }
}
