using Game.Interface;
using Microsoft.JSInterop;
using Game.Math;

namespace Blazor;

public class Scene : IScene
{
  private readonly IJSRuntime _js;
  private readonly float _width;
  private readonly float _height;
  private readonly float _scale;
  private IJSObjectReference _handle = default!;
  private Vector2 _camera;

  public Scene(IJSRuntime js,float width, float height, float scale)
  {
    _js = js;
    _width = width;
    _height = height;
    _scale = scale;
  }

  public async Task Init()
  {
    _handle = await _js.InvokeAsync<IJSObjectReference>("scene", 100, 100, 1);
    await _handle.InvokeVoidAsync("clear");
    await _handle.InvokeVoidAsync("dimensions",_width, _height, _scale);
  }

  public void Clear()
  {
      _handle.InvokeVoidAsync("clear");
  }

  public void ShakeCameraRandomly(float range)
  {
    if (range == 0)
      return;
    var random = new Random();
    var between = (range + range + 1);
    var x = (float) random.NextDouble() * between - range;
    var y = (float) random.NextDouble() * between - range;
    Camera(new Vector2(_camera.X + x, _camera.Y + y));
  }
  
  public async Task<Actor> CreateActor()
  {
    var actor = await _handle.InvokeAsync<IJSObjectReference>("createActor");
    return new Actor(_js ,actor);
  }

  public void Debug()
  {
    _js.InvokeVoidAsync("console.log", _handle);
  }

  public void Camera(Vector2 position)
  {
    _camera = position;
    _handle.InvokeVoidAsync("camera", position.X, position.Y);
  }
}