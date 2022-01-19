using OpenTK.Mathematics;

namespace PolySnake;

public readonly record struct SnakePosition(Vector2 Position, float Direction)
{
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