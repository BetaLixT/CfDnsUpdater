using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BetaLixT.CloudFlare.Models.Requests
{
    public class CreateDnsRecordRequestBody
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("ttl")]
        public int? Ttl { get; set; }

        [JsonProperty("priority")]
        public int? Priority { get; set; }

        [JsonProperty("proxied")]
        public bool? Proxied { get; set; }
    }
}
