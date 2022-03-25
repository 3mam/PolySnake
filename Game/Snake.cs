using System.Drawing;
using OpenTK.Mathematics;

namespace Game;

public class Snake
{
  private readonly Actor _head;
  private readonly Actor _body;
  private readonly Actor _tail;

  private readonly SnakePosition[] _bodyPositions = new SnakePosition[Environment.MaxSnakeLenght];
  private readonly SnakePosition _tailPosition;
  private readonly SnakePosition _headPosition;
  
  private int _lenght = Environment.SnakeLenght;
  public int Lenght
  {
    get => _lenght;
    set
    {
      if (value >= Environment.MaxSnakeLenght)
        _lenght = Environment.MaxSnakeLenght - 1;
      else
        _lenght = value;
    }
  }
  public Vector2 Position => _headPosition.Position;
  
  public float Speed { get; set; } = Environment.Speed;

  public Snake()
  {
    _head = Environment.Scene.CreateActor();
    _body = Environment.Scene.CreateActor();
    _tail = Environment.Scene.CreateActor();

    _head.UploadData(Assets.Head);
    _head.Scale(Environment.Scale);
    _head.Color(Environment.SnakeColor);

    _body.UploadData(Assets.Body);
    _body.Scale(Environment.Scale);
    _body.Color(Environment.SnakeColor);

    _tail.UploadData(Assets.Tail);
    _tail.Scale(Environment.Scale);
    _tail.Color(Environment.SnakeColor);

    _headPosition = new SnakePosition(Environment.StarPosition, Environment.StarDirection);
    for (var i = 0; i < _bodyPositions.Length; i++)
      _bodyPositions[i] = new SnakePosition(Environment.StarPosition, Environment.StarDirection);
    _tailPosition = new SnakePosition(Environment.StarPosition, Environment.StarDirection);
  }

  public void Move(float delta, float direction)
  {
    _headPosition.Move(Speed * delta, direction);
    _bodyPositions[0].Motion(_headPosition.Position);

    for (var i = 1; i <= Lenght; i++)
      _bodyPositions[i].Motion(_bodyPositions[i - 1].Position);

    _tailPosition.Motion(_bodyPositions[Lenght - 1].Position);
  }

  public void Draw()
  {
    _head.Rotation(_headPosition.Direction - 90f);
    _head.Position(_headPosition.Position);
    _head.Draw();

    for (var i = 0; i < Lenght; i++)
    {
      _body.Rotation(_bodyPositions[i].Direction);
      _body.Position(_bodyPositions[i].Position);
      _body.Draw();
    }

    _tail.Rotation(_tailPosition.Direction);
    _tail.Position(_tailPosition.Position);
    _tail.Draw();
  }

  public bool MoveWhenSmashWithWall(Wall wall)
  {
    const float recoil = 5f;
    switch (wall)
    {
      case Wall.Left:
        _headPosition.Direction = 180f - _headPosition.Direction;
        _headPosition.Position += new Vector2(recoil, 0);
        return true;
      case Wall.Right:
        _headPosition.Direction = 180f - _headPosition.Direction;
        _headPosition.Position -= new Vector2(recoil, 0);
        return true;
      case Wall.Top:
        _headPosition.Direction = -_headPosition.Direction;
        _headPosition.Position -= new Vector2(0, recoil);
        return true;
      case Wall.Bottom:
        _headPosition.Direction = MathF.Abs(_headPosition.Direction);
        _headPosition.Position += new Vector2(0, recoil);
        return true;
      case Wall.None:
      default:
        return false;
    }
  }

  public void Reset()
  {
    Speed = Environment.Speed;
    Lenght = Environment.SnakeLenght;
    _headPosition.Position = Environment.StarPosition;
    _headPosition.Direction = Environment.StarDirection;
    for (var i = 0; i < Lenght; i++)
      _bodyPositions[i].Position = _headPosition.Position - new Vector2(15f * i, 0);
    _tailPosition.Position = _headPosition.Position - new Vector2(15f * Lenght, 0);
  }
}