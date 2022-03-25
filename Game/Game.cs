using Game.Interface;
using Game.PowerItem;

namespace Game;

public class Game
{
  private readonly Level _level = new();
  private readonly Walls _walls = new();
  private readonly Hud _hud = new();
  private readonly Timer _shakeCameraDuration = new(200);
  private readonly Snake _snake = new();
  private readonly (IPowerUp food, IPowerUp speed) _power;

  private void SnakeSize()
    => _snake.Lenght += 1;

  private void SnakeSpeed(bool boost)
    => _snake.Speed = boost ? Environment.SpeedUp : Environment.Speed;

  private float _direction;
  
  public Game()
  {
    _power.food = new Food(_snake);
    _power.food.Trigger(SnakeSize);
    _power.speed = new Speed(_snake);
    _power.speed.Trigger(SnakeSpeed);
    Reset();
  }

  public void Update(float delta)
  {
    _snake.Move(delta, _direction);
    var wall = _walls.CheckCollideWith(_snake);
    if (_snake.MoveWhenSmashWithWall(wall))
      _shakeCameraDuration.Reset();
    if (_shakeCameraDuration.Duration())
      Environment.Scene.ShakeCameraRandomly(Environment.ShakeCameraRange);
    else
      Environment.Scene.Camera(Environment.CameraPosition);
    _power.food.Update();
    _power.speed.Update();
  }

  public void Draw()
  {
    Environment.Scene.Clear();
    _hud.Draw();
    _level.Draw();
    _power.food.Draw();
    _power.speed.Draw();
    _snake.Draw();
  }

  public void Reset()
  {
    _snake.Reset();
    _power.food.Reset();
    _power.speed.Reset();
  }

  public void SnakeMove(float direction) => _direction = direction;
}