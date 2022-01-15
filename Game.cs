using OpenTK.Mathematics;

namespace PolySnake;

public class Game
{
  private Actor _head = default!;
  private Actor _body = default!;
  private Actor _tail = default!;
  private float _scale = 0.025f;
  private Vector2 _startPosition;
  private Vector2 _position;
  private float _direction = 90f;

  public static Game Create(Scene scene)
  {
    var game = new Game
    {
      _head = scene.CreateActor(),
      _body = scene.CreateActor(),
      _startPosition = new Vector2(scene.Width, scene.Height),
      _position = new Vector2(scene.Width, scene.Height),
    };
    game._head.UploadData(Snake.Head);
    game._head.Position(game._startPosition);
    game._head.Scale(game._scale);
    
    game._body.UploadData(Snake.Body);
    game._body.Scale(0.5f);
    game._body.Position(game._startPosition);

    return game;
  }

  public void Move(float delta, float direction)
  {
    var speed = 100f * delta;
    _direction += direction * speed;
    var radian = _direction / 180f * MathF.PI;
    _position.X += MathF.Cos(radian) * speed;
    _position.Y += MathF.Sin(radian) * speed;
    _head.Rotation(_direction - 90f);
    _head.Position(_position);
  }

  public void Draw()
  {
    _head.Show();
    _body.Show();
  }
}