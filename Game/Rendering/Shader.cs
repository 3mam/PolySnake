using OpenTK.Graphics.OpenGL4;

namespace Game.Rendering;

public class Shader
{
  private int _program;
  private int _angle;
  private int _position;
  private int _color;
  private int _index;
  private int _camera;
  private int _alpha;
  private int _vertexPosition;
  private int _size;
  private int _dimensions;

  public static Shader Load(string vertex, string fragment)
  {
    var program = Program.Load(vertex, fragment);
    GL.UseProgram(program);
    var angle = GL.GetUniformLocation(program, "angle");
    var index = GL.GetUniformLocation(program, "index");
    var camera = GL.GetUniformLocation(program, "camera");
    var position = GL.GetUniformLocation(program, "position");
    var alpha = GL.GetUniformLocation(program, "alpha");
    var color = GL.GetUniformLocation(program, "color");
    var size = GL.GetUniformLocation(program, "size");
    var vertexPosition = GL.GetAttribLocation(program, "vertex_position");
    var dimensions = GL.GetUniformLocation(program, "dimensions");

    GL.Uniform1(index, 0f);
    GL.Uniform1(alpha, 1f);

    return new Shader()
    {
      _program = program,
      _angle = angle,
      _position = position,
      _color = color,
      _index = index,
      _camera = camera,
      _alpha = alpha,
      _vertexPosition = vertexPosition,
      _size = size,
      _dimensions = dimensions,
    };
  }

  public void Color(float red, float green, float blue)
  {
    GL.Uniform3(_color, red, green, blue);
  }

  public void Index(int val)
  {
    GL.Uniform1(_index, val);
  }

  public void Alpha(float val = 1f)
  {
    GL.Uniform1(_alpha, val);
  }

  public void Position(float x, float y)
  {
    GL.Uniform2(_position, x, y);
  }

  public void Camera(float x, float y)
  {
    GL.Uniform2(_camera, x, y);
  }

  public void Rotate(float angle)
  {
    GL.Uniform1(_angle, angle);
  }

  public void Size(float x, float y)
  {
    GL.Uniform2(_size, x, y);
  }

  public void Dimensions(float width, float height, float scale)
  {
    GL.Uniform3(_dimensions, width, height, scale);
  }

  public void Active()
  {
    GL.UseProgram(_program);
    GL.EnableVertexAttribArray(_vertexPosition);
    GL.VertexAttribPointer(_vertexPosition, 2, VertexAttribPointerType.Float, false, 0, 0);
  }
}