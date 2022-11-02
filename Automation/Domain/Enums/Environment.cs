using System.ComponentModel;

namespace Automation.Domain.Enums
{
    public enum Environment
    {
        [Description("DEV")]
        DEV,
        [Description("QA-TOO-PERSONAL-QA2")]
        QATOOPERSONAL,
        [Description("QA-ODD")]
        QAODD,
        [Description("QA-EVEN")]
        QAEVEN,
        [Description("QA-HOTFIX")]
        QAHOTFIX,
        [Description("STAGING")]
        STAGING,
        [Description("PROD")]
        PROD
    }
}
