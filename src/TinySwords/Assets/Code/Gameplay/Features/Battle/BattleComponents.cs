using Code.Gameplay.Features.Battle.Animators;
using Code.Gameplay.Features.Units.Animators;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Battle
{
  [Game] public class AttackRequest : IComponent { }
  [Game] public class CanAttack : IComponent { }
  [Game] public class CanAttackNow : IComponent { }
  [Game] public class Attacking : IComponent { }
  [Game] public class NotAttacking : IComponent { }
  [Game] public class MakeHit : IComponent { }
  [Game] public class FinishAttack : IComponent { }
  [Game] public class AttackCooldown : IComponent { public float Value; }
  [Game] public class AttackInterval : IComponent { public float Value; }
  [Game] public class AttackDirection : IComponent { public Vector2 Value; }
  [Game] public class AnimateAttackRequest : IComponent { }
  [Game] public class AttackAnimator : IComponent { public IAttackAnimator Value; }
  [Game] public class AnimationSpeedChanger : IComponent { public IAnimationSpeedChanger Value; }
}
