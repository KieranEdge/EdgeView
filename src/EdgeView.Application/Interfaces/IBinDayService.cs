using EdgeView.Domain.Entities;

namespace EdgeView.Application.Interfaces;

public interface IBinDayService
{
    Task<BinCollection> GetNextBinAsync(string houseNumber, string postcode);
}
