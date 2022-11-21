using MathNet.Numerics.Distributions;
using Strategy.Interfaces;

namespace Strategy.Implementation
{
    public class StrategySimulation : ISimulation
    {
        private readonly uint timeStepsCount;
        private readonly IContinuousDistribution distribution;
        private readonly double startAmount;

        public StrategySimulation(double startAmount, uint timeStepsCount, IContinuousDistribution distribution)
        {
            this.timeStepsCount = timeStepsCount;
            this.distribution = distribution;
            this.startAmount = startAmount;
        }

        public double GetOverallReturn()
        {
            // Based on the formula, OverallReturn does not depend on the startAmount
            // But it got included here to show that it is possible to use it.

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
