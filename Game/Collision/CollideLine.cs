using OpenTK.Mathematics;

namespace Game.Collision;

public readonly struct CollideLine
{
  public bool Equals(CollideLine other)
  {
    return X1.Equals(other.X1) && Y1.Equals(other.Y1) && X2.Equals(other.X2) && Y2.Equals(other.Y2);
  }

  public override bool Equals(object? obj)
  {
    return obj is CollideLine other && Equals(other);
  }

  public override int GetHashCode()
  {
    return HashCode.Combine(X1, Y1, X2, Y2);
  }

  public readonly float X1;
  public readonly float Y1;
  public readonly float X2;
  public readonly float Y2;
  
  public CollideLine(float x1, float y1, float x2, float y2)
  {
    X1 = x1;
    Y1 = y1;
    X2 = x2;
    Y2 = y2;
  }
  
  public CollideLine(Vector2 a, Vector2 b)
  {
    X1 = a.X;
    Y1 = a.Y;
    X2 = b.X;
    Y2 = b.Y;
  }
  
  public bool Collide(CollideLine line) =>
    Collision.Collide.LineToLine(this, line);

  public bool Collide(CollideCircle circle) =>
    Collision.Collide.LineToCircle(this, circle);

  public static bool operator ==(CollideLine a, CollideLine b) =>
    a.Collide(b);

  public static bool operator !=(CollideLine a, CollideLine b) =>
    !a.Collide(b);

  public static bool operator ==(CollideLine a, CollideCircle b) =>
    a.Collide(b);

  public static bool operator !=(CollideLine a, CollideCircle b) =>
    !a.Collide(b);
}