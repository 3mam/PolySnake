using System.Drawing;
using Game.Collision;
using OpenTK.Mathematics;

namespace Game;

public class Game
{
  private readonly Scene _scene;

  private readonly Actor _level;
  private readonly Actor _apple;
  private readonly Actor _thunder;
  
  private readonly Walls _walls;
  private readonly PowerUps _power;

  private readonly Timer _shakeCameraDuration = new(200);
  private readonly Timer _foodReposition = new(10000);
  private readonly Timer _speedVisibilityDuration = new(5000);
  private readonly Timer _speedShowUp = new(10000);
  private readonly Timer _speedDuration = new(3000);

  private readonly float _shakeCameraRange = -25f;

  public readonly Snake Snake;
  public static Game Create(Scene scene) => new Game(scene);

  private Game(Scene scene)
  {
    _scene = scene;
    Snake = new Snake(scene);
    _level = scene.CreateActor();
    _apple = scene.CreateActor();
    _thunder = scene.CreateActor();

    _walls = Walls.SmashWith(Snake.Position);
    _power = PowerUps.SmashWith(Snake.Position);
    Reset();

    _level.UploadData(Assets.Level);
    _level.Color(Color.SeaGreen);
    _level.Position(new Vector2(_scene.Width, _scene.Height));
    _level.Rotation(90f);
    _level.Scale(5f);

    _apple.UploadData(Assets.Apple);
    _apple.Color(Color.Chartreuse);
    _apple.Scale(Environment.Scale + 0.01f);

    _thunder.UploadData(Assets.Thunder);
    _thunder.Scale(Environment.Scale + 0.01f);
    _thunder.Color(Color.Gold);
  }

  public void Draw()
  {
    
    if (_walls.CheckCollide())
      _shakeCameraDuration.Reset();
    if (_shakeCameraDuration.Duration())
      _scene.ShakeCameraRandomly(_shakeCameraRange);
    else
      _scene.ShakeCameraRandomly(0);

    _level.Draw();

    Snake.Draw();

    if (_power.CheckFoodCollide())
    {
      Environment.SnakeLenght++;
      _power.PlaceFoodRandomly();
      _foodReposition.Reset();
    }

    if (!_foodReposition.Duration(true))
      _power.PlaceFoodRandomly();

    _apple.Position(_power.FoodPosition);
    _apple.Draw();

    if (_power.CheckSpeedCollide())
    {
      Environment.Speed = 450f;
      _speedDuration.Reset();
      _speedVisibilityDuration.Stop();
    }

    if (!_speedDuration.Duration())
      Environment.Speed = 300f;
    
    _thunder.Position(_power.SpeedPosition);
    if (_speedVisibilityDuration.Duration())
      _thunder.Draw();

    if (!_speedShowUp.Duration(true))
    {
      _speedVisibilityDuration.Reset();
      _power.PlaceSpeedRandomly();
    }
  }

  public void Reset()
  {
    Snake.Reset();
    _power.PlaceFoodRandomly();
    _power.PlaceSpeedRandomly();
    _foodReposition.Reset();
    _speedShowUp.Reset();
  }
}