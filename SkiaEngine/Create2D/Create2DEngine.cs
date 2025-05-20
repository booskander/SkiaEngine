using System.Windows.Forms;
using System.Drawing;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using SkiaSharp.Views;

namespace SkiaEngine.Create2D;

internal class Create2DEngine
{
    private Form window = new Form();
    private SKGLControl glControl = new SKGLControl();
    public Create2DEngine()
    {
        window.Size = new Size(800, 600);
        window.Text = "Skia Engine Window";
        window.Controls.Add(glControl);
        window.Resize += Window_Resize;
        glControl.Size = window.Size;
        glControl.PaintSurface += Renderer;
        Thread looper = new Thread(Looper);
        Console.WriteLine("starting engine...");
        
        looper.Start();
        Application.Run(window);
    }

    // this thread is used to force refresh
    void Looper()
    {
        while (true)
        {
            Console.WriteLine("inside the looper thread");
            glControl.Invalidate();
        }
    }

    private void Renderer(object? sender, SKPaintGLSurfaceEventArgs e)
    {
        e.Surface.Canvas.Clear(SKColors.DarkGray);
        e.Surface.Canvas.DrawCircle(new SKPoint(50, 50), 50, new SKPaint()
        {
            Color = SKColors.Brown,
        });
    }

    private void Window_Resize(object sender, EventArgs e)
    {
        glControl.Size = window.Size;
    }
}