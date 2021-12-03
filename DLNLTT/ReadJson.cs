using Newtonsoft.Json;
using System.IO;

namespace DLNLTT
{
    public class ReadJson
    { 
        public ConfigJsonModel GetDataFromJson()
        {
            ConfigJsonModel items = new ConfigJsonModel();
            using (StreamReader r = new StreamReader("appsettings.json"))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<ConfigJsonModel>(json);
            }
            return items;
        }
        
    }
}
