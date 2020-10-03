using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;

namespace lab_1
{
    public static class FileWorker
    {
        public static List<Profile> GetProfilesFromFile()
        {
            List<Profile> list = null;
            var serializer = new DataContractJsonSerializer(typeof(List<Profile>));
            using (var fs = new FileStream($"{Directory.GetCurrentDirectory()}\\profiles.json", FileMode.OpenOrCreate))
            {
                list = serializer.ReadObject(fs) as List<Profile>;
            }
            //if (list?.Count == 0 || list == null) throw new Exception("нет профилей");
            foreach (var profile in list)
                profile.Password = PasswordEncoding.Decrypt(profile.Password, profile.Login);

            return list;
        }

        public static void AddNewProfile(Profile profile)
        {
            var list = GetProfilesFromFile();
            for (int i = 0; i < list.Count; i++)
                list[i].Password = PasswordEncoding.Encrypt(list[i].Password, list[i].Login);
            list.Add(profile);
            var serializer = new DataContractJsonSerializer(typeof(List<Profile>));
            using (var fs = new FileStream($"{Directory.GetCurrentDirectory()}\\profiles.json", FileMode.OpenOrCreate))
            {
                serializer.WriteObject(fs, list);
            }
        }
    }
}
