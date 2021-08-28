using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace  BetaLixT.DnsUpdater.Api.Helpers
{
    public static class ServerIpHelper
    {
        public static async Task<string> GetMyWanIpAsync(HttpClient httpClient)
        {
            return await httpClient.GetStringAsync("http://whatismyip.akamai.com/");
        }

        public static async Task<string> GetMyWanIpAsync()
        {
            var httpClient = new HttpClient();
            return await httpClient.GetStringAsync("http://whatismyip.akamai.com/");
        }
    }
}
