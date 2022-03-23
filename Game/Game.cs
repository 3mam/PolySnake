using System.Drawing;
using Game.Collision;
using OpenTK.Mathematics;

namespace Game;

public class Game
{
  private readonly Scene _scene;
  private readonly Actor _head;
  private readonly Actor _body;
  private readonly Actor _tail;
  private readonly Actor _level;
  private readonly Actor _apple;
  private readonly Actor _thunder;
  private readonly SnakePosition _snakeHeadPosition;
  private readonly SnakePosition[] _snakeBodyPositions = new SnakePosition[1000];
  private readonly SnakePosition _snakeTailPosition;
  private int _snakeLenght = 0;
  private readonly float _scale = 0.025f;
  private float _speed = 300f;
  private readonly Vector2 _starPosition;
  private readonly float _starDirection = 0.01f;
  private readonly Walls _walls;
  private readonly PowerUps _power;

  private readonly Timer _shakeCameraDuration = new(200);
  private readonly Timer _foodReposition = new(10000);
  private readonly Timer _speedVisibilityDuration = new(5000);
  private readonly Timer _speedShowUp = new(10000);
  private readonly Timer _speedDuration = new(3000);

  private readonly float _shakeCameraRange = -25f;
  public static Game Create(Scene scene) => new Game(scene);

  private Game(Scene scene)
  {
    _scene = scene;
    _head = scene.CreateActor();
    _body = scene.CreateActor();
    _tail = scene.CreateActor();
    _level = scene.CreateActor();
    _apple = scene.CreateActor();
    _thunder = scene.CreateActor();

    var red = Color.Red;

    _head.UploadData(Assets.Head);
    _head.Scale(_scale);
    _head.Color(red);

    _body.UploadData(Assets.Body);
    _body.Scale(_scale);
    _body.Color(red);

    _tail.UploadData(Assets.Tail);
    _tail.Scale(_scale);
    _tail.Color(red);

    _starPosition = new Vector2(_scene.Width, _scene.Height);
    _snakeHeadPosition = new SnakePosition(_starPosition, _starDirection);
    for (var i = 0; i < _snakeBodyPositions.Length; i++)
      _snakeBodyPositions[i] = new SnakePosition(_starPosition, _starDirection);
    _snakeTailPosition = new SnakePosition(_starPosition, _starDirection);

    _walls = Walls.SmashWith(_snakeHeadPosition);
    _power = PowerUps.SmashWith(_snakeHeadPosition);
    Reset();

    _level.UploadData(Assets.Level);
    _level.Color(Color.SeaGreen);
    _level.Position(new Vector2(_scene.Width, _scene.Height));
    _level.Rotation(90f);
    _level.Scale(5f);

    _apple.UploadData(Assets.Apple);
    _apple.Color(Color.Chartreuse);
    _apple.Scale(_scale + 0.01f);

    _thunder.UploadData(Assets.Thunder);
    _thunder.Scale(_scale + 0.01f);
    _thunder.Color(Color.Gold);
  }

  public void Move(float delta, float direction)
  {
    _snakeHeadPosition.Move(_speed * delta, direction);
    _snakeBodyPositions[0].Motion(_snakeHeadPosition.Position);

    for (var i = 1; i < _snakeLenght; i++)
      _snakeBodyPositions[i].Motion(_snakeBodyPositions[i - 1].Position);

    _snakeTailPosition.Motion(_snakeBodyPositions[_snakeLenght - 1].Position);
  }

  public void Draw()
  {
    if (_walls.CheckCollide())
      _shakeCameraDuration.Reset();
    if (_shakeCameraDuration.Duration())
      _scene.ShakeCameraRandomly(_shakeCameraRange);
    else
      _scene.ShakeCameraRandomly(0);

    _level.Draw();

    _head.Rotation(_snakeHeadPosition.Direction - 90f);
    _head.Position(_snakeHeadPosition.Position);
    _head.Draw();

    for (var i = 0; i < _snakeLenght; i++)
    {
      _body.Rotation(_snakeBodyPositions[i].Direction);
      _body.Position(_snakeBodyPositions[i].Position);
      _body.Draw();
    }

    _tail.Rotation(_snakeTailPosition.Direction);
    _tail.Position(_snakeTailPosition.Position);
    _tail.Draw();

    if (_power.CheckFoodCollide())
    {
      _snakeLenght++;
      _power.PlaceFoodRandomly();
      _foodReposition.Reset();
    }

    if (!_foodReposition.Duration(true))
      _power.PlaceFoodRandomly();

    _apple.Position(_power.FoodPosition);
    _apple.Draw();

    if (_power.CheckSpeedCollide())
    {
      _speed = 450f;
      _speedDuration.Reset();
      _speedVisibilityDuration.Stop();
    }

    if (!_speedDuration.Duration())
      _speed = 300f;
    
    _thunder.Position(_power.SpeedPosition);
    if (_speedVisibilityDuration.Duration())
      _thunder.Draw();

    if (!_speedShowUp.Duration(true))
    {
      _speedVisibilityDuration.Reset();
      _power.PlaceSpeedRandomly();
    }
  }

  public void Reset()
  {
    _snakeLenght = 3;
    _snakeHeadPosition.Position = _starPosition;
    _snakeHeadPosition.Direction = _starDirection;
    for (var i = 0; i < _snakeLenght; i++)
      _snakeBodyPositions[i].Position = _starPosition - new Vector2(15f * i, 0);
    _snakeTailPosition.Position = _starPosition - new Vector2(15f * _snakeLenght, 0);

    _power.PlaceFoodRandomly();
    _power.PlaceSpeedRandomly();
    _foodReposition.Reset();
    _speedShowUp.Reset();
  }
}