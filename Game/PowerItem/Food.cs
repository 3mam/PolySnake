using Game.Collision;
using Game.Interface;
using OpenTK.Mathematics;

namespace Game.PowerItem;

public class Food : IPowerUp
{
  private readonly Actor _apple = Environment.Scene.CreateActor();
  private readonly Timer _foodReposition = new(Environment.FoodReplaceTime);
  private int _id;
  private Action<bool> _trigger = default!;

  private readonly Vector2[] _net =
    new Vector2[Environment.PowerUpNetWidth * Environment.PowerUpNetHeight];

  private bool _collide;

  public Food()
  {
    _apple.UploadData(Assets.Apple);
    _apple.Color(Environment.FoodColor);
    _apple.Scale(Environment.Scale + 0.01f);

    for (var y = 0; y < Environment.PowerUpNetHeight; y++)
    for (var x = 0; x < Environment.PowerUpNetWidth; x++)
      _net[(y * Environment.PowerUpNetWidth) + x] = new Vector2(145f + (50f * x), 90f + (25f * y));
  }

  private void PlaceRandomly()
    => _id = new Random().Next(0, _net.Length);

  public void Collide(ICollide snake)
    => _collide = new CollideCircle(_net[_id], 15f) == (CollideCircle) snake;

  public void Trigger(Action<bool> fn)
    => _trigger += fn;

  public void Draw()
  {
    _apple.Position(_net[_id]);
    _apple.Draw();
  }

  public void Update()
  {
    if (_collide)
    {
      _trigger(true);
      PlaceRandomly();
      _foodReposition.Reset();      
      _trigger(false);
    }

    if (!_foodReposition.Duration(true))
      PlaceRandomly();
  }

  public void Reset()
  {
    _foodReposition.Reset();
    PlaceRandomly();
  }
}