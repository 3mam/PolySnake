namespace Game;

public class Timer
{
  private long _timeEnd;
  private readonly long _millisecond;

  public Timer(int millisecond) =>
    _millisecond = new TimeSpan(0, 0, 0, 0, millisecond).Ticks;

  public bool Finally(bool trigger)
  {
    if (trigger)
      Reset();
    return DateTime.Now.Ticks <= _timeEnd;
  }

  public bool Finally()
  {
    if (DateTime.Now.Ticks <= _timeEnd)
      return false;
    Reset();
    return true;
  }

  public void Reset() => _timeEnd = DateTime.Now.Ticks + _millisecond;
}