using System.Net.Sockets;
using Poly.Interface;
using Poly.PowerItem;

namespace Poly;

public class Game
{
  private readonly IScene _scene;
  private readonly Arena _arena = new();
  private readonly Walls _walls = new();
  private readonly Hud _hud = new();
  private readonly Timer _shakeCameraDuration = new(200);
  private readonly Snake _snake = new();
  private readonly (IPowerUp food, IPowerUp speed) _power;
  private readonly SpawnPoints _spawnPoints = new();

  private void SnakeSize(bool trigger)
  {
    if (trigger)
      _snake.Lenght += 1;
  }

  private void SnakeSpeed(bool boost)
    => _snake.Speed = boost ? Settings.SpeedUp : Settings.Speed;

  private float _direction;
  
  public Game(IScene scene)
  {
    _scene = scene;
    _snake.CollideWith(_walls);
    _power.food = new Food();
    _snake.CollideWith(_power.food);
    _power.food.SpawnPoints(_spawnPoints);
    _power.food.Trigger(SnakeSize);
    _power.speed = new Speed();  
    _snake.CollideWith(_power.speed);
    _power.speed.SpawnPoints(_spawnPoints);
    _power.speed.Trigger(SnakeSpeed);
    Reset();
  }

  public void Update(float delta)
  {
    _snake.Move(delta, _direction);
    if (_snake.MoveWhenSmashWithWall(_walls.Current))
      _shakeCameraDuration.Reset();
    if (_shakeCameraDuration.Duration())
      _scene.ShakeCameraRandomly(Settings.ShakeCameraRange);
    else
      _scene.Camera(Settings.CameraPosition);

    _power.food.Update();
    _power.speed.Update();
  }

  public void Draw()
  {
    _scene.Clear();
    _hud.Draw();
    _arena.Draw();
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