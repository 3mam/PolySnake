using System.Drawing;
using OpenTK.Mathematics;

namespace Game;

public static class Environment
{
  public const float CenterWidth = 1000f;
  public const float CenterHeight = 500f;
  public const int MaxSnakeLenght = 10000;
  public const int SnakeLenght = 3;
  public static readonly Vector2 CameraPosition = new (0, -15);
  public const float ShakeCameraRange = -25f;
  public const float Scale = 0.025f;
  public const float Speed  = 300f;
  public const float SpeedUp  = 450f;
  public static Scene Scene = default!;
  public static Vector2 StarPosition => new(CenterWidth, CenterHeight);
  public const float StarDirection = 0.01f;
  
  public static readonly Color SnakeColor = Color.Red;
  public static readonly Color FoodColor = Color.Chartreuse;
  public static readonly Color SpeedColor = Color.Gold;
  public static readonly Color LevelColor = Color.SeaGreen;
  public static readonly Color HudColor = Color.White;

}