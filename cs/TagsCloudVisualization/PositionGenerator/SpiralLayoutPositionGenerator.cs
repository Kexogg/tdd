using SkiaSharp;

namespace TagsCloudVisualization.PositionGenerator;

public class SpiralLayoutPositionGenerator(SKPoint center, double step = 0.01) : IPositionGenerator
{
    private double angle;

    public SKPoint GetNextPosition()
    {
        var radius = step * angle;
        var x = (float)(center.X + radius * Math.Cos(angle));
        var y = (float)(center.Y + radius * Math.Sin(angle));

        angle += step;

        return new SKPoint(x, y);
    }
}