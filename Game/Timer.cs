namespace Game;

public class Timer
{
  private long _timeEnd;
  private readonly long _millisecond;

  public Timer(int millisecond) =>
    _millisecond = new TimeSpan(0, 0, 0, 0, millisecond).Ticks;
  public bool Trigger(bool start)
  {
    if (start)
      _timeEnd = DateTime.Now.Ticks + _millisecond;
    return DateTime.Now.Ticks <= _timeEnd;
  }
}