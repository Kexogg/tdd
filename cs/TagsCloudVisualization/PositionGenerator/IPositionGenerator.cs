using SkiaSharp;

namespace TagsCloudVisualization.PositionGenerator;

public interface IPositionGenerator
{
    SKPoint GetNextPosition();
}