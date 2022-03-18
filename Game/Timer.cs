namespace Game;

public static class Timer
{
  public static Func<bool, bool> Create(long duration)
  {
    long timeEnd = 0;
    return (bool start) =>
    {
      if (start)
        timeEnd = DateTime.Now.Ticks + duration;
      return DateTime.Now.Ticks <= timeEnd;
    };
  }
}