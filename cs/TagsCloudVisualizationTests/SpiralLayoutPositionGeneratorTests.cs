using TagsCloudVisualization.PositionGenerator;

namespace TagsCloudVisualizationTests;

[TestFixture]
public class SpiralLayoutPositionGeneratorTests
{
    [TestCase(0, 0)]
    [TestCase(1, 0)]
    [TestCase(10, 10)]
    public void GetNextPosition_ReturnsCenterPosition_OnFirstCall(int centerX, int centerY)
    {
        var center = new SKPoint(centerX, centerY);
        var generator = new SpiralLayoutPositionGenerator(center);

        var position = generator.GetNextPosition();

        position.Should().Be(center);
    }

    [TestCase(2, 0.019601332f, 0.0039733867f)]
    [TestCase(50, 0.1418311f, -0.47946215f)]
    [TestCase(100, -0.8390715f, -0.5440211f)]
    public void GetNextPosition_ReturnsCorrectPosition_OnSubsequentCalls(int iterations, float expectedX, float expectedY)
    {
        var center = new SKPoint(0, 0);
        var generator = new SpiralLayoutPositionGenerator(center, 0.1);

        for (var i = 0; i < iterations; i++) generator.GetNextPosition();

        var position = generator.GetNextPosition();
        position.Should().Be(new SKPoint(expectedX, expectedY));
    }
    

    [Test]
    public void GetNextPosition_ReturnsCorrectPosition_WithNonZeroCenter_OnSubsequentCalls()
    {
        var center = new SKPoint(10, 10);
        var generator = new SpiralLayoutPositionGenerator(center, 0.1);

        generator.GetNextPosition();
        var position = generator.GetNextPosition();

        position.Should().Be(new SKPoint(10.00995f, 10.0009985f));
    }

    [Test]
    public void GetNextPosition_ReturnsCorrectPosition_WithSpecifiedStep()
    {
        var center = new SKPoint(0, 0);
        var generator = new SpiralLayoutPositionGenerator(center, 0.5);

        generator.GetNextPosition();
        var position = generator.GetNextPosition();

        position.Should().Be(new SKPoint(0.21939564f, 0.11985639f));
    }
}