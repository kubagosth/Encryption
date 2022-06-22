using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace SymmetricEncryption
{
    public partial class MainWindow : Window
    {
        private SymmetricCryptography symmetricCryptography;
        private SymmetricAlgorithm symmetricAlgorithm;
        private Stopwatch time;
        private string selectedEncryptionType;
        private byte[] keyByte;
        private byte[] ivByte;

        public MainWindow()
        {
            InitializeComponent();
            time = new Stopwatch();
        }

        private void SelectedCryptography_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem)SelectedCryptography.SelectedItem;
            selectedEncryptionType = item.Content.ToString();

            switch (selectedEncryptionType)
            {
                case "DES":
                    symmetricCryptography = new DES();
                    symmetricAlgorithm = new DESCryptoServiceProvider();
                    break;
                case "TripleDES":
                    symmetricCryptography = new TripleDES();
                    symmetricAlgorithm = new TripleDESCryptoServiceProvider();
                    break;
                case "AES":
                    symmetricCryptography = new AES();
                    symmetricAlgorithm = new AesCryptoServiceProvider();
                    break;
            }
        }

        private void Button_GenerateKeyAndIv(object sender, RoutedEventArgs e)
        {
            if (selectedEncryptionType != null)
            {
                symmetricAlgorithm.GenerateKey();
                symmetricAlgorithm.GenerateIV();

                keyByte = symmetricAlgorithm.Key;
                ivByte = symmetricAlgorithm.IV;

                Key.Text = Convert.ToBase64String(keyByte);
                Iv.Text = Convert.ToBase64String(ivByte);
            }
            else
            {
                MessageBox.Show("Select Encryption type from the dropdown menu");
            }
        }

        private void Button_Encrypt(object sender, RoutedEventArgs e)
        {
            byte[] text = Encoding.UTF8.GetBytes(PlainASC.Text);
            byte[] encryptedText;
            PlainHEX.Text = BitConverter.ToString(text);
            try
            {
                if (selectedEncryptionType != null)
                {
                    time.Restart();
                    time.Start();
                    if (selectedEncryptionType == "DES" ||
                        selectedEncryptionType == "TripleDES" ||
                        selectedEncryptionType == "AES")
                    {
                        encryptedText = symmetricCryptography.Encrypt(text, keyByte, ivByte);
                        CipherASC.Text = Convert.ToBase64String(encryptedText);
                        CipherHEX.Text = BitConverter.ToString(encryptedText);
                    }
                    time.Stop();
                    EncryptionTime.Content = "Time/message at encryption : " + time.ElapsedMilliseconds + "ms";
                }
                else
                {
                    MessageBox.Show("Select Encryption type from the dropdown menu");
                }
            }
            catch (ArgumentException)
            {
                MessageBox.Show("You have to generate a Valid Key and Iv");
            }
            catch (System.Security.Cryptography.CryptographicException)
            {
                MessageBox.Show("Wrong Key and Iv for this encryption type");
            }

        }

        private void Button_Decrypt(object sender, RoutedEventArgs e)
        {
            byte[] text = Convert.FromBase64String(CipherASC.Text);
            byte[] decryptedText;
            try
            {
                if (selectedEncryptionType != null)
                {
                    time.Restart();
                    time.Start();
                    if (selectedEncryptionType == "DES" ||
                        selectedEncryptionType == "TripleDES" ||
                        selectedEncryptionType == "AES")
                    {
                        decryptedText = symmetricCryptography.Decrypt(text, keyByte, ivByte);
                        PlainASC.Text = Encoding.UTF8.GetString(decryptedText);
                        PlainHEX.Text = BitConverter.ToString(decryptedText);
                    }
                    time.Stop();
                    DecryptionTime.Content = "Time/message at decryption : " + time.ElapsedMilliseconds + "ms";
                }
                else
                {
                    MessageBox.Show("Select Encryption type from the dropdown menu");
                }
            }
            catch (ArgumentException)
            {
                MessageBox.Show("You have to generate a Valid Key and Iv");
            }
            catch (System.Security.Cryptography.CryptographicException)
            {
                MessageBox.Show("Wrong Key and Iv for this decryption type");
            }
        }
    }
}
