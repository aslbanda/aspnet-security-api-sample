using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicrosoftGraph_Security_API_Sample.Models.Responses
{
    public class DeviceScoreResponse
    {
        public double TotalScore { get; set; }

        public double DeviceScorePercentage { get; set; }

        public double DeviceScore { get; set; }
    }
}
