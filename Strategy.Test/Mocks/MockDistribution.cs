using MathNet.Numerics.Distributions;
namespace Strategy.Test.Mocks
{
	public class MockDistribution : IContinuousDistribution
	{
        private double[] numbers;
        private int state;

		public MockDistribution(double[] numbers)
		{
            this.numbers = numbers;
            this.state = 0;
        }

        public double Sample()
        {
            double number = numbers[state];
            state = (state + 1) % numbers.Length;
            return number;
        }

        public double Mode => throw new NotImplementedException();
        public double Minimum => throw new NotImplementedException();
        public double Maximum => throw new NotImplementedException();
        public double Mean => throw new NotImplementedException();
        public double Variance => throw new NotImplementedException();
        public double StdDev => throw new NotImplementedException();
        public double Entropy => throw new NotImplementedException();
        public double Skewness => throw new NotImplementedException();
        public double Median => throw new NotImplementedException();
        public Random RandomSource { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double CumulativeDistribution(double x) => throw new NotImplementedException();
        public double Density(double x) => throw new NotImplementedException();
        public double DensityLn(double x) => throw new NotImplementedException();
        public void Samples(double[] values) => throw new NotImplementedException();
        public IEnumerable<double> Samples() => throw new NotImplementedException();
    }
}

