using System;
using Microsoft.EntityFrameworkCore;

namespace Rss.Core.Models.Context
{
    public sealed class RssDbContext : DbContext
    {
        private readonly string _conntextionString;

        public RssDbContext(string conntextionString)
        {
            if (string.IsNullOrEmpty(conntextionString))
                throw new ArgumentOutOfRangeException(nameof(conntextionString));

            _conntextionString = conntextionString;
        }

        public DbSet<RssRecord> RssRecords { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_conntextionString);
        }
    }
}
