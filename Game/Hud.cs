using OpenTK.Mathematics;
using Poly.Interface;

namespace Poly;

public class Hud
{
  private readonly IActor _life;
  private readonly IActor _heart;

  public Hud()
  {
    _life = AssetManager.GetActor(AssetList.Life);
    _life.Position(new Vector2(200, 980));
    _life.Scale(0.2f);
    _life.Color(Settings.HudColor);

    _heart = AssetManager.GetActor(AssetList.Heart);
    _heart.Position(new Vector2(310, 980));
    _heart.Scale(0.05f);
    _heart.Color(Settings.HudColor);
  }

  public void Draw()
  {
    _life.Draw();
    _heart.Draw();
  }
}