using SkiaSharp;

namespace TagsCloudVisualization.Render;

public interface IRenderer
{
    void CreateRectangles(SKRect[] rectangles);
    void CreateImage(string path);
}