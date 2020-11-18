using System;
using System.IO;
using System.Windows.Input;
using CIP_lab_3.Model;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Win32;

namespace CIP_lab_3.ViewModel
{
    public class TrisemusWindowViewModel : ViewModelBase
    {
        private string _encryptText = String.Empty;
        private string _decryptText = String.Empty;
        private string _secretEncrypt = String.Empty;
        private string _secretDecrypt = String.Empty;
        private string _countOfColumns = String.Empty;
        private string _language;
        private int _countOfColumnsInt = 0;

        public ICommand OpenFile { get; }
        public ICommand EncryptAndSaveCommand { get; }

        public string CountOfColumns
        {
            get => _countOfColumns;
            set
            {
                SetValue(ref _countOfColumns, value);
                int.TryParse(_countOfColumns, out _countOfColumnsInt);
            }
        }

        public string EncryptText
        {
            get => _encryptText;
            set => SetValue(ref _encryptText, value);
        }

        public string Language
        {
            get => _language;
            set => SetValue(ref _language, value);
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
            bool isEnglish = Language.Contains("English");
            var magicSquare = new TrisemusAlgorithm( EncryptText, SecretEncrypt, _countOfColumnsInt, isEnglish);
            var text = magicSquare.Encrypt();
            using (var writer = new StreamWriter($"{Directory.GetCurrentDirectory()}//CryptedText.txt"))
                writer.Write(text);
            EncryptText = String.Empty;
            SecretEncrypt = String.Empty;
        }

        private void OnDecryptPressed()
        {
            OpenFileDialog dialog = new OpenFileDialog {InitialDirectory = $"{Directory.GetCurrentDirectory()}"};
            if (dialog.ShowDialog().Value)
            {
                string loadFile = dialog.FileName;
                using (var reader = new StreamReader(loadFile))
                {
                    string text = reader.ReadToEnd();
                    bool isEnglish = Language.Contains("English");
                    var trisemus = new TrisemusAlgorithm(text, SecretDecrypt, _countOfColumnsInt, isEnglish);
                    var decryptedText = trisemus.Decrypt();
                    DecryptText = decryptedText;
                }
            }
        }

        private bool CanDecrypt() => SecretDecrypt.Length > 0;

        private bool CanEncryptAndSave() => EncryptText.Length > 0 &&
                                            SecretEncrypt.Length > 0 &&
                                            int.TryParse(CountOfColumns, out _countOfColumnsInt);

        public TrisemusWindowViewModel()
        {
            OpenFile = new RelayCommand(OnDecryptPressed, CanDecrypt);
            EncryptAndSaveCommand = new RelayCommand(SaveEncryptedFile, CanEncryptAndSave);
        }
    }
}
