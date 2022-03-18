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
  private SnakePosition _snakeHeadPosition = default!;
  private readonly SnakePosition[] _snakeBodyPositions = new SnakePosition[100];
  private SnakePosition _snakeTailPosition = default!;
  private readonly int _snakeLenght = 30;
  private readonly float _scale = 0.025f;
  private readonly float _speed = 650f;
  private readonly Vector2 _starPosition;

  private readonly Func<bool, bool> _shakeCameraDuration =
    Timer.Create(new TimeSpan(0, 0, 0, 0, 200).Ticks);

  private readonly float _shakeCameraRange = -25f;

  private readonly CollideLine _wallLeft =
    new(new Vector2(100f, 1000f), new Vector2(100f, 0f));

  private readonly CollideLine _wallTop =
    new(new Vector2(0f, 950f), new Vector2(2000f, 950f));

  private readonly CollideLine _wallRight =
    new(new Vector2(1900f, 0f), new Vector2(1900f, 1000f));

  private readonly CollideLine _wallBottom =
    new(new Vector2(0f, 50f), new Vector2(2000f, 50f));

  public static Game Create(Scene scene) => new Game(scene);

  private Game(Scene scene)
  {
    _scene = scene;
    _head = scene.CreateActor();
    _body = scene.CreateActor();
    _tail = scene.CreateActor();
    _level = scene.CreateActor();
    _apple = scene.CreateActor();

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

    _starPosition = new Vector2(_scene.Width / 4, _scene.Height / 2);
    InitSnake(_starPosition);

    _level.UploadData(Assets.Level);
    _level.Color(Color.SeaGreen);
    _level.Position(new Vector2(_scene.Width, _scene.Height));
    _level.Rotation(90f);
    _level.Scale(5f);

    _apple.UploadData(Assets.Apple);
    _apple.Color(Color.Chartreuse);
  }

  private void SnakeMove()
  {
    _snakeBodyPositions[0].Motion(_snakeHeadPosition.Position);

    for (var i = 1; i < _snakeLenght; i++)
      _snakeBodyPositions[i].Motion(_snakeBodyPositions[i - 1].Position);

    _snakeTailPosition.Motion(_snakeBodyPositions[_snakeLenght - 1].Position);
  }

  public void Move(float delta, float direction)
  {
    _snakeHeadPosition.Move(_speed * delta, direction);
    SnakeMove();
  }

  private void InitSnake(Vector2 startPosition)
  {
    _snakeHeadPosition = new SnakePosition(startPosition, 0f);
    for (var i = 0; i < _snakeLenght; i++)
    {
      _snakeBodyPositions[i] = new SnakePosition(
        startPosition - new Vector2(0, 15f + (10f * i)), 0);
    }

    _snakeTailPosition = new SnakePosition(
      startPosition - new Vector2(0, 15f + (10f * _snakeLenght - 1)), 0);
  }

  public void Draw()
  {
    var collide = CheckWallCollide();
    if (_shakeCameraDuration(collide))
      ShakeCameraRandomly(_shakeCameraRange);
    else
      ShakeCameraRandomly(0);

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

    _apple.Position(new Vector2(200f, 200f));
    _apple.Scale(_scale);
    _apple.Draw();
  }

  public void Reset()
  {
    InitSnake(_starPosition);
  }

  private void ShakeCameraRandomly(float range)
  {
    var random = new Random();
    var between = (range + range + 1);
    var x = (float) random.NextDouble() * between - range;
    var y = (float) random.NextDouble() * between - range;
    _scene.Camera(x, y);
  }

  private bool CheckWallCollide()
  {
    var head = new CollideCircle(_snakeHeadPosition.Position, 15f);
    var collide = false;
    const float recoil = 100f;
    if (_wallLeft == head)
    {
      _snakeHeadPosition.Direction = 180f - _snakeHeadPosition.Direction;
      _snakeHeadPosition.Position += new Vector2(recoil,0);
      collide = true;
    }

    if (_wallRight == head)
    {
      _snakeHeadPosition.Direction = 180f - _snakeHeadPosition.Direction;
      _snakeHeadPosition.Position -= new Vector2(recoil,0);
      collide = true;
    }

    if (_wallTop == head)
    {
      _snakeHeadPosition.Direction = -_snakeHeadPosition.Direction;
      _snakeHeadPosition.Position -= new Vector2(0,recoil);
      collide = true;
    }

    if (_wallBottom == head)
    {
      _snakeHeadPosition.Direction = MathF.Abs(_snakeHeadPosition.Direction);
      _snakeHeadPosition.Position += new Vector2(0,recoil);
      collide = true;
    }

    return collide;
  }
}