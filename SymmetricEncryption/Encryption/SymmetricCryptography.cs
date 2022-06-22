namespace SymmetricEncryption
{
    public abstract class SymmetricCryptography
    {
        public abstract byte[] Encrypt(byte[] dataToEncrypt, byte[] key, byte[] iv);
        public abstract byte[] Decrypt(byte[] dataToDecrypt, byte[] key, byte[] iv);
    }
}
