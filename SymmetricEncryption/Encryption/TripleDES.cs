using System.IO;
using System.Security.Cryptography;

namespace SymmetricEncryption
{
    public class TripleDES : SymmetricCryptography
    {
        public override byte[] Decrypt(byte[] dataToDecrypt, byte[] key, byte[] iv)
        {
            using (var tripleDes = new TripleDESCryptoServiceProvider())
            {
                return Decrypt(tripleDes, dataToDecrypt,key,iv);
            }
        }

        public override byte[] Encrypt(byte[] dataToEncrypt, byte[] key, byte[] iv)
        {
            using (var tripleDes = new TripleDESCryptoServiceProvider())
            {
                return Encrypt(tripleDes, dataToEncrypt,key,iv);
            }
        }
    }
}
