using System.Drawing;
using Game.Collision;
using OpenTK.Mathematics;

namespace Game;

public class Game
{
  private readonly Actor _level;

  private readonly Walls _walls;
  private readonly PowerUps _powers;

  private readonly Timer _shakeCameraDuration = new(200);
  private readonly float _shakeCameraRange = -25f;
  
  public readonly Snake Snake = new ();
  public Game()
  {
    _level = Environment.Scene.CreateActor();
    _walls = Walls.SmashWith(Snake.Position);
    _powers = PowerUps.SmashWith(Snake.Position);
    Reset();

    _level.UploadData(Assets.Level);
    _level.Color(Color.SeaGreen);
    _level.Position(new Vector2(Environment.CenterWidth, Environment.CenterHeight));
    _level.Rotation(90f);
    _level.Scale(5f);
  }

  public void Draw()
  {
    if (_walls.CheckCollide())
      _shakeCameraDuration.Reset();
    if (_shakeCameraDuration.Duration())
      Environment.Scene.ShakeCameraRandomly(_shakeCameraRange);
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