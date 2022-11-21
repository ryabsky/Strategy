using Strategy.Interfaces;
using Strategy.Exceptions;
using Strategy.Data;

namespace Strategy.Implementation
{
    public class Experiment : IExperiment
    {
        private readonly IEnumerable<ISimulation> simulations;
        private readonly ICollection<Task<double>> simulationTasks = new List<Task<double>>();
        public ExperimentStatus Status { get; private set; } = ExperimentStatus.NotStarted;
        public uint ScenariosCount { get; private set; }

        public Experiment(uint scenariosCount, uint timeStepsCount)
            : this(scenariosCount, timeStepsCount, 0, 1) { }

        public Experiment(uint scenariosCount, uint timeStepsCount, double mean, double std)
        {
            var factory = new SimulationFactory(mean, std);
            ScenariosCount = scenariosCount;
            simulations = new object[scenariosCount].Select(x => factory.CreateSimulation(timeStepsCount));
        }

        public Experiment(uint scenariosCount, uint timeStepsCount, ISimulationFactory simulationFactory)
        {
            ScenariosCount = scenariosCount;
            simulations = new object[scenariosCount].Select(x => simulationFactory.CreateSimulation(timeStepsCount));
        }

        public async Task RunAsync()
        {
            Status = ExperimentStatus.InProgress;

            foreach (var simulation in simulations)
            {
                var simulationTask = new Task<double>(simulation.GetOverallReturn);
                simulationTask.Start();
                simulationTasks.Add(simulationTask);
            }

            await Task.WhenAll(simulationTasks);
            Status = ExperimentStatus.Completed;
        }

        public List<double> GetExperimentResult()
        {
            if (Status != ExperimentStatus.Completed)
            {
                throw new ExperimentNotCompletedException("Experiment not completed.");
            }

            return simulationTasks.Select(x => x.Result).ToList();
        }
	}
}
