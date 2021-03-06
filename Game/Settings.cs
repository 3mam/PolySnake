using System.Drawing;
using Game.Math;

namespace Game;

public static class Settings
{
  public const float CenterWidth = 1000f;
  public const float CenterHeight = 500f;
  public const int MaxSnakeLenght = 10000;
  public const int SnakeLenght = 3;
  public static readonly Vector2 CameraPosition = new (0, -15);
  public const float ShakeCameraRange = 20f;
  public const float Scale = 0.025f;
  public const float Speed  = 300f;
  public static Vector2 StarPosition => new(CenterWidth, CenterHeight);
  public const float StarDirection = 0.01f;
  public const float Recoil = 5f;
  
  public const int PowerUpNetWidth = 35;
  public const int PowerUpNetHeight = 34;

  public const int Life = 3;
  public const int MaxLife = 6;
  
  public static readonly Color SnakeColor = Color.Orange;
  public static readonly Color FoodColor = Color.Chartreuse;
  public static readonly Color SpeedColor = Color.Gold;
  public static readonly Color LevelColor = Color.DarkSlateGray;
  public static readonly Color TextColor = Color.White;
  public static readonly Color HeartEnableColor = Color.Red;
  public static readonly Color HeartDisableColor = Color.DimGray;
  public static readonly Color NotSelectedColor = Color.Gray;

  public const float SpeedUp  = 450f;
  public const int FoodReplaceTime = 10000; // 10s
  public const int SpeedUpDuration = 3000; // 3s
  public const int SpeedVisibilityTime = 5000; // 5s
  public const int ShowSpeedItemAtTime = 10000; // 10s

  public const int PointForFood = 100;
  public const int PointMultiplier = 10;
}