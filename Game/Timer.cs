using System;

namespace Game;

public class Timer
{
  private long _timeEnd;
  private readonly long _ticks;

  public Timer(int millisecond) =>
    _ticks = new TimeSpan(0, 0, 0, 0, millisecond).Ticks;

  public bool Duration(bool persistent = false)
  {
    var isTime = DateTime.Now.Ticks < _timeEnd;
    if (persistent && !isTime)
      Reset();
    return isTime;
  }

  public void Reset() => _timeEnd = DateTime.Now.Ticks + _ticks;
  public void Stop() => _timeEnd = 0;
}