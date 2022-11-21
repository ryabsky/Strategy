namespace Strategy.Data
{
    public class Asset
    {
        public Asset(string name, double startAmount, double mean, double std)
        {
            Name = name;
            StartAmount = startAmount;
            Mean = mean;
            Std = std;
        }

        public string Name { get; }
        public double StartAmount { get; }
        public double Mean { get; }
        public double Std { get; }
    }
}
