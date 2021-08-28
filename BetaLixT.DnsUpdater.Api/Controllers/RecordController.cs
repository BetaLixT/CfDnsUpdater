using BetaLixT.DnsUpdater.Api.Models.ApiRequests;
using BetaLixT.DnsUpdater.Api.Models.ApiResponsesCommon;
using BetaLixT.DnsUpdater.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BetaLixT.DnsUpdater.Api.Controllers
{
    [Route("api/records")]
    [ApiController]
    public class RecordController : ControllerBase
    {
        private readonly DnsUpdaterService _dnsUpdaterService;
        public RecordController(DnsUpdaterService dnsUpdaterService)
        {
            this._dnsUpdaterService = dnsUpdaterService;
        }

        [HttpGet]
        public async Task<IActionResult> ListAsync()
        {
            return this.Ok(new SuccessResponseContent<List<Database.Entities.DnsRecord>>(await this._dnsUpdaterService.ListRecordsAsync()));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] NewRecordRequestBody requestBody)
        {
            return this.Ok(new SuccessResponseContent<Database.Entities.DnsRecord>(
                await this._dnsUpdaterService.CreateRecordsAsync(requestBody.ZoneId, requestBody.RecordName)));
        }

        [HttpPost("sync")]
        public async Task<IActionResult> SyncRecordsAsync()
        {
            await this._dnsUpdaterService.UpdateAllRecordsAsync();
            return this.Ok(new SuccessResponseContent());
        }
    }
}
