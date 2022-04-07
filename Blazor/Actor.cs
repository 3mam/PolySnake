using System.Drawing;
using Game.Interface;
using Microsoft.JSInterop;
using Game.Math;

namespace Blazor;

public class Actor : IActor
{
  private readonly IJSObjectReference _handle;
  private readonly IJSRuntime _js;

  public Actor(IJSRuntime js, IJSObjectReference obj)
  {
    _js = js;
    _handle = obj;
  }

  public void UploadData(float[] data)
  {
    _handle.InvokeVoidAsync("uploadData", data);
  }

  public void Position(Vector2 cord)
  {
    _handle.InvokeVoidAsync("position", cord.X, cord.Y);
  }
  
  public void Rotation(float degrees)
  {
    _handle.InvokeVoidAsync("rotation", degrees);
  }
  
  public void Transparency(float alpha=1f)
  {
    _handle.InvokeVoidAsync("transparency", alpha);
  }
  
  public void Index(int val)
  {
    _handle.InvokeVoidAsync("index", val);
  }
  
  public void Scale(float size)
  {
    _handle.InvokeVoidAsync("scale", size, size);
  }
  
  public void Color(Color color)
  {
    _handle.InvokeVoidAsync("color", color.R,color.G,color.B);
  }
  
  public void Draw()
  {
    _handle.InvokeVoidAsync("draw");
  }
  
  public void Debug()
  {
    _js.InvokeVoidAsync("console.log", _handle);
  }

}