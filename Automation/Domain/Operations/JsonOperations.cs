using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

        public static bool IsJson(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            input.Trim();
            if ((input.StartsWith("{")) && input.EndsWith("}") || (input.StartsWith("[")) && input.EndsWith("]"))
            {

                try
                {
                    var obj = JToken.Parse(input);
                    return true;
                }
                catch (JsonReaderException jre)
                {
                    return false;
                }
                catch (Exception e)
                {
                    throw new Exception("Error in JsonOperations.IsJson method");
                }
            }
            else
            {
                return false;
            }
        }
    }


}
