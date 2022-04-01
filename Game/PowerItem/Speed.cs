using System;
using OpenTK.Mathematics;
using Poly.Collision;
using Poly.Interface;

namespace Poly.PowerItem;

public class Speed : IPowerUp
{
  private readonly IActor _thunder;
  private readonly Timer _speedVisibilityDuration = new(Settings.SpeedVisibilityTime);
  private readonly Timer _speedShowUp = new(Settings.ShowSpeedItemAtTime);
  private readonly Timer _speedDuration = new(Settings.SpeedUpDuration);
  private Action<bool> _trigger = default!;

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

  public void Collide(ICollide snake) =>
    _collide = new CollideCircle(_spawnPoint, 15f) == (CollideCircle) snake;

  public void Trigger(Action<bool> fn)
    => _trigger += fn;

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
}