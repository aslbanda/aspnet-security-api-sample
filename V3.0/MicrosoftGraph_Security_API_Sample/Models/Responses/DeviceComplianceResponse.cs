using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicrosoftGraph_Security_API_Sample.Models.Responses
{
    public class DeviceComplianceResponse
    {
        public double? NonCompliant { get; set; }

        public double? Compliant { get; set; }

        public double? NonCompliantPercentage { get; set; }
    }
}
