namespace Game.Collision;

public static class Collide
{
  public static bool CircleToCirecle(CollideCircle a, CollideCircle b)
  {
    var x = a.X - b.X;
    var y = a.Y - b.Y;
    var distance = MathF.Sqrt((x * x) + (y * y));
    var radius = a.R + b.R;
    return distance <= radius;
  }

  public static bool LineToLine(CollideLine a, CollideLine b)
  {
    float uA = ((b.X2 - b.X1) * (a.Y1 - b.Y1) - (b.Y2 - b.Y1) * (a.X1 - b.X1)) /
               ((b.Y2 - b.Y1) * (a.X2 - a.X1) - (b.X2 - b.X1) * (a.Y2 - a.Y1));
    float uB = ((a.X2 - a.X1) * (a.Y1 - b.Y1) - (a.Y2 - a.Y1) * (a.X1 - b.X1)) /
               ((b.Y2 - b.Y1) * (a.X2 - a.X1) - (b.X2 - b.X1) * (a.Y2 - a.Y1));

    return uA is >= 0 and <= 1 && uB is >= 0 and <= 1;
  }

  private static bool pointCircle(float px, float py, float cx, float cy, float r)
  {
    float distX = px - cx;
    float distY = py - cy;
    float distance = MathF.Sqrt((distX * distX) + (distY * distY));
    return distance <= r;
  }

  private static float dist(float x1, float y1, float x2, float y2) =>
    MathF.Sqrt((MathF.Pow(x1 - x2, 2) + MathF.Pow(y1 - y2, 2)));

  private static bool linePoint(float x1, float y1, float x2, float y2, float px, float py)
  {
    float d1 = dist(px, py, x1, y1);
    float d2 = dist(px, py, x2, y2);
    float lineLen = dist(x1, y1, x2, y2);

    float buffer = 0.1f;
    return d1 + d2 >= lineLen - buffer && d1 + d2 <= lineLen + buffer;
  }

  public static bool LineToCircle(CollideLine a, CollideCircle b)
  {
    var inside1 = pointCircle(a.X1, a.Y1, b.X, b.Y, b.R);
    var inside2 = pointCircle(a.X2, a.Y2, b.X, b.Y, b.R);
    if (inside1 || inside2)
      return true;

    float distX = a.X1 - a.X2;
    float distY = a.Y1 - a.Y2;
    float len = MathF.Sqrt((distX * distX) + (distY * distY));
    float dot = (((b.X - a.X1) * (a.X2 - a.X1)) + ((b.Y - a.Y1) * (a.Y2 - a.Y1))) / MathF.Pow(len, 2);
    float closestX = a.X1 + (dot * (a.X2 - a.X1));
    float closestY = a.Y1 + (dot * (a.Y2 - a.Y1));
    bool onSegment = linePoint(a.X1, a.Y1, a.X2, a.Y2, closestX, closestY);
    if (!onSegment)
      return false;

    distX = closestX - b.X;
    distY = closestY - b.Y;
    float distance = MathF.Sqrt((distX * distX) + (distY * distY));
    return distance <= b.R;
  }
}