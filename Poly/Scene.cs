using Game.Interface;
using OpenTK.Graphics.OpenGL4;
using Game.Math;

namespace Poly;

public class Scene : IScene
{
  private readonly Shader _shader = Shader.Load(ShaderDefault.Vertex, ShaderDefault.Fragment);

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
    GL.ClearColor(0, 0, 0, 1f);
    GL.ClearDepth(1);
    GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
  }

  public Actor CreateActor() => new(_shader);

  public void Camera(Vector2 position)
  {
    _shader.Camera(position.X, position.Y);
  }
}