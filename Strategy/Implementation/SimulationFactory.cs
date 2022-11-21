using MathNet.Numerics.Distributions;
using Strategy.Interfaces;

namespace Strategy.Implementation
{
    public class SimulationFactory : ISimulationFactory
    {
        private readonly double mean;
        private readonly double std;

        public SimulationFactory(double mean, double std)
        {
            this.mean = mean;
            this.std = std;
        }

        public ISimulation CreateSimulation(uint timeStepsCount)
        {
            return new StrategySimulation(timeStepsCount, new Normal(mean, std));
        }
    }
}

