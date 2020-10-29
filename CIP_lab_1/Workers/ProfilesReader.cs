using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using CIP_lab_1.Models;

namespace CIP_lab_1.Workers
{
    public class ProfilesReader<T> where T : Profile
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

        public List<T> GetProfiles(bool getDecrypted = false)
        {
            List<T> list;
            var passwordWorker = new RandomPasswordTripleDES();
            var serializer = new DataContractJsonSerializer(typeof(List<T>));
            using (var fs = new FileStream($"{Path}", FileMode.Open))
                list = serializer.ReadObject(fs) as List<T>;
            if (list == null) throw new Exception("list is NULL");
            if (getDecrypted)
                for (int i = 0; i < list.Count; i++)
                    list[i].Password = passwordWorker.Decrypt(list[i].Password, list[i].Login);

            return list;
        }
    }
}
