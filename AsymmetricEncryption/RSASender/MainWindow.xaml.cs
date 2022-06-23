using Microsoft.Win32;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Xml;

namespace RSASender
{
    public partial class MainWindow : Window
    {
        string keyPath = "";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_FindPublicKey(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.xml)|*.xml";
            if (openFileDialog.ShowDialog() == true)
            {
                keyPath = openFileDialog.FileName;
                PublicKeyPath.Text = keyPath;
            }
            XmlDocument document = new XmlDocument();
            document.Load(keyPath);
            XmlNodeList xnList = document.SelectNodes("RSAKeyValue");
            foreach (XmlNode xn in xnList)
            {
                ExponentTextBox.Text = xn["Exponent"].InnerText;
                ModulusTextBox.Text = xn["Modulus"].InnerText;
            }
        }

        private void Button_Encrypt(object sender, RoutedEventArgs e)
        {
            CipherbytesTextBox.Text = Convert.ToBase64String(Encrypt(keyPath, Encoding.UTF8.GetBytes(PlaintextTextBox.Text)));
        }

        public byte[] Encrypt(string publickeypath, byte[] data)
        {
            byte[] chiper;
            using (var rsa = new RSACryptoServiceProvider(4096))
            {
                rsa.PersistKeyInCsp = false;
                rsa.FromXmlString(File.ReadAllText(publickeypath));

                chiper = rsa.Encrypt(data, true);
            }
            return chiper;
        }
    }
}
