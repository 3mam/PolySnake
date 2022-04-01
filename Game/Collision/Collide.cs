using System;

namespace Poly.Collision;

public static class Collide
{
  public static bool CircleToCircle(CollideCircle a, CollideCircle b)
  {
    var x = a.X - b.X;
    var y = a.Y - b.Y;
    var distance = Math.Sqrt(x * x + y * y);
    var radius = a.R + b.R;
    return distance <= radius;
  }

  public static bool LineToLine(CollideLine a, CollideLine b)
  {
    var uA = ((b.X2 - b.X1) * (a.Y1 - b.Y1) - (b.Y2 - b.Y1) * (a.X1 - b.X1)) /
             ((b.Y2 - b.Y1) * (a.X2 - a.X1) - (b.X2 - b.X1) * (a.Y2 - a.Y1));
    var uB = ((a.X2 - a.X1) * (a.Y1 - b.Y1) - (a.Y2 - a.Y1) * (a.X1 - b.X1)) /
             ((b.Y2 - b.Y1) * (a.X2 - a.X1) - (b.X2 - b.X1) * (a.Y2 - a.Y1));
    return uA is >= 0 and <= 1 && uB is >= 0 and <= 1;
  }

  private static bool PointCircle(double px, double py, double cx, double cy, double r)
  {
    var distX = px - cx;
    var distY = py - cy;
    var distance = Math.Sqrt(distX * distX + distY * distY);
    return distance <= r;
  }

  private static double Dist(double x1, double y1, double x2, double y2)
    => Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));

  private static bool LinePoint(double x1, double y1, double x2, double y2, double px, double py)
  {
    var d1 = Dist(px, py, x1, y1);
    var d2 = Dist(px, py, x2, y2);
    var lineLen = Dist(x1, y1, x2, y2);
    const double buffer = 0.1f;
    return d1 + d2 >= lineLen - buffer && d1 + d2 <= lineLen + buffer;
  }

  public static bool LineToCircle(CollideLine a, CollideCircle b)
  {
    var inside1 = PointCircle(a.X1, a.Y1, b.X, b.Y, b.R);
    var inside2 = PointCircle(a.X2, a.Y2, b.X, b.Y, b.R);
    if (inside1 || inside2) return true;
    var distX = a.X1 - a.X2;
    var distY = a.Y1 - a.Y2;
    var len = Math.Sqrt(distX * distX + distY * distY);
    var dot = ((b.X - a.X1) * (a.X2 - a.X1) + (b.Y - a.Y1) * (a.Y2 - a.Y1)) / Math.Pow(len, 2);
    var closestX = a.X1 + dot * (a.X2 - a.X1);
    var closestY = a.Y1 + dot * (a.Y2 - a.Y1);
    var onSegment = LinePoint(a.X1, a.Y1, a.X2, a.Y2, closestX, closestY);
    if (!onSegment) return false;
    distX = closestX - b.X;
    distY = closestY - b.Y;
    var distance = Math.Sqrt(distX * distX + distY * distY);
    return distance <= b.R;
  }
}