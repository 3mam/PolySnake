using System;
using Game.Interface;
using OpenTK.Mathematics;

namespace Game.Hud;

public class Menu
{
  private readonly IActor _newGame;
  private readonly IActor _continue;
  private readonly IActor _exit;
  private int _selected;
  public bool DisableContinue { get; set; }
  public bool Visible { get; set; }
  
  public MenuSelect Option { 
    get => (MenuSelect)_selected; 
    set => _selected = (int)value;
  }

  public Menu()
  {
    _newGame = AssetManager.GetActor(AssetList.NewGame);
    _continue = AssetManager.GetActor(AssetList.Continue);
    _exit = AssetManager.GetActor(AssetList.Exit);

    _continue.Position(new Vector2(1000, 600));
    _continue.Scale(0.3f);
    _continue.Color(Settings.TextColor);

    _newGame.Position(new Vector2(1000, 500));
    _newGame.Scale(0.3f);
    _newGame.Color(Settings.NotSelectedColor);

    _exit.Position(new Vector2(1000, 400));
    _exit.Scale(0.3f);
    _exit.Color(Settings.NotSelectedColor);
  }
  
  public void SelectOption(int val)
  {
    if (!Visible)
      return;
    var newVal = _selected + val;
    if (DisableContinue)
      _selected = newVal switch
      {
        (< (int) MenuSelect.NewGame) => (int) MenuSelect.Exit,
        (> (int) MenuSelect.Exit) => (int) MenuSelect.NewGame,
        _ => newVal
      };
    else
      _selected = newVal switch
      {
        (< (int) MenuSelect.Continue) => (int) MenuSelect.Exit,
        (> (int) MenuSelect.Exit) => (int) MenuSelect.Continue,
        _ => newVal
      };
  }

  public void Draw()
  {
    if (!Visible)
      return;

    _continue.Color(Settings.NotSelectedColor);
    _newGame.Color(Settings.NotSelectedColor);
    _exit.Color(Settings.NotSelectedColor);

    switch ((MenuSelect) _selected)
    {
      case MenuSelect.Continue:
        _continue.Color(Settings.TextColor);
        break;
      case MenuSelect.NewGame:
        _newGame.Color(Settings.TextColor);
        break;
      case MenuSelect.Exit:
        _exit.Color(Settings.TextColor);
        break;
    }
    
    _continue.Draw();
    _newGame.Draw();
    _exit.Draw();
  }
}