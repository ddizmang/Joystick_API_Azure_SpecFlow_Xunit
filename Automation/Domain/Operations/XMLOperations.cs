using System;
using System.Xml;
using Newtonsoft.Json;

namespace Automation.Domain.Operations
{
    public static class XMLOperations
    {
        public static string ParseToJson(string xmlMessage)
        {
            try
            {
                XmlDocument xd = new XmlDocument();
                xd.LoadXml(xmlMessage);
                return JsonConvert.SerializeXmlNode(xd);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
