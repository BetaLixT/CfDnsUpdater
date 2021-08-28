using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BetaLixT.DnsUpdater.Api.Database.Entities
{
    public class DnsRecord
    {
        public long Id { get; set; }
        public string CloudFlareId { get; set; }
        public string RecordName { get; set; }
        public string ZoneId { get; set; }
        public string Ip { get; set; }
    }
}
