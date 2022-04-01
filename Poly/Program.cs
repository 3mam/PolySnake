using OpenTK.Graphics.OpenGL4;

namespace Poly;

public static class Program
{
  private static int LoadShader(string code, ShaderType type)
  {
    var shader = GL.CreateShader(type);
    GL.ShaderSource(shader, code);
    GL.CompileShader(shader);

    GL.GetShader(shader, ShaderParameter.CompileStatus, out var result);
    if (result != (int) All.True)
    {
      var infoLog = GL.GetShaderInfoLog(shader);
      Console.WriteLine(infoLog);
      return -1;
    }

    return shader;
  }

  public static int Load(string vertex, string fragment)
  {
    var ver = LoadShader(vertex, ShaderType.VertexShader);
    var frag = LoadShader(fragment, ShaderType.FragmentShader);
    var id = GL.CreateProgram();
    GL.AttachShader(id, ver);
    GL.AttachShader(id, frag);
    GL.LinkProgram(id);
    return id;
  }
}