using Microsoft.VisualStudio.TestTools.UnitTesting;
using Poly.Collision;

namespace Test;

[TestClass]
public class CollisionTest
{
  [TestMethod]
  public void CircleToCircle()
  {
    var a = new CollideCircle(10, 10, 10);
    var b = new CollideCircle(15, 15, 10);
    Assert.IsTrue(Collide.CircleToCircle(a, b));
  }

  [TestMethod]
  public void LineToLine()
  {
    var a = new CollideLine(10, 10, 20, 10);
    var b = new CollideLine(15, 5, 15, 15);
    Assert.IsTrue(Collide.LineToLine(a, b));
    Assert.IsTrue(a==b);
  }

  [TestMethod]
  public void LineToCircle()
  {
    var a = new CollideLine(10, 10, 20, 10);
    var b = new CollideCircle(15, 5, 5);
    Assert.IsTrue(Collide.LineToCircle(a, b));   
    Assert.IsTrue(a==b);
  }
}