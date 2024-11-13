using NUnit.Framework.Interfaces;
using TagsCloudVisualization.Renderer;

namespace TagsCloudVisualizationTests;

[TestFixture]
public class RendererTests
{
    [SetUp]
    public void SetUp()
    {
        render = new Renderer(new SKSize(100, 100));
    }

    [TearDown]
    public void TearDown()
    {
        if (TestContext.CurrentContext.Result.Outcome.Status != TestStatus.Failed) return;

        if (!Directory.Exists("tests"))
            Directory.CreateDirectory("tests");
        var filename = "tests/renderer_" + TestContext.CurrentContext.Test.ID + ".png";
        render.CreateImage(filename);
        var path = Path.Combine(Directory.GetCurrentDirectory(), filename);
        Console.WriteLine($"Attempted to save result to file {path}");


        if (File.Exists("image.png"))
            File.Delete("image.png");
    }

    private Renderer render;

    [Test]
    public void CreateImage_ShouldCreateImage()
    {
        render.CreateImage("image.png");
        Assert.That(File.Exists("image.png"));
    }

    [TestCase(0, 200)]
    [TestCase(-1, 50)]
    public void CreateRectangles_ShouldThrowException_WhenRectanglesAreOutOfBounds(int topLeft, int bottomRight)
    {
        Assert.Throws<ArgumentException>(() =>
            render.CreateRectangles([new SKRect(topLeft, topLeft, bottomRight, bottomRight)]));
    }

    [TestCase(0, 0, 0, 0)]
    [TestCase(50, 50, 0, 0)]
    [TestCase(50, 0, 50, 0)]
    public void CreateRectangles_ShouldThrowException_WhenRectanglesAreInvalid(int topLeft, int topRight,
        int bottomLeft, int bottomRight)
    {
        Assert.Throws<ArgumentException>(() =>
            render.CreateRectangles([new SKRect(topLeft, topRight, bottomLeft, bottomRight)]));
    }
}