using System.Text.Json;
using EdgeView.Application.Interfaces;
using EdgeView.Domain.Entities;
using EdgeView.Infrastructure.Data;
using EdgeView.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace EdgeView.Infrastructure.Repositories;

public class CacheRepository : ICacheRepository
{
    private readonly AppDbContext _db;

    public CacheRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<BinCollection?> GetAsync(string cacheKey)
    {
        var entry = await _db.CacheEntries.FirstOrDefaultAsync(c =>
            c.CacheKey == cacheKey && c.ExpiresAt > DateTime.UtcNow);

        if (entry == null) return null;

        return JsonSerializer.Deserialize<BinCollection>(entry.JsonValue);
    }

    public async Task SaveAsync(string cacheKey, BinCollection data, TimeSpan duration)
    {
        var json = JsonSerializer.Serialize(data);

        var entry = new CacheEntry
        {
            CacheKey = cacheKey,
            JsonValue = json,
            ExpiresAt = DateTime.UtcNow.Add(duration),
            CreatedAt = DateTime.UtcNow
        };

        _db.CacheEntries.RemoveRange(_db.CacheEntries.Where(c => c.CacheKey == cacheKey));
        await _db.CacheEntries.AddAsync(entry);
        await _db.SaveChangesAsync();
    }
}
