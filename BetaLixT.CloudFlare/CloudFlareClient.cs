using BetaLixT.CloudFlare.Models.Entities;
using BetaLixT.CloudFlare.Models.Enums;
using BetaLixT.CloudFlare.Models.Requests;
using BetaLixT.CloudFlare.Models.Responses;
using BetaLixT.CloudFlare.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BetaLixT.CloudFlare
{
    public class CloudFlareClient
    {
        private const string Domain = "api.cloudflare.com";

        private readonly CloudFlareOptions _cloudFlareOptions;
        private readonly HttpClient _httpClient;
        public CloudFlareClient(IOptions<CloudFlareOptions> options)
        {
            this._cloudFlareOptions = options.Value;
            this._httpClient = new HttpClient();

            this._httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {this._cloudFlareOptions.Token}");
        }

        public async Task<ResponseBody<DnsRecord>> CreateDnsRecord(
            string zoneIdentifier,
            DnsRecordType recordType,
            string dnsRecordName,
            string recordContent,
            int ttl,
            int? priority = null,
            bool? proxied = false)
        {
            var content = JsonConvert.SerializeObject(new CreateDnsRecordRequestBody
            {
                Type = recordType.ToString(),
                Name = dnsRecordName,
                Content = recordContent,
                Ttl = ttl,
                Priority = priority,
                Proxied = proxied
            });

            var response = await this._httpClient.PostAsync(
                $"https://{Domain}/client/v4/zones/{zoneIdentifier}/dns_records",
                new StringContent(content)
                );

            return JsonConvert.DeserializeObject<ResponseBody<DnsRecord>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ResponseBody<DnsRecord>> UpdateDnsRecord(
            string cloudFlareRecordId,
            string zoneIdentifier,
            DnsRecordType recordType,
            string dnsRecordName,
            string recordContent,
            int ttl,
            int? priority = null,
            bool? proxied = false)
        {
            var content = JsonConvert.SerializeObject(new CreateDnsRecordRequestBody
            {
                Type = recordType.ToString(),
                Name = dnsRecordName,
                Content = recordContent,
                Ttl = ttl,
                Priority = priority,
                Proxied = proxied
            });

            var response = await this._httpClient.PostAsync(
                $"https://{Domain}/client/v4/zones/{zoneIdentifier}/dns_records/{cloudFlareRecordId}",
                new StringContent(content)
                );

            return JsonConvert.DeserializeObject<ResponseBody<DnsRecord>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ResponseBody<List<DnsRecord>>> ListDnsRecordsAsync(
            string zoneIdentifier,
            Match match = Match.all,
            string dnsRecordName = null,
            string order = null,
            int page = 1,
            int perPage = 20,
            string content = null,
            DnsRecordType? type = null,
            bool? proxied = null,
            Direction? direction = null
            )
        {
            var queries = $"match={match.ToString()}&page={page}&per_page={perPage}";

            if(dnsRecordName != null)
            {
                queries += $"&name={dnsRecordName}";
            }
            if (order != null)
            {
                queries += $"&order={order}";
            }
            if (content != null)
            {
                queries += $"&content={content}";
            }
            if (type != null)
            {
                queries += $"&type={type.ToString()}";
            }
            if (proxied != null)
            {
                queries += $"&proxied={proxied}";
            }
            if (direction != null)
            {
                queries += $"&direction={direction.ToString()}";
            }

            var response = await this._httpClient.GetAsync(
                $"https://{Domain}/client/v4/zones/{zoneIdentifier}/dns_records?{queries}");

            return JsonConvert.DeserializeObject<ResponseBody<List<DnsRecord>>>(await response.Content.ReadAsStringAsync());
        }
    }

    public enum Match
    {
        all,
        any
    }

    public enum Direction
    {
        desc,
        asc
    }
}
