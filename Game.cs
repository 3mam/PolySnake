using OpenTK.Mathematics;

namespace PolySnake;

public class Game
{
  private Actor _head = default!;
  private Actor _body = default!;
  private Actor _tail = default!;
  private SnakePosition _snakeHeadPosition;
  private SnakePosition[] _snakeBodyPositions = default!;
  private SnakePosition _snakeTailPosition;
  private int _snakeLenght = 20;
  private float _scale = 0.025f;
  private float _speed = 200f;

  public static Game Create(Scene scene)
  {
    var game = new Game
    {
      _head = scene.CreateActor(),
      _body = scene.CreateActor(),
      _tail = scene.CreateActor(),
      _snakeBodyPositions = new SnakePosition[100],
    };
    
    game._head.UploadData(Snake.Head);
    game._head.Scale(game._scale);

    game._body.UploadData(Snake.Body);
    game._body.Scale(game._scale);

    game._tail.UploadData(Snake.Tail);
    game._tail.Scale(game._scale);

    game.InitSnake(new Vector2(scene.Width, scene.Height));
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
    _snakeHeadPosition = new SnakePosition(headPosition, direct);
    _snakeBodyPositions[0] = _snakeBodyPositions[0].Motion(_snakeHeadPosition.Position);
    
    for (var i = 1; i < _snakeLenght; i++)
      _snakeBodyPositions[i] = _snakeBodyPositions[i].Motion(_snakeBodyPositions[i - 1].Position);

    _snakeTailPosition = _snakeTailPosition.Motion(_snakeBodyPositions[_snakeLenght - 1].Position);
  }

  private void InitSnake(Vector2 startPosition)
  {
    _snakeHeadPosition = new SnakePosition(startPosition, 90f);
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
    _head.Rotation(_snakeHeadPosition.Direction-90f);
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
}