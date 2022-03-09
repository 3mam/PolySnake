using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using Game.Rendering;
using Vector2 = System.Numerics.Vector2;

namespace Game;

public class Scene
{
  private readonly Shader _shader = Shader.Load(ShaderDefault.Vertex, ShaderDefault.Fragment);
  public float Width;
  public float Height;
  private float _scale;
  private Vector2 _camera = new();

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
    scene.Width = width;
    scene.Height = height;
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
    Actor.Create(_shader, new Vector3(Width, Height, _scale));

  public void Camera(float x, float y)
  {
    _shader.Camera(x, y);
    _camera.X = x;
    _camera.Y = y;
  }
}