using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation.Data.JsonData.Environments;
using Automation.Domain.Operations;
using Automation.Domain.Resources;
using TechTalk.SpecFlow;

namespace Automation.Tests.BDD.Hooks
{
    [Binding]
    public sealed class Hooks
    {
        private ScenarioContext _scenarioContext;
        private static FeatureContext _featureContext;
        //private static TestContext _testContext;

        //public Hooks(ScenarioContext scenarioContext, TestContext testContext)
        //{
        //    _scenarioContext = scenarioContext;
        //    _testContext = testContext;
        //}

        public Hooks(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            _featureContext = featureContext;
            _scenarioContext = scenarioContext;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            Debug.WriteLine("Joystick - BDD - Hooks : Before Test Run");
            Debug.WriteLine("============================================");
            Debug.WriteLine("");
            Debug.WriteLine(" Start of - Environment Variables");
            string environmentSetting = EnvironmentOperations.GetEnvironmentName();
            Environments environments = new Environments();
            Environments environment = environments.GetEnvironmentDetails().FirstOrDefault(x => x.Environment == environmentSetting);
            SetupEnvironmentValue(environment.Environment);
            Debug.WriteLine(" End of - Environment Variables");
            Debug.WriteLine("");
            Debug.WriteLine(" Start of - Test Run Resources");
            TestRun_Resources.URL = environment.URL;
            TestRun_Resources.DBConnectionString = environment.DBConnectionString;
            TestRun_Resources.WorkerV3LocalURL = environment.WorkerV3LocalURL;
            TestRun_Resources.WorkerV3BaseId = environment.WorkerV3BaseId;
            TestRun_Resources.IDPAuthUrl = environment.IDPAuthUrl;
            TestRun_Resources.IDPGrantType = environment.IDPGrantType;
            TestRun_Resources.IDPClientId = environment.IDPClientId;
            TestRun_Resources.IDPScope = environment.IDPScope;
            TestRun_Resources.IDPResourceSecurityId = environment.IDPResourceSecurityId;
            TestRun_Resources.IDPAgencySecret = environment.IDPAgencySecret;
            TestRun_Resources.HL7FileWaitTime_Global = environment.HL7FileWaitTime_Global;
            TestRun_Resources.HL7FilePrefix_Global = environment.HL7FilePrefix_Global;
            TestRun_Resources.StockOutboundORU_DropLoc = environment.StockOutboundORU_DropLoc;
            TestRun_Resources.BearerToken = null;
            TestRun_Resources.BasicAuth = null;
            TestRun_Resources.FormContent = null;
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            Debug.WriteLine("Feature Title: " + featureContext.FeatureInfo.Title);
        }

        [BeforeScenario]
        public void BeforeScenario(ScenarioContext scenarioContext)
        {
            Debug.WriteLine("Scenario Title: " + scenarioContext.ScenarioInfo.Title);

            #region Global Variables
            _scenarioContext.Add("sqlConnectionString", TestRun_Resources.DBConnectionString);
            _scenarioContext.Add("%TestRun_Resources.StockOutboundORU_DropLoc%", TestRun_Resources.StockOutboundORU_DropLoc);

            #endregion

            #region Agency API Credentials Global Variables
            _scenarioContext.Add("%IDPGrantType%", TestRun_Resources.IDPGrantType);
            _scenarioContext.Add("%IDPClientId%", TestRun_Resources.IDPClientId);
            _scenarioContext.Add("%IDPScope%", TestRun_Resources.IDPScope);
            _scenarioContext.Add("%IDPResourceSecurityId%", TestRun_Resources.IDPResourceSecurityId);
            _scenarioContext.Add("%IDPAgencySecret%", TestRun_Resources.IDPAgencySecret);
            #endregion


            #region HL7 Global Variables
            //Set hl7FileWaitTime (can be overriden using GIVEN hl7 file wait # mins step)
            _scenarioContext.Add("hl7FileWaitTime", TestRun_Resources.HL7FileWaitTime_Global);
            //Set hl7FileLocation (can be overriden using GIVEN hl7 file prefix <string> step)
            _scenarioContext.Add("hl7FilePrefix", TestRun_Resources.HL7FilePrefix_Global);
            #endregion

            #region XML Global Variables
            //Set hl7FileWaitTime (can be overriden using GIVEN xml file wait # mins step)
            _scenarioContext.Add("xmlFileWaitTime", TestRun_Resources.XMLFileWaitTime_Global);
            //Set hl7FileLocation (can be overriden using GIVEN xml file prefix <string> step)
            _scenarioContext.Add("xmlFilePrefix", TestRun_Resources.XMLFilePrefix_Global);
            #endregion
        }

        [AfterStep]
        public void AfterStep(ScenarioContext scenarioContext)
        {
            Debug.WriteLine("Step Title: " + scenarioContext.StepContext.StepInfo.Text);
            Debug.WriteLine("Step Def: " + scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString());
        }
        public static void SetupEnvironmentValue(string env)
        {
            switch (env.ToUpper())
            {
                case "DEV":
                    TestRun_Resources.AppEnv = Domain.Enums.Environment.DEV;
                    break;
                case "QA-TOO-PERSONAL-QA2":
                    TestRun_Resources.AppEnv = Domain.Enums.Environment.QATOOPERSONAL;
                    break;
                case "QA-EVEN":
                    TestRun_Resources.AppEnv = Domain.Enums.Environment.QAEVEN;
                    break;
                case "QA-ODD":
                    TestRun_Resources.AppEnv = Domain.Enums.Environment.QAODD;
                    break;
                case "QA-HOTFIX":
                    TestRun_Resources.AppEnv = Domain.Enums.Environment.QAHOTFIX;
                    break;
                case "STAGING":
                    TestRun_Resources.AppEnv = Domain.Enums.Environment.STAGING;
                    break;
                case "PROD":
                    TestRun_Resources.AppEnv = Domain.Enums.Environment.PROD;
                    break;
            }
        }
    }
}
