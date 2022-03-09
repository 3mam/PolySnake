using OpenTK.Mathematics;

namespace Game;

public class Game
{
  private Actor _head = default!;
  private Actor _body = default!;
  private Actor _tail = default!;
  private SnakePosition _snakeHeadPosition = default!;
  private SnakePosition[] _snakeBodyPositions = new SnakePosition[100];
  private SnakePosition _snakeTailPosition = default!;
  private int _snakeLenght = 30;
  private float _scale = 0.025f;
  private float _speed = 50f;
  private Vector2 _starPosition;

  public static Game Create(Scene scene)
  {
    var game = new Game
    {
      _head = scene.CreateActor(),
      _body = scene.CreateActor(),
      _tail = scene.CreateActor(),
    };

    var red = System.Drawing.Color.Red;

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
    return game;
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
    Console.WriteLine($"{direct} {MathF.Atan2(headPosition.X, headPosition.Y)*180f/MathF.PI}");
    _snakeHeadPosition = new SnakePosition(headPosition, direct);
    _snakeBodyPositions[0] = _snakeBodyPositions[0].Motion(_snakeHeadPosition.Position);

    for (var i = 1; i < _snakeLenght; i++)
      _snakeBodyPositions[i] = _snakeBodyPositions[i].Motion(_snakeBodyPositions[i - 1].Position);

    _snakeTailPosition = _snakeTailPosition.Motion(_snakeBodyPositions[_snakeLenght - 1].Position);
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
}