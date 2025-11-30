using EdgeView.Application.Interfaces;
using EdgeView.Domain.Entities;

namespace EdgeView.Application.Services;

public class BinDayService : IBinDayService
{
    private readonly IBinScraper _scraper;
    private readonly ICacheRepository _cacheRepo;

    public BinDayService(IBinScraper scraper, ICacheRepository cacheRepo)
    {
        _scraper = scraper;
        _cacheRepo = cacheRepo;
    }

    public async Task<BinCollection> GetNextBinAsync(string houseNumber, string postcode)
    {
        var cacheKey = $"BinCollection_{houseNumber}_{postcode}";

        // Try persistent cache first
        var cached = await _cacheRepo.GetAsync(cacheKey);
        if (cached != null)
            return cached;

        // Scrape fresh data
        var result = await _scraper.GetNextBinAsync(houseNumber, postcode);

        // Save to persistent cache
        await _cacheRepo.SaveAsync(cacheKey, result, TimeSpan.FromHours(12));

        return result;
    }
}
