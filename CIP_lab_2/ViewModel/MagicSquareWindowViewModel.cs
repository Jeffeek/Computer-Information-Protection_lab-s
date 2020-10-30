using System.IO;
using System.Windows.Input;
using CIP_lab_2.Model;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Win32;

namespace CIP_lab_2.ViewModel
{
    public class MagicSquareWindowViewModel : ViewModelBase
    {
        private string _encryptText;
        private string _decryptText;
        private string _secretEncrypt;
        private string _secretDecrypt;

        public ICommand OpenFile { get; }
        public ICommand EncryptAndSaveCommand { get; }

        public string EncryptText
        {
            get => _encryptText;
            set => SetValue(ref _encryptText, value);
        }

        public string SecretEncrypt
        {
            get => _secretEncrypt;
            set => SetValue(ref _secretEncrypt, value);
        }

        public string DecryptText
        {
            get => _decryptText;
            set => SetValue(ref _decryptText, value);
        }

        public string SecretDecrypt
        {
            get => _secretDecrypt;
            set => SetValue(ref _secretDecrypt, value);
        }

        private void SaveEncryptedFile()
        {
            var magicSquare = new MagicSquare();
            var text = magicSquare.Encrypt(EncryptText, SecretEncrypt);
            using (var writer = new StreamWriter($"{Directory.GetCurrentDirectory()}//CryptedText.txt"))
            {
                writer.Write(text);
            }
        }

        private void OnDecryptPressed()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog().Value && SecretDecrypt.Length > 0)
            {
                string loadFile = dialog.FileName;
                using (var reader = new StreamReader(loadFile))
                {
                    string text = reader.ReadToEnd();
                    var magicSquare = new MagicSquare();
                    var decryptedText = magicSquare.Decrypt(text, SecretDecrypt);
                    DecryptText = decryptedText;
                }
            }
        }

        public MagicSquareWindowViewModel()
        {
            OpenFile = new RelayCommand(OnDecryptPressed, () => SecretDecrypt.Length > 0);
            EncryptAndSaveCommand = new RelayCommand(SaveEncryptedFile,
                () => EncryptText.Length > 0 && SecretEncrypt.Length > 0);
        }
    }
}
