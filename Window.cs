using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;
using PolySnake.Rendering;

namespace PolySnake;

public class Window : GameWindow
{
  private Scene _scene = default!;
  Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
    : base(gameWindowSettings, nativeWindowSettings)
  {
  }

  public static Window Create()
  {
    var nativeWindowSettings = new NativeWindowSettings()
    {
      Size = new Vector2i(320, 180),
      Title = "PolySnake",
      Flags = ContextFlags.ForwardCompatible, // This is needed to run on macos
    };
    return new Window(GameWindowSettings.Default, nativeWindowSettings);
  }

  protected override void OnLoad()
  {
    base.OnLoad();
    _scene = Scene.Init();
  }

  protected override void OnUpdateFrame(FrameEventArgs e)
  {
    // Check if the Escape button is currently being pressed.
    if (KeyboardState.IsKeyDown(Keys.Escape))
    {
      // If it is, close the window.
      Close();
    }

    base.OnUpdateFrame(e);
  }

  protected override void OnResize(ResizeEventArgs e)
  {
    base.OnResize(e);
    double fWidth = e.Width;
    double fHeight = e.Height;

    const double targetAspectRatio = 16.0 / 9.0;
    double viewWidth = fWidth;
    double viewHeight = fWidth / targetAspectRatio;

    if (viewHeight > fHeight)
    {
      viewWidth = fHeight * targetAspectRatio;
      viewHeight = fHeight;
    }

    double viewX = (fWidth / 2) - (viewWidth / 2);
    double viewY = (fHeight / 2) - (viewHeight / 2);

    GL.Viewport((int) viewX, (int) viewY, (int) viewWidth, (int) viewHeight);
  }

  protected override void OnRenderFrame(FrameEventArgs e)
  {
    base.OnRenderFrame(e);
    //var delta = (float)e.Time;
    _scene.Clear();
    _scene.Show();
    SwapBuffers();
  }
}