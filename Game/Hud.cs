using OpenTK.Mathematics;

namespace Game;

public class Hud
{
  private Actor _life = Settings.Scene.CreateActor();
  private Actor _heart = Settings.Scene.CreateActor();

  public Hud()
  {
    _life.UploadData(Assets.Life);
    _life.Position(new Vector2(200,980));
    _life.Scale(0.2f);
    _life.Color(Settings.HudColor);
    
    _heart.UploadData(Assets.Heart);
    _heart.Position(new Vector2(310,980));
    _heart.Scale(0.05f);
    _heart.Color(Settings.HudColor);
  }

  public void Draw()
  {
    _life.Draw();
    _heart.Draw();
  }
}