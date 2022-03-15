using OpenTK.Mathematics;

namespace Game.Collision;

public readonly struct CollideCircle
{
  public bool Equals(CollideCircle other)
  {
    return X.Equals(other.X) && Y.Equals(other.Y) && R.Equals(other.R);
  }

  public override bool Equals(object? obj)
  {
    return obj is CollideCircle other && Equals(other);
  }

  public override int GetHashCode()
  {
    return HashCode.Combine(X, Y, R);
  }

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

  public bool Collide(CollideCircle circle) =>
    Collision.Collide.CircleToCirecle(this, circle);

  public bool Collide(CollideLine line) =>
    Collision.Collide.LineToCircle(line, this);

  public static bool operator ==(CollideCircle a, CollideCircle b) =>
    a.Collide(b);

  public static bool operator !=(CollideCircle a, CollideCircle b) =>
    !a.Collide(b);

  public static bool operator ==(CollideCircle a, CollideLine b) =>
    a.Collide(b);

  public static bool operator !=(CollideCircle a, CollideLine b) =>
    !a.Collide(b);
}