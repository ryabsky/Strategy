using Strategy.Data;

namespace Strategy.Interfaces
{
    public interface IExperiment
    {
        public ExperimentStatus Status { get; }
        public uint ScenariosCount { get; }
        Task RunAsync();
        public List<double> GetExperimentResult();
    }
}
