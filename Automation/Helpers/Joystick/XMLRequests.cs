using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Automation.Domain.Enums;
using Automation.Domain.Models.Joystick;
using Automation.Domain.Operations;
using Automation.Domain.Resources;

namespace Automation.Helpers.Joystick
{
    public class XMLRequests
    {
        public static APIResponse GetXmlRequest(string scenarioTitle, string xmlFileLocation, string xmlFilePrefix = "", int xmlFileWaitTime = 0)
        {
            APIResponse apiResponse = new APIResponse();
            try
            {
                //var timeout = new TimeSpan(0, hl7FileWaitTime, 0);
                Thread.Sleep(new TimeSpan(0, xmlFileWaitTime, 0));
                var xmlFile =
                    IOOperations.GetFirstFileByFileExtAndCreateDate(xmlFileLocation, FileTypes.FileType.XML,
                        xmlFilePrefix);
                apiResponse.RequestDurationMilliseconds = "";
                apiResponse.TestName = scenarioTitle;
                apiResponse.Environment = TestRun_Resources.AppEnv.ToString();
                var responseBody = File.ReadAllText(xmlFile);
                apiResponse.RequestType = "XMLRequest-GET";
                apiResponse.RequestBody = "";
                apiResponse.ResponseCode = "";
                apiResponse.ResponseBody = responseBody;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return apiResponse;
        }
    }
}
