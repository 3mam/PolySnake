using Game.Collision;
using Game.Interface;
using OpenTK.Mathematics;

namespace Game.PowerItem;

public class Speed : IPowerUp
{
  private readonly Actor _thunder = Environment.Scene.CreateActor();
  private readonly Timer _speedVisibilityDuration = new(Environment.SpeedVisibilityTime);
  private readonly Timer _speedShowUp = new(Environment.ShowSpeedItemAtTime);
  private readonly Timer _speedDuration = new(Environment.SpeedUpDuration);

  private int _id;
  private Action<bool> _trigger = default!;

  private readonly Vector2[] _net =
    new Vector2[Environment.PowerUpNetWidth * Environment.PowerUpNetHeight];

  private bool _visible;
  private bool _collide;

  public Speed()
  {
    _thunder.UploadData(Assets.Thunder);
    _thunder.Scale(Environment.Scale + 0.01f);
    _thunder.Color(Environment.SpeedColor);

    for (var y = 0; y < Environment.PowerUpNetHeight; y++)
    for (var x = 0; x < Environment.PowerUpNetWidth; x++)
      _net[(y * Environment.PowerUpNetWidth) + x] = new Vector2(145f + (50f * x), 90f + (25f * y));
  }

  private void PlaceRandomly()
    => _id = new Random().Next(0, _net.Length);

  public void Collide(ICollide snake) =>
    _collide = new CollideCircle(_net[_id], 15f) == (CollideCircle) snake;

  public void Trigger(object fn) => _trigger = (Action<bool>) fn;

  public void Draw()
  {
    if (_visible)
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
      if (_collide)
      {
        _trigger(true);
        _speedDuration.Reset();
        _speedVisibilityDuration.Stop();
      }

      _visible = true;
    }
    else
    {
      _visible = false;
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