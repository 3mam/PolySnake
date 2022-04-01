using System;

namespace Poly.Interface;

public interface ITrigger
{
  void Trigger(Action<bool> fn);
}