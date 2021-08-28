using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BetaLixT.DnsUpdater.Api.Options
{
    public class UpdateDnsOptions
    {
        public const string OptionsKey = "UpdateDnsOptions";

        public string Schedule { get; set; }
    }
}
