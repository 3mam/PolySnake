namespace Game;

public static class Assets
{
  public static readonly float[] Head =
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

  public static readonly float[] Body =
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

  public static readonly float[] Tail =
  {
    0f, 0.6f,
    0f, -0.6f,
    -0.6f, 0.3f,

    0f, 0.6f,
    0.6f, 0.3f,
    0f, -0.6f,
  };

  public static readonly float[] Level =
  {
    -0.18f, -0.32f,
    -0.18f, 0.32f,
    0.18f, 0.32f,
    0.18f, 0.32f,
    0.18f, -0.32f,
    -0.18f, -0.32f,
  };

  public static readonly float[] Apple =
  {
    0.3489f, -0.0633f,
    0.5607f, 0.3220f,
    0.2954f, 0.3995f,
    0.6849f, -0.5583f,
    0.3489f, -0.0633f,
    0.3649f, -0.9301f,
    -0.0465f, -0.8668f,
    0.3489f, -0.0633f,
    -0.0465f, -0.0668f,
    0.3489f, -0.0633f,
    -0.0465f, 0.3116f,
    -0.0465f, -0.0668f,
    -0.4419f, -0.0633f,
    -0.6537f, 0.3220f,
    -0.8252f, -0.0596f,
    -0.4419f, -0.0633f,
    -0.7779f, -0.5583f,
    -0.4579f, -0.9301f,
    -0.0465f, -0.8668f,
    -0.4419f, -0.0633f,
    -0.4579f, -0.9301f,
    -0.0465f, 0.3116f,
    -0.4419f, -0.0633f,
    -0.0465f, -0.0668f,
    0.2624f, 0.5948f,
    0.0864f, 0.7909f,
    -0.0262f, 0.4521f,
    0.2624f, 0.5948f,
    0.3750f, 0.9296f,
    0.0864f, 0.7909f,
    0.3489f, -0.0633f,
    0.7322f, -0.0596f,
    0.5607f, 0.3220f,
    0.6849f, -0.5583f,
    0.7322f, -0.0596f,
    0.3489f, -0.0633f,
    -0.0465f, -0.8668f,
    0.3649f, -0.9301f,
    0.3489f, -0.0633f,
    0.3489f, -0.0633f,
    0.2954f, 0.3995f,
    -0.0465f, 0.3116f,
    -0.4419f, -0.0633f,
    -0.3884f, 0.3995f,
    -0.6537f, 0.3220f,
    -0.4419f, -0.0633f,
    -0.8252f, -0.0596f,
    -0.7779f, -0.5583f,
    -0.0465f, -0.8668f,
    -0.0465f, -0.0668f,
    -0.4419f, -0.0633f,
    -0.0465f, 0.3116f,
    -0.3884f, 0.3995f,
    -0.4419f, -0.0633f,
  };

  public static readonly float[] Thunder =
  {
    -0.5890f, -0.1178f,
    0.1178f, 0.1178f,
    -0.1178f, -0.1178f,
    -0.1178f, -0.1178f,
    0.7068f, 0.1178f,
    -0.8246f, -0.9424f,
    -0.5890f, -0.1178f,
    0.7068f, 0.9424f,
    0.1178f, 0.1178f,
    -0.1178f, -0.1178f,
    0.1178f, 0.1178f,
    0.7068f, 0.1178f,
  };

