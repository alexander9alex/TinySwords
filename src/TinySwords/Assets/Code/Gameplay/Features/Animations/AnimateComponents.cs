using Code.Gameplay.Features.Animations.Animators;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Animations
{
  [Game] public class AnimateAttack : IComponent { }
  [Game] public class AnimateTakenDamage : IComponent { }
  [Game] public class AnimateDeath : IComponent { }
  [Game] public class AttackAnimator : IComponent { public IAttackAnimator Value; }
  [Game] public class SelectingAnimator : IComponent { public ISelectingAnimator Value; }
  [Game] public class LookDirection : IComponent { public Vector2 Value; }
  [Game] public class MoveAnimator : IComponent { public IMoveAnimator Value; }
  [Game] public class DamageTakenAnimator : IComponent { public IDamageTakenAnimator Value; }

  [Game] public class DisplayTimer : IComponent { public float Value; }
  [Game] public class HideTimer : IComponent { public float Value; }
  [Game] public class DeathAnimator : IComponent { public IDeathAnimator Value; }
}
