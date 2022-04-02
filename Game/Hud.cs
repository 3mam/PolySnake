using Game.Interface;
using OpenTK.Mathematics;

namespace Game;

public class Hud
{
  private readonly Life _life = new();

  public void Draw()
  {
    _life.Draw();
  }

  public void Update(int life)
  {
    _life.Update(life);
  }
}