using EdgeView.Domain.Entities;

namespace EdgeView.Application.Interfaces;

public interface ICacheRepository
{
    Task<BinCollection?> GetAsync(string cacheKey);
    Task SaveAsync(string cacheKey, BinCollection data, TimeSpan duration);
}
