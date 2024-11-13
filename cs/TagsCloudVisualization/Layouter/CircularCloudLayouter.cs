using SkiaSharp;
using TagsCloudVisualization.PositionGenerator;

namespace TagsCloudVisualization.Layouter;

public class CircularCloudLayouter : ICircularCloudLayouter
{
    private readonly IPositionGenerator positionGenerator;
    private readonly List<SKRect> rectangles;

    public CircularCloudLayouter(SKPoint center)
    {
        rectangles = [];
        positionGenerator = new SpiralLayoutPositionGenerator(center);
    }

    public SKRect PutNextRectangle(SKSize rectangleSize)
    {
        if (rectangleSize.Width <= 0 || rectangleSize.Height <= 0)
            throw new ArgumentException("Rectangle size must be positive", nameof(rectangleSize));

        SKRect rectangle;

        do
        {
            var centerOfRectangle = positionGenerator.GetNextPosition();
            var rectanglePosition = new SKPoint(centerOfRectangle.X - rectangleSize.Width / 2,
                centerOfRectangle.Y - rectangleSize.Height / 2);
            rectangle = new SKRect(
                rectanglePosition.X,
                rectanglePosition.Y,
                rectanglePosition.X + rectangleSize.Width,
                rectanglePosition.Y + rectangleSize.Height);
        } while (rectangles.Any(r => r.IntersectsWith(rectangle)));

        rectangles.Add(rectangle);
        return rectangle;
    }

    public SKRect[] GetRectangles()
    {
        return rectangles.ToArray();
    }
}