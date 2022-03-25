using Game.Collision;
using Game.Interface;
using OpenTK.Mathematics;

namespace Game.PowerItem;

public class Speed : IPowerUp
{
  private readonly Actor _thunder = Environment.Scene.CreateActor();
  private readonly Timer _speedVisibilityDuration = new(5000);
  private readonly Timer _speedShowUp = new(10000);
  private readonly Timer _speedDuration = new(3000);

  private int _id;
  private Action<bool> _trigger = default!;
  private readonly Snake _snake;

  private readonly Vector2[] _net =
    new Vector2[Environment.PowerUpNetWidth * Environment.PowerUpNetHeight];

  private bool _visable;

  public Speed(Snake snake)
  {
    _snake = snake;
    _thunder.UploadData(Assets.Thunder);
    _thunder.Scale(Environment.Scale + 0.01f);
    _thunder.Color(Environment.SpeedColor);

    for (var y = 0; y < Environment.PowerUpNetHeight; y++)
    for (var x = 0; x < Environment.PowerUpNetWidth; x++)
      _net[(y * Environment.PowerUpNetWidth) + x] = new Vector2(145f + (50f * x), 90f + (25f * y));
  }
  private void PlaceRandomly() => _id = new Random().Next(0, _net.Length);

  private bool CheckCollide() =>
    new CollideCircle(_net[_id], 15f) == new CollideCircle(_snake.Position, 15f);

  public void Trigger(object fn) => _trigger = (Action<bool>) fn;

  public void Draw()
  {
    if (_visable)
    {
      _thunder.Position(_net[_id]);
      _thunder.Draw();
    }
  }

  public void Update()
  {
    if (!_speedDuration.Duration())
      _trigger(false);

    if (_speedVisibilityDuration.Duration())
    {
      if (CheckCollide())
      {
        _trigger(true);
        _speedDuration.Reset();
        _speedVisibilityDuration.Stop();
      }
      _visable = true;
    }
    else
    {
      _visable = false;
    }

    if (!_speedShowUp.Duration(true))
    {
      _speedVisibilityDuration.Reset();
      PlaceRandomly();
    }
  }

  public void Reset()
  {
    PlaceRandomly();
    _speedShowUp.Reset();
  }
}