namespace StrategyService.Models
{
    public class EstimationModel
    {
        public string Name { get; set; } = string.Empty;
        public double StartAmount { get; set; }
        public double? Percentile1 { get; set; } = null;
        public double? Percentile5 { get; set; } = null;
    }
}
