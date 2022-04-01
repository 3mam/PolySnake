using Game.Collision;

namespace Game.Interface;

public interface ICollide
{
  bool Collide(CollideCircle circle);
  bool Collide(CollideLine line);
}