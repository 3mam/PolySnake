namespace PolySnake;

public static class Snake
{
  public static float[] Head = new[]
  {
    -0.25f, 1f,
    -0.5f, -1f,
    -1f, -0.3f,

    -0.25f, 1f,
    0.5f, -1f, 
    -0.5f, -1f,

    -0.25f, 1f,
    1f, -0.3f, 
    0.5f, -1f, 

    -0.25f, 1f,
    0.25f, 1f, 
    1f, -0.3f, 
  };

  public static float[] Body = new[]
  {
    0f, 0.6f,
    -0.6f, -0.3f,
    -0.6f, 0.3f,

    0f, 0.6f, 
    0f, -0.6f,
    -0.6f, -0.3f,

    0f, 0.6f, 
    0.6f, -0.3f, 
    0f, -0.6f, 

    0f, 0.6f, 
    0.6f, 0.3f,
    0.6f, -0.3f,
  };

  public static float[] Tail = new[]
  {
    0f, 0.6f,
    0f, -0.6f,
    -0.6f, 0.3f,

    0f, 0.6f,
    0.6f, 0.3f,
    0f, -0.6f,
  };
}