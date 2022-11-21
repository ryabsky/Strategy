namespace StrategyService.Models
{
    public class PortfolioModel
    {
        public uint T { get; set; }
        public uint S { get; set; }
        public ICollection<AssetModel> assets { get; set; } = new List<AssetModel>();
    }
}