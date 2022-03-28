using OpenTK.Mathematics;

namespace Game;

public class SpawnPoints
{
  private const int Lenght = Settings.PowerUpNetWidth* Settings.PowerUpNetHeight;
  public int MaxPoints => Lenght;

  private readonly Vector2[] _netPoints =
    new Vector2[Lenght];

  private readonly bool[] _reservedPoints =
    new bool[Lenght];

  public SpawnPoints()
  {
    for (var y = 0; y < Settings.PowerUpNetHeight; y++)
    for (var x = 0; x < Settings.PowerUpNetWidth; x++)
      _netPoints[(y * Settings.PowerUpNetWidth) + x] = new Vector2(145f + (50f * x), 90f + (25f * y));
  }

  public Vector2? ReservePoint(int point)
  {
    if (point >= Lenght)
      return null;
    if (_reservedPoints[point])
      return null;
    _reservedPoints[point] = true;
    return _netPoints[point];
  }

  public void FreePoint(int point)
  {
    if (point <= Lenght)
      _reservedPoints[point] = false;
  }
  
  public (int id, Vector2 point) RandomPoint()
  {
    while (true)
    {
      var id = new Random().Next(0, Lenght);
      var point = ReservePoint(id);
      if (point == null) continue;
      return (id, (Vector2) point);
    }
  }
}