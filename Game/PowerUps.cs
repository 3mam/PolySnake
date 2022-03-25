using System.Drawing;
using Game.Collision;
using OpenTK.Mathematics;

namespace Game;

public class PowerUps
{
  private const int NetWidth = 35;
  private const int NetHeight = 34;
  private readonly Vector2[] _net = new Vector2[NetWidth * NetHeight];
  private int _idFood = 0;
  private int _idSpeed = 0;
  private SnakePosition _snake = default!;
  private Vector2 FoodPosition => _net[_idFood];
  private Vector2 SpeedPosition => _net[_idSpeed];
  private readonly Actor _apple = Environment.Scene.CreateActor();
  private readonly Actor _thunder = Environment.Scene.CreateActor();
  private readonly Timer _foodReposition = new(10000);
  private readonly Timer _speedVisibilityDuration = new(5000);
  private readonly Timer _speedShowUp = new(10000);
  private readonly Timer _speedDuration = new(3000);

  public Action FoodLogic { private get; set; } = default!;
  public Action<bool> SpeedLogic { private get; set; } = default!;

  private PowerUps()
  {
    _apple.UploadData(Assets.Apple);
    _apple.Color(Environment.FoodColor);
    _apple.Scale(Environment.Scale + 0.01f);

    _thunder.UploadData(Assets.Thunder);
    _thunder.Scale(Environment.Scale + 0.01f);
    _thunder.Color(Environment.SpeedColor);

    for (var y = 0; y < NetHeight; y++)
    for (var x = 0; x < NetWidth; x++)
      _net[(y * NetWidth) + x] = new Vector2(145f + (50f * x), 90f + (25f * y));
  }

  public static PowerUps SmashWith(SnakePosition snake)
    => new() {_snake = snake};

  private bool CheckFoodCollide() =>
    new CollideCircle(_net[_idFood], 15f) == new CollideCircle(_snake.Position, 15f);

  private void PlaceFoodRandomly() => _idFood = new Random().Next(0, NetWidth * NetHeight);

  private bool CheckSpeedCollide() =>
    new CollideCircle(_net[_idSpeed], 15f) == new CollideCircle(_snake.Position, 15f);

  private void PlaceSpeedRandomly() => _idSpeed = new Random().Next(0, NetWidth * NetHeight);

  public void Draw()
  {
    if (CheckFoodCollide())
    {
      FoodLogic();
      PlaceFoodRandomly();
      _foodReposition.Reset();
    }

    if (!_foodReposition.Duration(true))
      PlaceFoodRandomly();

    _apple.Position(FoodPosition);
    _apple.Draw();


    if (!_speedDuration.Duration())
      SpeedLogic(false);

    if (_speedVisibilityDuration.Duration())
    {
      if (CheckSpeedCollide())
      {
        SpeedLogic(true);
        _speedDuration.Reset();
        _speedVisibilityDuration.Stop();
      }
      _thunder.Position(SpeedPosition);
      _thunder.Draw();
    }

    if (!_speedShowUp.Duration(true))
    {
      _speedVisibilityDuration.Reset();
      PlaceSpeedRandomly();
    }
  }

  public void Reset()
  {
    PlaceFoodRandomly();
    PlaceSpeedRandomly();
    _foodReposition.Reset();
    _speedShowUp.Reset();
  }
}