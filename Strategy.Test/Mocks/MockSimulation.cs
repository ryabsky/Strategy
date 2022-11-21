using MathNet.Numerics.Distributions;
using Strategy.Interfaces;

namespace Strategy.Test.Mocks
{
    public class MockSimulation : ISimulation
    {
        private readonly IContinuousDistribution distribution;

        public MockSimulation(IContinuousDistribution distribution)
        {
            this.distribution = distribution;
        }

        public double GetOverallReturn()
        {
            return distribution.Sample();
        }
    }

    public class MockVeryLongSimulation : ISimulation
    {
        private readonly IContinuousDistribution distribution;

        public MockVeryLongSimulation(IContinuousDistribution distribution)
        {
            this.distribution = distribution;
        }

        public double GetOverallReturn()
        {
            Thread.Sleep(100500);
            return distribution.Sample();
        }
    }
}
