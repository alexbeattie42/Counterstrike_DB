using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CounterStrikeDB
{
    class Util
    {
        public static string BaseDirectory = AppDomain.CurrentDomain.BaseDirectory; // current folder the program is running in
        public static string DataDirectory = BaseDirectory + "_data\\"; // path to json data

        // utility to serialize json
        public static void SaveJSON(dynamic data, string filename, bool dataDir = true)
        {
            File.WriteAllText((dataDir ? DataDirectory : BaseDirectory) + filename, JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented));
        }

        // utility to deserialize json
        public static T LoadJSON<T>(string filename, bool dataDir = true)
        {
            return JsonConvert.DeserializeObject<T>(File.ReadAllText((dataDir ? DataDirectory : BaseDirectory) + filename));
        }
    }
}
