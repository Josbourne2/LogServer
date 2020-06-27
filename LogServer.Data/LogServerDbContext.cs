using System;
using System.Collections.Generic;
using System.Text;
using LogServer.Core;
using Microsoft.EntityFrameworkCore;

namespace LogServer.Data
{
    public class LogServerDbContext : DbContext
    {
        public LogServerDbContext()
        {
        }

        public LogServerDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LogEvent>()
                .Property(e => e.Id)
                .UseIdentityColumn();

            modelBuilder.Entity<LogEvent>()
                .Property(e => e.MessageTemplate)
                .HasColumnType("VARCHAR(255)");

            modelBuilder.Entity<LogEvent>()
                .Property(e => e.Properties)
                .HasColumnType("VARCHAR(4000)");
        }

        public DbSet<LogEvent> LogEvents { get; set; }
    }
}