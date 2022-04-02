using System;
using System.Net.Sockets;
using Game.Interface;
using Game.PowerItem;

namespace Game;

public class Game
{
  private readonly IScene _scene;
  private readonly Arena _arena = new();
  private readonly Walls _walls = new();
  private readonly HudDisplay _hudDisplay = new();
  private readonly Timer _shakeCameraDuration = new(200);
  private readonly Snake _snake = new();
  private readonly (IPowerUp food, IPowerUp speed) _power;
  private readonly SpawnPoints _spawnPoints = new();
  private int _life = Settings.Life;

  private int Life
  {
    get => _life;
    set
    {
      if (_life is > 0 and <= Settings.MaxLife)
        _life = value;
    }
  }

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
    if (_shakeCameraDuration.Duration())
      _scene.ShakeCameraRandomly(Settings.ShakeCameraRange);
    else
      _scene.Camera(Settings.CameraPosition);
    
    //if (Life == 0)
    //  return;
    
    _snake.Move(delta, _direction);
    if (_snake.MoveWhenSmashWithWall(_walls.Current))
    {
      _shakeCameraDuration.Reset();
      Life--;
    }
    _hudDisplay.Update(Life, 9237864);
    _power.food.Update();
    _power.speed.Update();
  }

  public void Draw()
  {
    _scene.Clear();
    _hudDisplay.Draw();
    _arena.Draw();    
    _snake.Draw();
    _power.food.Draw();
    _power.speed.Draw();
  }

  public void Reset()
  {
    _life = Settings.Life;
    _snake.Reset();
    _power.food.Reset();
    _power.speed.Reset();
  }

  public void SnakeMove(float direction) => _direction = direction;
}