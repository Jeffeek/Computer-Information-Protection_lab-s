using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using lab_1.Models;

namespace lab_1.Workers
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
            for (int i = 0; i < list.Count; i++)
                list[i].Password = PasswordWorker.Decrypt(list[i].Password, list[i].Login);

            return list;
        }

        public static void AddNewProfile(Profile profile)
        {
            var list = GetProfilesFromFile();
            for (int i = 0; i < list.Count; i++)
                list[i].Password = PasswordWorker.Encrypt(list[i].Password, list[i].Login);
            list.Add(profile);
            var serializer = new DataContractJsonSerializer(typeof(List<Profile>));
            using (var writer = new StreamWriter($"{Directory.GetCurrentDirectory()}\\profiles.json"))
                writer.Write(string.Empty);
            using (var fs = new FileStream($"{Directory.GetCurrentDirectory()}\\profiles.json", FileMode.Open))
            {
                serializer.WriteObject(fs, list);
            }
        }

        public static void RefreshAll(List<Profile> list)
        {
            var serializer = new DataContractJsonSerializer(typeof(List<Profile>));
            using (var writer = new StreamWriter($"{Directory.GetCurrentDirectory()}\\profiles.json"))
                writer.Write(string.Empty);
            using (var fs = new FileStream($"{Directory.GetCurrentDirectory()}\\profiles.json", FileMode.Open))
            {
                serializer.WriteObject(fs, list);
            }
        }
    }
}
