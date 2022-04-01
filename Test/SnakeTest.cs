using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Game;
using OpenTK.Mathematics;

namespace Test;

[TestClass]
public class SnakeTest
{
  [TestMethod]
  public void Direction()
  {
    var pos = new SnakePosition(new Vector2(), 799f);
    Console.WriteLine(pos.Direction);
  }
  
  [TestMethod]
  public void RandomBetweenNumber()
  {
    var range = 25f;
    var random = new Random();
    var between = (range + range + 1);
    var x = (float) random.NextDouble() * between - (range);
    var y = (float) random.NextDouble() * between - (range);
    Console.WriteLine($"{x} {y}");
  }
  
  [TestMethod]
  public void Time()
  {
    var abc = Func<int, float>(int a) =>
    {
      return (int b) => a + b;
    };
    Console.WriteLine(abc(2)(4));
  }
}