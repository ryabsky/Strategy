using Strategy.Data;

namespace Strategy
{
    public interface IPortfolioService
    {
        ulong Submit(IEnumerable<Asset> assets, uint scenariosCount, uint timeStepsCount);
        string? GetStatus(ulong portfolioId);
        IEnumerable<AssetEstimation>? GetResult(ulong portfolioId);
    }
}
