using System.ComponentModel;

namespace Automation.Domain.Enums
{
    public enum Environment
    {
        [Description("FB1")]
        FB1,
        [Description("FB2")]
        FB2,
        [Description("FB3")]
        FB3,
        [Description("Test")]
        Test,
        [Description("Stage")]
        Stage,
        [Description("Prod_Gold")]
        Production_Gold,
        [Description("Prod")]
        Production
    }
}
