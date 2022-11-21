using Strategy.Interfaces;

namespace Strategy.Data
{
	public class AssetExperiment
	{
        public Asset Asset { get; }
        public IExperiment Experiment { get; }

		public AssetExperiment(Asset asset, IExperiment experiment)
		{
			Asset = asset;
			Experiment = experiment;
		}
    }
}