  public static readonly float[] Life =
  {
    0.5619f, 0.1659f,
    0.6400f, 0.0409f,
    0.6400f, 0.1659f,
    0.5619f, 0.1659f,
    0.5619f, 0.0409f,
    0.6400f, 0.0409f,
    0.6400f, -0.2091f,
    0.5619f, -0.2091f,
    0.5619f, -0.0841f,
    0.6400f, -0.2091f,
    0.5619f, -0.0841f,
    0.6400f, -0.0841f,
    0.2572f, -0.1466f,
    0.1947f, -0.2091f,
    0.3822f, -0.2091f,
    -0.7428f, 0.2284f,
    -0.7428f, -0.1466f,
    -0.6803f, -0.1466f,
    0.2572f, -0.1466f,
    0.3822f, -0.2091f,
    0.3822f, -0.1466f,
    0.3822f, -0.0216f,
    0.2572f, -0.0216f,
    0.2572f, 0.0409f,
    0.1947f, -0.1466f,
    0.1947f, -0.2091f,
    0.2572f, -0.1466f,
    0.1947f, -0.1466f,
    0.2572f, -0.1466f,
    0.2572f, -0.0216f,
    0.1947f, 0.0409f,
    0.1947f, -0.1466f,
    0.2572f, -0.0216f,
    0.2572f, 0.0409f,
    0.1947f, 0.0409f,
    0.2572f, -0.0216f,
    0.3822f, -0.0216f,
    0.2572f, 0.0409f,
    0.3822f, 0.0409f,
    0.2572f, 0.0409f,
    0.2572f, 0.1659f,
    0.1947f, 0.0409f,
    -0.3678f, -0.1466f,
    -0.3053f, -0.1466f,
    -0.2584f, -0.2091f,
    -0.6803f, -0.1466f,
    -0.7428f, -0.2091f,
    -0.5240f, -0.2091f,
    0.3822f, 0.1659f,
    0.3822f, 0.2284f,
    0.2572f, 0.1659f,
    0.1947f, 0.2284f,
    0.1947f, 0.0409f,
    0.2572f, 0.1659f,
    0.2572f, 0.1659f,
    0.3822f, 0.2284f,
    0.1947f, 0.2284f,
    -0.1178f, -0.2091f,
    -0.1178f, 0.2284f,
    -0.0553f, 0.0409f,
    -0.0553f, -0.0216f,
    -0.0553f, 0.0409f,
    0.0697f, 0.0409f,
    0.0697f, 0.2284f,
    0.0697f, 0.1659f,
    -0.0553f, 0.1659f,
    -0.3053f, -0.1466f,
    -0.2584f, -0.1466f,
    -0.2584f, -0.2091f,
    -0.4147f, 0.2284f,
    -0.2584f, 0.2284f,
    -0.3678f, 0.1659f,
    -0.4147f, 0.1659f,
    -0.4147f, 0.2284f,
    -0.3678f, 0.1659f,
    -0.3678f, 0.1659f,
    -0.3053f, 0.1659f,
    -0.3053f, -0.1466f,
    -0.4147f, -0.1466f,
    -0.3678f, -0.1466f,
    -0.4147f, -0.2091f,
    -0.2584f, 0.2284f,
    -0.2584f, 0.1659f,
    -0.3053f, 0.1659f,
    0.0697f, 0.2284f,
    -0.0553f, 0.1659f,
    -0.1178f, 0.2284f,
    -0.0553f, -0.2091f,
    -0.1178f, -0.2091f,
    -0.0553f, -0.0216f,
    -0.2584f, 0.2284f,
    -0.3053f, 0.1659f,
    -0.3678f, 0.1659f,
    -0.3678f, 0.1659f,
    -0.3053f, -0.1466f,
    -0.3678f, -0.1466f,
    -0.2584f, -0.2091f,
    -0.4147f, -0.2091f,
    -0.3678f, -0.1466f,
    -0.7428f, 0.2284f,
    -0.6803f, -0.1466f,
    -0.6803f, 0.2284f,
    -0.6803f, -0.1466f,
    -0.5240f, -0.2091f,
    -0.5240f, -0.1466f,
    -0.7428f, -0.1466f,
    -0.7428f, -0.2091f,
    -0.6803f, -0.1466f,
    -0.0553f, -0.0216f,
    0.0697f, 0.0409f,
    0.0697f, -0.0216f,
    -0.0553f, 0.1659f,
    -0.0553f, 0.0409f,
    -0.1178f, 0.2284f,
    -0.1178f, -0.2091f,
    -0.0553f, 0.0409f,
    -0.0553f, -0.0216f,
  };

  public static readonly float[] Heart =
  {
    0.0000f, 0.4284f,
    0.7098f, 0.8077f,
    0.1605f, 0.8077f,
    0.8993f, 0.0182f,
    0.0000f, 0.4284f,
    0.8993f, 0.4284f,
    0.0000f, 0.4284f,
    0.8993f, 0.0182f,
    0.0000f, -0.8531f,
    0.7098f, 0.8077f,
    0.0000f, 0.4284f,
    0.8993f, 0.4284f,
    0.0000f, 0.4284f,
    -0.1605f, 0.8077f,
    -0.7098f, 0.8077f,
    -0.8993f, 0.0182f,
    -0.8993f, 0.4284f,
    0.0000f, 0.4284f,
    0.0000f, 0.4284f,
    0.0000f, -0.8531f,
    -0.8993f, 0.0182f,
    -0.7098f, 0.8077f,
    -0.8993f, 0.4284f,
    0.0000f, 0.4284f,
  };
}