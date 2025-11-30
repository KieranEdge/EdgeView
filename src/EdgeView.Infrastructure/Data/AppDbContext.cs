using System;
using EdgeView.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;


namespace EdgeView.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<CacheEntry> CacheEntries => Set<CacheEntry>();
    }
}

