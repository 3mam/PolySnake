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
    Timer.Init(new TimeSpan(0,0,0,0,200).Ticks);
  private readonly float _shakeCameraRange = -25f;

  private readonly CollideLine _wallLeft =
    new CollideLine(new Vector2(100f, 1000f), new Vector2(100f, 0f));

  private readonly CollideLine _wallTop =
    new CollideLine(new Vector2(0f, 950f), new Vector2(2000f, 950f));

  private readonly CollideLine _wallRight =
    new CollideLine(new Vector2(1900f, 0f), new Vector2(1900f, 1000f));

  private readonly CollideLine _wallBottom =
    new CollideLine(new Vector2(0f, 50f), new Vector2(2000f, 50f));

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
  private void SnakeMove(Vector2 position, float direction)
  {
    _snakeHeadPosition.Update(position, direction);
    _snakeBodyPositions[0].Motion(_snakeHeadPosition.Position);

    for (var i = 1; i < _snakeLenght; i++)
      _snakeBodyPositions[i].Motion(_snakeBodyPositions[i - 1].Position);

    _snakeTailPosition.Motion(_snakeBodyPositions[_snakeLenght - 1].Position);
  }

  public void Move(float delta, float direction)
  {
    var speed = _speed * delta;
    var direct = _snakeHeadPosition.Direction + (direction * speed);
    var radian = direct / 180f * MathF.PI;
    var headPosition = _snakeHeadPosition.Position + new Vector2(
      MathF.Cos(radian) * speed,
      MathF.Sin(radian) * speed
    );
    SnakeMove(headPosition, direct);
  }

  private void DirectMove(float direction)
  {
    var radian = direction / 180f * MathF.PI;
    var headPosition = _snakeHeadPosition.Position + new Vector2(
      MathF.Cos(radian),
      MathF.Sin(radian)
    );
    SnakeMove(headPosition, direction);
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
    if (_wallLeft == head)
    {
      DirectMove(180f - _snakeHeadPosition.Direction);
      collide = true;
    }

    if (_wallRight == head)
    {
      DirectMove(180f - _snakeHeadPosition.Direction);
      collide = true;
    }

    if (_wallTop == head)
    {
      DirectMove(-_snakeHeadPosition.Direction);
      collide = true;
    }

    if (_wallBottom == head)
    {
      DirectMove(MathF.Abs(_snakeHeadPosition.Direction));
      collide = true;
    }

    return collide;
  }
}