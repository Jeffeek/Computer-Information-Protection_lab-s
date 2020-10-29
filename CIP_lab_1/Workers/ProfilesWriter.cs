using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using CIP_lab_1.Models;

namespace CIP_lab_1.Workers
{
    public class ProfilesWriter<T> where T : Profile
    {
        private string _path;
        public string Path
        {
            get => _path;
            set
            {
                if (String.IsNullOrEmpty(value)) throw new ArgumentException(nameof(value));
                if (String.IsNullOrWhiteSpace(value)) throw new ArgumentException(nameof(value));
                _path = value;
            }
        }

        public void AddNewProfile(T profile)
        {
            var reader = new ProfilesReader<T>
            {
                Path = Path
            };
            var list = reader.GetProfiles();
            list.Add(profile);
            RewriteAll(list);
        }

        public void RewriteAll(List<T> list)
        {
            var serializer = new DataContractJsonSerializer(typeof(List<T>));
            using (var writer = new StreamWriter($"{Path}"))
                writer.Write(string.Empty);
            using (var fs = new FileStream($"{Path}", FileMode.Open))
                serializer.WriteObject(fs, list);
        }
    }
}
