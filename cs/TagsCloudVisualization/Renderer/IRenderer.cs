using SkiaSharp;

namespace TagsCloudVisualization.Renderer;

public interface IRenderer
{
    void CreateRectangles(SKRect[] rectangles);
    void CreateImage(string path);
}