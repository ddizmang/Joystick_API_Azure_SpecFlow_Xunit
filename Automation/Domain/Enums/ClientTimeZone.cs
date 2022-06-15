using System.ComponentModel;

namespace Automation.Domain.Enums
{
    public enum ClientTimeZone
    {
        [Description("Eastern Standard Time")]
        Eastern,
        [Description("Eastern - No DS")]
        Eastern_NoDS,
        [Description("Central Standard Time")]
        Central,
        [Description("Mountain Standard Time")]
        MountainStandardTime,
        [Description("Mountain - No DS")]
        Mountain_NoDS,
        [Description("Pacific Standard Time")]
        Pacific,
        [Description("Alaskan Standard Time")]
        Alaska,
        [Description("Hawaiian Standard Time")]
        Hawaii,
        [Description("Universal Coordinated Time")]
        UniversalCoordinatedTime
    }
}
