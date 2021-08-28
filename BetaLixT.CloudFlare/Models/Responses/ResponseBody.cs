using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BetaLixT.CloudFlare.Models.Responses
{
    public class ResponseBody <T>
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("errors")]
        public List<CfError> Errors { get; set; }

        [JsonProperty("messages")]
        public List<string> Messages { get; set; }

        [JsonProperty("result")]
        public T Result { get; set; }
    }

    public struct CfError
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
