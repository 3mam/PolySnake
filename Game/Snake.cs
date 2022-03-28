using System.Drawing;
using Game.Collision;
using Game.Interface;
using OpenTK.Mathematics;

namespace Game;

public class Snake
{
  private Action<ICollide> _collideList = default!;
  private readonly Actor _head;
  private readonly Actor _body;
  private readonly Actor _tail;

  private readonly SnakePosition[] _bodyPositions = new SnakePosition[Settings.MaxSnakeLenght];
  private readonly SnakePosition _tailPosition;
  private readonly SnakePosition _headPosition;

  private int _lenght = Settings.SnakeLenght;
  private const float CollideRadius = 15f;

  public int Lenght
  {
    get => _lenght;
    set
    {
      if (value >= Settings.MaxSnakeLenght)
        _lenght = Settings.MaxSnakeLenght - 1;
      else
        _lenght = value;
    }
  }

  public float Speed { get; set; } = Settings.Speed;

  public Snake()
  {
    _head = Settings.Scene.CreateActor();
    _body = Settings.Scene.CreateActor();
    _tail = Settings.Scene.CreateActor();

    _head.UploadData(Assets.Head);
    _head.Scale(Settings.Scale);
    _head.Color(Settings.SnakeColor);

    _body.UploadData(Assets.Body);
    _body.Scale(Settings.Scale);
    _body.Color(Settings.SnakeColor);

    _tail.UploadData(Assets.Tail);
    _tail.Scale(Settings.Scale);
    _tail.Color(Settings.SnakeColor);

    _headPosition = new SnakePosition(Settings.StarPosition, Settings.StarDirection);
    for (var i = 0; i < _bodyPositions.Length; i++)
      _bodyPositions[i] = new SnakePosition(Settings.StarPosition, Settings.StarDirection);
    _tailPosition = new SnakePosition(Settings.StarPosition, Settings.StarDirection);
  }

  public void Move(float delta, float direction)
  {
    _headPosition.Move(Speed * delta, direction);
    _bodyPositions[0].Motion(_headPosition.Position);

    for (var i = 1; i <= Lenght; i++)
      _bodyPositions[i].Motion(_bodyPositions[i - 1].Position);

    _tailPosition.Motion(_bodyPositions[Lenght - 1].Position);
    CheckCollide();
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

  public bool MoveWhenSmashWithWall(WallsList wallsList)
  {
    switch (wallsList)
    {
      case WallsList.Left:
        _headPosition.Direction = 180f - _headPosition.Direction;
        _headPosition.Position += new Vector2(Settings.Recoil, 0);
        return true;
      case WallsList.Right:
        _headPosition.Direction = 180f - _headPosition.Direction;
        _headPosition.Position -= new Vector2(Settings.Recoil, 0);
        return true;
      case WallsList.Top:
        _headPosition.Direction = -_headPosition.Direction;
        _headPosition.Position -= new Vector2(0, Settings.Recoil);
        return true;
      case WallsList.Bottom:
        _headPosition.Direction = MathF.Abs(_headPosition.Direction);
        _headPosition.Position += new Vector2(0, Settings.Recoil);
        return true;
      case WallsList.None:
      default:
        return false;
    }
  }

  public void Reset()
  {
    Speed = Settings.Speed;
    Lenght = Settings.SnakeLenght;
    _headPosition.Position = Settings.StarPosition;
    _headPosition.Direction = Settings.StarDirection;
    for (var i = 0; i < Lenght; i++)
      _bodyPositions[i].Position = _headPosition.Position - new Vector2(15f * i, 0);
    _tailPosition.Position = _headPosition.Position - new Vector2(15f * Lenght, 0);
  }

  public void CollideWith(ICollideEvent item)
    => _collideList += item.Collide;

  private void CheckCollide()
  {
    var snake = new CollideCircle(_headPosition.Position, CollideRadius);
    _collideList(snake);
  }
}