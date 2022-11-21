using MathNet.Numerics.Distributions;
using Strategy.Interfaces;
using Strategy.Implementation;

namespace Strategy.Test.Mocks
{
    public class MockSimulationFactory : ISimulationFactory
    {
        private readonly IContinuousDistribution distribution;

        public MockSimulationFactory(IContinuousDistribution distribution)
        {
            this.distribution = distribution;
        }

        public ISimulation CreateSimulation(double startAmount, uint timeStepsCount)
        {
            return new StrategySimulation(startAmount, timeStepsCount, distribution);
        }
    }
}