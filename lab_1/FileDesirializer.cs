using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace lab_1
{
    public static class FileDesirializer
    {
        public static List<Profile> GetProfilesFromFile()
        {
            List<Profile> list = null;
            var serializer = new DataContractJsonSerializer(typeof(List<Profile>));
            using (var fs = new FileStream($"{Directory.GetCurrentDirectory()}\\profiles.json", FileMode.OpenOrCreate))
            {
                list = serializer.ReadObject(fs) as List<Profile>;
            }
            if (list?.Count == 0 || list == null) throw new Exception("нет профилей");
            return list;
        }
    }
}
