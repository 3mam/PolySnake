using Game.Collision;
using Game.Interface;
using OpenTK.Mathematics;

namespace Game.PowerUp;

public class Food :
  ITrigger, IDraw, IUpdate, IReset
{
  private readonly Actor _apple = Environment.Scene.CreateActor();
  private readonly Timer _foodReposition = new(10000);
  private int _id;
  private Action _trigger = default!;
  private readonly Snake _snake;

  private readonly Vector2[] _net =
    new Vector2[Environment.PowerUpNetWidth * Environment.PowerUpNetHeight];
  
  public Food(Snake snake)
  {
    _snake = snake;
    _apple.UploadData(Assets.Apple);
    _apple.Color(Environment.FoodColor);
    _apple.Scale(Environment.Scale + 0.01f);

    for (var y = 0; y < Environment.PowerUpNetHeight; y++)
    for (var x = 0; x < Environment.PowerUpNetWidth; x++)
      _net[(y * Environment.PowerUpNetWidth) + x] = new Vector2(145f + (50f * x), 90f + (25f * y));
  }

  private void PlaceRandomly() => _id = new Random().Next(0, _net.Length);

  private bool CheckCollide() =>
    new CollideCircle(_net[_id], 15f) == new CollideCircle(_snake.Position, 15f);

  public void Trigger(object fn) => _trigger = (Action) fn;

  public void Draw()
  {
    _apple.Position(_net[_id]);
    _apple.Draw();
  }

  public void Update()
  {
    if (CheckCollide())
    {
      _trigger();
      PlaceRandomly();
      _foodReposition.Reset();
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