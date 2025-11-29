using EdgeView.Application.Interfaces;
using EdgeView.Domain.Entities;

namespace EdgeView.Application.Services;

public class BinDayService : IBinDayService
{
    private readonly IBinScraper _scraper;

    public BinDayService(IBinScraper scraper)
    {
        _scraper = scraper;
    }

    public async Task<BinCollection> GetNextBinAsync(string houseNumber, string postcode)
    {
        return await _scraper.GetNextBinAsync(houseNumber, postcode);
    }
}
