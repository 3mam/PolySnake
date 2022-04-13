using System;
using Game.Collision;
using Game.Interface;
using Game.Math;

namespace Game;

public sealed class Walls : ICollideEvent , ITrigger
{
  private readonly CollideLine _wallLeft =
    new(new Vector2(100f, 1000f), new Vector2(100f, 0f));

  private readonly CollideLine _wallTop =
    new(new Vector2(0f, 950f), new Vector2(2000f, 950f));

  private readonly CollideLine _wallRight =
    new(new Vector2(1900f, 0f), new Vector2(1900f, 1000f));

  private readonly CollideLine _wallBottom =
    new(new Vector2(0f, 50f), new Vector2(2000f, 50f));

  public Action<bool> Trigger { get; set; }
  public WallsList Current { get; private set; } = WallsList.None;

  public void Collide(Func<ICollide, bool> snake)
  {
    if (snake(_wallLeft)) Current = WallsList.Left;
    else if (snake(_wallRight)) Current = WallsList.Right;
    else if (snake(_wallTop)) Current = WallsList.Top;
    else if (snake(_wallBottom)) Current = WallsList.Bottom;
    else Current = WallsList.None;
    Trigger(Current != WallsList.None);
  }
}