using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Xml;
using Microsoft.Win32;

namespace RSAReceiver
{
    public partial class MainWindow : Window
    {
        string keyPath;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Button_GenerateNewKeys(object sender, RoutedEventArgs e)
        {
            string keyPathPrivate = "";
            string keyPathPublic = "";

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ValidateNames = false;
            dialog.CheckFileExists = false;
            dialog.CheckPathExists = true;
            dialog.FileName = "Folder";

            if (dialog.ShowDialog() == true)
            {
                keyPathPrivate = Path.GetDirectoryName(dialog.FileName);
                keyPathPublic = Path.GetDirectoryName(dialog.FileName);
            }
            keyPathPrivate += "\\PrivateKey.Xml";
            keyPathPublic += "\\PublicKey.Xml";

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(4096);
            rsa.PersistKeyInCsp = false;
            File.WriteAllText(keyPathPublic, rsa.ToXmlString(false));
            File.WriteAllText(keyPathPrivate, rsa.ToXmlString(true));
        }

        private void Button_FileFinder(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.xml)|*.xml";
            if (openFileDialog.ShowDialog() == true)
            {
                keyPath = openFileDialog.FileName;
                KeyPathTextBox.Text = keyPath;
            }
            XmlDocument document = new XmlDocument();
            document.Load(keyPath);
            XmlNodeList xnList = document.SelectNodes("RSAKeyValue");

            foreach (XmlNode xn in xnList)
            {
                ExponentTextBox.Text = xn["Exponent"].InnerText;
                ModulusTextBox.Text = xn["Modulus"].InnerText;
                DTextBox.Text = xn["D"].InnerText;
                DPTextBox.Text = xn["DP"].InnerText;
                DQTextBox.Text = xn["DQ"].InnerText;
                InvQTextBox.Text = xn["InverseQ"].InnerText;
                PTextBox.Text = xn["P"].InnerText;
                QTextBox.Text = xn["Q"].InnerText;
            }
        }

        private void Button_Decrypt(object sender, RoutedEventArgs e)
        {
            DecryptedTextBox.Text = Encoding.UTF8.GetString(DecryptData(keyPath, Convert.FromBase64String(CipherTextBox.Text)));
        }

        public byte[] DecryptData(string privateKeyPath, byte[] dataToDecrypt)
        {
            byte[] decryptedText;

            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.FromXmlString(File.ReadAllText(privateKeyPath));
                decryptedText = rsa.Decrypt(dataToDecrypt, true);
            }

            return decryptedText;
        }
    }
}
