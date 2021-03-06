using System;
using Game.Collision;
using Game.Interface;
using Game.Math;

namespace Game.PowerItem;

public class Speed : IPowerUp
{
  private readonly IActor _thunder;
  private readonly Timer _speedVisibilityDuration = new(Settings.SpeedVisibilityTime);
  private readonly Timer _speedShowUp = new(Settings.ShowSpeedItemAtTime);
  private readonly Timer _speedDuration = new(Settings.SpeedUpDuration);

  private bool _visible;
  private bool _collide;
  private SpawnPoints _spawnPoints = default!;
  private Vector2 _spawnPoint;
  private int _pointId;

  public Speed()
  {
    _thunder = AssetManager.GetActor(AssetList.Thunder);
    _thunder.Scale(Settings.Scale + 0.01f);
    _thunder.Color(Settings.SpeedColor);
  }

  private void PlaceRandomly()
  {
    var p = _spawnPoints.RandomPoint();
    _pointId = p.id;
    _spawnPoint = p.point;
  }

  public void Collide(Func<ICollide, bool> snake) =>
    _collide = snake(new CollideCircle(_spawnPoint, 15f));

  public void Draw()
  {
    if (_visible)
    {
      _thunder.Position(_spawnPoint);
      _thunder.Draw();
    }
  }

  public void Update()
  {
    if (!_speedDuration.Duration())
      Trigger(false);

    if (_speedVisibilityDuration.Duration())
    {
      if (_collide)
      {
        Trigger(true);
        _speedDuration.Reset();
        _speedVisibilityDuration.Stop();
      }

      _visible = true;
    }
    else
    {
      _visible = false;
      _spawnPoints.FreePoint(_pointId);
    }

    if (!_speedShowUp.Duration(true))
    {
      _speedVisibilityDuration.Reset();
      PlaceRandomly();
    }
  }

  public void Reset()
  {
    _spawnPoints.FreePoint(_pointId);
    PlaceRandomly();
    _speedShowUp.Reset();
  }

  public void SpawnPoints(SpawnPoints spawnPoints)
    => _spawnPoints = spawnPoints;

  public Action<bool> Trigger { get; set; }
}