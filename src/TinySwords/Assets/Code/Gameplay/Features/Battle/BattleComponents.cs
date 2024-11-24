using Code.Gameplay.Features.Units.Animations.Animators;
using Entitas;

namespace Code.Gameplay.Features.Battle
{
  [Game] public class AttackRequest : IComponent { }
  [Game] public class CanAttack : IComponent { }
  [Game] public class AnimateAttack : IComponent { }
  [Game] public class Attacking : IComponent { }
  [Game] public class AttackTarget : IComponent { public int Value; }
  [Game] public class AttackCooldown : IComponent { public float Value; }
  [Game] public class AttackInterval : IComponent { public float Value; }
  [Game] public class AttackAnimator : IComponent { public IAttackAnimator Value; }
}
