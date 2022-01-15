using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using PolySnake.Rendering;
using Vector2 = System.Numerics.Vector2;

namespace PolySnake;

public class Scene
{
  private readonly Shader _shader = Shader.Load(ShaderDefault.Vertex, ShaderDefault.Fragment);
  public float Width;
  public float Height;
  private float _scale;
  private readonly int _texturePalette;
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

    var palette = new byte[32 * 32 * 3];
    _texturePalette = GL.GenTexture();
    GL.ActiveTexture(TextureUnit.Texture0);
    GL.BindTexture(TextureTarget.Texture2D, _texturePalette);
    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int) TextureMinFilter.Nearest);
    GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, 32, 32, 0, PixelFormat.Rgb,
      PixelType.UnsignedByte, palette);
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

  public void UploadPalette(byte[] colors, int palette)
  {
    GL.ActiveTexture(TextureUnit.Texture0);
    GL.BindTexture(TextureTarget.Texture2D, _texturePalette);
    GL.TexSubImage2D(TextureTarget.Texture2D, 0, 0, palette, 32, 1, PixelFormat.Rgb, PixelType.UnsignedByte, colors);
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