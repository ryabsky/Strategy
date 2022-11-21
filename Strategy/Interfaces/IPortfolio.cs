using Strategy.Data;

namespace Strategy.Interfaces
{
    public interface IPortfolio
    {
        Task Simulate();
        IEnumerable<AssetEstimation> GetPortfolioResults();
        ExperimentStatus Status { get; }
    }
}

