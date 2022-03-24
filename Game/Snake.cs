using System.Drawing;
using OpenTK.Mathematics;

namespace Game;

public class Snake
{
  private readonly Actor _head;
  private readonly Actor _body;
  private readonly Actor _tail;
  
  private readonly SnakePosition _snakeHeadPosition;
  private readonly SnakePosition[] _snakeBodyPositions = new SnakePosition[Environment.MaxSnakeLenght];
  private readonly SnakePosition _snakeTailPosition;

  public SnakePosition Position => _snakeHeadPosition;

  public Snake(Scene scene)
  {
    _head = scene.CreateActor();
    _body = scene.CreateActor();
    _tail = scene.CreateActor();
    var red = Color.Red;
    
    _head.UploadData(Assets.Head);
    _head.Scale(Environment.Scale);
    _head.Color(red);

    _body.UploadData(Assets.Body);
    _body.Scale(Environment.Scale);
    _body.Color(red);

    _tail.UploadData(Assets.Tail);
    _tail.Scale(Environment.Scale);
    _tail.Color(red);
    
    _snakeHeadPosition = new SnakePosition(Environment.StarPosition, Environment.StarDirection);
    for (var i = 0; i < _snakeBodyPositions.Length; i++)
      _snakeBodyPositions[i] = new SnakePosition(Environment.StarPosition, Environment.StarDirection);
    _snakeTailPosition = new SnakePosition(Environment.StarPosition, Environment.StarDirection);
  }
  
  public void Move(float delta, float direction)
  {
    _snakeHeadPosition.Move(Environment.Speed * delta, direction);
    _snakeBodyPositions[0].Motion(_snakeHeadPosition.Position);

    for (var i = 1; i < Environment.SnakeLenght; i++)
      _snakeBodyPositions[i].Motion(_snakeBodyPositions[i - 1].Position);

    _snakeTailPosition.Motion(_snakeBodyPositions[Environment.SnakeLenght - 1].Position);
  }

  public void Draw()
  {
    _head.Rotation(_snakeHeadPosition.Direction - 90f);
    _head.Position(_snakeHeadPosition.Position);
    _head.Draw();

    for (var i = 0; i < Environment.SnakeLenght; i++)
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
    Environment.SnakeLenght = 3;
    _snakeHeadPosition.Position = Environment.StarPosition;
    _snakeHeadPosition.Direction = Environment.StarDirection;
    for (var i = 0; i < Environment.SnakeLenght; i++)
      _snakeBodyPositions[i].Position = Environment.StarPosition - new Vector2(15f * i, 0);
    _snakeTailPosition.Position = Environment.StarPosition - new Vector2(15f * Environment.SnakeLenght, 0);
  }
}