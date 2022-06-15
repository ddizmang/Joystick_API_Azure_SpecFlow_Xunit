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
        private static ScenarioContext _scenarioContext;
        //private static TestContext _testContext;

        //public Hooks(ScenarioContext scenarioContext, TestContext testContext)
        //{
        //    _scenarioContext = scenarioContext;
        //    _testContext = testContext;
        //}

        public Hooks(ScenarioContext scenarioContext)
        {
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
            TestRun_Resources.RabbitHostName = environment.RabbitHostName;
            TestRun_Resources.RabbitUserName = environment.RabbitUserName;
            TestRun_Resources.RabbitPassword = environment.RabbitPassword;
            TestRun_Resources.BearerToken = null;
            TestRun_Resources.BasicAuth = null;
            TestRun_Resources.FormContent = null;
        }

        public static void SetupEnvironmentValue(string env)
        {
            switch (env)
            {
                case "Test":
                    TestRun_Resources.AppEnv = Domain.Enums.Environment.Test;
                    break;
                case "Stage":
                    TestRun_Resources.AppEnv = Domain.Enums.Environment.Stage;
                    break;
                case "Prod":
                    TestRun_Resources.AppEnv = Domain.Enums.Environment.Production;
                    break;

            }
        }
    }
}
