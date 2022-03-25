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
  private readonly Snake _snake = new();


  public Game()
  {
    _walls = Walls.SmashWith(_snake.Head);
    _powers = PowerUps.SmashWith(_snake.Head);

    _powers.FoodLogic = () => _snake.SnakeLenght++;
    _powers.SpeedLogic = boost => _snake.Speed = boost ? Environment.SpeedUp : Environment.Speed;
   
    Reset();
  }

  public void Draw()
  {
    Environment.Scene.Clear();

    var wall = _walls.CheckCollideWith(_snake);
    if (_snake.MoveWhenSmashWithWall(wall))
      _shakeCameraDuration.Reset();
    if (_shakeCameraDuration.Duration())
      Environment.Scene.ShakeCameraRandomly(Environment.ShakeCameraRange);
    else
      Environment.Scene.Camera(Environment.CameraPosition);
    _hud.Draw();
    _level.Draw();
    _snake.Draw();
    _powers.Draw();
  }

  public void Reset()
  {
    _snake.Reset();
    _powers.Reset();
  }

  public void Move(float delta, float direction) => _snake.Move(delta, direction);
}