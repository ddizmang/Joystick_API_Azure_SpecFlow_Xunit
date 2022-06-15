using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Domain.Models.Joystick
{
    public class APIResponse
    {
        public string TestName { get; set; }
        public string RequestType { get; set; }
        public string Environment { get; set; }
        public string ResponseCode { get; set; }
        public string RequestBody { get; set; }
        public string ResponseBody { get; set; }
        public string RequestDurationMilliseconds { get; set; }
    }
}
