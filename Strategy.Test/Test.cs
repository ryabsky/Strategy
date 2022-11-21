namespace Strategy.Test;
using Strategy.Test.Mocks;
using Moq;
using Strategy.Interfaces;
using Strategy.Implementation;
using Strategy.Exceptions;
using Strategy.Data;

public class Tests
{
    private Mock<ISimulationFactory> mockSimulationFactory;
    private Mock<ISimulationFactory> mockSimulationFactoryVeryLong;
    private double[] numbers = new[] { 0.1, -0.02, -0.08, 0.12, 0.006, -0.2 };
    private double tol = 1e-5;

    public Tests()
    {
        mockSimulationFactory = new Mock<SimulationFactory>(MockBehavior.Strict, 0.0, 1.0).As<ISimulationFactory>();
        mockSimulationFactory.Setup(m => m.CreateSimulation(It.IsAny<uint>()))
            .Returns(() => {
                var distribution = new MockDistribution(numbers);
                var mockSimulation = new MockSimulation(distribution);
                return mockSimulation;
            });

        mockSimulationFactoryVeryLong = new Mock<SimulationFactory>(MockBehavior.Strict, 0.0, 1.0).As<ISimulationFactory>();
        mockSimulationFactoryVeryLong.Setup(m => m.CreateSimulation(It.IsAny<uint>()))
            .Returns(() => {
                var distribution = new MockDistribution(numbers);
                var mockSimulation = new MockVeryLongSimulation(distribution);
                return mockSimulation;
            });
    }

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test_Experiment_ScenariosCount()
    {
        uint scenariosCount = 20;
        var experiment = new Experiment(scenariosCount, timeStepsCount: 30);
        Assert.That(experiment.ScenariosCount, Is.EqualTo(scenariosCount));
        Assert.That(experiment.Status, Is.EqualTo(ExperimentStatus.NotStarted));
    }

    [Test]
    public async Task Test_Experiment_Run()
    {
        var experiment = new Experiment(scenariosCount: 2, timeStepsCount: 5, mockSimulationFactory.Object);
        await experiment.RunAsync();

        Assert.That(experiment.Status, Is.EqualTo(ExperimentStatus.Completed));
    }

    [Test]
    public async Task Test_Experiment_GetExperimentResult()
    {
        uint scenariosCount = 2;

        var experiment = new Experiment(scenariosCount, timeStepsCount: 5, mockSimulationFactory.Object);
        await experiment.RunAsync();
        var result = experiment.GetExperimentResult();

        Assert.That(result.Count, Is.EqualTo(scenariosCount));
        Assert.That(result[0], Is.EqualTo(0.1).Within(tol));
        Assert.That(result[1], Is.EqualTo(0.1).Within(tol));
    }

    [Test]
    public void Test_Experiment_GetExperimentResultLong()
    {
        var veryLongExperiment = new Experiment(scenariosCount: 2, timeStepsCount: 5, mockSimulationFactoryVeryLong.Object);
        veryLongExperiment.RunAsync();

        Thread.Sleep(10);

        Assert.That(veryLongExperiment.Status, Is.EqualTo(ExperimentStatus.InProgress));
        Assert.Throws<ExperimentNotCompletedException>(() => veryLongExperiment.GetExperimentResult());
    }

    [Test]
    public void Test_Simulation()
    {
        uint timeStepsCount = 3;
        var simulation = new StrategySimulation(timeStepsCount, new MockDistribution(numbers));

        var result = simulation.GetOverallReturn();
        Assert.That(result, Is.EqualTo(0.0).Within(tol));
    }
}
