using Game;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Poly;

public class Window : GameWindow
{
  private Game.Game _game = default!;

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
    var scene = Scene.Create(Settings.CenterWidth, Settings.CenterHeight, 1f);
    foreach (var name in Enum.GetValues(typeof(AssetList)))
    {
      if ((AssetList) name == AssetList.None)
        continue;
      var actor = scene.CreateActor();
      var obj = typeof(Assets).GetField(name.ToString() ?? string.Empty);
      var asset = (float[]) obj?.GetValue(null)!;
      actor.UploadData(asset);
      AssetManager.AddActor((AssetList) name, actor);
    }
    _game = new global::Game.Game(scene);
  }

  protected override void OnUpdateFrame(FrameEventArgs e)
  {
    base.OnUpdateFrame(e);
    _game.SnakeMoveStraight();

    if (KeyboardState.IsKeyPressed(Keys.Space) ||
        KeyboardState.IsKeyPressed(Keys.Enter))
      _game.Enter();
    if (KeyboardState.IsKeyPressed(Keys.Escape))
      _game.ToggleMenu();
    if (KeyboardState.IsKeyPressed(Keys.W) ||
        KeyboardState.IsKeyPressed(Keys.Up))
      _game.SelectNextOption();
    if (KeyboardState.IsKeyPressed(Keys.S) ||
        KeyboardState.IsKeyPressed(Keys.Down))
      _game.SelectPreviousOption();

    if (KeyboardState.IsKeyDown(Keys.A) ||
        KeyboardState.IsKeyDown(Keys.Left))
          _game.SnakeMoveLeft();
    if (KeyboardState.IsKeyDown(Keys.D) ||
        KeyboardState.IsKeyDown(Keys.Right))
          _game.SnakeMoveRight();

#if DEBUG
    if (KeyboardState.IsKeyDown(Keys.Q))
      Close();
#endif
    
    if (_game.Exit)
      Close();
    
    _game.Update((float) e.Time);
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
    _game.Draw();
    SwapBuffers();
  }
}