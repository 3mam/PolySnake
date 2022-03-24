using System.Drawing;
using Game.Collision;
using OpenTK.Mathematics;

namespace Game;

public class Game
{
  private readonly Level _level = new();
  private readonly Walls _walls;
  private readonly PowerUps _powers;

  private readonly Timer _shakeCameraDuration = new(200);
  private const float ShakeCameraRange = -25f;

  public readonly Snake Snake = new();

  public Game()
  {
    _walls = Walls.SmashWith(Snake.Position);
    _powers = PowerUps.SmashWith(Snake.Position);
    Reset();
  }

  public void Draw()
  {
    if (_walls.CheckCollide())
      _shakeCameraDuration.Reset();
    if (_shakeCameraDuration.Duration())
      Environment.Scene.ShakeCameraRandomly(ShakeCameraRange);
    else
      Environment.Scene.ShakeCameraRandomly(0);

    _level.Draw();
    Snake.Draw();
    _powers.Draw();
  }

  public void Reset()
  {
    Snake.Reset();
    _powers.Reset();
  }
}