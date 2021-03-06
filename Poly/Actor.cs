using System.Drawing;
using Game.Interface;
using OpenTK.Graphics.OpenGL4;
using Game.Math;

namespace Poly;

public class Actor : IActor
{
  private readonly int _buffer = GL.GenBuffer();
  private int _bufferSize;
  private Shader _shader = default!;
  private Vector2 _position;
  private int _index;
  private float _alpha = 1.0f;
  private float _angle;
  private float _size = 1.0f;
  private Color _color;
  public Actor (Shader shader)
  {
    _shader = shader;
  }

  public void UploadData(float[] data)
  {
    GL.BindBuffer(BufferTarget.ArrayBuffer, _buffer);
    GL.BufferData(BufferTarget.ArrayBuffer, data.Length * sizeof(float), data, BufferUsageHint.StaticDraw);
    _bufferSize = data.Length / 2;
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

  public void Scale(float size)
  {
    _size = size;
  }
  
  public void Color(Color color)
  {
    _color = color;
  }
  public void Draw()
  {
    GL.BindBuffer(BufferTarget.ArrayBuffer, _buffer);
    _shader.Active();
    _shader.Position(_position.X, _position.Y);
    _shader.Rotate(_angle);
    _shader.Alpha(_alpha);
    _shader.Index(_index);
    _shader.Size(_size, _size);
    _shader.Color(_color.R/255f, _color.G/255f, _color.B/255f);
    GL.DrawArrays(PrimitiveType.Triangles, 0, _bufferSize);
  }
}