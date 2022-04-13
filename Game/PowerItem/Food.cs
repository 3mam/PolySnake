using System;
using Game.Collision;
using Game.Interface;
using Game.Math;

namespace Game.PowerItem;

public class Food : IPowerUp
{
  private readonly IActor _apple;
  private readonly Timer _foodReposition = new(Settings.FoodReplaceTime);

  private bool _collide;
  private SpawnPoints _spawnPoints = default!;
  private Vector2 _spawnPoint;
  private int _pointId;
  
  public Food()
  {
    _apple = AssetManager.GetActor(AssetList.Apple);
    _apple.Color(Settings.FoodColor);
    _apple.Scale(Settings.Scale + 0.01f);
}

  private void PlaceRandomly()
  {
    var p = _spawnPoints.RandomPoint();
    _pointId = p.id;
    _spawnPoint = p.point;
  }

  public void Collide(Func<ICollide, bool> snake)
    => _collide = snake(new CollideCircle(_spawnPoint, 15f));

  public void Draw()
  {
    _apple.Position(_spawnPoint);
    _apple.Draw();
  }

  public void Update()
  {
    if (_collide)
    {
      Trigger(true);
        Reset();
      Trigger(false);
    }

    if (!_foodReposition.Duration(true))
    {
      _spawnPoints.FreePoint(_pointId);
      PlaceRandomly();
    }
  }

  public void Reset()
  {
    _foodReposition.Reset();
    _spawnPoints.FreePoint(_pointId);
    PlaceRandomly();
  }

  public void SpawnPoints(SpawnPoints spawnPoints)
    => _spawnPoints = spawnPoints;

  public Action<bool> Trigger { get; set; }
}