using Game.Collision;
using OpenTK.Mathematics;

namespace Game;

public class PowerUps
{
  private const int Width = 35;
  private const int Height = 34;
  private readonly Vector2[] _net = new Vector2[Width * Height];
  private int _idFood = 0;
  private readonly SnakePosition _snake;
  public Vector2 FoodPosition => _net[_idFood];

  private PowerUps(SnakePosition snake)
  {
    _snake = snake;
    for (var y = 0; y < Height; y++)
    for (var x = 0; x < Width; x++)
      _net[(y * Width) + x] = new Vector2(145f + (50f * x), 90f + (25f * y));
  }

  public static PowerUps SmashWith(SnakePosition snake) => new(snake);

  public bool CheckFoodCollide() =>
    new CollideCircle(_net[_idFood], 15f) == new CollideCircle(_snake.Position, 15f);

  public void PlaceFoodRandomly() => _idFood = new Random().Next(0, Width * Height);
}