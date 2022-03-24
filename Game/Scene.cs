using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using Game.Rendering;

namespace Game;

public class Scene
{
  private readonly Shader _shader = Shader.Load(ShaderDefault.Vertex, ShaderDefault.Fragment);
  private float _width;
  private float _height;
  private float _scale;

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
    scene._width = width;
    scene._height = height;
    scene._scale = scale;
    return scene;
  }

  public void Clear()
  {
    GL.ClearColor(1f, 0.5f, 0.5f, 1f);
    GL.ClearDepth(1);
    GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
  }

  public Actor CreateActor() =>
    Actor.Create(_shader, new Vector3(_width, _height, _scale));

  private void Camera(float x, float y)
   => _shader.Camera(x, y);

    public void ShakeCameraRandomly(float range)
  {
    var random = new Random();
    var between = (range + range + 1);
    var x = (float) random.NextDouble() * between - range;
    var y = (float) random.NextDouble() * between - range;
    Camera(x, y);
  }
}