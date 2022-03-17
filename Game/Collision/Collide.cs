namespace Game.Collision;

public static class Collide
{
  public static bool CircleToCirecle(CollideCircle a, CollideCircle b)
  {
    double x = a.X - b.X;
    double y = a.Y - b.Y;
    double distance = Math.Sqrt((x * x) + (y * y));
    double radius = a.R + b.R;
    return distance <= radius;
  }

  public static bool LineToLine(CollideLine a, CollideLine b)
  {
    double uA = ((b.X2 - b.X1) * (a.Y1 - b.Y1) - (b.Y2 - b.Y1) * (a.X1 - b.X1)) /
               ((b.Y2 - b.Y1) * (a.X2 - a.X1) - (b.X2 - b.X1) * (a.Y2 - a.Y1));
    double uB = ((a.X2 - a.X1) * (a.Y1 - b.Y1) - (a.Y2 - a.Y1) * (a.X1 - b.X1)) /
                ((b.Y2 - b.Y1) * (a.X2 - a.X1) - (b.X2 - b.X1) * (a.Y2 - a.Y1));

    return uA is >= 0 and <= 1 && uB is >= 0 and <= 1;
  }

  private static bool pointCircle(double px, double py, double cx, double cy, double r)
  {
    double distX = px - cx;
    double distY = py - cy;
    double distance = Math.Sqrt((distX * distX) + (distY * distY));
    return distance <= r;
  }

  private static double dist(double x1, double y1, double x2, double y2) =>
    Math.Sqrt((Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2)));

  private static bool linePoint(double x1, double y1, double x2, double y2, double px, double py)
  {
    double d1 = dist(px, py, x1, y1);
    double d2 = dist(px, py, x2, y2);
    double lineLen = dist(x1, y1, x2, y2);

    double buffer = 0.1f;
    return d1 + d2 >= lineLen - buffer && d1 + d2 <= lineLen + buffer;
  }

  public static bool LineToCircle(CollideLine a, CollideCircle b)
  {
    var inside1 = pointCircle(a.X1, a.Y1, b.X, b.Y, b.R);
    var inside2 = pointCircle(a.X2, a.Y2, b.X, b.Y, b.R);
    if (inside1 || inside2)
      return true;

    double distX = a.X1 - a.X2;
    double distY = a.Y1 - a.Y2;
    double len = Math.Sqrt((distX * distX) + (distY * distY));
    double dot = (((b.X - a.X1) * (a.X2 - a.X1)) + ((b.Y - a.Y1) * (a.Y2 - a.Y1))) / Math.Pow(len, 2);
    double closestX = a.X1 + (dot * (a.X2 - a.X1));
    double closestY = a.Y1 + (dot * (a.Y2 - a.Y1));
    bool onSegment = linePoint(a.X1, a.Y1, a.X2, a.Y2, closestX, closestY);
    if (!onSegment)
      return false;

    distX = closestX - b.X;
    distY = closestY - b.Y;
    double distance = Math.Sqrt((distX * distX) + (distY * distY));
    return distance <= b.R;
  }
}