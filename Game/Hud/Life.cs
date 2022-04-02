using Game.Interface;
using OpenTK.Mathematics;

namespace Game;

public class Life
{
  private readonly IActor _life;
  private readonly IActor _heart;
  private readonly (bool enable, Vector2 position)[] _hearts=
    new (bool, Vector2)[Settings.MaxLife];
  public Life()
  {
    _life = AssetManager.GetActor(AssetList.Life);
    _life.Position(new Vector2(200, 980));
    _life.Scale(0.2f);
    _life.Color(Settings.TextColor);
    
    for (var i = 0; i < Settings.MaxLife; i++)
      _hearts[i] = (false, new Vector2(310+60*i, 980));
    _heart = AssetManager.GetActor(AssetList.Heart);
    _heart.Scale(0.05f);
  }
  public void Draw()
  {
    _life.Draw();
    foreach (var hear in _hearts)
    {
      _heart.Position(hear.position);
      _heart.Color(hear.enable ? Settings.HeartEnableColor : Settings.HeartDisableColor);
      _heart.Draw();
    }
  }

  public void Update(int hearts)
  {
    for (var i=0; i < hearts; i++)
      _hearts[i].enable = true;

    for (var i = hearts; i < _hearts.Length; i++)
      _hearts[i].enable = false;
  }
}