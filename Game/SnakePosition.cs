using System.Diagnostics;
using OpenTK.Mathematics;

namespace Game;

public class SnakePosition
{
  private readonly float _direction;
  public Vector2 Position { get; }

  public float Direction
  {
    get => _direction switch
    {
      > 180 => -(360 - _direction),
      < -180 => 360 - _direction,
      _ => _direction
    };
    private init => _direction = value;
  }

  public SnakePosition(Vector2 position, float direction)
  {
    Position = position;
    Direction = direction;
  }

  public SnakePosition Motion(Vector2 target)
  {
    var direction = target - Position;
    var angle = MathF.Atan2(direction.X, direction.Y);
    direction.Normalize();
    direction *= -15f;
    return new SnakePosition(
      target + direction,
      -(angle * 180f / MathF.PI));
  }
}