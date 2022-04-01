using OpenTK.Mathematics;
using Poly.Interface;

namespace Poly;

public class Arena
{
  private readonly IActor _level;

  public Arena()
  {
    _level = AssetManager.GetActor(AssetList.Level);
    _level.Color(Settings.LevelColor);
    _level.Position(new Vector2(Settings.CenterWidth, Settings.CenterHeight));
    _level.Rotation(90f);
    _level.Scale(5f);
  }

  public void Draw() => _level.Draw();
}