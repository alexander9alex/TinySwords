using Code.Gameplay.Features.Units.Data;
using Entitas;

namespace Code.Gameplay.Features.Unit
{
  [Game] public class HasTarget : IComponent {
    [Game] public class TeamColorComponent : IComponent { public TeamColor Value; }
    [Game] public class AttackReach : IComponent { public float Value; }
  }
}
