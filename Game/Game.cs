using System;
using Game.Hud;
using Game.Interface;
using Game.Math;
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
  private readonly Menu _menu = new();
  private int _life = Settings.Life;
  private bool _start;
  private float _direction;
  private int _score;
  private int _pointMultiplier;
  private Vector2 _cameraPostion = Settings.CameraPosition;

  private int Life
  {
    get => _life;
    set
    {
      if (_life is >= 0 and <= Settings.MaxLife)
        _life = value;
    }
  }

  public bool Exit { get; private set; }

  private void SnakeSize(bool trigger)
  {
    if (!trigger) return;
    _snake.Lenght += 1;
    _score += Settings.PointForFood * _pointMultiplier;
  }

  private void SnakeSpeed(bool boost)
  {
    if (boost)
    {
      _snake.Speed = Settings.SpeedUp;
      _pointMultiplier = Settings.PointMultiplier;
    }
    else
    {
      _snake.Speed = Settings.Speed;
      _pointMultiplier = 1;
    }
  }

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
    Begin();
  }

  private void Begin()
  {
    _start = false;
    _menu.Visible = true;
    _menu.DisableContinue = true;
    _menu.Option = MenuSelect.NewGame;
  }

  public void Update(float delta)
  {
    if (_shakeCameraDuration.Duration())
    {
      ShakeCameraRandomly(Settings.ShakeCameraRange);
    }
    else
    {
      _cameraPostion = Settings.CameraPosition;
      _scene.Camera(_cameraPostion);
    }

    if (_menu.Visible)
      return;

    if (Life == 0)
      Begin();

    _snake.Move(delta, _direction);
    if (_snake.MoveWhenSmashWithWall(_walls.Current))
    {
      _shakeCameraDuration.Reset();
      Life--;
    }

    _hudDisplay.Update(Life, _score);
    _power.food.Update();
    _power.speed.Update();
  }

  public void Draw()
  {
    _scene.Clear();
    _hudDisplay.Draw();
    _arena.Draw();
    if (_start)
    {
      _snake.Draw();
      _power.food.Draw();
      _power.speed.Draw();
    }

    _menu.Draw();
  }

  private void Reset()
  {
    Life = Settings.Life;
    _score = 0;
    _snake.Reset();
    _power.food.Reset();
    _power.speed.Reset();
    _hudDisplay.Update(Life, 0);
  }

  public void SnakeMoveLeft() => _direction = 1;
  public void SnakeMoveRight() => _direction = -1;
  public void SnakeMoveStraight() => _direction = 0;

  public void ToggleMenu()
  {
    if (_start)
    {
      _menu.Visible = !_menu.Visible;
      _menu.Option = MenuSelect.Continue;
    }
    else
    {
      _menu.Visible = true;
    }
  }

  public void SelectNextOption()
    => _menu.SelectOption(-1);

  public void SelectPreviousOption()
    => _menu.SelectOption(1);

  public void Enter()
  {
    switch (_menu.Option)
    {
      case MenuSelect.Exit:
        Exit = true;
        break;
      case MenuSelect.NewGame:
        Reset();
        _start = true;
        _menu.DisableContinue = false;
        _menu.Option = MenuSelect.Continue;
        _menu.Visible = false;
        break;
      case MenuSelect.Continue:
        _menu.Visible = false;
        break;
    }
  }

  private void ShakeCameraRandomly(float range)
  {
    if (range == 0)
      return;
    var random = new Random();
    var between = range * 2 + 1;
    var x = random.NextSingle() * between - range;
    var y = random.NextSingle() * between - range;
    _scene.Camera(new Vector2(_cameraPostion.X + x, _cameraPostion.Y + y));
  }
}