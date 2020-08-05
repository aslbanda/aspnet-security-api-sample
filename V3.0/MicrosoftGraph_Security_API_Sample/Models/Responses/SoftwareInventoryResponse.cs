using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicrosoftGraph_Security_API_Sample.Models.Responses
{
    public class SoftwareInventoryResponse
    {
        public string Name { get; set; }

        public string Vendor { get; set; }

        public int Weaknesses { get; set; }

        public string ExposedDevices { get; set; }

        public double ImpactScore { get; set; }
    }
}
