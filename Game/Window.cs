using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;

namespace Game;

public class Window : GameWindow
{
  private Scene _scene = default!;
  private Game _game = default!;
  private Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
    : base(gameWindowSettings, nativeWindowSettings)
  {
  }

  public static Window Create()
  {
    var nativeWindowSettings = new NativeWindowSettings()
    {
      Size = new Vector2i(640, 360),
      Title = "PolySnake",
      Flags = ContextFlags.ForwardCompatible, // This is needed to run on macos
    };
    return new Window(GameWindowSettings.Default, nativeWindowSettings);
  }

  protected override void OnLoad()
  {
    base.OnLoad();
    _scene = Scene.Create(1000, 500, 1f);
    _game = Game.Create(_scene);
  }

  protected override void OnUpdateFrame(FrameEventArgs e)
  {
    base.OnUpdateFrame(e);
    if (KeyboardState.IsKeyDown(Keys.Escape))
      Close();
    var direction = 0f;
    if (KeyboardState.IsKeyDown(Keys.A))
      direction = 1f;
    if (KeyboardState.IsKeyDown(Keys.D))
      direction = -1f;
    if (KeyboardState.IsKeyDown(Keys.R))
      _game.Reset();
    _game.Snake.Move((float)e.Time, direction);
    //_game.CheckCollide();
  }

  protected override void OnResize(ResizeEventArgs e)
  {
    base.OnResize(e);
    float fWidth = e.Width;
    float fHeight = e.Height;

    const float targetAspectRatio = 16.0f / 9.0f;
    var viewWidth = fWidth;
    var viewHeight = fWidth / targetAspectRatio;

    if (viewHeight > fHeight)
    {
      viewWidth = fHeight * targetAspectRatio;
      viewHeight = fHeight;
    }

    var viewX = (fWidth / 2) - (viewWidth / 2);
    var viewY = (fHeight / 2) - (viewHeight / 2);

    GL.Viewport((int) viewX, (int) viewY, (int) viewWidth, (int) viewHeight);
  }

  protected override void OnRenderFrame(FrameEventArgs e)
  {
    base.OnRenderFrame(e);
    _scene.Clear();
    _game.Draw();
    SwapBuffers();
  }
}