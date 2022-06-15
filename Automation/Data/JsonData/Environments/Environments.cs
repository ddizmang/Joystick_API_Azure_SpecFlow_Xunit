using System.Collections.Generic;
using Automation.Domain.Operations;

namespace Automation.Data.JsonData.Environments
{
    public class Environments
    {
        public string Environment { get; set; }
        public string URL { get; set; }
        public string RabbitHostName { get; set; }
        public string RabbitUserName { get; set; }
        public string RabbitPassword { get; set; }
        public List<Environments> GetEnvironmentDetails()
        {
            List<Environments> allEnvironments = null;

            //TODO: Switch JSONPath to TestRun_Resources class
            //allEnvironments = JsonHelper
            //    .GetObjectData<AppEnvironment>(TestRun_Resources.JSonDataFileLocation + "Environment.json").Environments;
            allEnvironments =
                JsonOperations.GetObjectData<AppEnvironment>(".\\Data\\JsonData\\Environments\\Environment.json").Environments;
            return allEnvironments;
        }
        public class AppEnvironment
        {
            public List<Environments> Environments { get; set; }
        }
    }
}
