using System.IO;

namespace Automation.Domain.Operations
{
    public static class JsonOperations
    {
        public static T GetObjectData<T>(string path)
        {
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
            }
        }
    }
}
