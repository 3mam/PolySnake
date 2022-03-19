using Game.Collision;
using OpenTK.Mathematics;

namespace Game;

public sealed class Walls
{
  private readonly CollideLine _wallLeft =
    new(new Vector2(100f, 1000f), new Vector2(100f, 0f));

  private readonly CollideLine _wallTop =
    new(new Vector2(0f, 950f), new Vector2(2000f, 950f));

  private readonly CollideLine _wallRight =
    new(new Vector2(1900f, 0f), new Vector2(1900f, 1000f));

  private readonly CollideLine _wallBottom =
    new(new Vector2(0f, 50f), new Vector2(2000f, 50f));

  private readonly SnakePosition _snake;

  private Walls(SnakePosition snake) => _snake = snake;
  public static Walls SmashWith(SnakePosition snake) => new(snake);

  public bool CheckCollide()
  {
    var head = new CollideCircle(_snake.Position, 15f);
    var collide = false;
    const float recoil = 5f;
    if (_wallLeft == head)
    {
      _snake.Direction = 180f - _snake.Direction;
      _snake.Position += new Vector2(recoil, 0);
      collide = true;
    }

    if (_wallRight == head)
    {
      _snake.Direction = 180f - _snake.Direction;
      _snake.Position -= new Vector2(recoil, 0);
      collide = true;
    }

    if (_wallTop == head)
    {
      _snake.Direction = -_snake.Direction;
      _snake.Position -= new Vector2(0, recoil);
      collide = true;
    }

    if (_wallBottom == head)
    {
      _snake.Direction = MathF.Abs(_snake.Direction);
      _snake.Position += new Vector2(0, recoil);
      collide = true;
    }

    return collide;
  }
}