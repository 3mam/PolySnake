using System;
using Game.Interface;
using OpenTK.Mathematics;

namespace Game.Collision;

public readonly struct CollideCircle : ICollide
{
  private bool Equals(CollideCircle other)
    => X.Equals(other.X) && Y.Equals(other.Y) && R.Equals(other.R);

  public override bool Equals(object? obj)
  {
    if (ReferenceEquals(null, obj)) return false;
    return obj.GetType() == GetType() && Equals((CollideCircle) obj);
  }

  public override int GetHashCode()
    => HashCode.Combine(X, Y, R);

  public readonly double X;
  public readonly double Y;
  public readonly double R;

  public CollideCircle(double x, double y, double radius)
  {
    X = x;
    Y = y;
    R = radius;
  }

  public CollideCircle(Vector2d point, double radius)
  {
    X = point.X;
    Y = point.Y;
    R = radius;
  }

  public bool Collide(CollideCircle circle)
    => Collision.Collide.CircleToCircle(this, circle);

  public bool Collide(CollideLine line)
    => Collision.Collide.LineToCircle(line, this);

  public static bool operator ==(CollideCircle a, CollideCircle b)
    => a.Collide(b);

  public static bool operator !=(CollideCircle a, CollideCircle b)
    => !a.Collide(b);

  public static bool operator ==(CollideCircle a, CollideLine b)
    => a.Collide(b);

  public static bool operator !=(CollideCircle a, CollideLine b)
    => !a.Collide(b);
}