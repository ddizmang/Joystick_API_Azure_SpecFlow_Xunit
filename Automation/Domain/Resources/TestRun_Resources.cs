using System;
using System.Collections.Generic;
using System.Diagnostics;
using Environment = Automation.Domain.Enums.Environment;

namespace Automation.Domain.Resources
{
    public static class TestRun_Resources
    {
        [ThreadStatic]
        public static string URL;
        public static Environment AppEnv;
        public static Dictionary<string, string> FormContent;
        public static string BearerToken;
        public static string BasicAuth;
        public static string DBConnectionString;
        public static string IDPAuthUrl;
        public static string IDPGrantType { get; set; }
        public static string IDPClientId { get; set; }
        public static string IDPScope { get; set; }
        public static string IDPResourceSecurityId { get; set; }
        public static string IDPAgencySecret { get; set; }
        public static string WorkerV3LocalURL;
        public static string WorkerV3BaseId;
        public static int HL7FileWaitTime_Global;
        public static string HL7FilePrefix_Global;
        public static int XMLFileWaitTime_Global;
        public static string XMLFilePrefix_Global;
        public static string StockOutboundORU_DropLoc;

        public static void DebugReportError(string Title, string ExceptionMessage, Exception e)
        {
            Debug.WriteLine($"{Title} : {ExceptionMessage}");
            Debug.WriteLine($"Error Message : {e.Message}");
            Debug.WriteLine($"Error Stack : {e.StackTrace}");
        }

    }
}
