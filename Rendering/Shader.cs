using OpenTK.Graphics.OpenGL4;

namespace PolySnake.Rendering;

public class Shader
{
  private int _program;
  private int _rotate;
  private int _position;
  private int _switchPalette;
  private int _index;
  private int _camera;
  private int _alpha;
  private int _vertexPosition;
 
  public static Shader Load(string vertex, string fragment)
  {
    var program = Program.Load(vertex, fragment);
    GL.UseProgram(program);
    var rotate = GL.GetUniformLocation(program, "rotate");
    var index = GL.GetUniformLocation(program, "index");
    var camera = GL.GetUniformLocation(program, "camera");
    var position = GL.GetUniformLocation(program, "position");
    var switchPalette = GL.GetUniformLocation(program, "switch_palette");
    var alpha = GL.GetUniformLocation(program, "alpha");
    var palette = GL.GetUniformLocation(program, "palette");
    var vertexPosition = GL.GetAttribLocation(program, "vertex_position");
    
    GL.Uniform1(palette, 0);
    GL.Uniform1(index, 0f);
    GL.Uniform1(alpha, 1f);
    
    return new Shader()
    {
      _program = program,
      _rotate = rotate,
      _position = position,
      _switchPalette = switchPalette,
      _index = index,
      _camera = camera,
      _alpha = alpha,
      _vertexPosition = vertexPosition,
    };
  }
 
  public void Palette(int val = 0)
  {
    GL.Uniform1(_switchPalette, val);
  }
  
  public void Index(int val = 0)
  {
    GL.Uniform1(_index, (float)val / 100);
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
    GL.Uniform1(_rotate, angle);
  }
  
  public void Active()
  {
    GL.UseProgram(_program);
    GL.EnableVertexAttribArray(_vertexPosition);
    GL.VertexAttribPointer(_vertexPosition, 4, VertexAttribPointerType.Float, false, 0, 0);
  }
}