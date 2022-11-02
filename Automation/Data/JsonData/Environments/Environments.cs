using System.Collections.Generic;
using System.Diagnostics;
using Automation.Domain.Operations;

namespace Automation.Data.JsonData.Environments
{
    public class Environments
    {
        public string Environment { get; set; }
        public string URL { get; set; }
        public string DBConnectionString { get; set; }
        public string IDPAuthUrl { get; set; }
        public string IDPGrantType { get; set; }
        public string IDPClientId { get; set; }
        public string IDPScope { get; set; }
        public string IDPResourceSecurityId { get; set; }
        public string IDPAgencySecret { get; set; }
        public string WorkerV3LocalURL { get; set; }
        public string WorkerV3BaseId { get; set; }
        public int HL7FileWaitTime_Global { get; set; }
        public string HL7FilePrefix_Global { get; set; }
        public int XMLFileWaitTime_Global { get; set; }
        public string XMLFilePrefix_Global { get; set; }
        public string StockOutboundORU_DropLoc { get; set; }

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
