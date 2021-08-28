using System;
using System.Collections.Generic;
using System.Text;

namespace BetaLixT.CloudFlare.Options
{
    public class CloudFlareOptions
    {
        public const string OptionsKey = "CloudFlareOptions";

        public string Token { get; set; }
        public string Email { get; set; }
    }
}
