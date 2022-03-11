using System.Drawing;
using Game.Collision;
using OpenTK.Mathematics;

namespace Game;

public class Game
{
  private Actor _head = default!;
  private Actor _body = default!;
  private Actor _tail = default!;
  private Actor _level = default!;
  private SnakePosition _snakeHeadPosition = default!;
  private SnakePosition[] _snakeBodyPositions = new SnakePosition[100];
  private SnakePosition _snakeTailPosition = default!;
  private int _snakeLenght = 30;
  private float _scale = 0.025f;
  private float _speed = 650f;
  private Vector2 _starPosition;

  private CollideLine _wallLeft =
    new CollideLine(new Vector2(100f, 1000f), new Vector2(100f, 0f));

  private CollideLine _wallTop =
    new CollideLine(new Vector2(0f, 950f), new Vector2(2000f, 950f));

  private CollideLine _wallRight =
    new CollideLine(new Vector2(1900f, 0f), new Vector2(1900f, 1000f));

  private CollideLine _wallBottom =
    new CollideLine(new Vector2(0f, 50f), new Vector2(2000f, 50f));


  public static Game Create(Scene scene)
  {
    var game = new Game
    {
      _head = scene.CreateActor(),
      _body = scene.CreateActor(),
      _tail = scene.CreateActor(),
      _level = scene.CreateActor(),
    };

    var red = Color.Red;

    game._head.UploadData(Snake.Head);
    game._head.Scale(game._scale);
    game._head.Color(red);

    game._body.UploadData(Snake.Body);
    game._body.Scale(game._scale);
    game._body.Color(red);

    game._tail.UploadData(Snake.Tail);
    game._tail.Scale(game._scale);
    game._tail.Color(red);

    game._starPosition = new Vector2(scene.Width / 4, scene.Height / 2);
    game.InitSnake(game._starPosition);

    game._level.UploadData(Level.Zero);
    game._level.Color(Color.SeaGreen);
    game._level.Position(new Vector2(scene.Width, scene.Height));
    game._level.Rotation(90f);
    game._level.Scale(5f);
    return game;
  }

  private void SnakeMove(Vector2 position, float direction)
  {
    _snakeHeadPosition = new SnakePosition(position, direction);
    _snakeBodyPositions[0] = _snakeBodyPositions[0].Motion(_snakeHeadPosition.Position);

    for (var i = 1; i < _snakeLenght; i++)
      _snakeBodyPositions[i] = _snakeBodyPositions[i].Motion(_snakeBodyPositions[i - 1].Position);

    _snakeTailPosition = _snakeTailPosition.Motion(_snakeBodyPositions[_snakeLenght - 1].Position);
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
    Console.WriteLine($"{direct} {headPosition.X} {headPosition.Y}");
    SnakeMove(headPosition, direct);
  }

  public void DirectMove(float direction)
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
  }

  public void Reset()
  {
    InitSnake(_starPosition);
  }
  
  public void CheckCollide()
  {
    var head = new CollideCircle(_snakeHeadPosition.Position, 15f);
    if (_wallLeft == head)
      DirectMove(180f - _snakeHeadPosition.Direction);
    if (_wallRight == head)
      DirectMove(180f - _snakeHeadPosition.Direction);
    if (_wallTop == head)
      DirectMove(-_snakeHeadPosition.Direction);
    if (_wallBottom == head)
      DirectMove(MathF.Abs(_snakeHeadPosition.Direction));
  }
}