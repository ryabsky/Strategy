using Strategy.Data;
using Strategy.Interfaces;
using Strategy.Implementation;

namespace Strategy
{
    public class PortfolioService : IPortfolioService
    {
        ulong counter = 0;
        private static object sync = new object();
        private readonly Dictionary<ulong, IPortfolio> portfolios = new Dictionary<ulong, IPortfolio>();
        private readonly Dictionary<ulong, IEnumerable<AssetEstimation>> estimations = new Dictionary<ulong, IEnumerable<AssetEstimation>>();

        public PortfolioService()
        {
        }

        public ulong Submit(IEnumerable<Asset> assets, uint scenariosCount, uint timeStepsCount)
        {
            ulong id;
            lock (sync)
            {
                counter++;
                id = counter;
            }

            var portfolio = new Portfolio(assets, scenariosCount, timeStepsCount);

            portfolios.Add(id, portfolio);

            portfolio.Simulate();

            return id;
        }

        public string? GetStatus(ulong portfolioId)
        {
            if (portfolios.ContainsKey(portfolioId))
            {
                return portfolios[portfolioId].Status.ToString();
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<AssetEstimation>? GetResult(ulong portfolioId)
        {
            if (!portfolios.ContainsKey(portfolioId))
            {
                return null;
            }

            if (estimations.ContainsKey(portfolioId))
            {
                return estimations[portfolioId];
            }
            else
            {
                var results = portfolios[portfolioId].GetPortfolioResults();
                estimations[portfolioId] = results;
                return results;
            }
        }
    }
}
