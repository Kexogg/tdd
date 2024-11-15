using SkiaSharp;

namespace TagsCloudVisualization.PositionGenerator;

public class SpiralLayoutPositionGenerator : IPositionGenerator
{
    private double angle;
    private readonly SKPoint center;
    private readonly double step;

    public SpiralLayoutPositionGenerator(SKPoint center, double step = 0.01)
    {
        this.center = center;
        this.step = step;
    }

    public SKPoint GetNextPosition()
    {
        var radius = step * angle;
        var x = (float)(center.X + radius * Math.Cos(angle));
        var y = (float)(center.Y + radius * Math.Sin(angle));

        angle += step;

        return new SKPoint(x, y);
    }
}