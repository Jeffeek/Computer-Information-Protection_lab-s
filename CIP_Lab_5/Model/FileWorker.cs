using System;
using System.IO;
using System.Windows.Navigation;

namespace CIP_Lab_5.Model
{
    class FileWorker
    {
        public string Path { get; set; }

        public FileWorker(string path)
        {
            Path = path;
        }

        public string Read()
        {
            string result = "";
            using (var reader = new StreamReader(Path))
            {
                result = reader.ReadToEnd();
            }

            return result;
        }

        public void Write(string text)
        {
            using (var writer = new StreamWriter(Path, false))
            {
                writer.Write(text);
            }
        }
    }
}
