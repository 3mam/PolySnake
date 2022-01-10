using OpenTK.Mathematics;

namespace PolySnake.Collision;

public readonly struct Circle
{
  public readonly float X;
  public readonly float Y;
  public readonly float R;

  public Circle(float x, float y, float radius)
  {
    X = x;
    Y = y;
    R = radius;
  }

  public Circle(Vector2 point, float radius)
  {
    point.Deconstruct(out X, out Y);
    R = radius;
  }

  public Circle(Vector3 point)
  {
    point.Deconstruct(out X, out Y, out R);
  }

  public bool Collide(Circle circle)
  {
    var x = (X + R) - (circle.X + circle.R);
    var y = (Y + R) - (circle.Y + circle.R);
    var distance = (float) Math.Sqrt(x * x + y * y);
    return distance < R + circle.R;
  }
}