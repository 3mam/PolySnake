using System;

namespace Game.Interface;

public interface ITrigger
{
  void Trigger(Action<bool> fn);
}