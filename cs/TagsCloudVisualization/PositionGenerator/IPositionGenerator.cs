using SkiaSharp;

namespace TagsCloudVisualization.PositionGenerator;

public interface IPositionGenerator
{
    public SKPoint GetNextPosition();
}