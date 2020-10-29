using System;
using CIP_lab_1.Models;

namespace CIP_lab_1.Workers
{
    public class FileWorker<T> where T : Profile
    {
        private ProfilesReader<T> _reader;
        private ProfilesWriter<T> _writer;
        private string _path;
        public string Path
        {
            get => _path;
            private set
            {
                if (String.IsNullOrEmpty(value)) throw new ArgumentException(nameof(value));
                if (String.IsNullOrWhiteSpace(value)) throw new ArgumentException(nameof(value));
                _path = value;
                _reader.Path = value;
                _writer.Path = value;
            }
        }

        public ProfilesReader<T> Reader => _reader;
        public ProfilesWriter<T> Writer => _writer;

        public FileWorker(string path)
        {
            _reader = new ProfilesReader<T>();
            _writer = new ProfilesWriter<T>();
            Path = path;
        }
    }
}
