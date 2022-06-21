
Encrypter.Encrypter encrypter = new Encrypter.Encrypter();

string text = encrypter.Encrypt("Hello world");
Console.WriteLine(text);
text = encrypter.Decrypt(text);
Console.WriteLine(text);

Console.ReadLine();

