namespace Strategy.Interfaces
{
    public interface ISimulationFactory
    {
        ISimulation CreateSimulation(uint timeStepsCount);
    }
}
