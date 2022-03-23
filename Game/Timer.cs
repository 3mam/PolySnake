namespace Game;

public class Timer
{
  private long _timeEnd;
  private readonly long _millisecond;

  public Timer(int millisecond) =>
    _millisecond = new TimeSpan(0, 0, 0, 0, millisecond).Ticks;

  public bool Duration(bool looping = false)
  {
    var isTime = DateTime.Now.Ticks < _timeEnd;
    if (looping && !isTime)
      Reset();
    return isTime;
  }

  public void Reset() => _timeEnd = DateTime.Now.Ticks + _millisecond;
  public void Stop() => _timeEnd = 0;
}