using Game.Hud;

namespace Game;

public class HudDisplay
{
  private readonly Life _life = new();
  private readonly Score _score = new();

  public void Draw()
  {
    _life.Draw();
    _score.Draw();
  }

  public void Update(int life, int score)
  {
    _life.Update(life);
    _score.Update(score);
  }
}