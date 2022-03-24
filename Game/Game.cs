using System.Drawing;
using Game.Collision;
using OpenTK.Mathematics;

namespace Game;

public class Game
{
  private readonly Level _level = new();
  private readonly Walls _walls;
  private readonly PowerUps _powers;
  private readonly Hud _hud = new ();

  private readonly Timer _shakeCameraDuration = new(200);

  public readonly Snake Snake = new();


  public Game()
  {
    _walls = Walls.SmashWith(Snake.Head);
    _powers = PowerUps.SmashWith(Snake.Head);

    _powers.FoodLogic = () => Snake.SnakeLenght++;
    _powers.SpeedLogic = boost => Snake.Speed = boost ? Environment.SpeedUp : Environment.Speed;

    Reset();
  }

  public void Draw()
  {
    var wall = _walls.CheckCollideWith(Snake);
    if (Snake.MoveWhenSmashWithWall(wall))
      _shakeCameraDuration.Reset();
    if (_shakeCameraDuration.Duration())
      Environment.Scene.ShakeCameraRandomly(Environment.ShakeCameraRange);
    else
      Environment.Scene.Camera(Environment.CameraPosition);
    _hud.Draw();
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