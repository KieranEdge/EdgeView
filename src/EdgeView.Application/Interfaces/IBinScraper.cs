using EdgeView.Domain.Entities;

namespace EdgeView.Application.Interfaces;

public interface IBinScraper
{
    Task<BinCollection> GetNextBinAsync(string houseNumber, string postcode);
}
