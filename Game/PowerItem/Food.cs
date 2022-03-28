using Game.Collision;
using Game.Interface;
using OpenTK.Mathematics;

namespace Game.PowerItem;

public class Food : IPowerUp
{
  private readonly Actor _apple = Settings.Scene.CreateActor();
  private readonly Timer _foodReposition = new(Settings.FoodReplaceTime);
  private Action<bool> _trigger = default!;

  private bool _collide;
  private SpawnPoints _spawnPoints = default!;
  private Vector2 _spawnPoint;
  private int _pointId;
  
  public Food()
  {
    _apple.UploadData(Assets.Apple);
    _apple.Color(Settings.FoodColor);
    _apple.Scale(Settings.Scale + 0.01f);
}

  private void PlaceRandomly()
  {
    var p = _spawnPoints.RandomPoint();
    _pointId = p.id;
    _spawnPoint = p.point;
  }

  public void Collide(ICollide snake)
    => _collide = new CollideCircle(_spawnPoint, 15f) == (CollideCircle) snake;

  public void Trigger(Action<bool> fn)
    => _trigger += fn;

  public void Draw()
  {
    _apple.Position(_spawnPoint);
    _apple.Draw();
  }

  public void Update()
  {
    if (_collide)
    {
      _trigger(true);
        Reset();
      _trigger(false);
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
}