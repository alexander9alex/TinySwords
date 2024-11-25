using Code.Gameplay.Features.Units.Data;
using Entitas;

namespace Code.Gameplay.Features.Units
{
  [Game] public class Unit : IComponent { }
  [Game] public class Alive : IComponent { }
  [Game] public class Dead : IComponent { }
  [Game] public class Speed : IComponent { public float Value; }
  [Game] public class Damage : IComponent { public float Value; }
  [Game] public class CurrentHp : IComponent { public float Value; }
  [Game] public class MaxHp : IComponent { public float Value; }
  [Game] public class AttackReach : IComponent { public float Value; }
  [Game] public class TeamColorComponent : IComponent { public TeamColor Value; }
}
