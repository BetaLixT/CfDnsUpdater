using BetaLixT.DnsUpdater.Api.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BetaLixT.DnsUpdater.Api.Database.Context
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; private set; }
        public DbSet<DnsRecord> DnsRecords { get; private set; }
        public DbSet<Log> Logs { get; private set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

    }
}
