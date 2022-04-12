using System;

namespace Game.Interface;

public interface ICollideEvent
{
  void Collide(Func<ICollide, bool> snake);
}