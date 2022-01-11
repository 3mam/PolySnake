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
    var x = X - circle.X;
    var y = Y - circle.Y;
    var distance = MathF.Sqrt((x * x) + (y * y));
    var radius = R + circle.R;
    return distance <= radius;
  }

  public static bool operator ==(CollideCircle a, CollideCircle b) =>
    a.Collide(b);

  public static bool operator !=(CollideCircle a, CollideCircle b) =>
    !a.Collide(b);

  public bool Equals(CollideCircle other) =>
    X.Equals(other.X) && Y.Equals(other.Y) && R.Equals(other.R);

  public override bool Equals(object? obj) =>
    obj is CollideCircle other && Equals(other);

  public override int GetHashCode() =>
    HashCode.Combine(X, Y, R);
}