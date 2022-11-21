using MathNet.Numerics.Distributions;
using Strategy.Interfaces;

namespace Strategy.Implementation
{
	public class StrategySimulation : ISimulation
    {
        private readonly uint timeStepsCount;
        private readonly IContinuousDistribution distribution;

        public StrategySimulation(uint timeStepsCount, IContinuousDistribution distribution)
        {
            this.timeStepsCount = timeStepsCount;
            this.distribution = distribution;
        }

        public double GetOverallReturn()
        {
            double pow = 0.0;

            for (int i = 0; i < timeStepsCount; i++)
            {
                double x = distribution.Sample();
                pow += x;
            }

            return Math.Exp(pow) - 1;
        }
	}
}
