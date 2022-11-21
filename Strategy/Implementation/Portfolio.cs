using MathNet.Numerics.Statistics;
using Strategy.Interfaces;
using Strategy.Data;
using Strategy.Exceptions;

namespace Strategy.Implementation
{
    public class Portfolio : IPortfolio
    {
        private readonly IEnumerable<AssetExperiment> experiments;
        public ExperimentStatus Status { get; private set; } = ExperimentStatus.NotStarted;

        public Portfolio(IEnumerable<Asset> assets, uint scenariosCount, uint timeStepsCount)
        {
            experiments = assets.Select(asset => new AssetExperiment(
                asset, new Experiment(scenariosCount, timeStepsCount, asset.Mean, asset.Std))).ToList();
        }

        public async Task Simulate()
        {
            Status = ExperimentStatus.InProgress;
            await Task.WhenAll(experiments.Select(x => x.Experiment.RunAsync()));
            Status = ExperimentStatus.Completed;
        }

        public IEnumerable<AssetEstimation> GetPortfolioResults()
        {
            if (Status != ExperimentStatus.Completed)
            {
                throw new ExperimentNotCompletedException("Experiments in portfolio not completed.");
            }

            return experiments.Select(x =>
            {
                var samples = x.Experiment.GetExperimentResult();
                return new AssetEstimation(
                    x.Asset.Name,
                    x.Asset.StartAmount,
                    samples.Any() ? Statistics.Percentile(samples, 1) : null,
                    samples.Any() ? Statistics.Percentile(samples, 5) : null);
            });
        }
    }
}
