using System.IO;
using System.Security.Cryptography;

namespace SymmetricEncryption
{
    public abstract class SymmetricCryptography
    {
        public abstract byte[] Encrypt(byte[] dataToEncrypt, byte[] key, byte[] iv);
        public abstract byte[] Decrypt(byte[] dataToDecrypt, byte[] key, byte[] iv);

        public byte[] Encrypt(SymmetricAlgorithm symmetricAlgorithm, byte[] dataToEncrypt, byte[] key, byte[] iv)
        {
            symmetricAlgorithm.Mode = CipherMode.ECB;
            symmetricAlgorithm.Padding = PaddingMode.PKCS7;

            symmetricAlgorithm.Key = key;
            symmetricAlgorithm.IV = iv;

            using (var memoryStream = new MemoryStream())
            {
                var cryptoStream = new CryptoStream(memoryStream, symmetricAlgorithm.CreateEncryptor(), CryptoStreamMode.Write);

                cryptoStream.Write(dataToEncrypt, 0, dataToEncrypt.Length);
                cryptoStream.FlushFinalBlock();

                return memoryStream.ToArray();
            }
        }

        public byte[] Decrypt(SymmetricAlgorithm symmetricAlgorithm, byte[] dataToDecrypt, byte[] key, byte[] iv)
        {
            symmetricAlgorithm.Mode = CipherMode.ECB;
            symmetricAlgorithm.Padding = PaddingMode.PKCS7;

            symmetricAlgorithm.Key = key;
            symmetricAlgorithm.IV = iv;

            using (var memoryStream = new MemoryStream())
            {
                var cryptoStream = new CryptoStream(memoryStream, symmetricAlgorithm.CreateDecryptor(), CryptoStreamMode.Write);

                cryptoStream.Write(dataToDecrypt, 0, dataToDecrypt.Length);
                cryptoStream.FlushFinalBlock();

                var decryptBytes = memoryStream.ToArray();

                return decryptBytes;
            }
        }
    }
}
