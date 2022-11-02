using System;
using System.IO;
using System.Threading;
using Automation.Domain.Enums;
using Automation.Domain.Models.Joystick;
using Automation.Domain.Operations;
using Automation.Domain.Resources;

namespace Automation.Helpers.Joystick
{
    public static class HL7Requests
    {
        public static APIResponse GetHL7Request(string scenarioTitle, string hl7FileLocation, string hl7FilePrefix = "", int hl7FileWaitTime = 0)
        {
            APIResponse apiResponse = new APIResponse();
            try
            {
                Thread.Sleep(new TimeSpan(0, hl7FileWaitTime, 0));
                var hl7File =
                    IOOperations.GetFirstFileByFileExtAndCreateDate(hl7FileLocation, FileTypes.FileType.HL7,
                        hl7FilePrefix);
                apiResponse.RequestDurationMilliseconds = "";
                apiResponse.TestName = scenarioTitle;
                apiResponse.Environment = TestRun_Resources.AppEnv.ToString();
                var responseBody = File.ReadAllText(hl7File);
                apiResponse.RequestType = "HL7Request-GET";
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
