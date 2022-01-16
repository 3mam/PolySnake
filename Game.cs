using OpenTK.Mathematics;

namespace PolySnake;

public class Game
{
  private Actor _head = default!;
  private Actor _body = default!;
  private Actor _tail = default!;
  private SnakePosition[] _snakePositions = default!;
  private SnakePosition _snakeTailPosition = default!;
  private int _snakeLenght = 20;
  private float _scale = 0.025f;
  private Vector2 _startPosition;
  private Vector2 _position;
  private float _direction = 90f;
  private float _viewDirection = 90f;
  private float _speed = 200f;
  private float _delta;

  public static Game Create(Scene scene)
  {
    var game = new Game
    {
      _head = scene.CreateActor(),
      _body = scene.CreateActor(),
      _tail = scene.CreateActor(),
      _startPosition = new Vector2(scene.Width, scene.Height),
      _position = new Vector2(scene.Width, scene.Height),
      _snakePositions = new SnakePosition[100],
    };
    game._head.UploadData(Snake.Head);
    game._head.Scale(game._scale);

    game._body.UploadData(Snake.Body);
    game._body.Scale(game._scale);

    game._tail.UploadData(Snake.Tail);
    game._tail.Scale(game._scale);
    
    game.InitBody();
    game._snakeTailPosition = new SnakePosition(
      game._startPosition - new Vector2(0, 15f + (10f * game._snakeLenght-1)),
      game._direction - game._viewDirection);
  
    return game;
  }

  private Vector2 MotionHead(float direction)
  {
    var speed = _speed * _delta;
    var radian = direction / 180f * MathF.PI;
    return new Vector2(
      MathF.Cos(radian) * speed,
      MathF.Sin(radian) * speed
    );
  }

  public void Move(float delta, float direction)
  {
    _delta = delta;
    _direction += direction * (_delta * _speed);
    _position += MotionHead(_direction);
    MoveBody();
    _snakeTailPosition = _snakeTailPosition.Motion(_snakePositions[_snakeLenght - 1].Position);
  }

  private void InitBody()
  {
    for (var i = 0; i < _snakeLenght; i++)
    {
      _snakePositions[i] = new SnakePosition(
        _startPosition - new Vector2(0, 15f + (10f * i)),
        _direction - _viewDirection);
    }
  }

  private void MoveBody()
  {
    _snakePositions[0] = _snakePositions[0].Motion(_position);
    for (int i = 1; i < _snakeLenght; i++)
      _snakePositions[i] = _snakePositions[i].Motion(_snakePositions[i - 1].Position);
  }

  private void DrawBody()
  {
    for (var i = 0; i < _snakeLenght; i++)
    {
      _body.Rotation(_snakePositions[i].Direction);
      _body.Position(_snakePositions[i].Position);
      _body.Draw();
    }
  }

  public void Draw()
  {
    _head.Rotation(_direction - _viewDirection);
    _head.Position(_position);
    _head.Draw();
    DrawBody();    
    _tail.Rotation(_snakeTailPosition.Direction);
    _tail.Position(_snakeTailPosition.Position);
    _tail.Draw();
  }
}