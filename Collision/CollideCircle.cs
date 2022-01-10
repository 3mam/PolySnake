using Microsoft.VisualBasic.CompilerServices;
using OpenTK.Mathematics;

namespace PolySnake.Collision;

public readonly struct CollideCircle
{
  public readonly float X;
  public readonly float Y;
  public readonly float R;

  public CollideCircle(float x, float y, float radius)
  {
    X = x;
    Y = y;
    R = radius;
  }

  public CollideCircle(Vector2 point, float radius)
  {
    point.Deconstruct(out X, out Y);
    R = radius;
  }

  public CollideCircle(Vector3 point)
  {
    point.Deconstruct(out X, out Y, out R);
  }

  public bool Collide(CollideCircle circle)
  {
    var x = (X + R) - (circle.X + circle.R);
    var y = (Y + R) - (circle.Y + circle.R);
    var distance = MathF.Sqrt(x * x + y * y);
    return distance < R + circle.R;
  }

  public static bool operator ==(CollideCircle a, CollideCircle b) =>
    a.Collide(b);

  public static bool operator !=(CollideCircle a, CollideCircle b) =>
    !a.Collide(b);
}