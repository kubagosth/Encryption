using System.IO;
using System.Security.Cryptography;

namespace SymmetricEncryption
{
    public class AES : SymmetricCryptography
    {
        public override byte[] Decrypt(byte[] dataToDecrypt, byte[] key, byte[] iv)
        {
            using (SymmetricAlgorithm aes = new AesCryptoServiceProvider())
            {
                return Decrypt(aes, dataToDecrypt, key, iv);    
            }
        }

        public override byte[] Encrypt(byte[] dataToEncrypt, byte[] key, byte[] iv)
        {
            using (var aes = new AesCryptoServiceProvider())
            {
                return Encrypt(aes, dataToEncrypt, key, iv);
            }
        }
    }
}
