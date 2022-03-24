using OpenTK.Mathematics;

namespace Game;

public static class Environment
{
  public const float CenterWidth = 1000f;
  public const float CenterHeight = 500f;
  public const int MaxSnakeLenght = 10000;
  public static readonly Vector2 CameraPosition = new (0, -40);
  private static int _snakeLenght = 3;

  public static int SnakeLenght
  {
    get => _snakeLenght;
    set
    {
      if (value >= MaxSnakeLenght)
        _snakeLenght = MaxSnakeLenght - 1;
      else
        _snakeLenght = value;
    }
  }

  public static float Scale => 0.025f;
  public static float Speed { get; set; } = 300f;
  public static Scene Scene = default!;


  public static Vector2 StarPosition => new(CenterWidth, CenterHeight);
  public const float StarDirection = 0.01f;
}