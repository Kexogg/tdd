using SkiaSharp;

namespace TagsCloudVisualization.Renderer;

public class Renderer : IRenderer
{
    private readonly SKBitmap bitmap;
    private readonly SKPaint paint;

    public Renderer(SKSize size)
    {
        bitmap = new SKBitmap((int)size.Width, (int)size.Height);
        paint = new SKPaint
        {
            Color = SKColors.Black,
            IsStroke = true,
            TextSize = 24
        };
        using var canvas = new SKCanvas(bitmap);
        {
            canvas.Clear(SKColors.LightGray);
        }
    }

    public void CreateRectangles(SKRect[] rectangles)
    {
        using var canvas = new SKCanvas(bitmap);
        foreach (var rectangle in rectangles)
        {
            if (rectangle.Left < 0 || rectangle.Top < 0 || rectangle.Right > bitmap.Width ||
                rectangle.Bottom > bitmap.Height)
                throw new ArgumentException("Rectangle is out of bounds");
            if (rectangle.Left >= rectangle.Right || rectangle.Top >= rectangle.Bottom)
                throw new ArgumentException("Rectangle is invalid");
            canvas.DrawRect(rectangle, paint);
            paint.Color = new SKColor((byte)(paint.Color.Red + 21), (byte)(paint.Color.Green + 43),
                (byte)(paint.Color.Blue + 67));
        }
    }

    public void CreateImage(string path)
    {
        using var image = SKImage.FromBitmap(bitmap);
        using var data = image.Encode(SKEncodedImageFormat.Png, 100);
        using var stream = File.OpenWrite(path);
        data.SaveTo(stream);
    }
}