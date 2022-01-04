using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace PW_Lab
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private string _plainText;
        private string _encryptedText;
        private string _encryptionKey;

        public string PlainText
        {
            get => _plainText;
            set
            {
                _plainText = value;
                Encrypt();
                OnPropertyChanged();
            }
        }

        public string EncryptedText
        {
            get => _encryptedText;
            set
            {
                _encryptedText = value;
                OnPropertyChanged();
            }
        }

        public string EncryptionKey
        {
            get => _encryptionKey;
            set
            {
                if (value.Length > 16)
                    return;

                _encryptionKey = value;

                Encrypt();
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow()
        {
            InitializeComponent();
            _plainText = string.Empty;
            _encryptedText = string.Empty;

            DataContext = this;
        }

        public void SaveData(object sender, EventArgs args)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "txt|*.txt";
            dialog.Title = "Open an text file";

            if (dialog.ShowDialog() ?? false)
                File.WriteAllText(dialog.FileName, EncryptedText);
            
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        private void Encrypt()
        {
            if (PlainText == null || PlainText.Length <= 0)
                return;

            if (EncryptionKey == null || EncryptionKey.Length < 16)
                return;

            byte[] encrypted;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(EncryptionKey);
                aesAlg.IV = Encoding.UTF8.GetBytes("supersecretivkey");

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(PlainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            EncryptedText = BitConverter.ToString(encrypted);
        }
    }
}
