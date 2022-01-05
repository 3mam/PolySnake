using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;
using PolySnake.Tools;

namespace PolySnake;

public class Window : GameWindow
{
  int _program;
  int _rotate;
  float _rot;

  Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
      : base(gameWindowSettings, nativeWindowSettings) { }

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
    var vertex = new []{
      -0.5f,-0.5f,
      -0.5f, 0.5f,
      0.5f, 0.5f,

      0.5f, 0.5f,
      0.5f, -0.5f,
      -0.5f, -0.5f,
    };

    var buffer = GL.GenBuffer();
    GL.BindBuffer(BufferTarget.ArrayBuffer, buffer);
    GL.BufferData(BufferTarget.ArrayBuffer, vertex.Length * sizeof(float), vertex, BufferUsageHint.StaticDraw);

    var vertexArray = GL.GenVertexArray();
    GL.BindVertexArray(vertexArray);

    GL.Enable(EnableCap.Blend);
    GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
    GL.Enable(EnableCap.DepthTest);
    GL.DepthMask(false);

    _program = Gl.CreateShader(Shader.Vertex, Shader.Fragment);
    GL.UseProgram(_program);
    
    _rotate = GL.GetUniformLocation(_program, "rotate");
    var sVertexPosition = GL.GetAttribLocation(_program, "vertex_position");
    GL.EnableVertexAttribArray(sVertexPosition);
    GL.VertexAttribPointer(sVertexPosition, 2, VertexAttribPointerType.Float, false, 0, 0);
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
    GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
    GL.Uniform1(_rotate, _rot);
    _rot+=0.01f;
    SwapBuffers();
  }
}
