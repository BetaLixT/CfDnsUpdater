using BetaLixT.DnsUpdater.Api.Database.Context;
using BetaLixT.DnsUpdater.Api.Helpers;
using BetaLixT.DnsUpdater.Api.Options;
using BetaLixT.DnsUpdater.Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BetaLixT.DnsUpdater.Api.CronJobs
{
    public class UpdateDns : CronService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        public UpdateDns(IOptions<UpdateDnsOptions> options, IServiceScopeFactory scopeFactory) : base(options.Value.Schedule)
        {
            this._scopeFactory = scopeFactory;
        }

        protected override async Task ProcessAsync()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dnsUpdaterService = scope.ServiceProvider.GetRequiredService<DnsUpdaterService>();
                await dnsUpdaterService.UpdateAllRecordsAsync();
            }
            
        }
    }
}
