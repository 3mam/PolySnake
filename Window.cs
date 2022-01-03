using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;

class Window : GameWindow
{
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
      Flags = ContextFlags.ForwardCompatible,// This is needed to run on macos
    };
    return new Window(GameWindowSettings.Default, nativeWindowSettings);
  }
  protected override void OnLoad()
  {
    base.OnLoad();
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

  }

  protected override void OnRenderFrame(FrameEventArgs e)
  {
    base.OnRenderFrame(e);
    var delta = (float)e.Time;
    GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
    GL.ClearColor(1f, 0.5f, 0.5f, 1f);
    SwapBuffers();
  }
}
