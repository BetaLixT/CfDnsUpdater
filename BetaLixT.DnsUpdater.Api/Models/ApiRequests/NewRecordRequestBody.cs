using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BetaLixT.DnsUpdater.Api.Models.ApiRequests
{
    public class NewRecordRequestBody
    {
        public string ZoneId { get; set; }
        public string RecordName { get; set; }
    }
}
