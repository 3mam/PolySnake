using OpenTK.Mathematics;
using Poly.Collision;
using Poly.Interface;

namespace Poly;

public sealed class Walls : ICollideEvent
{
  private readonly CollideLine _wallLeft =
    new(new Vector2(100f, 1000f), new Vector2(100f, 0f));

  private readonly CollideLine _wallTop =
    new(new Vector2(0f, 950f), new Vector2(2000f, 950f));

  private readonly CollideLine _wallRight =
    new(new Vector2(1900f, 0f), new Vector2(1900f, 1000f));

  private readonly CollideLine _wallBottom =
    new(new Vector2(0f, 50f), new Vector2(2000f, 50f));

  public WallsList Current { get; private set; } = WallsList.None;

 public void Collide(ICollide item)
  {
    var head = (CollideCircle) item;
    if (_wallLeft == head)
      Current = WallsList.Left;
    else if (_wallRight == head)
      Current = WallsList.Right;
    else if (_wallTop == head)
      Current = WallsList.Top;
    else if (_wallBottom == head)
      Current = WallsList.Bottom;
    else 
      Current = WallsList.None;
  }
}