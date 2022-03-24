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

  public Wall CheckCollideWith(Snake snake)
  {
    var head = new CollideCircle(snake.Head.Position, 15f);
    if (_wallLeft == head)
      return Wall.Left;
    if (_wallRight == head)
      return Wall.Right;
    if (_wallTop == head)
      return Wall.Top;
    if (_wallBottom == head)
      return Wall.Bottom;
    return Wall.None;
  }
}