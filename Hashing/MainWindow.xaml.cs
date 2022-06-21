using System;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Hashing
{
    public partial class MainWindow : Window
    {
        private Stopwatch timer;
        private string selectedEncryptionType;
        private string plainText;
        private byte[] key;
        private HMAC hmac;
        private Hash hash;

        public MainWindow()
        {
            InitializeComponent();
            timer = new Stopwatch();
            hmac = new HMAC();
            hash = new Hash();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem)SelectedCryptography.SelectedItem;
            selectedEncryptionType = item.Content.ToString();
        }

        private void Button_ComputeMac(object sender, RoutedEventArgs e)
        {
            timer.Reset();
            timer.Start();
            plainText = PlainAscii.Text;

            key = hmac.GenerateKey();
            Key.Text = ConvertToText(key);
            byte[] hashBytes = null;
            switch (selectedEncryptionType)
            {
                case "Hash-SHA1":
                    hashBytes = hash.ComputeHashSha1(ConvertToBytes(plainText));
                    break;
                case "Hash-SHA256":
                    hashBytes = hash.ComputeHashSha256(ConvertToBytes(plainText));
                    break;
                case "Hash-SHA512":
                    hashBytes = hash.ComputeHashSha512(ConvertToBytes(plainText));
                    break;
                case "Hash-MD5":
                    hashBytes = hash.ComputeHashMd5(ConvertToBytes(plainText));
                    break;
                case "HMAC-SHA1":
                    hashBytes = hmac.ComputeHmacsha1(ConvertToBytes(plainText), key);
                    break;
                case "HMAC-SHA256":
                    hashBytes = hmac.ComputeHmacsha256(ConvertToBytes(plainText), key);
                    break;
                case "HMAC-SHA512":
                    hashBytes = hmac.ComputeHmacsha512(ConvertToBytes(plainText), key);
                    break;
                case "HMAC-MD5":
                    hashBytes = hmac.ComputeHmacmd5(ConvertToBytes(plainText), key);
                    break;
            }
            if (hashBytes != null)
            {
                MacAscii.Text = ConvertToText(hashBytes);
                MacHex.Text = BitConverter.ToString(hashBytes);
            }

            timer.Stop();
            TimeElapsed.Content = "Time elapsed in milliseconds : " + timer.ElapsedMilliseconds;
        }

        private void Button_VertifyMac(object sender, RoutedEventArgs e)
        {
            switch (selectedEncryptionType)
            {
                case "Hash-SHA1":
                    CheckValid(MacAscii.Text == ConvertToText(hash.ComputeHashSha1(ConvertToBytes(PlainAscii.Text))));
                    break;
                case "Hash-SHA256":
                    CheckValid(MacAscii.Text == ConvertToText(hash.ComputeHashSha256(ConvertToBytes(PlainAscii.Text))));
                    break;
                case "Hash-SHA512":
                    CheckValid(MacAscii.Text == ConvertToText(hash.ComputeHashSha512(ConvertToBytes(PlainAscii.Text))));
                    break;
                case "Hash-MD5":
                    CheckValid(MacAscii.Text == ConvertToText(hash.ComputeHashMd5(ConvertToBytes(PlainAscii.Text))));
                    break;
                case "HMAC-SHA1":
                    CheckValid(MacAscii.Text == ConvertToText(hmac.ComputeHmacsha1(ConvertToBytes(PlainAscii.Text), key)));
                    break;
                case "HMAC-SHA256":
                    CheckValid(MacAscii.Text == ConvertToText(hmac.ComputeHmacsha256(ConvertToBytes(PlainAscii.Text), key)));
                    break;
                case "HMAC-SHA512":
                    CheckValid(MacAscii.Text == ConvertToText(hmac.ComputeHmacsha512(ConvertToBytes(PlainAscii.Text), key)));
                    break;
                case "HMAC-MD5":
                    CheckValid(MacAscii.Text == ConvertToText(hmac.ComputeHmacmd5(ConvertToBytes(PlainAscii.Text), key)));
                    break;
            }
        }

        private void CheckValid(bool valid)
        {
            if (valid)
            {
                MessageBox.Show("Valid");
            }
            else
            {
                MessageBox.Show("Invalid");
            }
        }

        private string ConvertToText(byte[] encryptedBytes)
        {
            return Convert.ToBase64String(encryptedBytes);
        }
        private byte[] ConvertToBytes(string text)
        {
            return Encoding.UTF8.GetBytes(text);
        }
    }
}
