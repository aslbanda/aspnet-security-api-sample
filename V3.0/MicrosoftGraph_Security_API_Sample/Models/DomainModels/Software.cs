using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicrosoftGraph_Security_API_Sample.Models.DomainModels
{
    public class Software
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("vendor")]
        public string Vendor { get; set; }

        [JsonProperty("weaknesses")]
        public int Weaknesses { get; set; }

        [JsonProperty("publicExploit")]
        public bool PublicExploit { get; set; }

        [JsonProperty("activeAlert")]
        public bool ActiveAlert { get; set; }

        [JsonProperty("exposedMachines")]
        public int ExposedMachines { get; set; }

        [JsonProperty("impactScore")]
        public double ImpactScore { get; set; }
    }
}
