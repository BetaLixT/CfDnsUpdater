using BetaLixT.CloudFlare.Models.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BetaLixT.CloudFlare.Models.Entities
{
    public class DnsRecord
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public DnsRecordType Type { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("proxiable")]
        public bool Proxiable { get; set; }

        [JsonProperty("proxied")]
        public bool Proxied { get; set; }

        [JsonProperty("ttl")]
        public int Ttl { get; set; }

        [JsonProperty("locked")]
        public bool Locked { get; set; }

        [JsonProperty("zone_id")]
        public string ZoneId { get; set; }

        [JsonProperty("zone_name")]
        public string ZoneName { get; set; }

        [JsonProperty("created_on")]
        public DateTimeOffset CreatedOn { get; set; }

        [JsonProperty("modified_on")]
        public DateTimeOffset ModifiedOn { get; set; }
    }
}
