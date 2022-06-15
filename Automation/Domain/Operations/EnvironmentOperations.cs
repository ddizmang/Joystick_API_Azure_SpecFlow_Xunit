using System;
using System.Diagnostics;

namespace Automation.Domain.Operations
{
    public static class EnvironmentOperations
    {
        public static string GetEnvironmentName()
        {
            var value = "";
            try
            {

                value = Environment.GetEnvironmentVariable("ENVIRONMENT");

            }
            catch (Exception e)
            {
                Debug.WriteLine($"Environment Helper : GetEnvironmentName");
                Debug.WriteLine($"Error Message : {e.Message}");
                Debug.WriteLine($"Error Stack : {e.StackTrace}");
            }

            return value;
        }
    }
}
