using BetaLixT.DnsUpdater.Api.Database.Context;
using BetaLixT.DnsUpdater.Api.Database.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BetaLixT.DnsUpdater.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // Uncomment once services have been implemented and dependencies added
            InitializeServices(host);
            host.Run();
        }

        private static void InitializeServices(IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            try
            {

                var context = services.GetRequiredService<DatabaseContext>();
                context.Database.EnsureCreated();
                if (!context.Users.Any())
                {
                    context.Users.Add(new User { Username = "admin", PasswordHash = "admin" });
                    context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred initializing the database.");
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
