namespace Strategy.Interfaces
{
    public interface ISimulationFactory
    {
        ISimulation CreateSimulation(double startAmount, uint timeStepsCount);
    }
}
