using System.Drawing;
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
  private SnakePosition _snakeHeadPosition;
  private readonly SnakePosition[] _snakeBodyPositions = new SnakePosition[100];
  private SnakePosition _snakeTailPosition;
  private readonly int _snakeLenght = 10;
  private readonly float _scale = 0.025f;
  private readonly float _speed = 300f;
  private readonly Vector2 _starPosition;
  private readonly float _starDirection = 0f;
  private readonly Walls _walls;

  private readonly Func<bool, bool> _shakeCameraDuration =
    Timer.Create(new TimeSpan(0, 0, 0, 0, 200).Ticks);

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
    _snakeHeadPosition = new SnakePosition(_starPosition, _starDirection);
    for (var i = 0; i < _snakeLenght; i++)
      _snakeBodyPositions[i] = new SnakePosition(_starPosition, _starDirection);
    _snakeTailPosition = new SnakePosition(_starPosition, _starDirection);
    Reset();

    _level.UploadData(Assets.Level);
    _level.Color(Color.SeaGreen);
    _level.Position(new Vector2(_scene.Width, _scene.Height));
    _level.Rotation(90f);
    _level.Scale(5f);

    _apple.UploadData(Assets.Apple);
    _apple.Color(Color.Chartreuse);
    _walls = Walls.SmashWith(_snakeHeadPosition);
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
    var collide = _walls.CheckCollide();
    if (_shakeCameraDuration(collide))
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

    _apple.Position(new Vector2(200f, 200f));
    _apple.Scale(_scale);
    _apple.Draw();
  }

  public void Reset()
  {
    _snakeHeadPosition.Position = _starPosition;
    _snakeHeadPosition.Direction = _starDirection;
    for (var i = 0; i < _snakeLenght; i++)
      _snakeBodyPositions[i].Position = _starPosition - new Vector2(15f * i, 0);
    _snakeTailPosition.Position = _starPosition - new Vector2(15f * _snakeLenght, 0);
  }
}