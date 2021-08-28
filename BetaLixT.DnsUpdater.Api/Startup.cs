using BetaLixT.CloudFlare;
using BetaLixT.CloudFlare.Options;
using BetaLixT.DnsUpdater.Api.CronJobs;
using BetaLixT.DnsUpdater.Api.Database.Context;
using BetaLixT.DnsUpdater.Api.Options;
using BetaLixT.DnsUpdater.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BetaLixT.DnsUpdater.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData, Environment.SpecialFolderOption.Create);
            Console.WriteLine(appData);
            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlite($"Data Source={appData}/database.db")
            );

            services.AddOptions();
            services.Configure<CloudFlareOptions>(Configuration.GetSection(CloudFlareOptions.OptionsKey));
            services.Configure<UpdateDnsOptions>(Configuration.GetSection(UpdateDnsOptions.OptionsKey));
            services.AddTransient<CloudFlareClient>();
            services.AddTransient<DnsUpdaterService>();
            services.AddControllers();
            services.AddHostedService<UpdateDns>();

            // - Adding Swagger service
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "BetaLixT DnsUpdater API"
                });

                // Set the comments path for the Swagger JSON and UI.
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection();

            app.UseRouting();

            // app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(s => s.SwaggerEndpoint("/swagger/v1/swagger.json", "api"));
        }
    }
}
