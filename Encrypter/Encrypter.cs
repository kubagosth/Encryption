namespace Encrypter
{
    public class Encrypter
    {
        public string Encrypt(string message)
        {
            string output = string.Empty;

            for (int i = 0; i < message.Length; i++)
            {
                char c = message[i];
                c++;
                output += c;
            }
            return output;
        }

        public string Decrypt(string message)
        {
            string output = string.Empty;

            for (int i = 0; i < message.Length; i++)
            {
                char c = message[i];
                c--;
                output += c;
            }
            return output;
        }

    }
}
