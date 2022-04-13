using System;

namespace Game.Interface;

public interface ITrigger
{
  Action<bool> Trigger { get; set; }
}