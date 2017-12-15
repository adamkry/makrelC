using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace makrelC.Data
{
    public class JsonDb
    {
        protected string JsonFolder { get; set; }

        public JsonDb(string jsonFolder)
        {
            JsonFolder = jsonFolder;
        }

        public void Save<T>(List<T> items)
        {
            var json = JsonConvert.SerializeObject(items);
            string path = GetFileName<T>();
            MakeCopy(path);
            File.WriteAllText(path, json);
        }

        public List<T> Get<T>()
        {
            string fileName = GetFileName<T>();
            if (!File.Exists(fileName))
            {
                return new List<T>();
            }
            string json = File.ReadAllText(fileName);
            return JsonConvert.DeserializeObject<List<T>>(json);
        }

        private void MakeCopy(string path)
        {
            if (File.Exists(path))
            {
                string newPath = path + DateTime.Now.ToString("yyyyMMdd_hhmmss");
                if (File.Exists(newPath))
                {
                    File.Delete(newPath);
                }
                File.Move(path, newPath);
            }
        }

        private string GetFileName<T>()
        {
            return Path.Combine(JsonFolder, typeof(T).Name + ".json");
        }
    }
}
