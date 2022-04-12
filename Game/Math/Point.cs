using System;

namespace Game.Math;

public class Point
{
  private readonly Vector2 _point;
  private Vector2 _pivot;
  private Vector2 _position;
  private float _angle;
  private float _scale = 1f;

  public Vector2 Value
  {
    get
    {
      var s = MathF.Sin(_angle);
      var c = MathF.Cos(_angle);
      var x = _point.X - _pivot.X;
      var y = _point.Y - _pivot.Y;
      var xNew = x * c - y * s;
      var yNew = x * s + y * c;
      return new Vector2(
        (xNew + _pivot.X) * _scale + _position.X,
        (yNew + _pivot.Y) * _scale + _position.Y
      );
    }
  }

  public Point(float x, float y)
    => _point = new Vector2(x, y);

  public Point Pivot(Vector2 point)
  {
    _pivot = point;
    return this;
  }

  public Point Rotate(float angle)
  {
    _angle = angle;
    return this;
  }

  public Point Scale(float size)
  {
    _scale = size;
    return this;
  }

  public Point Position(Vector2 point)
  {
    _position = point;
    return this;
  }
}