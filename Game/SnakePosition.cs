using OpenTK.Mathematics;

namespace Game;

public class SnakePosition
{
  private float _direction;
  public Vector2 Position { get; private set; }

  public float Direction
  {
    get => _direction switch
    {
      > 180 => -(360 - _direction),
      < -180 => 360 - _direction,
      _ => _direction
    };
    private set => _direction = value;
  }

  public SnakePosition(Vector2 position, float direction)
  {
    Position = position;
    Direction = direction;
  }

  public void Motion(Vector2 target)
  {
    var direction = target - Position;
    var angle = MathF.Atan2(direction.X, direction.Y);
    direction.Normalize();
    direction *= -15f;
    Position = target + direction;
    Direction = -(angle * 180f / MathF.PI);
  }

  public void Update(Vector2 position, float direction)
  {
    Position = position;
    Direction = direction;
  }
}