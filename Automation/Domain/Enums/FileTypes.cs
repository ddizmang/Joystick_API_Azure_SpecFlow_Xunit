using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Domain.Enums
{
    public class FileTypes
    {
        public enum FileType
        {
            [Description("XML")]
            XML,
            [Description("HL7")]
            HL7,
            [Description("CSV")]
            CSV
        }
    }
}
