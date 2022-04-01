using Poly.Collision;

namespace Poly.Interface;

public interface ICollide
{
  bool Collide(CollideCircle circle);
  bool Collide(CollideLine line);
}