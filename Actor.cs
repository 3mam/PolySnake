using OpenTK.Graphics.OpenGL4;
using PolySnake.Rendering;
using OpenTK.Mathematics;
using PolySnake.Collision;

namespace PolySnake;

public class Actor
{
  private readonly int _buffer = GL.GenBuffer();
  private int _bufferSize;
  private Shader _shader = default!;
  private Vector2 _position;
  private int _index;
  private float _alpha = 1;
  private float _angle;
  private Vector2 _size = new Vector2(1f, 1f);
  private float _radius;

  public CollideCircle Boundary => new CollideCircle(_position, _radius);

  public static Actor Create(Shader shader) => new()
  {
    _shader = shader,
  };

  public void UploadData(float[] data)
  {
    GL.BindBuffer(BufferTarget.ArrayBuffer, _buffer);
    GL.BufferData(BufferTarget.ArrayBuffer, data.Length * sizeof(float), data, BufferUsageHint.StaticDraw);
    _bufferSize = data.Length / 4;
  }

  public void Position(Vector2 cord)
  {
    _position = cord;
  }

  public void Rotation(float degrees)
  {
    _angle = degrees;
  }

  public void Transparency(float alpha = 1)
  {
    _alpha = alpha;
  }

  public void Index(int index = 0)
  {
    _index = index;
  }

  public void Scale(Vector2 size)
  {
    _size = size;
  }

  public void Radius(float radius)
  {
    _radius = radius;
  }

  public bool Collide(Actor actor) => Boundary == actor.Boundary;

  public void Show()
  {
    GL.BindBuffer(BufferTarget.ArrayBuffer, _buffer);
    _shader.Active();
    _shader.Position(_position.X, _position.Y);
    _shader.Rotate(_angle);
    _shader.Alpha(_alpha);
    _shader.Index(_index);
    _shader.Size(_size.X, _size.Y);
    GL.DrawArrays(PrimitiveType.Triangles, 0, _bufferSize);
  }
}