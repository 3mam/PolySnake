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

  public Scene(IJSRuntime js, float width, float height, float scale)
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
    await _handle.InvokeVoidAsync("dimensions", _width, _height, _scale);
  }

  public void Clear()
  {
    _handle.InvokeVoidAsync("clear");
  }
  
  public async Task<Actor> CreateActor()
  {
    var actor = await _handle.InvokeAsync<IJSObjectReference>("createActor");
    return new Actor(_js, actor);
  }

  public void Debug()
  {
    _js.InvokeVoidAsync("console.log", _handle);
  }

  public void Camera(Vector2 position)
  {
    _handle.InvokeVoidAsync("camera", position.X, position.Y);
  }
}