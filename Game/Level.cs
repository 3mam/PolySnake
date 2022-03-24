using System.Drawing;
using OpenTK.Mathematics;

namespace Game;

public class Level
{
  private readonly Actor _level;

  public Level()
  {
    _level = Environment.Scene.CreateActor();
    _level.UploadData(Assets.Level);
    _level.Color(Color.SeaGreen);
    _level.Position(new Vector2(Environment.CenterWidth, Environment.CenterHeight));
    _level.Rotation(90f);
    _level.Scale(5f);
  }

  public void Draw() => _level.Draw();
}