using SkiaSharp;
using TagsCloudVisualization.PositionGenerator;

namespace TagsCloudVisualization.Layouter;

public interface ICircularCloudLayouter
{
    SKRect PutNextRectangle(SKSize rectangleSize);
    SKRect[] GetRectangles();
}