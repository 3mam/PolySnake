using Game.Interface;
using Game.Math;

namespace Game.Hud;

public class Score
{
  private readonly IActor _score;
  private readonly IActor[] _numbers = new IActor[10];
  private string _scoreString;

  public Score()
  {
    _score = AssetManager.GetActor(AssetList.Score);
    _score.Position(new Vector2(1000, 980));
    _score.Scale(0.2f);
    _score.Color(Settings.TextColor);

    _numbers[0] = AssetManager.GetActor(AssetList.N0);
    _numbers[1] = AssetManager.GetActor(AssetList.N1);
    _numbers[2] = AssetManager.GetActor(AssetList.N2);
    _numbers[3] = AssetManager.GetActor(AssetList.N3);
    _numbers[4] = AssetManager.GetActor(AssetList.N4);
    _numbers[5] = AssetManager.GetActor(AssetList.N5);
    _numbers[6] = AssetManager.GetActor(AssetList.N6);
    _numbers[7] = AssetManager.GetActor(AssetList.N7);
    _numbers[8] = AssetManager.GetActor(AssetList.N8);
    _numbers[9] = AssetManager.GetActor(AssetList.N9);
    foreach (var t in _numbers)
    {
      t.Scale(0.2f);
      t.Color(Settings.TextColor);
    }
  }

  public void Update(int score)
    => _scoreString = score.ToString();

  public void Draw()
  {
    _score.Draw();
    for (var i = 0; i < _scoreString.Length; i++)
    {
      var s = _scoreString[i];
      var n = int.Parse(s.ToString());
      _numbers[n].Position(new Vector2(1120 + 35 * i, 980.5f));
      _numbers[n].Draw();
    }
  }
}