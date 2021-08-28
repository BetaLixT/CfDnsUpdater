using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BetaLixT.DnsUpdater.Api.Database.Entities
{
    public class Log
    {
        public long Id { get; set; }
        public long? DnsRecordId { get; set; }
        public LogEvent LogEvent { get; set; }
        public string Details { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public enum LogEvent
    {
        DnsUpdated,
        DnsUpdateFailed,
        NoUpdateRequired,
        IpFetchFailed
    }
}
