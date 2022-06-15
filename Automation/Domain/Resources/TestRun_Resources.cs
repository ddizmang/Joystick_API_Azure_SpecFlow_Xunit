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
        public static string RabbitHostName;
        public static string RabbitUserName;
        public static string RabbitPassword;

        public static void DebugReportError(string Title, string ExceptionMessage, Exception e)
        {
            Debug.WriteLine($"{Title} : {ExceptionMessage}");
            Debug.WriteLine($"Error Message : {e.Message}");
            Debug.WriteLine($"Error Stack : {e.StackTrace}");
        }

    }
}
