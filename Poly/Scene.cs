using Game.Interface;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace Poly;

public class Scene : IScene
{
  private readonly Shader _shader = Shader.Load(ShaderDefault.Vertex, ShaderDefault.Fragment);
  private Vector2 _camera;

  private Scene()
  {
    GL.Enable(EnableCap.Blend);
    GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
    GL.Enable(EnableCap.DepthTest);
    GL.DepthMask(true);
    GL.DepthFunc(DepthFunction.Lequal);
    GL.DepthRange(-1, 1);

    GL.BindVertexArray(GL.GenVertexArray());
  }

  public static Scene Create(float width, float height, float scale)
  {
    var scene = new Scene();
    scene._shader.Dimensions(width, height, scale);
    return scene;
  }

  public void Clear()
  {
    GL.ClearColor(1f, 0.5f, 0.5f, 1f);
    GL.ClearDepth(1);
    GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
  }

  public Actor CreateActor() => new(_shader);

  public void Camera(Vector2 position)
  {
    _camera = position;
    _shader.Camera(_camera.X, _camera.Y);
  }

  public void ShakeCameraRandomly(float range)
  {
    if (range == 0)
      return;
    var random = new Random();
    var between = (range + range + 1);
    var x = (float) random.NextDouble() * between - range;
    var y = (float) random.NextDouble() * between - range;
    _shader.Camera(_camera.X + x, _camera.Y + y);
  }
}