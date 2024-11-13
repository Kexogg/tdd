using NUnit.Framework.Interfaces;
using TagsCloudVisualization.Layouter;

namespace TagsCloudVizualizationTests;

[TestFixture]
public class CircularCloudLayouterTests
{
    [SetUp]
    public void SetUp()
    {
        layouter = new CircularCloudLayouter(new SKPoint(500, 500));
    }

    [TearDown]
    public void TearDown()
    {
        if (TestContext.CurrentContext.Result.Outcome.Status != TestStatus.Failed) return;

        if (!Directory.Exists("tests"))
            Directory.CreateDirectory("tests");
        var filename = "tests/layouter_" + TestContext.CurrentContext.Test.ID + ".png";
        var renderer = new TagsCloudVisualization.Renderer.Renderer(new SKSize(1000, 1000));
        renderer.CreateRectangles(layouter.GetRectangles());
        renderer.CreateImage(filename);

        var path = Path.Combine(Directory.GetCurrentDirectory(), filename);
        Console.WriteLine($"Tag cloud visualization saved to file {path}");
    }

    private CircularCloudLayouter layouter;

    [Test]
    public void Constructor_ShouldCreateLayouter()
    {
        AssertionExtensions.Should((object)layouter).NotBeNull();
    }

    [Test]
    public void PutNextRectangle_ShouldThrowArgumentException_WhenSizeNotPositive()
    {
        Action action = () => layouter.PutNextRectangle(new SKSize(0, 100));
        action.Should().Throw<ArgumentException>();
    }

    [Test]
    public void PutNextRectangle_ShouldReturnRectangles()
    {
        var rectangles = new List<SKRect>();
        for (var i = 0; i < 10; i++)
        {
            rectangles.Add(layouter.PutNextRectangle(new SKSize(10, 10)));
        }

        rectangles.Should().HaveCount(10);
    }

    [Test]
    public void PutNextRectangle_ShouldNotIntersectRectangles()
    {
        var rectangles = new List<SKRect>();
        for (var i = 0; i < 10; i++)
        {
            rectangles.Add(layouter.PutNextRectangle(new SKSize(10, 10)));
        }

        for (var i = 0; i < rectangles.Count; i++)
        {
            for (var j = i + 1; j < rectangles.Count; j++)
            {
                rectangles[i].IntersectsWith(rectangles[j]).Should().BeFalse();
            }
        }
    }

    [Test]
    [Repeat(3)]
    public void PutNextRectangle_ShouldGenerateDenseLayout()
    {
        var rectangles = new List<SKRect>();
        float totalRectArea = 0;
        var random = new Random();
        for (var i = 0; i < 100; i++)
        {
            var size = new SKSize(random.Next(10, 100), random.Next(10, 100));
            var rect = layouter.PutNextRectangle(size);
            rectangles.Add(rect);
            totalRectArea += size.Width * size.Height;
        }

        var boundingCircleRadius = rectangles.Max(DistanceToCenter);
        var boundingCircleArea = Math.PI * Math.Pow(boundingCircleRadius, 2);

        var density = totalRectArea / boundingCircleArea;
        density.Should().BeGreaterOrEqualTo(0.7f);
    }

    [Test]
    [Repeat(3)]
    public void PutNextRectangle_ShouldPlaceRectanglesInCircle()
    {
        var rectangles = new List<SKRect>();
        var random = new Random();
        for (var i = 0; i < 100; i++)
        {
            var size = new SKSize(random.Next(10, 100), random.Next(10, 100));
            var rect = layouter.PutNextRectangle(size);
            rectangles.Add(rect);
        }

        var maxDistanceFromCenter = rectangles.Max(DistanceToCenter);
        const int expectedMaxDistance = 500;

        maxDistanceFromCenter.Should().BeLessOrEqualTo(expectedMaxDistance);
    }

    private static float DistanceToCenter(SKRect rect)
    {
        var center = new SKPoint(500, 500);
        var rectCenter = new SKPoint(rect.MidX, rect.MidY);
        return SKPoint.Distance(center, rectCenter);
    }
}