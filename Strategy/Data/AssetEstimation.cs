namespace Strategy.Data
{
	public class AssetEstimation
    {
        public string Name { get; }
        public double StartAmount { get; }
        public double? Percentile1 { get; }
        public double? Percentile5 { get; }

        public AssetEstimation(string name, double startAmount, double? percentile1, double? percentile5)
		{
            Name = name;
            StartAmount = startAmount;
            Percentile1 = percentile1;
            Percentile5 = percentile5;
		}
	}
}
